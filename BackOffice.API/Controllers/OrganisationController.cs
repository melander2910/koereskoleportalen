using BackOffice.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganisationController : ControllerBase
{
    private readonly IOrganisationService _organisationService;
    public OrganisationController(IOrganisationService organisationService)
    {
        _organisationService = organisationService;
    }
    
    [HttpGet("{id}" ,Name = "GetOrganisationById")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(200);
    }

    [HttpGet(Name = "GetAllOrganisations")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        return Ok(200);
    }

    [HttpPost(Name = "CreateOrganisation")]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] string model)
    {
        return Ok(200);
    }
    
    [HttpPut("{id}", Name = "UpdateOrganisation")]
    [Authorize]
    public async Task<IActionResult> Update(int id)
    {
        return Ok(200);
    }
    
    [HttpDelete("{id}", Name = "DeleteOrganisation")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(200);
    }
}