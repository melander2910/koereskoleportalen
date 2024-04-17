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
        // var subTenantInfo = await _subContext.ProductionUnits.Where(x => x.ProductionUnitNumber == subTenantId).FirstOrDefaultAsync();
        // if (subTenantInfo != null)
        // {
        //     SubTenantId = subTenantInfo.ProductionUnitNumber;
        //     return true;
        // }

        throw new Exception("Subtenant invalid");
    }
}