using BackOffice.API.Models.Abstracts;

namespace BackOffice.API.Models.DatabaseEntities;

public class Organisation : BaseEntity
{

    public Organisation()
    {
        
    }
    
    public string CVR { get; set; }
    public string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public bool? ClaimedByOwner { get; set; }

    public ICollection<ProductionUnit> ProductionUnits { get; set; }
    public ICollection<User> Users { get; set; }
    

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? CvrApiModifiedDate { get; set; }

    public string? IndustryCode { get; set; }
    public string? IndustryDescription { get; set; }

    public string? Municipality { get; set; } // kommune
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? StreetAddress { get; set; }
    public string? Zipcode { get; set; }
    public double? Latitude { get; set; }
    public double? Longtitude { get; set; }
    
    public bool? AdvertisementProtection { get; set; } // reklamebeskyttelse?
    public string? OrganisationType { get; set; } // I/S, APS, A/S ..?
    public string? Status { get; set; } // aktiv, inaktiv, konkurs ..?

}