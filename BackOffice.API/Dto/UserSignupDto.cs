namespace BackOffice.API.Dto;

public class UserSignupDto
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    
    public string Address { get; set; }
}