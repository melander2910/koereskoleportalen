using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Portal.API.Models;

[BsonIgnoreExtraElements]
public class DrivingSchool
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("address")]
    public string Address { get; set; }
    [BsonElement("zipcode")]
    public int Zip { get; set; }
    [BsonElement("city")]
    public string City { get; set; }
    
}