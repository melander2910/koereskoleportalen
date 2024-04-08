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

    public Task<Organisation> AddAsync(Organisation organisation)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Organisation>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Organisation>> GetAllByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Organisation> FindAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Organisation> Update(Guid id, Organisation organisation)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}