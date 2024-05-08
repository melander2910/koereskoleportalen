using System.Security.Claims;
using BackOffice.API.Dto;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

// This controller will be used to make calls using the TenantContext
[Route("api/[controller]")]
[ApiController]
public class TenantController : ControllerBase
{
    private readonly ITenantService _tenantService;
    private readonly ICurrentTenantService _currentTenantService;
    public TenantController(ITenantService tenantService, ICurrentTenantService currentTenantService)
    {
        _tenantService = tenantService;
        _currentTenantService = currentTenantService;
    }
    
    [HttpGet(Name = "GetTenantsByUserId")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Organisation>>> GetTenantsByUserId()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            var authorizedUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tenants = await _tenantService.GetAllByUserId(authorizedUserId);
            if (tenants != null)
            {
                return Ok(tenants);
            }
        }
        return StatusCode(401);
    }

    // TODO: remove this and just set request header on future requests?
    [HttpPost(Name = "SetTenantId")]
    [Authorize]
    public async Task<IActionResult> SetTenantId([FromBody] TenantRequestDto model)
    {
        var tenantClaims = HttpContext.User.Claims.Where(x => x.Type == "tenant").Select(x => x.Value);
        
        if (HttpContext.User.Identity.IsAuthenticated && tenantClaims.Contains(model.TenantId))
        {
            await _currentTenantService.SetTenant(model.TenantId);
        }

        return Ok(model);
    }
}