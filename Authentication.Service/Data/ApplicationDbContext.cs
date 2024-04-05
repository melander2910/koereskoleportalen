using Authentication.Service.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Service.Data;

public class ApplicationDbContext : IdentityDbContext<ExtendedIdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ExtendedIdentityUser> ApplicationUsers { get; set; }

}