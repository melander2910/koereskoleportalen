using BackOffice.API.Models;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services.Interfaces;

namespace BackOffice.API.Services;

public class OrganisationService : IOrganisationService
{
    private readonly IOrganisationRepository _organisationRepository;
    
    public OrganisationService(IOrganisationRepository organisationRepository)
    {
        _organisationRepository = organisationRepository;
    }

    public async Task<Organisation> AddAsync(Organisation organisation)
    {
        return await _organisationRepository.AddAsync(organisation);
    }

    public async Task<IEnumerable<Organisation>> GetAllAsync()
    {
        return await _organisationRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Organisation>> GetAllByUserIdAsync(Guid userId)
    {
        return await _organisationRepository.GetAllByUserIdAsync(userId);
    }

    public async Task<Organisation> FindAsync(Guid id)
    {
        return await _organisationRepository.FindAsync(id);
    }

    public async Task<Organisation> Update(Guid id, Organisation organisation)
    {
        return await _organisationRepository.Update(id, organisation);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _organisationRepository.Delete(id);
    }
}