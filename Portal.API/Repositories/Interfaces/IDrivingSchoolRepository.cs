using Portal.API.Models;

namespace Portal.API.Repositories.Interfaces;

public interface IDrivingSchoolRepository
{
    Task<List<DrivingSchool>> GetAllAsync();
    Task<DrivingSchool> GetAsync(string id);
}