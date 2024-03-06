using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Portal.API.Models;

namespace Portal.API.Services;

public class DrivingSchoolService
{
    private readonly IMongoCollection<DrivingSchool> _drivingSchoolCollection;

    public DrivingSchoolService(IOptions<PortalDatabaseSettings> portalDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            portalDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            portalDatabaseSettings.Value.DatabaseName);

        _drivingSchoolCollection = mongoDatabase.GetCollection<DrivingSchool>(
            portalDatabaseSettings.Value.DrivingSchoolsCollectionName);
    }
    
    public async Task<List<DrivingSchool>> GetAsync() =>
        await _drivingSchoolCollection.Find(_ => true).ToListAsync();
}