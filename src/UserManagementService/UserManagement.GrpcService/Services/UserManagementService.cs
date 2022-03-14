using Grpc.Core;
using MediatR;
using UserManagement.Application.Features.Commands.UserInfo;
using UserManagement.Application.Features.Commands.UserInfo.AcceptRegistration;
using UserManagement.Application.Features.Commands.UserInfo.DeclineRegistration;
using UserManagement.Application.Features.Commands.UserInfo.GetUserList;
using UserManagement.Application.Features.Commands.UserInfo.SetUserEnableStatus;

namespace UserManagement.GrpcService.Services;

public class UserManagementService:UserManagement.UserManagementBase
{
    private IMediator _mediator;
    public UserManagementService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override async Task<AcceptRegistrationResponse> AcceptRegistration(AcceptRegistrationRequest request, ServerCallContext context)
    {
        AcceptRegistrationResponse response = new AcceptRegistrationResponse();
        var result =await _mediator.Send(new AcceptRegistrationCommand(new Guid(request.UserId)));
        response.Message = result.Result;
        return response;
    }

    public override async Task<DeclineRegistrationResponse> DeclineRegistration(DeclineRegistrationRequest request, ServerCallContext context)
    {
        DeclineRegistrationResponse response = new DeclineRegistrationResponse();
        var result = await _mediator.Send(new DeclineRegistrationCommand(new Guid(request.UserId)));
        response.Message = result.Result;
        return response;
    }

    public override async Task<DisableUserResponse> DisableUser(DisableUserRequest request, ServerCallContext context)
    {
        DisableUserResponse response = new DisableUserResponse();
        var result = await _mediator.Send(new SetUserEnableCommand(new Guid(request.UserId),false));
        response.Message = result.ToString();
        return response;
    }

    public override async Task<EnableUserResponse> EnableUser(EnableUserRequest request, ServerCallContext context)
    {
        EnableUserResponse response = new EnableUserResponse();
        var result = await _mediator.Send(new SetUserEnableCommand(new Guid(request.UserId), true));
        response.Message = result.ToString();
        return response;
    }

    public override async Task<GetUserListResponse> GetUserList(GetUserListRequest request, ServerCallContext context)
    {
        GetUserListResponse response = new GetUserListResponse();
        var result = await _mediator.Send(new GetUserListCommand());
        response.User.AddRange(result.Select(t=>new User()
        {
            Email = t.Email,
            FirstName = t.FirstName,
            Id = t.Id.ToString(),
            LastName = t.LastName,
            UserName = t.UserName,

        }));
        return response;
    }
}