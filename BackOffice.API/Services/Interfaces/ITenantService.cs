using BackOffice.API.Models.DatabaseEntities;

namespace BackOffice.API.Services.Interfaces;

public interface ITenantService
{
    Task<IEnumerable<Organisation>> GetAllByUserId(string id);
    Task<Organisation> GetById(Guid id);

}