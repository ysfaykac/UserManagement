using MediatR;
using UserService.Domain.Models;

namespace UserService.Application.Features.Commands.User;

public class UpdateUserCommand:IRequest<ServiceResult<string>>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}