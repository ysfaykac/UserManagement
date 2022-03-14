using MediatR;

namespace UserService.Application.Features.Commands.User;

public class UserEnabledStatusUpdateCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    public bool IsEnabled { get; set; }

    public UserEnabledStatusUpdateCommand(Guid id,bool isEnabled)
    {
        Id = id;
        IsEnabled=isEnabled;
    }
}