using BackOffice.API.Data;
using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Repositories;

public class ProductionUnitRepository : IProductionUnitRepository
{
    private readonly Context _dbContext;

    public ProductionUnitRepository(Context dbContext)
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
        var gg = await _dbContext.ProductionUnits.ToListAsync();
        return gg;
    }

    public async Task<IEnumerable<ProductionUnit>> GetAllByUserIdAsync(Guid userId)
    {
        // TODO: Is there a better way to fetch ProductionUnits by userId?
        // query junction table to get ProductionUnit ids and then query ProductionUnit table with those ids?
        return await _dbContext.ProductionUnits.Where(pu => pu.Users.Any(user => user.Id == userId)).ToListAsync();
    }

    public async Task<ProductionUnit> FindAsync(Guid id)
    {
        return await _dbContext.ProductionUnits.FindAsync(id);
    }

    public async Task<ProductionUnit> Update(Guid id, ProductionUnit productionUnit)
    {
        // TODO: Update entire object or find object by id and change specific columns?
        // TODO: Update Entity or create new? Revision tables?
        var updatedProductionUnit = _dbContext.ProductionUnits.Update(productionUnit);
        await _dbContext.SaveChangesAsync();
        return updatedProductionUnit.Entity;
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}