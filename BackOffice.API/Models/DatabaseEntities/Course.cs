using BackOffice.API.Models.Abstracts;

namespace BackOffice.API.Models.DatabaseEntities;

// TODO: Multi tenancy subtenant testing purposes

public class Course : SubTenant
{
    public string Name { get; set; }
    public ProductionUnit ProductionUnit { get; set; }
    public Guid ProductionUnitId { get; set; }
}