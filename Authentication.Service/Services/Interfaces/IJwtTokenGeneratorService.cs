namespace Authentication.Service.Services.Interfaces;

public interface IJwtTokenGeneratorService
{
    string GenerateToken(ExtendedIdentityUser extendedIdentityUser, IEnumerable<string> roles);
    string GenerateRefreshToken();
}