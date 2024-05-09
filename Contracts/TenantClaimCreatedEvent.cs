namespace Contracts;

public class TenantClaimCreatedEvent
{
    public Guid UserId { get; set; }
    public string CVR { get; set; }
}