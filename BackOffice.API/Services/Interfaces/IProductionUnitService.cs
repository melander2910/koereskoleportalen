using BackOffice.API.Dto;
using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;

namespace BackOffice.API.Services.Interfaces;

public interface IProductionUnitService
{
    Task<ProductionUnit> AddAsync(ProductionUnitCreateDto productionUnitCreateDto);
    Task<IEnumerable<ProductionUnit>> GetAllAsync();
    Task<IEnumerable<ProductionUnit>> GetAllByUserIdAsync(Guid userId);
    Task<ProductionUnit> FindAsync(Guid id);
    Task<ProductionUnit> Update(Guid id, ProductionUnitUpdateDto productionUnitUpdateDto);
    Task<bool> Delete(Guid id);
    Task<ProductionUnit> FindByProductionUnitNumber(string productionUnitNumber);
}