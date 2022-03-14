using MediatR;

namespace UserManagement.Application.Features.Commands.UserInfo.UserUpdateUserName;

public class UserUpdateUserNameCommand:IRequest<bool>
{
    public UserUpdateUserNameCommand(Guid userId, string userName)
    {
        UserId = userId;
        UserName = userName;
    }

    public Guid UserId { get; set; }
    public string UserName { get; set; }
    
}