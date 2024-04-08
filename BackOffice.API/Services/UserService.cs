using BackOffice.API.Models;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services.Interfaces;

namespace BackOffice.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User> AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllByOrganisationIdAsync(Guid organisationId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllByProductionUnitIdAsync(Guid productionUnitId)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User> Update(Guid id, User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}