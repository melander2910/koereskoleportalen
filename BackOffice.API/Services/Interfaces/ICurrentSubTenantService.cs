namespace BackOffice.API.Services.Interfaces;

public interface ICurrentSubTenantService
{
    string SubTenantId { get; set; }
    public Task<bool> SetSubTenant(string subTenantFromHeader);
}