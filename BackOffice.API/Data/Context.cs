using BackOffice.API.Models.Abstracts;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Data;

// This context is used when organisation and Production unit have been determined.
public class Context : DbContext
{
    private readonly ICurrentTenantService _currentTenantService;
    private readonly ICurrentSubTenantService _currentSubTenantService;
    public string TenantId { get; set; }
    public string SubTenantId { get; set; }

    public Context(DbContextOptions<Context> options, ICurrentTenantService currentTenantService, ICurrentSubTenantService currentSubTenantService)
        : base(options)
    {
        _currentTenantService = currentTenantService;
        _currentSubTenantService = currentSubTenantService;
        TenantId = _currentTenantService.TenantId;
        SubTenantId = _currentSubTenantService.SubTenantId;
    }

    public DbSet<Organisation> Organisations { get; set; }
    public DbSet<ProductionUnit> ProductionUnits { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    public DbSet<ProductionUnitRemoved> ProductionUnitsRemoved { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // TODO: Can we avoid having to add query filter for each entity?
        // Tried to implement logic using interfaces and abstract classes, where all entities implementing abstract class or interface would have query filter.
        builder.Entity<ProductionUnit>().HasQueryFilter(x => x.TenantId == TenantId);
        // builder.Entity<Course>().HasQueryFilter(x => x.TenantId == TenantId && x.SubTenantId == SubTenantId);
    }
    
    public override int SaveChanges()
    {
        // foreach (var entry in ChangeTracker.Entries<SubTenant>().ToList())
        // {
        //     switch (entry.State)
        //     {
        //         case EntityState.Added:
        //         case EntityState.Modified:
        //             entry.Entity.TenantId = TenantId;
        //             entry.Entity.SubTenantId = SubTenantId;
        //             break;
        //     }
        // }
        // TODO: Subtenant id? ?
        // Will there be other than production unit that implements ITenant?
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
}