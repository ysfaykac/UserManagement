using EventBus.Base.Events;

namespace UserService.Api.IntegrationEvents
{
    public class UserRegisteredDeclinedIntegrationEvent: IntegrationEvent
    {
        public Guid UserId { get; set; }

        public UserRegisteredDeclinedIntegrationEvent(Guid userId)
        {
            UserId = userId;

        }
    }
}
