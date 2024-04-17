using BackOffice.API.Dto;
using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;

namespace BackOffice.API.Services.Interfaces;

public interface IOrganisationService
{
    Task<Organisation> AddAsync(OrganisationCreateDto organisationCreateDto);
    Task<IEnumerable<Organisation>> GetAllAsync();
    Task<IEnumerable<Organisation>> GetAllByUserIdAsync(Guid userId);
    Task<Organisation> FindAsync(Guid id);
    Task<Organisation> Update(Guid id, Organisation organisation);
    Task<bool> Delete(Guid id);
}