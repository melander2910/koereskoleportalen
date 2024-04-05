using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CvrSync.Service.Models;

public class DrivingSchool
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int ProductionUnitNumber { get; set; }
    public int CompanyNumber { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }
    public int IndustryCode { get; set; }
}