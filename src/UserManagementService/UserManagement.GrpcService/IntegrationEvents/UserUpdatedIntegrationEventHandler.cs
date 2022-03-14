using EventBus.Base.Abstraction;
using MediatR;
using UserManagement.Application.Features.Commands.UserInfo;
using UserManagement.Application.Features.Commands.UserInfo.UserUpdateUserName;

namespace UserManagement.GrpcService.IntegrationEvents
{
    public class UserUpdatedIntegrationEventHandler:IIntegrationEventHandler<UserUpdatedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserUpdatedIntegrationEvent> _logger;

        public UserUpdatedIntegrationEventHandler(IMediator mediator,ILogger<UserUpdatedIntegrationEvent> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Handle(UserUpdatedIntegrationEvent @event)
        {
            var response = await _mediator.Send(new UserUpdateUserNameCommand(@event.UserId,@event.UserName));
        }
    }
}
