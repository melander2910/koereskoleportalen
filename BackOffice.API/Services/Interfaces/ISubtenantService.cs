using BackOffice.API.Models.DatabaseEntities;

namespace BackOffice.API.Services.Interfaces;

public interface ISubTenantService
{
    Task<IEnumerable<ProductionUnit>> GetAllByUserId(string id);

}