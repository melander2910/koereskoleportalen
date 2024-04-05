using CvrSync.Service.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CvrSync.Service.Services;

public class MongoDBClient
{
    private readonly IMongoCollection<Company> _companyCollection;

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

        _companyCollection = mongoDatabase.GetCollection<Company>(portalDatabaseSettings.DrivingSchoolsCollectionName);
    }

    public async Task<Company?> GetAsync(int companyNumber)
    {
        return await _companyCollection.Find(x => x.CompanyNumber == companyNumber).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Company newCompany)
    {
        await _companyCollection.InsertOneAsync(newCompany);
    }

    public async Task UpdateAsync(string id, Company updatedCompany)
    {
        await _companyCollection.ReplaceOneAsync(x => x.Id == id, updatedCompany);
        
    }
    
}