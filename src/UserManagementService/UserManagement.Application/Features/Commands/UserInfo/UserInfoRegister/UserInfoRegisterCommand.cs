using MediatR;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Commands.UserInfo.UserInfoRegister;

public class UserInfoRegisterCommand:IRequest<ServiceResult<string>>
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public UserInfoRegisterCommand(Guid id,string username,string firstName, string lastName, string email)
    {
        Id = id;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}