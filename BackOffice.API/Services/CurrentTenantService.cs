using BackOffice.API.Services.Interfaces;


namespace BackOffice.API.Services;

public class CurrentTenantService: ICurrentTenantService
{
    
    public CurrentTenantService()
    {
    }
    
    public string TenantId { get; set; }
    
    public async Task<bool> SetTenant(string tenantId)
    {
        if (tenantId != null)
        {
            TenantId = tenantId;
            return true;
        }

        throw new Exception("Tenant invalid");
    }
    
   
}