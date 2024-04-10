using Portal.API.Models;

namespace Portal.API.Services.Interfaces;

public interface IOrganisationService
{
    Task<List<Organisation>> GetAsync();
    Task<Organisation> GetAsync(string id);
}