using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Portal.API.Models;
using Portal.API.Services;

namespace Portal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrivingSchoolsController : ControllerBase
{
    private readonly DrivingSchoolService _drivingSchoolService;

    public DrivingSchoolsController(DrivingSchoolService drivingSchoolService)
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