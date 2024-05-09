using System.Security.Claims;
using Authentication.Service.Dto;
using Authentication.Service.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // TODO: how should authentication responses be implemented? Logging, error handling?
    protected ResponseDto _response;
    
    public AuthController(IAuthService authService, IPublishEndpoint publishEndpoint)
    {
        _authService = authService;
        _response = new ResponseDto();
        
    }
    
    // TODO: Send 'User Signup Event' to RabbitMQ which will create the application user with matching Guid / Id
    // TODO: Send event upon email verification?
    // TODO: Apply default roles and permissions?
    [HttpPost("Signup", Name = "Signup")]
    public async Task<IActionResult> Signup([FromBody] RegistrationRequestDto model)
    {
        var errorMessage = await _authService.Register(model);
        if (!string.IsNullOrEmpty(errorMessage) && errorMessage != "Identity User Created")
        {
            _response.IsSuccess = false;
            _response.Message= errorMessage;
            return BadRequest(_response);
        }

        _response.IsSuccess = true;
        // TODO: do we return some kind of object? Login user on register? Email confirmation?
        //_response.Result = created user?
        return Ok(_response);
    }

    [HttpPost("Login", Name = "Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await _authService.Login(model);
        if (loginResponse.User == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Username or password is incorrect";
            return BadRequest(_response);
        }
        Response.Cookies.Append("jwtToken", loginResponse.JwtToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true
        });

        var responseLogin = new LoginResponseDto
        {
            User = loginResponse.User,
            IsLoggedIn = true,
            TenantClaims = loginResponse.TenantClaims
            
        };
        _response.Result = responseLogin;
        return Ok(_response);
    }
    
    [HttpPost("Logout", Name = "Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    // TODO: what about refresh token?
    // Response.Cookies.Append("refreshToken", loginResponse.RefreshToken, new CookieOptions
    // {
    //     HttpOnly = true,
    //     // TODO: what is this?
    //     SameSite = SameSiteMode.None,
    //     // TODO: enable if using https?
    //     Secure = false
    // });
    // AssignRole requires email and role
    // Who can Assign Roles?
    [HttpPost("AssignRole", Name = "AssignRole")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequestDto model)
    {
        var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role);
        if (!assignRoleSuccessful)
        {
            _response.IsSuccess = false;
            _response.Message = "Error encountered";
            return BadRequest(_response);
        }
        return Ok(_response);
    }

    [HttpPost("RefreshToken", Name = "RefreshToken")]
    public async Task<IActionResult> RefreshToken(RefreshTokenDto model)
    {
        var loginResult = await _authService.RefreshToken(model);
        if (loginResult.IsLoggedIn)
        {
            return Ok(loginResult);
        }

        return Unauthorized();
    }

    [HttpPost("Claim")]
    [Authorize]
    public async Task<IActionResult> CreateClaim([FromBody] CreateClaimDto createClaimDto)
    {
        var user = HttpContext.User;
        var result = await _authService.CreateClaim(user, createClaimDto);
        return Ok(result);
    }
    
    /*[HttpGet("Weather", Name = "GetWeatherForecast")]
    [Authorize]
    public IEnumerable<string> Get()
    {
        return ["e", "h", "g", "h"];
    }*/
    
}