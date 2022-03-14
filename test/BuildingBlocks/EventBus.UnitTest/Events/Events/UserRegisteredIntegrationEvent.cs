using EventBus.Base.Events;

namespace EventBus.UnitTest.Events.Events;

public class UserRegisteredIntegrationEvent:IntegrationEvent
{
    public int Id { get; set; }

    public UserRegisteredIntegrationEvent(int id)
    {
        Id = id;
    }
}