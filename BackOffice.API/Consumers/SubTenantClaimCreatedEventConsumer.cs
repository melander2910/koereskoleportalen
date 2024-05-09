using BackOffice.API.Services.Interfaces;
using Contracts;
using MassTransit;

namespace BackOffice.API.Consumers;

public sealed class SubTenantClaimCreatedEventConsumer : IConsumer<SubTenantClaimCreatedEvent>
{
    private readonly ICurrentTenantService _currentTenantService;
    private readonly IProductionUnitService _productionUnitService;
    private readonly IUserService _userService;

    public SubTenantClaimCreatedEventConsumer(ICurrentTenantService currentTenantService, IProductionUnitService productionUnitService, IUserService userService)
    {
        _currentTenantService = currentTenantService;
        _productionUnitService = productionUnitService;
        _userService = userService;
    }

    public async Task Consume(ConsumeContext<SubTenantClaimCreatedEvent> context)
    {
        var user = await _userService.FindAsync(context.Message.UserId);
        
        var productionUnit = await _productionUnitService.FindByProductionUnitNumber(context.Message.ProductionUnitNumber);

        await _userService.AddProductionUnitUserReference(user, productionUnit);
        Console.WriteLine("reference created");
    }
}
