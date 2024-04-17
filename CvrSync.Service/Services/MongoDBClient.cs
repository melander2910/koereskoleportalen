using CvrSync.Service.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CvrSync.Service.Services;

public class MongoDBClient
{
    private readonly IMongoCollection<Organisation> _organisationCollection;
    private readonly IMongoCollection<DrivingSchool> _drivingSchoolCollection;

    public MongoDBClient()
    {
        /*
         * TODO:
         * For some reason, the path string points to the appsettings from Portal.API? 
         */
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        PortalDatabaseSettings? portalDatabaseSettings = config.GetRequiredSection("PortalDatabase").Get<PortalDatabaseSettings>();
        
        Console.WriteLine(portalDatabaseSettings.DrivingSchoolsCollectionName);
        Console.WriteLine(portalDatabaseSettings.OrganisationsCollectionName);
        
        var mongoClient = new MongoClient(
            portalDatabaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(portalDatabaseSettings.DatabaseName);

        _organisationCollection = mongoDatabase.GetCollection<Organisation>(portalDatabaseSettings.OrganisationsCollectionName);
        _drivingSchoolCollection = mongoDatabase.GetCollection<DrivingSchool>(portalDatabaseSettings.DrivingSchoolsCollectionName);
    }

    public async Task<Organisation?> GetOrganisationAsync(int organisationNumber)
    {
        return await _organisationCollection.Find(x => x.OrganisationNumber == organisationNumber).FirstOrDefaultAsync();
    }

    public async Task CreateOrganisationAsync(Organisation newOrganisation)
    {
        await _organisationCollection.InsertOneAsync(newOrganisation);
    }

    public async Task UpdateOrganisationAsync(string id, Organisation updatedOrganisation)
    {
        await _organisationCollection.ReplaceOneAsync(x => x.Id == id, updatedOrganisation);
    }
    
    public async Task<List<DrivingSchool>> GetDrivingSchoolsByOrganisationNumberAsync(int organisationNumber)
    {
        return await _drivingSchoolCollection.Find(x => x.OrganisationNumber == organisationNumber).ToListAsync();
    }
    
    public async Task CreateDrivingSchoolAsync(DrivingSchool newDrivingSchool)
    {
        await _drivingSchoolCollection.InsertOneAsync(newDrivingSchool);
    }
    
}