namespace BackOffice.API.Models.Abstracts;

public abstract class Tenant : BaseEntity
{
    public string TenantId { get; set; }
}