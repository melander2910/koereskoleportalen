using Authentication.Service.Dto;
using Authentication.Service.Repositories.Interfaces;
using Authentication.Service.Services.Interfaces;

namespace Authentication.Service.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        ExtendedIdentityUser extendedIdentityUser = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };

        return await _authRepository.Register(extendedIdentityUser, registrationRequestDto.Password);
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        return await _authRepository.Login(loginRequestDto);
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        return await _authRepository.AssignRole(email, roleName);
    }

    public async Task<LoginResponseDto> RefreshToken(RefreshTokenDto model)
    {
        return await _authRepository.RefreshToken(model);
    }
}