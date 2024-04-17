using MongoDB.Driver;
using Portal.API.Data;
using Portal.API.Models;
using Portal.API.Repositories.Interfaces;

namespace Portal.API.Repositories;

public class DrivingSchoolRepository : IDrivingSchoolRepository
{
    private readonly MongoDbClient _mongoDbClient;

    public DrivingSchoolRepository(MongoDbClient mongoDbClient)
    {
        _mongoDbClient = mongoDbClient;
    }

    public async Task<List<DrivingSchool>> GetAllAsync()
    {
        return await _mongoDbClient.DrivingSchoolCollection.Find(_ => true).ToListAsync();
    }

    public async Task<DrivingSchool> GetAsync(string id)
    {
        return await _mongoDbClient.DrivingSchoolCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    }
}