namespace BackOffice.API.Models;

public class User : BaseEntity
{
    // Properties shared by all user types
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    
    
    public ICollection<Organisation> Organisations { get; set; }
    public ICollection<ProductionUnit> ProductionUnits { get; set; }
}