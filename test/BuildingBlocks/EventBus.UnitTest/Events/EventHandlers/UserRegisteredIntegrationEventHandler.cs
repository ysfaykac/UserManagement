using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EventBus.Base.Abstraction;
using EventBus.UnitTest.Events.Events;

namespace EventBus.UnitTest.Events.EventHandlers;

public class UserRegisteredIntegrationEventHandler : IIntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public Task Handle(UserRegisteredIntegrationEvent @event)
    {
        Debug.WriteLine("Handle method worked with id: " + @event.Id);
        return Task.CompletedTask;
    }
}