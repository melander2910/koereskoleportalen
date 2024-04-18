using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services.Interfaces;

namespace BackOffice.API.Services;

public class TenantService : ITenantService
{
    private readonly ITenantRepository _tenantRepository;

    public TenantService(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }
    public async Task<IEnumerable<Organisation>> GetAllByUserId(string id)
    {
        var userGuid = Guid.Parse(id);
        return await _tenantRepository.GetTenantsByUserId(userGuid);
    }

    public Task<Organisation> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
}