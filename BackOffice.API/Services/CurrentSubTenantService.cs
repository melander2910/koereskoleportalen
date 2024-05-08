using BackOffice.API.Data;
using BackOffice.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Services;

public class CurrentSubTenantService : ICurrentSubTenantService
{
    // private readonly SubTenantDbContext _subContext;
    
    // public CurrentSubTenantService(SubTenantDbContext subContext)
    // {
    //     _subContext = subContext;
    // }
    
    public string SubTenantId { get; set; }
    
    public async Task<bool> SetSubTenant(string subTenantId)
    {
        if (subTenantId != null)
        {
            SubTenantId = subTenantId;
            return true;
        }
        
        throw new Exception("SubTenant invalid");
    }
}