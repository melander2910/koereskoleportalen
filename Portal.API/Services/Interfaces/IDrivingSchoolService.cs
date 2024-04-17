using Portal.API.Models;

namespace Portal.API.Services.Interfaces;

public interface IDrivingSchoolService
{
    Task<List<DrivingSchool>> GetAsync();
    Task<DrivingSchool> GetAsync(string id);
}