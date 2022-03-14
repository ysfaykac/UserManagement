using EventBus.Base.Events;

namespace UserManagement.Application.IntegrationEvents;

public class UserRegisteredDeclinedIntegrationEvent:IntegrationEvent
{
    public Guid UserId { get; set; }

    public UserRegisteredDeclinedIntegrationEvent(Guid userId)
    {
        UserId = userId;

    }
}