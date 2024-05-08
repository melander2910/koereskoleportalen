using BackOffice.API.Consumers;
using BackOffice.API.Data;
using BackOffice.API.Extensions;
using BackOffice.API.Middleware;
using BackOffice.API.Repositories;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services;
using BackOffice.API.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Context
builder.Services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// TenantDbContext
builder.Services.AddDbContext<TenantDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// SubTenantDbContext
builder.Services.AddDbContext<SubTenantDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IOrganisationService, OrganisationService>();
builder.Services.AddScoped<IProductionUnitService, ProductionUnitService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IOrganisationRepository, OrganisationRepository>();
builder.Services.AddScoped<IProductionUnitRepository, ProductionUnitRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();
builder.Services.AddScoped<ICurrentSubTenantService, CurrentSubTenantService>();

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ISubTenantService, SubTenantService>();

builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<ISubTenantRepository, SubTenantRepository>();

builder.Services.AddMassTransit(registrationConfigurator =>
{
    registrationConfigurator.SetKebabCaseEndpointNameFormatter();
    registrationConfigurator.AddConsumer<UserCreatedEventConsumer>();
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

// TODO: Create env variable?
app.UseCors(options => options.WithOrigins(["http://localhost:5173", "http://localhost:3000"]).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

if (!app.Environment.IsDevelopment())
{
    // app.UseHttpsRedirection();

}

// app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<TenantResolver>();
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

