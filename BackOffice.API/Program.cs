using BackOffice.API.Data;
using BackOffice.API.Extensions;
using BackOffice.API.Repositories;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services;
using BackOffice.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();


builder.Services.AddScoped<IOrganisationService, OrganisationService>();
builder.Services.AddScoped<IProductionUnitService, ProductionUnitService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IOrganisationRepository, OrganisationRepository>();
builder.Services.AddScoped<IProductionUnitRepository, ProductionUnitRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

// WebApplicationBuilderExtension
builder.AddApplicationAuthentication();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
        var _dbContext = scope.ServiceProvider.GetRequiredService<Context>();
        if (_dbContext.Database.GetPendingMigrations().Count() > 0)
        {
            _dbContext.Database.Migrate();
        }
    }
}

