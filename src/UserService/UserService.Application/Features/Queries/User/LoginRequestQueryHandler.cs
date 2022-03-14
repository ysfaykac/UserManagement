using MediatR;
using UserService.Application.Abstract;
using UserService.Application.Dtos.User;
using UserService.Application.Services;
using UserService.Domain.Models;

namespace UserService.Application.Features.Queries.User;

public class LoginRequestQueryHandler : IRequestHandler<LoginRequestQuery, AuthenticationTicketInfo>
{
    private readonly IAuthService _authService;

    public LoginRequestQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<AuthenticationTicketInfo> Handle(LoginRequestQuery request, CancellationToken cancellationToken)
    {
        return _authService.GetAuthentication(request.UserName, request.Password);
    }
}