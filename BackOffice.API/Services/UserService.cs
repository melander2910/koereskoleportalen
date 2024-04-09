using BackOffice.API.Dto;
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

    public async Task<User> AddAsync(UserSignupDto userDto)
    {
        var user = new User
        {
            Id = userDto.Id,
            CreatedDate = DateTime.UtcNow,
            Firstname = userDto.Firstname,
            Lastname = userDto.Lastname,
            PhoneNumber = userDto.PhoneNumber,
            Address = userDto.Address
        };
        
        return await _userRepository.AddAsync(user);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<IEnumerable<User>> GetAllByOrganisationIdAsync(Guid organisationId)
    {
        return await _userRepository.GetAllByOrganisationIdAsync(organisationId);
    }

    public async Task<IEnumerable<User>> GetAllByProductionUnitIdAsync(Guid productionUnitId)
    {
        return await _userRepository.GetAllByProductionUnitIdAsync(productionUnitId);
    }

    public async Task<User> FindAsync(Guid id)
    {
        return await _userRepository.FindAsync(id);
    }

    public async Task<User> Update(Guid id, UserUpdateDto userDto)
    {
        var user = await _userRepository.FindAsync(id);
        user.ModifiedDate = DateTime.UtcNow;
        user.Firstname = userDto.Firstname;
        user.Lastname = userDto.Lastname;
        user.Address = userDto.Address;
        
        return await _userRepository.Update(id, user);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _userRepository.Delete(id);
    }
}