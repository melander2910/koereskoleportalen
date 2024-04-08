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
    
   public async Task GetCompanies()
    {
        // Configuration for the Elasticsearch connection
        string companiesIndex = "/cvr-permanent/virksomhed/";
        var settings = new ConnectionSettings(new Uri("http://distribution.virk.dk"))
            .BasicAuthentication(_cvrUsername, _cvrPassword)
            .DefaultIndex(companiesIndex);
        
        // Create an Elasticsearch client
        var client = new ElasticClient(settings);
        
        // Querying Elastic Search API
        var response = await client.SearchAsync<CompanyQuery>(s => s
                .Size(1000)
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .Term(t => t.Organisation.MetaData.BusinessType.BusinessTypeCode, "855300"),
                            m => m.Term(t => t.Organisation.MetaData.Status, "aktiv")))));
        
        var doc = response.Documents;

        foreach (var query in doc)
        {
            var newCompany = new Company
            {
                CompanyNumber = query.Organisation.CompanyNumber,
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
                newCompany.PhoneNumber = query.Organisation.MetaData.ContactInformation[0];
                newCompany.Email = query.Organisation.MetaData.ContactInformation[1];
            }
            
            await _mongoDbClient.CreateAsync(newCompany);
        }
    }
    
    public async Task GetProductionUnits()
    {
        // Configuration for the Elasticsearch connection
        string companiesIndex = "/cvr-permanent/produktionsenhed/";
        var settings = new ConnectionSettings(new Uri("http://distribution.virk.dk"))
            .BasicAuthentication(_cvrUsername, _cvrPassword)
            .DefaultIndex(companiesIndex);
        
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

            var newProductionUnit = new DrivingSchool
            {
                ProductionUnitNumber = query.Unit.ProductionUnitNumber,
                CompanyNumber = query.Unit.CompanyRelation[0].CompanyNumber,
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
            
            Console.WriteLine($"{newProductionUnit.ProductionUnitNumber}, {newProductionUnit.CompanyNumber}");

            var matchingCompany = await _mongoDbClient.GetAsync(newProductionUnit.CompanyNumber);

            if (matchingCompany != null)
            {
                matchingCompany.DrivingSchools.Add(newProductionUnit);
                await _mongoDbClient.UpdateAsync(matchingCompany.Id, matchingCompany);
            }
        }
    } 
}