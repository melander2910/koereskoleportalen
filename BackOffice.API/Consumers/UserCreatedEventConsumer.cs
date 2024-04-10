using BackOffice.API.Dto;
using BackOffice.API.Services.Interfaces;
using Contracts;
using MassTransit;

namespace BackOffice.API.Consumers;

public sealed class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{
    private readonly IUserService _userService;

    public UserCreatedEventConsumer(IUserService userService)
    {
        _userService = userService;
    }
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    { 
        Console.WriteLine("Consuming user created " + context.Message.Id);
        var user = new UserSignupDto
        {
            Id = context.Message.Id,
            Firstname = context.Message.Firstname,
            Lastname = context.Message.Lastname,
            PhoneNumber = context.Message.PhoneNumber,
            Address = context.Message.Address
        };
        await _userService.AddAsync(user);
    }
    
}

// public class UserConsumerDefinition : ConsumerDefinition<UserCreatedEventConsumer>
// {
//     protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserCreatedEventConsumer> consumerConfigurator)
//     {
//         consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
//     }
// }