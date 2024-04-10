using CvrSync.Service.Models;
using Microsoft.Extensions.Configuration;
using Nest;

namespace CvrSync.Service.Services;

public class ElasticSearchService
{
    private MongoDBClient _mongoDbClient;
    private string _cvrUsername;
    private string _cvrPassword;
    
    public ElasticSearchService()
    {
        _mongoDbClient = new MongoDBClient();
                
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        CvrAuthenticationSettings cvrAuthenticationSettingsSettings = config.GetRequiredSection("CvrAuth").Get<CvrAuthenticationSettings>();

        _cvrUsername = cvrAuthenticationSettingsSettings.Username;
        _cvrPassword = cvrAuthenticationSettingsSettings.Password;
    }
    
   public async Task GetOrganisations()
    {
        // Configuration for the Elasticsearch connection
        string organisationIndex = "/cvr-permanent/virksomhed/";
        var settings = new ConnectionSettings(new Uri("http://distribution.virk.dk"))
            .BasicAuthentication(_cvrUsername, _cvrPassword)
            .DefaultIndex(organisationIndex);
        
        // Create an Elasticsearch client
        var client = new ElasticClient(settings);
        
        // Querying Elastic Search API
        var response = await client.SearchAsync<OrganisationQuery>(s => s
                .Size(1000)
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .Term(t => t.Organisation.MetaData.BusinessType.BusinessTypeCode, "855300"),
                            m => m.Term(t => t.Organisation.MetaData.Status, "aktiv")))));
        
        var doc = response.Documents;

        foreach (var query in doc)
        {
            var newOrganisation = CreateOrganisation(query);
            
            await _mongoDbClient.CreateAsync(newOrganisation);
        }
    }
    
    public async Task GetProductionUnits()
    {
        // Configuration for the Elasticsearch connection
        string productionUnitIndex = "/cvr-permanent/produktionsenhed/";
        var settings = new ConnectionSettings(new Uri("http://distribution.virk.dk"))
            .BasicAuthentication(_cvrUsername, _cvrPassword)
            .DefaultIndex(productionUnitIndex);
        
        // Create an Elasticsearch client
        var client = new ElasticClient(settings);
        
        // Querying Elastic Search API
        var response = await client.SearchAsync<DrivingSchoolQuery>(s => s
            .Size(2000)
            .Query(q => q
                .Bool(b => b
                    .Must(m => m
                            .Term(t => t.Unit.MetaData.BusinessType.BusinessTypeCode, "855300"),
                        m => m.Term(t => t.Unit.MetaData.Status, "aktiv")))));
        
        
        var doc = response.Documents;
        
        foreach (var query in doc)
        {
            var newDrivingSchool = CreateDrivingSchool(query);
            
            Console.WriteLine($"{newDrivingSchool.Name}");
            
            var matchingCompany = await _mongoDbClient.GetAsync(newDrivingSchool.OrganisationNumber);

            if (matchingCompany != null)
            {
                matchingCompany.DrivingSchools.Add(newDrivingSchool);
                await _mongoDbClient.UpdateAsync(matchingCompany.Id, matchingCompany);
            }
        }
    } 
    
    public Organisation CreateOrganisation(OrganisationQuery query)
    {
        var newOrganisation = new Organisation
        {
            OrganisationNumber = query.Organisation.OrganisationNumber,
            Name = query.Organisation.MetaData.NewestName.Name,
            Address = $"{query.Organisation.MetaData.Address.RoadName ?? ""} " +
                      $"{query.Organisation.MetaData.Address.HouseNumber?.ToString() ?? ""}" +
                      $"{(query.Organisation.MetaData.Address.Story != null ? ", " : "")}" +
                      $"{query.Organisation.MetaData.Address.Story?.ToString() ?? ""}" +
                      $"{(query.Organisation.MetaData.Address.Story != null ? "." : "")}" +
                      $"{query.Organisation.MetaData.Address.Door?.ToString() ?? ""}",
            ZipCode = query.Organisation.MetaData.Address.ZipCode,
            City = query.Organisation.MetaData.Address.Municipality.Name,
            DrivingSchools = new List<DrivingSchool>()
        };
        
        // TODO:
        // probably fix this hardcoded way to find emails / phone numbers
        if (query.Organisation.MetaData.ContactInformation.Count == 2)
        {
            newOrganisation.PhoneNumber = query.Organisation.MetaData.ContactInformation[0];
            newOrganisation.Email = query.Organisation.MetaData.ContactInformation[1];
        }

        return newOrganisation;
    }

    public DrivingSchool CreateDrivingSchool(DrivingSchoolQuery query)
    {
        var newDrivingSchool = new DrivingSchool
        {
            ProductionUnitNumber = query.Unit.ProductionUnitNumber,
            OrganisationNumber = query.Unit.OrganisationRelations[0].OrganisationNumber,
            Name = query.Unit.MetaData.NewestName.Name,
            Address = $"{query.Unit.MetaData.Address.RoadName ?? ""} " +
                      $"{query.Unit.MetaData.Address.HouseNumber?.ToString() ?? ""}" +
                      $"{(query.Unit.MetaData.Address.Story != null ? ", " : "")}" +
                      $"{query.Unit.MetaData.Address.Story?.ToString() ?? ""}" +
                      $"{(query.Unit.MetaData.Address.Story != null ? "." : "")}" +
                      $"{query.Unit.MetaData.Address.Door?.ToString() ?? ""}",
            ZipCode = query.Unit.MetaData.Address.ZipCode,
            City = query.Unit.MetaData.Address.Municipality.Name,
            Status = query.Unit.MetaData.Status
        };
        
        return newDrivingSchool;
    }
}