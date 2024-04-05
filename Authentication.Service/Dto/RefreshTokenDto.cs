namespace Authentication.Service.Dto;

public class RefreshTokenDto
{
    public string RefreshToken { get; set; }
    public string JwtToken { get; set; }
}