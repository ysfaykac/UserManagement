using MediatR;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Commands.UserInfo.DeclineRegistration;

public class DeclineRegistrationCommand : IRequest<ServiceResult<string>>
{
    public DeclineRegistrationCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }

}