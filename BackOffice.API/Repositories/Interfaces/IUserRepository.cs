using BackOffice.API.Models;

namespace BackOffice.API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> GetAllByOrganisationIdAsync(Guid organisationId);
    Task<IEnumerable<User>> GetAllByProductionUnitIdAsync(Guid productionUnitId);
    Task<User> FindAsync(Guid id);
    Task<User> Update(Guid id, User user);
    Task<bool> Delete(Guid id);
}