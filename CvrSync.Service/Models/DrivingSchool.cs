using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace CvrSync.Service.Models;

public class DrivingSchool
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int ProductionUnitNumber { get; set; }
    public int OrganisationNumber { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }
    public int IndustryCode { get; set; }
}