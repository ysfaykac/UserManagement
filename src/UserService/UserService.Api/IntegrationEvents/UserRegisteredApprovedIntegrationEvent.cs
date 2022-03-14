using EventBus.Base.Events;

namespace UserService.Api.IntegrationEvents;

public class UserRegisteredApprovedIntegrationEvent : IntegrationEvent
{
    public Guid UserId { get; set; }

    public UserRegisteredApprovedIntegrationEvent(Guid userId)
    {
        UserId = userId;

    }
}