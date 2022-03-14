using UserManagement.Api.Models;

namespace UserManagement.Api.Services;

public class UserManagementService:IUserManagementService
{
    private UserManagement.UserManagementClient _userManagementClient;
    public UserManagementService(UserManagement.UserManagementClient userManagementClient)
    {
        _userManagementClient = userManagementClient;
    }
    public async Task<string> AcceptRegistration(Guid userId)
    {
        var result =await _userManagementClient.AcceptRegistrationAsync(new AcceptRegistrationRequest()
        {
            UserId = userId.ToString()
        });
        return result.Message;
    }

    public async Task<string> DeclineRegistration(Guid userId)
    {
        var result = await _userManagementClient.DeclineRegistrationAsync(new DeclineRegistrationRequest()
        {
            UserId = userId.ToString()
        });
        return result.Message;
    }

    public async Task<string> DisableUser(Guid userId)
    {
        var result = await _userManagementClient.DisableUserAsync(new DisableUserRequest()
        {
            UserId = userId.ToString()
        });
        return result.Message;
    }

    public async Task<string> EnableUser(Guid userId)
    {
        var result = await _userManagementClient.EnableUserAsync(new EnableUserRequest()
        {
            UserId = userId.ToString()
        });
        return result.Message;
    }

    public async Task<List<UserModel>> GetUserList()
    {
        var result = await _userManagementClient.GetUserListAsync(new GetUserListRequest()
        {
        });
        var userList = result.User.Select(t => new UserModel()
        {
            Email = t.Email,
            FirstName = t.FirstName,
            Id = t.Id,
            LastName = t.LastName,
            UserName = t.UserName
        }).ToList();
        return userList;
    }
}