using Portal.API.Models;
using Portal.API.Repositories.Interfaces;
using Portal.API.Services.Interfaces;

namespace Portal.API.Services;

public class OrganisationService : IOrganisationService
{
    private readonly IOrganisationRepository _organisationRepository;

    public OrganisationService(IOrganisationRepository organisationRepository)
    {
        _organisationRepository = organisationRepository;
    }

    public async Task<List<Organisation>> GetAsync()
    {
        return await _organisationRepository.GetAllAsync();
    }
    
    public async Task<Organisation> GetAsync(string id)
    {
        return await _organisationRepository.GetAsync(id);
    }
}