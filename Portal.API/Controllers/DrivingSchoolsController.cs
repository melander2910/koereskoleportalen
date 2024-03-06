using Microsoft.AspNetCore.Mvc;
using Portal.API.Models;
using Portal.API.Services;

namespace Portal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrivingSchoolsController
{
    private readonly DrivingSchoolService _drivingSchoolService;

    public DrivingSchoolsController(DrivingSchoolService drivingSchoolService) =>
        _drivingSchoolService = drivingSchoolService;
    
    [HttpGet]
    public async Task<List<DrivingSchool>> Get() =>
        await _drivingSchoolService.GetAsync();
}