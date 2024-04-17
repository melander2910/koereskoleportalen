namespace BackOffice.API.Models.Abstracts;

public abstract class SubTenant : BaseEntity
{
    public string TenantId { get; set; }
    public string SubTenantId { get; set; }
}