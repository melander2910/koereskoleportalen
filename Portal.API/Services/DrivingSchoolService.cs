using Portal.API.Models;
using Portal.API.Repositories.Interfaces;

namespace Portal.API.Services.Interfaces;

public class DrivingSchoolService : IDrivingSchoolService
{
    private readonly IDrivingSchoolRepository _drivingSchoolRepository;

    public DrivingSchoolService(IDrivingSchoolRepository drivingSchoolRepository)
    {
        _drivingSchoolRepository = drivingSchoolRepository;
    }

    public async Task<List<DrivingSchool>> GetAsync()
    {
        return await _drivingSchoolRepository.GetAllAsync();
    }

    public async Task<DrivingSchool> GetAsync(string id)
    {
        return await _drivingSchoolRepository.GetAsync(id);
    }
}