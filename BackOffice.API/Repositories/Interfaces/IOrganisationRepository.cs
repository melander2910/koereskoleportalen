using BackOffice.API.Models;

namespace BackOffice.API.Repositories.Interfaces;

public interface IOrganisationRepository
{
    Task<Organisation> AddAsync(Organisation organisation);
    Task<IEnumerable<Organisation>> GetAllAsync();
    Task<IEnumerable<Organisation>> GetAllByUserIdAsync(Guid userId);
    Task<Organisation> FindAsync(Guid id);
    Task<Organisation> Update(Guid id, Organisation organisation);
    Task<bool> Delete(Guid id);
}