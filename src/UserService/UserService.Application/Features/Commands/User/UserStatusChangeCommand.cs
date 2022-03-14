using MediatR;
using UserService.Domain.AggregateModels;

namespace UserService.Application.Features.Commands.User;

public class UserStatusChangeCommand:IRequest<bool>
{
    public UserStatusChangeCommand(Guid userId, UserStatus userStatus)
    {
        UserId = userId;
        UserStatus = userStatus;
    }
    public Guid UserId { get; set; }
    public UserStatus UserStatus { get; set; }
}