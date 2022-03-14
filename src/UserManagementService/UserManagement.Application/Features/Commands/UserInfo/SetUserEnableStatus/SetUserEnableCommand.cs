using MediatR;

namespace UserManagement.Application.Features.Commands.UserInfo.SetUserEnableStatus;

public class SetUserEnableCommand:IRequest<bool>
{
    public SetUserEnableCommand(Guid userId, bool isEnabled)
    {
        UserId = userId;
        IsEnabled = isEnabled;
    }

    public Guid UserId { get; set;}
    public bool IsEnabled { get; set; }
}