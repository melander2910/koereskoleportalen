using BackOffice.API.Models.DatabaseEntities;

namespace BackOffice.API.Repositories.Interfaces;

public interface ISubTenantRepository
{
    Task<IEnumerable<ProductionUnit>> GetSubTenantsByUserId(Guid id);

}