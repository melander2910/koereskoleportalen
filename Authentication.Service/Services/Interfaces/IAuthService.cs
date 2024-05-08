using System.Security.Claims;
using Authentication.Service.Dto;
using Authentication.Service.Models;
using Authentication.Service.Utils;

namespace Authentication.Service.Services.Interfaces;

public interface IAuthService
{
    Task<string> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
    Task<LoginResponseDto> RefreshToken(RefreshTokenDto refreshTokenDto);
    Task<bool> CreateClaim(ClaimsPrincipal user, CreateClaimDto createClaimDto);
}