using Microsoft.AspNetCore.Mvc;
using Portal.API.Models;
using Portal.API.Services.Interfaces;

namespace Portal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrivingSchoolsController : ControllerBase
{
    private readonly IOrganisationService _organisationService;

    public DrivingSchoolsController(IOrganisationService organisationService)
    {
        _organisationService = organisationService;
    }
    
    [HttpGet]
    public async Task<List<Organisation>> Get()
    {
        return await _organisationService.GetAsync();
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Organisation>> Get(string id)
    {
        var drivingSchool = await _organisationService.GetAsync(id);

        if (drivingSchool == null)
        {
            return NotFound();
        }

        return drivingSchool;
    }
}