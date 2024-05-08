using BackOffice.API.Models;
using BackOffice.API.Models.Abstracts;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Data;

// This context is used when organisation have been determined, but Production Unit haven't
// Production Unit is out Sub Tenant.

public class SubTenantDbContext : DbContext
{
    private readonly ICurrentTenantService _currentTenantService;

    public string TenantId { get; set; }

    public SubTenantDbContext(DbContextOptions<SubTenantDbContext> options, ICurrentTenantService currentTenantService) : base(options)
    {
        _currentTenantService = currentTenantService;
        TenantId = _currentTenantService.TenantId;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ProductionUnit>().HasQueryFilter(x => x.TenantId == TenantId);
    }
    
    public override int SaveChanges()
    {   
        foreach (var entry in ChangeTracker.Entries<Tenant>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    entry.Entity.TenantId = TenantId;
                    break;
            }
        }
        
        var result = base.SaveChanges();
        return result;
    }
    
    public DbSet<ProductionUnit> ProductionUnits { get; set; }
    
    // add other dbsets
    // example add 'Courses' if courses should be viewable across sub tenant context / across production units
}