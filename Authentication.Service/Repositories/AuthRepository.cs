using Authentication.Service.Dto;
using Authentication.Service.Repositories.Interfaces;
using Authentication.Service.Services.Interfaces;
using Authentication.Service.Utils;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Service.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;

    public AuthRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, IJwtTokenGeneratorService jwtTokenGeneratorService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
    }
    public async Task<string> Register(ApplicationUser applicationUser, string password)
    {
        try
        {
            var result = await  _userManager.CreateAsync(applicationUser, password);
            if (result.Succeeded)
            {
                // email Normalized? ToLower/ToUpper?
                var userToReturn = _dbContext.ApplicationUsers.First(u => u.UserName == applicationUser.Email);
                // return created user?
                return "User successfully created";
            }
            return result.Errors.FirstOrDefault().Description;
        }
        catch (Exception ex)
        {
            return "Error: ";
        }
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());

        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if(user == null || isValid == false)
        {
            return new LoginResponseDto() { User = null, Token = "" };
        }

        // if user was found, Generate JWT Token
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGeneratorService.GenerateToken(user, roles);

        UserDto userDTO = new()
        {
            Email = user.Email,
            Id = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            User = userDTO,
            Token = token
        };

        return loginResponseDto;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        if (!Enum.IsDefined(typeof(Enums.AuthorizationRoles), roleName))
        {
            return false;
        }

        var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        if (user != null)
        {
            
            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                // create role if it does not exist
                _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }
            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }

        return false;
    }
}