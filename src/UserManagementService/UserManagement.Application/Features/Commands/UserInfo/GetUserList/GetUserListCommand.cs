using MediatR;
using UserManagement.Application.Dtos;

namespace UserManagement.Application.Features.Commands.UserInfo.GetUserList;

public class GetUserListCommand:IRequest<List<UserInfoDto>>
{
    
}