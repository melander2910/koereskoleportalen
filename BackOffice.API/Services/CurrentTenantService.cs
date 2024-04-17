using BackOffice.API.Data;
using BackOffice.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Services;

public class CurrentTenantService: ICurrentTenantService
{
    private readonly ITenantService _tenantService;
    
    public CurrentTenantService(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }
    
    public string TenantId { get; set; }
    
    public async Task<bool> SetTenant(string tenantId)
    {
        var guidId = Guid.Parse(tenantId);
        var tenantInfo = await _tenantService.GetById(guidId);
        if (tenantInfo != null)
        {
            TenantId = tenantInfo.Name;
            return true;
        }

        throw new Exception("Tenant invalid");
    }
    
   
}