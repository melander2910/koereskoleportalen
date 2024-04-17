namespace CvrSync.Service.Models;

public class PortalDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;
    public string OrganisationsCollectionName { get; set; } = null!;

    public string DrivingSchoolsCollectionName { get; set; } = null!;
}