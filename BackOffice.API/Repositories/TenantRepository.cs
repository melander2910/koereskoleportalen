using BackOffice.API.Data;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly TenantDbContext _tenantDbContext;

    public TenantRepository(TenantDbContext tenantDbContext)
    {
        _tenantDbContext = tenantDbContext;
    }
    public async Task<IEnumerable<Organisation>> GetTenantsByUserId(Guid userId)
    {
        return await _tenantDbContext.Organisations.Where(org => org.Users.Any(user => user.Id == userId)).ToListAsync();
    }

    public async Task<Organisation> GetById(Guid id)
    {
        return await _tenantDbContext.Organisations.FindAsync(id);
    }
}