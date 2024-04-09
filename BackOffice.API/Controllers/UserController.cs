using BackOffice.API.Dto;
using BackOffice.API.Models;
using BackOffice.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{id}" ,Name = "GetUserById")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.FindAsync(id);
        return Ok(user);
    }

    [HttpGet(Name = "GetAllUsers")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpPost(Name = "CreateUser")]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] UserSignupDto model)
    {
        await _userService.AddAsync(model);
        return Ok(200);
    }
    
    [HttpPut("{id}", Name = "UpdateUser")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, UserUpdateDto model)
    {
        var updatedUser = await _userService.Update(id, model);
        return Ok(200);
    }
    
    [HttpDelete("{id}", Name = "DeleteUser")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(200);
    }
}