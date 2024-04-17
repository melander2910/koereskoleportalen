using BackOffice.API.Dto;
using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services.Interfaces;

namespace BackOffice.API.Services;

public class ProductionUnitService : IProductionUnitService
{
    private readonly IProductionUnitRepository _productionUnitRepository;
    
    public ProductionUnitService(IProductionUnitRepository productionUnitRepository)
    {
        _productionUnitRepository = productionUnitRepository;
    }

    public async Task<ProductionUnit> AddAsync(ProductionUnitCreateDto productionUnitCreateDto)
    {
        // TODO: Implement
        var productionUnit = new ProductionUnit();
        return await _productionUnitRepository.AddAsync(productionUnit);
    }

    public async Task<IEnumerable<ProductionUnit>> GetAllAsync()
    {
        return await _productionUnitRepository.GetAllAsync();
    }

    public async Task<IEnumerable<ProductionUnit>> GetAllByUserIdAsync(Guid userId)
    {
        return await _productionUnitRepository.GetAllByUserIdAsync(userId);
    }

    public async Task<ProductionUnit> FindAsync(Guid id)
    {
        return await _productionUnitRepository.FindAsync(id);
    }

    public async Task<ProductionUnit> Update(Guid id, ProductionUnit productionUnit)
    {
        return await _productionUnitRepository.Update(id, productionUnit);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _productionUnitRepository.Delete(id);
    }
}