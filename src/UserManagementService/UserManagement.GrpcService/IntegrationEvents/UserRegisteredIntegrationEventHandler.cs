using EventBus.Base.Abstraction;
using MediatR;
using UserManagement.Application.Features.Commands.UserInfo;
using UserManagement.Application.Features.Commands.UserInfo.UserInfoRegister;

namespace UserManagement.GrpcService.IntegrationEvents;

public class UserRegisteredIntegrationEventHandler:IIntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    private IMediator _mediator;
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger;

    public UserRegisteredIntegrationEventHandler(IMediator mediator,ILogger<UserRegisteredIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Handle(UserRegisteredIntegrationEvent @event)
    {
        var response = await _mediator.Send(new UserInfoRegisterCommand(@event.UserId, @event.UserName,
            @event.FirstName, @event.LastName, @event.Email));
        _logger.LogInformation(response.Result);
    }
}