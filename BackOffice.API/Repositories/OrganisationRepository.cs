using BackOffice.API.Data;
using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.API.Repositories;

public class OrganisationRepository : IOrganisationRepository
{
    private readonly Context _dbContext;
    
    public OrganisationRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Organisation> AddAsync(Organisation organisation)
    {
        await _dbContext.Organisations.AddAsync(organisation);
        await _dbContext.SaveChangesAsync();
        return organisation;
    }

    public async Task<IEnumerable<Organisation>> GetAllAsync()
    {
        return await _dbContext.Organisations.ToListAsync();
    }

    public async Task<IEnumerable<Organisation>> GetAllByUserIdAsync(Guid userId)
    {
        // TODO: Is there a better way to fetch Organisations by userId?
        return await _dbContext.Organisations.Where(org => org.Users.Any(user => user.Id == userId)).ToListAsync();
    }

    public async Task<Organisation> FindAsync(Guid id)
    {
        return await _dbContext.Organisations.FindAsync(id);
    }

    public async Task<Organisation> Update(Guid id, Organisation organisation)
    {
        _dbContext.Organisations.Update(organisation);
        await _dbContext.SaveChangesAsync();
        return organisation;
    }

    public async Task<bool> Delete(Guid id)
    {
        var organisation = await _dbContext.Organisations.FindAsync(id);
        if (organisation == null)
        {
            throw new ArgumentNullException(nameof(organisation));
        }
        
        _dbContext.Organisations.Remove(organisation);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<Organisation> FindByCvrAsync(string cvr)
    {
        return await _dbContext.Organisations.FirstOrDefaultAsync(o => o.CVR == cvr);
    }
}