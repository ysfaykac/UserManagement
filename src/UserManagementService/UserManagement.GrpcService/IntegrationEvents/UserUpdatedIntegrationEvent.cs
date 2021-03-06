using EventBus.Base.Events;

namespace UserManagement.GrpcService.IntegrationEvents
{

    public class UserUpdatedIntegrationEvent : IntegrationEvent
    {
        public UserUpdatedIntegrationEvent(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
        public Guid UserId { get; set; }

        public string UserName { get; set; }

    }
}
