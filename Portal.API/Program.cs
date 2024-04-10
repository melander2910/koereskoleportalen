using Portal.API.Data;
using Portal.API.Models;
using Portal.API.Repositories;
using Portal.API.Repositories.Interfaces;
using Portal.API.Services;
using Portal.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PortalDatabaseSettings>(
    builder.Configuration.GetSection("PortalDatabase"));

builder.Services.AddSingleton<MongoDbClient>();

builder.Services.AddScoped<IOrganisationService, OrganisationService>();

builder.Services.AddScoped<IOrganisationRepository, OrganisationRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
