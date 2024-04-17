using System.Security.Claims;
using Authentication.Service.Models;

namespace Authentication.Service.Services.Interfaces;

public interface IJwtTokenGeneratorService
{
    string GenerateToken(ExtendedIdentityUser extendedIdentityUser, IEnumerable<string> roles, IList<Claim> claims);
    string GenerateRefreshToken();
}