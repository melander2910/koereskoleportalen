namespace Authentication.Service.Dto;

public class LoginResponseDto
{
    public UserDto User { get; set; }
    public bool IsLoggedIn { get; set; }
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
    
    public List<string> TenantClaims { get; set; }
}