using BackOffice.API.Models.Abstracts;

namespace BackOffice.API.Models.DatabaseEntities;

public class User : BaseEntity
{
    public User()
    {
        
    }
    // Properties shared by all user types
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }


    public ICollection<Organisation> Organisations { get; set; } = new List<Organisation>();
    public ICollection<ProductionUnit> ProductionUnits { get; set; } = new List<ProductionUnit>();
}