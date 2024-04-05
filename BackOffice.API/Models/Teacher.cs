namespace BackOffice.API.Models;

public class Teacher : User
{
    public ICollection<Class> Classes { get; set; }
    
    
}