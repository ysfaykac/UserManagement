using EventBus.Base.Events;

namespace UserManagement.GrpcService.IntegrationEvents;

public class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRegisteredIntegrationEvent(Guid userId, string userName, string firstName, string lastName, string email)
    {
        UserId = userId;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}