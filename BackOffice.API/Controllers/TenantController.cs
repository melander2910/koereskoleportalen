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
    public TenantController(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }
    
    [HttpGet(Name = "GetTenantsByUserId")]
    [Authorize]
    public async Task<IActionResult> GetTenantsByUserId()
    {
        var tenants = await _tenantService.GetAllByUserId();
        return Ok(tenants);
    }
}