using System.Security.Claims;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

// This controller will be used to make calls using the SubTenantContext
[Route("api/[controller]")]
[ApiController]
public class SubTenantController : ControllerBase
{
    private readonly ISubTenantService _subTenantService;

    public SubTenantController(ISubTenantService subTenantService)
    {
        _subTenantService = subTenantService;

    }
    
    [HttpGet(Name = "GetSubTenantsByUserId")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ProductionUnit>>> GetSubTenantsByUserId()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            var authorizedUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var subTenants = await _subTenantService.GetAllByUserId(authorizedUserId);
            
            if (subTenants != null)
            {
                return Ok(subTenants);
            }
        }
        return StatusCode(401);
    }
}