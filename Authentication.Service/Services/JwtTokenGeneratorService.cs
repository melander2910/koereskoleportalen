using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Authentication.Service.Models;
using Authentication.Service.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Service.Services;

public class JwtTokenGeneratorService : IJwtTokenGeneratorService
{
    private readonly JwtOptions _jwtOptions;
    public JwtTokenGeneratorService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateToken(ExtendedIdentityUser extendedIdentityUser, IEnumerable<string> roles, IList<Claim> claims)
    {
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
        
        
        var claimList = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, extendedIdentityUser.Email),
            new Claim(JwtRegisteredClaimNames.Sub, extendedIdentityUser.Id),
            new Claim(JwtRegisteredClaimNames.Name, extendedIdentityUser.UserName)
        };

        foreach (var claim in claims)
        {
            claimList.Add(new Claim(claim.Type, claim.Value));
        }

        claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _jwtOptions.Audience,
            Issuer = _jwtOptions.Issuer,
            Subject = new ClaimsIdentity(claimList),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var numberGenerator = RandomNumberGenerator.Create())
        {
            numberGenerator.GetBytes(randomNumber);
        }

        return Convert.ToBase64String(randomNumber);
    }
}