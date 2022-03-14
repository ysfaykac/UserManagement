using EventBus.Base.Events;

namespace UserManagement.Application.IntegrationEvents;

public class UserRegisteredApprovedIntegrationEvent:IntegrationEvent
{
    public Guid UserId { get; set; }
   
    public UserRegisteredApprovedIntegrationEvent(Guid userId)
    {
        UserId = userId;
       
    }
}