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
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(200);
    }

    [HttpGet(Name = "GetAllUsers")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        return Ok(200);
    }

    [HttpPost(Name = "CreateUser")]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] string model)
    {
        return Ok(200);
    }
    
    [HttpPut("{id}", Name = "UpdateUser")]
    [Authorize]
    public async Task<IActionResult> Update(int id)
    {
        return Ok(200);
    }
    
    [HttpDelete("{id}", Name = "DeleteUser")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(200);
    }
}