using EventBus.Base.Abstraction;
using MediatR;
using UserService.Application.Features.Commands.User;
using UserService.Domain.AggregateModels;

namespace UserService.Api.IntegrationEvents;

public class UserEnabledStatusUpdateIntegrationEventHandler:IIntegrationEventHandler<UserEnabledStatusUpdateIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserEnabledStatusUpdateIntegrationEventHandler> _logger;
    public UserEnabledStatusUpdateIntegrationEventHandler(IMediator mediator, ILogger<UserEnabledStatusUpdateIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Handle(UserEnabledStatusUpdateIntegrationEvent @event)
    {
        var response = await _mediator.Send(new UserEnabledStatusUpdateCommand(@event.UserId, @event.IsEnabled));
        _logger.LogInformation($"UserRegisteredApprovedIntegrationEvent result: {response}");
    }
}