using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services.Interfaces;

namespace BackOffice.API.Services;

public class SubTenantService : ISubTenantService
{
    private readonly ISubTenantRepository _subTenantRepository;
    public SubTenantService(ISubTenantRepository subTenantRepository)
    {
        _subTenantRepository = subTenantRepository;
    }
    public async Task<IEnumerable<ProductionUnit>> GetAllByUserId(string id)
    {
        var userGuid = Guid.Parse(id);
        return await _subTenantRepository.GetSubTenantsByUserId(userGuid);
    }
}