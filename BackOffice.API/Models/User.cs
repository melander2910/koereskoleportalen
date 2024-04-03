namespace BackOffice.API.Models;

public class User : BaseEntity
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    
    public string Address { get; set; }
    
    


}