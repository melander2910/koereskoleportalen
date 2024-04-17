using BackOffice.API.Models.DatabaseEntities;

namespace BackOffice.API.Repositories.Interfaces;

public interface ITenantRepository
{
    Task<IEnumerable<Organisation>> GetTenantsByUserId(Guid userId);
}