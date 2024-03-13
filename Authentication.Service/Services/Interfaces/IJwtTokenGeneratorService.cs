namespace Authentication.Service.Services.Interfaces;

public interface IJwtTokenGeneratorService
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
}