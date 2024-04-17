using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Data;

// This context is used when organisation haven't been determined yet
// Organisation is out Tenant.
public class TenantDbContext : DbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
    {
        
    }

    public DbSet<Organisation> Organisations { get; set; }
}