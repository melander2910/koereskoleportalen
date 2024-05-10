using System.Security.Claims;
using Authentication.Service.Dto;
using Authentication.Service.Models;

namespace Authentication.Service.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<ExtendedIdentityUser> Register(ExtendedIdentityUser extendedIdentityUser, string password);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
    Task<LoginResponseDto> RefreshToken(RefreshTokenDto refreshTokenDto);
    Task<string> CreateClaim(ClaimsPrincipal user, CreateClaimDto createClaimDto);
}
