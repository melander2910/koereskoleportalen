using Microsoft.AspNetCore.Mvc;
using Portal.API.Models;
using Portal.API.Services.Interfaces;

namespace Portal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrivingSchoolController : ControllerBase
{
    private readonly IDrivingSchoolService _drivingSchoolService;

    public DrivingSchoolController(IDrivingSchoolService drivingSchoolService)
    {
        _drivingSchoolService = drivingSchoolService;
    }
    
    [HttpGet]
    public async Task<List<DrivingSchool>> Get()
    {
        return await _drivingSchoolService.GetAsync();
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<DrivingSchool>> Get(string id)
    {
        var drivingSchool = await _drivingSchoolService.GetAsync(id);

        if (drivingSchool == null)
        {
            return NotFound();
        }

        return drivingSchool;
    }
}