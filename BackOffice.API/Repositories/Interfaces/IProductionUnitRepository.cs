using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;

namespace BackOffice.API.Repositories.Interfaces;

public interface IProductionUnitRepository
{
    Task<ProductionUnit> AddAsync(ProductionUnit productionUnit);
    Task<IEnumerable<ProductionUnit>> GetAllAsync();
    Task<IEnumerable<ProductionUnit>> GetAllByUserIdAsync(Guid userId);
    Task<ProductionUnit> FindAsync(Guid id);
    Task<ProductionUnit> Update(Guid id, ProductionUnit productionUnit);
    Task<bool> Delete(Guid id, ProductionUnitRemoved productionUnitRemoved);
    Task<ProductionUnit> FindByProductionUnitNumber(string productionUnitNumber);
}