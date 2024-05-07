namespace BackOffice.API.Models.DatabaseEntities;

public class ProductionUnitRemoved
{
    public int Id { get; set; }
    public string TenantId { get; set; }
    public DateTime  RemovedDate { get; set; }
    public ProductionUnit ProductionUnit { get; set; }
}