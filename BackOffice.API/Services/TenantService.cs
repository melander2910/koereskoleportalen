using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services.Interfaces;

namespace BackOffice.API.Services;

public class TenantService : ITenantService
{
    private readonly ITenantRepository _tenantRepository;
    private readonly HttpContext _httpContext;

    public TenantService(ITenantRepository tenantRepository, HttpContext httpContext)
    {
        _tenantRepository = tenantRepository;
        _httpContext = httpContext;
    }
    public async Task<IEnumerable<Organisation>> GetAllByUserId()
    {
        var authorizedUserId = _httpContext.User.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
        var userGuid = Guid.Parse(authorizedUserId);
        return await _tenantRepository.GetTenantsByUserId(userGuid);
    }

    public Task<Organisation> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
}