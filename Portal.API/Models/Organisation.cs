using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Portal.API.Models;

[BsonIgnoreExtraElements]
public class Organisation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("Name")]
    [BsonRepresentation(BsonType.String)]
    public string? Name { get; set; }
    [BsonElement("Address")]
    public string Address { get; set; }
    [BsonElement("ZipCode")]
    public int Zip { get; set; }
    [BsonElement("City")]
    public string City { get; set; }
    
}