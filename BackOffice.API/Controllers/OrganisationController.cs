using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganisationController : ControllerBase
{

    public OrganisationController()
    {
        
    }

    
    
    [HttpPost("CreateOrganisation", Name = "CreateOrganisation")]
    [Authorize]
    public async Task<IActionResult> CreateOrganisation([FromBody] string model)
    {
        
            
        return Ok(200);
    }
}