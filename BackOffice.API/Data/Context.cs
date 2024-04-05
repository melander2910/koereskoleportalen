using BackOffice.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Data;

public class Context : DbContext
{    
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    
    public DbSet<Organisation> Organisations { get; set; }
    public DbSet<ProductionUnit> ProductionUnits { get; set; }
    
}