using MongoDB.Driver;
using Portal.API.Data;
using Portal.API.Models;
using Portal.API.Repositories.Interfaces;

namespace Portal.API.Repositories;

public class OrganisationRepository : IOrganisationRepository
{
    private readonly MongoDbClient _mongoDbClient;
    
    public OrganisationRepository(MongoDbClient mongoDbClient)
    {
        _mongoDbClient = mongoDbClient;
    }

    public async Task<List<Organisation>> GetAllAsync()
    {
        /* merge test*/
        return await _mongoDbClient.OrganisationCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Organisation> GetAsync(string id)
    {
        return await _mongoDbClient.OrganisationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}