using BackOffice.API.Services.Interfaces;
using Contracts;
using MassTransit;

namespace BackOffice.API.Consumers;

public sealed class TenantClaimCreatedEventConsumer : IConsumer<TenantClaimCreatedEvent>
{
    private readonly IOrganisationService _organisationService;
    private readonly IUserService _userService;

    public TenantClaimCreatedEventConsumer(IOrganisationService organisationService, IUserService userService)
    {
        _organisationService = organisationService;
        _userService = userService;
    }
    
    public async Task Consume(ConsumeContext<TenantClaimCreatedEvent> context)
    {
        var user = await _userService.FindAsync(context.Message.UserId);
        var organisation = await _organisationService.FindByCvrAsync(context.Message.CVR);
        
        await _userService.AddOrganisationUserReference(user, organisation);
        Console.WriteLine("reference created");
    }
}