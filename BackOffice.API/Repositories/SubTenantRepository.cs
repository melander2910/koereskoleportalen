using BackOffice.API.Data;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Repositories;

public class SubTenantRepository : ISubTenantRepository
{
    private readonly SubTenantDbContext _subTenantDbContext;

    public SubTenantRepository(SubTenantDbContext subTenantDbContext)
    {
        _subTenantDbContext = subTenantDbContext;
    }
    public async Task<IEnumerable<ProductionUnit>> GetSubTenantsByUserId(Guid userId)
    {
        return await _subTenantDbContext.ProductionUnits.Where(pu => pu.Users.Any(user => user.Id == userId)).ToListAsync();
    }
}