using Authentication.Service.Dto;
using Authentication.Service.Utils;

namespace Authentication.Service.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<string> Register(ExtendedIdentityUser extendedIdentityUser, string password);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
    Task<LoginResponseDto> RefreshToken(RefreshTokenDto refreshTokenDto);
}