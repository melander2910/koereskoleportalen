using BackOffice.API.Data;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Repositories;

public class ProductionUnitRepository : IProductionUnitRepository
{
    private readonly TenantDbContext _dbContext;

    public ProductionUnitRepository(TenantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductionUnit> AddAsync(ProductionUnit productionUnit)
    {
        await _dbContext.ProductionUnits.AddAsync(productionUnit);
        await _dbContext.SaveChangesAsync();
        return productionUnit;
    }

    public async Task<IEnumerable<ProductionUnit>> GetAllAsync()
    {
        var productionUnits = await _dbContext.ProductionUnits.Where(pu => pu.ProductionUnitsRemoved.Count == 0).ToListAsync();
        return productionUnits;
    }

    public async Task<IEnumerable<ProductionUnit>> GetAllByUserIdAsync(Guid userId)
    {
        // TODO: Is there a better way to fetch ProductionUnits by userId?
        // query junction table to get ProductionUnit ids and then query ProductionUnit table with those ids?
        return await _dbContext.ProductionUnits.Where(pu => pu.Users.Any(user => user.Id == userId && pu.ProductionUnitsRemoved.Count == 0)).ToListAsync();
    }

    public async Task<ProductionUnit> FindAsync(Guid id)
    {
        return await _dbContext.ProductionUnits.Where(pu => pu.ProductionUnitsRemoved.Count == 0).FirstOrDefaultAsync(pu => pu.Id == id);
    }

    public async Task<ProductionUnit> Update(Guid id, ProductionUnit productionUnit)
    {
        // TODO: Update entire object or find object by id and change specific columns?
        // TODO: Update Entity or create new? Revision tables?
        var updatedProductionUnit = _dbContext.ProductionUnits.Update(productionUnit);
        await _dbContext.SaveChangesAsync();
        return updatedProductionUnit.Entity;
    }

    public async Task<bool> Delete(Guid id, ProductionUnitRemoved productionUnitRemoved)
    {
        await _dbContext.ProductionUnitsRemoved.AddAsync(productionUnitRemoved);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ProductionUnit> FindByProductionUnitNumber(string productionUnitNumber)
    {
        return await _dbContext.ProductionUnits.FirstOrDefaultAsync(pu => pu.ProductionUnitNumber == productionUnitNumber);
    }
}