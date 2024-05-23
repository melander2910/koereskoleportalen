using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Portal.API.Models;

[BsonIgnoreExtraElements]
public class DrivingSchool
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("ProductionUnitNumber")]
    public int ProductionUnitNumber { get; set; }
    [BsonElement("OrganisationNumber")]
    public int OrganisationNumber { get; set; }
    [BsonElement("Name")]
    public string Name { get; set; }
    [BsonElement("Address")]
    public string Address { get; set; }
    [BsonElement("ZipCode")]
    public int Zip { get; set; }
    [BsonElement("City")]
    public string City { get; set; }
    [BsonElement("Price")]
    public double Price { get; set; }
    
    
}