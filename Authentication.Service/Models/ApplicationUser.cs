using Microsoft.AspNetCore.Identity;

namespace Authentication.Service;

public class ApplicationUser : IdentityUser
{
    // Custom properties needed?
    public string Name { get; set; }
}