using BackOffice.API.Data;
using BackOffice.API.Models;
using BackOffice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly Context _dbContext;

    public UserRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllByOrganisationIdAsync(Guid organisationId)
    {
        return await _dbContext.Users.Where(user => user.Organisations.Any(org => org.Id == organisationId)).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllByProductionUnitIdAsync(Guid productionUnitId)
    {
        return await _dbContext.Users.Where(user => user.ProductionUnits.Any(pu => pu.Id == productionUnitId)).ToListAsync();
    }

    public async Task<User> FindAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User> Update(Guid id, User user)
    {
        var updatedUser = _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return updatedUser.Entity;

    }

    public async Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}