using Portal.API.Models;

namespace Portal.API.Repositories.Interfaces;

public interface IOrganisationRepository
{
    Task<List<Organisation>> GetAllAsync();
    Task<Organisation> GetAsync(string id);
}