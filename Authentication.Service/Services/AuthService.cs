using Authentication.Service.Dto;
using Authentication.Service.Models;
using Authentication.Service.Repositories.Interfaces;
using Authentication.Service.Services.Interfaces;
using Contracts;
using MassTransit;

namespace Authentication.Service.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuthService(IAuthRepository authRepository, IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
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
        var createdUser = await _authRepository.Register(extendedIdentityUser, registrationRequestDto.Password);
        // TODO: Send message to RabbitMQ to create Application User with matching Guid
        // TODO: Create a createApplicationUserDto and perhaps implement automapper. 
        // We dont want to send tokens with this message to RabbitMQ, only Id, Firstname, Lastname
        
        await _publishEndpoint.Publish(
            new UserCreatedEvent
            {
                Id = new Guid(createdUser.Id), 
                Firstname = createdUser.Name, 
                Lastname = "TestLastinami", 
                PhoneNumber = createdUser.PhoneNumber,
                Address = "TestKolind 8210 Aarhus"
            });
       
        return "Identity User Created";
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