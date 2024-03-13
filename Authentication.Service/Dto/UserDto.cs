namespace Authentication.Service.Dto;

public class UserDto
{
    // string GUID
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    
    // creating users associated with a specific company?
    // public string CVR { get; set; }
}