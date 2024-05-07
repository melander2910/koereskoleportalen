using BackOffice.API.Models.Abstracts;

namespace BackOffice.API.Models.DatabaseEntities;

public class ProductionUnit : Tenant
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
    // public string TenantId { get; set; }

    public ICollection<User> Users { get; set; }
    
    // TODO: Multi tenancy subtenant testing purposes
    public ICollection<Course> Courses { get; set; }
    public ICollection<ProductionUnitRemoved> ProductionUnitsRemoved { get; set; } = new List<ProductionUnitRemoved>();
    
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