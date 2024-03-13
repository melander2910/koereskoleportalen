using Authentication.Service.Dto;
using Authentication.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Service.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthAPIController : ControllerBase
{
    private readonly IAuthService _authService;
    
    // TODO: how should authentication responses be implemented? Logging, error handling?
    protected ResponseDto _response;
    
    public AuthAPIController(IAuthService authService)
    {
        _authService = authService;
        _response = new ResponseDto();
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {
        var errorMessage = await _authService.Register(model);
        if (!string.IsNullOrEmpty(errorMessage))
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

    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await _authService.Login(model);
        if (loginResponse.User == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Username or password is incorrect";
            return BadRequest(_response);
        }
        _response.Result = loginResponse;
        return Ok(_response);
    }
    
    // AssignRole requires email and role
    // Who can Assign Roles?
    [HttpPost("AssignRole")]
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
}