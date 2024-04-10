using System.Text;
using Authentication.Service;
using Authentication.Service.Data;
using Authentication.Service.Models;
using Authentication.Service.Repositories;
using Authentication.Service.Repositories.Interfaces;
using Authentication.Service.Services;
using Authentication.Service.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ExtendedIdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddMassTransit(registrationConfigurator =>
{
    registrationConfigurator.SetKebabCaseEndpointNameFormatter();
    
    // MassTransit to use an EF outbox for message deduplication ??
    
    registrationConfigurator.UsingRabbitMq((registrationContext, factoryConfigurator) =>
    {
        factoryConfigurator.Host(builder.Configuration.GetSection("RabbitMQ").GetValue<string>("Host"), hostConfigurator =>
        {
            hostConfigurator.Username(builder.Configuration["RabbitMQ:Username"]);
            hostConfigurator.Password(builder.Configuration["RabbitMQ:Password"]);
        });
        
        factoryConfigurator.ConfigureEndpoints(registrationContext);
    });
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
ApplyMigration();
app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (_dbContext.Database.GetPendingMigrations().Count() > 0)
        {
            _dbContext.Database.Migrate();
        }
    }
}
