using BackOffice.API.Models;

namespace BackOffice.API.Services.Interfaces;

public interface IProductionUnitService
{
    Task<ProductionUnit> AddAsync(ProductionUnit productionUnit);
    Task<IEnumerable<ProductionUnit>> GetAllAsync();
    Task<IEnumerable<ProductionUnit>> GetAllByUserIdAsync(Guid userId);
    Task<ProductionUnit> FindAsync(Guid id);
    Task<ProductionUnit> Update(Guid id, ProductionUnit productionUnit);
    Task<bool> Delete(Guid id);
}