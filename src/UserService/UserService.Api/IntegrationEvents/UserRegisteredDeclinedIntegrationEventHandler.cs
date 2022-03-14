using EventBus.Base.Abstraction;
using MediatR;
using UserService.Application.Features.Commands.User;
using UserService.Domain.AggregateModels;

namespace UserService.Api.IntegrationEvents
{
    public class UserRegisteredDeclinedIntegrationEventHandler : IIntegrationEventHandler<UserRegisteredDeclinedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserRegisteredDeclinedIntegrationEventHandler> _logger;

        public UserRegisteredDeclinedIntegrationEventHandler(IMediator mediator, ILogger<UserRegisteredDeclinedIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(UserRegisteredDeclinedIntegrationEvent @event)
        {
            var response = await _mediator.Send(new UserStatusChangeCommand(@event.UserId, UserStatus.Declined));
            _logger.LogInformation($"UserRegisteredDeclinedIntegrationEvent result: {response}");
        }
    }
}
