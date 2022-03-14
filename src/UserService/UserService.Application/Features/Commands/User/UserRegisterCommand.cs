using MediatR;
using UserService.Domain.Models;

namespace UserService.Application.Features.Commands.User;

public class UserRegisterCommand:IRequest<ServiceResult<string>>
{

    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public UserRegisterCommand(string username, string password, string firstName, string lastName, string email)
    {
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}