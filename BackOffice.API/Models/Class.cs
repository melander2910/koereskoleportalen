namespace BackOffice.API.Models;

public class Class: BaseEntity
{
    // Type: Enum? : class for taking license for Car, Truck, MC etc
    public string Type { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    
    public ICollection<Teacher> Teachers { get; set; }
    public ICollection<Student> Students { get; set; }

}