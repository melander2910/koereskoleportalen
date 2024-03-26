using Microsoft.AspNetCore.Identity;

namespace Authentication.Service.Models;

public class ExtendedIdentityUser : IdentityUser
{
    // Custom properties needed?
    public string Name { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
    
}