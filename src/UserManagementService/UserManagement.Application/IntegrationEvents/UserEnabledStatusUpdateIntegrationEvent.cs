using EventBus.Base.Events;

namespace UserManagement.Application.IntegrationEvents;

public class UserEnabledStatusUpdateIntegrationEvent:IntegrationEvent
{
    public UserEnabledStatusUpdateIntegrationEvent(Guid userId, bool isEnabled)
    {
        UserId = userId;
        IsEnabled = isEnabled;
    }

    public Guid UserId { get; set; }
    public bool IsEnabled { get; set; }
    
}