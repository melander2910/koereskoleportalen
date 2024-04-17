using BackOffice.API.Dto;
using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IOrganisationService _organisationService;
    private readonly IProductionUnitService _productionUnitService;

    public UserController(IUserService userService, IOrganisationService organisationService, IProductionUnitService productionUnitService)
    {
        _userService = userService;
        _organisationService = organisationService;
        _productionUnitService = productionUnitService;
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
    
    [HttpGet]
    [Authorize]
    [Route("{id}/organisations", Name = "GetOrganisationsByUserId")]
    public async Task<ActionResult<IEnumerable<Organisation>>> GetOrganisationsByUserId(Guid id)
    {
        var userOrganisations = await _organisationService.GetAllByUserIdAsync(id);
        return Ok(userOrganisations);
    }
    
    [HttpGet]
    [Authorize]
    [Route("{id}/productionunits", Name = "GetProductionUnitsByUserId")]
    public async Task<ActionResult<IEnumerable<ProductionUnit>>> GetProductionUnitsByUserId(Guid id)
    {
        var userOrganisations = await _productionUnitService.GetAllByUserIdAsync(id);
        return Ok(userOrganisations);
    }
}