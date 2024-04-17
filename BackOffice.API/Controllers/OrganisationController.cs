using BackOffice.API.Dto;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganisationController : ControllerBase
{
    private readonly IOrganisationService _organisationService;
    private readonly IUserService _userService;
    public OrganisationController(IOrganisationService organisationService, IUserService userService)
    {
        _organisationService = organisationService;
        _userService = userService;
    }
    
    [HttpGet("{id}" ,Name = "GetOrganisationById")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var organisation = await _organisationService.FindAsync(id);
        return Ok(organisation);
    }

    [HttpGet(Name = "GetAllOrganisations")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var organisations = await _organisationService.GetAllAsync();
        return Ok(organisations);
    }
    
    [HttpPost(Name = "CreateOrganisation")]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] OrganisationCreateDto organisationCreateDto)
    {
        var organisation = await _organisationService.AddAsync(organisationCreateDto);
        return Ok(organisation);
    }
    
    [HttpPut("{id}", Name = "UpdateOrganisation")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id)
    {
        return Ok(200);
    }
    
    [HttpDelete("{id}", Name = "DeleteOrganisation")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(200);
    }
    
    [HttpGet]
    [Route("{id}/users", Name = "GetUsersByOrganisationId")]
    public async Task<ActionResult<IEnumerable<Organisation>>> GetUsersByOrganisationId(Guid id)
    {
        var userOrganisations = await _userService.GetAllByOrganisationIdAsync(id);
        return Ok(userOrganisations);
    }
}