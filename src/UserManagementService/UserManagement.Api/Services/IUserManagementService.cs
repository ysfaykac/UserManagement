using UserManagement.Api.Models;

namespace UserManagement.Api.Services;

public interface IUserManagementService
{
    Task<string> AcceptRegistration(Guid userId);
    Task<string> DeclineRegistration(Guid userId);
    Task<string> DisableUser(Guid userId);
    Task<string> EnableUser(Guid userId);
    Task<List<UserModel>> GetUserList();
}