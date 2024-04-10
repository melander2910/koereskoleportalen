using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Portal.API.Models;

namespace Portal.API.Data;

public class MongoDbClient
{
    public readonly IMongoCollection<Organisation> OrganisationCollection;

    public MongoDbClient(IOptions<PortalDatabaseSettings> portalDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            portalDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            portalDatabaseSettings.Value.DatabaseName);

        OrganisationCollection = mongoDatabase.GetCollection<Organisation>(
            portalDatabaseSettings.Value.DrivingSchoolsCollectionName);
    }
}