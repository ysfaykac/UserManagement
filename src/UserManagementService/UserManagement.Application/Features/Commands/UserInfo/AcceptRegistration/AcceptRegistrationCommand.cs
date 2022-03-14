using MediatR;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Commands.UserInfo.AcceptRegistration;

public class AcceptRegistrationCommand:IRequest<ServiceResult<string>>
{
    public AcceptRegistrationCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }

}