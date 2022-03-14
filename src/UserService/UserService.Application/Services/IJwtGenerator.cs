using UserService.Application.Dtos.User;
using UserService.Domain.Models;

namespace UserService.Application.Services;

public interface IJwtGenerator
{
    AuthenticationTicketInfo CreateToken(UserDto user);
    AuthenticationTicketInfo RefreshToken(AuthenticationTicketInfo token);

}