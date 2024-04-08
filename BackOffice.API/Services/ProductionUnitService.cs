using BackOffice.API.Models;
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

    public Task<ProductionUnit> AddAsync(ProductionUnit productionUnit)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductionUnit>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductionUnit>> GetAllByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductionUnit> FindAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductionUnit> Update(Guid id, ProductionUnit productionUnit)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}