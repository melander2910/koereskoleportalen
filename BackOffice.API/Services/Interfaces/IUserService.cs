using BackOffice.API.Dto;
using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;

namespace BackOffice.API.Services.Interfaces;

public interface IUserService
{
    Task<User> AddAsync(UserSignupDto user);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> GetAllByOrganisationIdAsync(Guid organisationId);
    Task<IEnumerable<User>> GetAllByProductionUnitIdAsync(Guid productionUnitId);
    Task<User> FindAsync(Guid id);
    Task<User> Update(Guid id, UserUpdateDto user);
    Task<bool> Delete(Guid id);
}