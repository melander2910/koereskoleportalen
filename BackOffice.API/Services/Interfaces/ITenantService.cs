using BackOffice.API.Models.DatabaseEntities;

namespace BackOffice.API.Services.Interfaces;

public interface ITenantService
{
    Task<IEnumerable<Organisation>> GetAllByUserId();
    Task<Organisation> GetById(Guid id);

}