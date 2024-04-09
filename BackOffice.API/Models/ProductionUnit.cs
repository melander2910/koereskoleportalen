namespace BackOffice.API.Models;

public class ProductionUnit : BaseEntity
{
    public string ProductionUnitNumber { get; set; }
    public string CVR { get; set; }
    public string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? CvrApiModifiedDate { get; set; }
    
    public Organisation Organisation { get; set; }
    public Guid OrganisationId { get; set; }

    public ICollection<User> Users { get; set; }
    
    public string? Municipality { get; set; } // kommune

    public string? IndustryCode { get; set; }
    public string? IndustryDescription { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? StreetAddress { get; set; }
    public string? Zipcode { get; set; }
    public double? Latitude { get; set; }
    public double? Longtitude { get; set; }
    
    public string? Status { get; set; } // aktiv, inaktiv, konkurs ..?
}