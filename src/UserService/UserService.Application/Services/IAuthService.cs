using UserService.Domain.Models;

namespace UserService.Application.Services;

public interface IAuthService
{
    Task<AuthenticationTicketInfo> GetAuthentication(string userName, string password);
}