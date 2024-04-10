using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CvrSync.Service.Models;

public class Organisation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int OrganisationNumber { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }
    public int IndustryCode { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public IList<DrivingSchool> DrivingSchools;
}