namespace BackOffice.API.Models;

public class Student : User
{
    public Class Class { get; set; }
    public int ClassId { get; set; }
}