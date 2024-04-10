using CvrSync.Service.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CvrSync.Service.Services;

public class MongoDBClient
{
    private readonly IMongoCollection<Organisation> _companyCollection;

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
        
        var mongoClient = new MongoClient(
            portalDatabaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(portalDatabaseSettings.DatabaseName);

        _companyCollection = mongoDatabase.GetCollection<Organisation>(portalDatabaseSettings.DrivingSchoolsCollectionName);
    }

    public async Task<Organisation?> GetAsync(int organisationNumber)
    {
        return await _companyCollection.Find(x => x.OrganisationNumber == organisationNumber).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Organisation newOrganisation)
    {
        await _companyCollection.InsertOneAsync(newOrganisation);
    }

    public async Task UpdateAsync(string id, Organisation updatedOrganisation)
    {
        await _companyCollection.ReplaceOneAsync(x => x.Id == id, updatedOrganisation);
        
    }
    
}