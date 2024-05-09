namespace Contracts;

public class SubTenantClaimCreatedEvent
{
    public Guid UserId { get; set; }
    public string ProductionUnitNumber { get; set; }
}