using MediatR;
using UserService.Application.Dtos.User;
using UserService.Domain.Models;

namespace UserService.Application.Features.Queries.User;

public class LoginRequestQuery:IRequest<AuthenticationTicketInfo>
{
    public LoginRequestQuery(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
}