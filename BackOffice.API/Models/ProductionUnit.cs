namespace BackOffice.API.Models;

public class ProductionUnit : BaseEntity
{
    public string ProductionNumber { get; set; }
    public string CVR { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CvrApiModifiedDate { get; set; }
    
    public Organisation Organisation { get; set; }
    public Guid OrganisationId { get; set; }
    
    // should production unit have a list of users? Teachers? Admins? Owners?
    // ICollection<User> Users { get; set; }

    
    public string Municipality { get; set; } // kommune
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string IndustryCode { get; set; }
    public string IndustryDescription { get; set; }
}