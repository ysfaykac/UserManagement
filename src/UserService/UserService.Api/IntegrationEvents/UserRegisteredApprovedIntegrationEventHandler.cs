using EventBus.Base.Abstraction;
using MediatR;
using UserService.Application.Features.Commands.User;
using UserService.Application.IntegrationEvents;
using UserService.Domain.AggregateModels;

namespace UserService.Api.IntegrationEvents;

public class UserRegisteredApprovedIntegrationEventHandler : IIntegrationEventHandler<UserRegisteredApprovedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserRegisteredApprovedIntegrationEventHandler> _logger;

    public UserRegisteredApprovedIntegrationEventHandler(IMediator mediator,ILogger<UserRegisteredApprovedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Handle(UserRegisteredApprovedIntegrationEvent @event)
    {
        var response = await _mediator.Send(new UserStatusChangeCommand(@event.UserId,UserStatus.Approved));
        _logger.LogInformation($"UserRegisteredApprovedIntegrationEvent result: {response}");
    }
}