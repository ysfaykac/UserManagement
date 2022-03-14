using Microsoft.EntityFrameworkCore;
using UserService.Domain.AggregateModels;

namespace UserService.Infrastructure.Context;

public class UserDbContextSeed
{
    public async Task SeedAsync(UserDbContext context, IServiceProvider serviceProvider)
    {
        await using (context)
        {
            context.Database.Migrate();
            if (!context.Roles.Any())
            {
                var datas = GetPredefinedRoles();
                context.Roles.AddRange(datas);
            }
            if (!context.UserStatus.Any())
            {
                var datas = GetPredefinedUserStatus();
                context.UserStatus.AddRange(datas);
            }
            await context.SaveChangesAsync();
        }
    }

    private IEnumerable<Role> GetPredefinedRoles()
    {
        List<Role> Roles = new List<Role>()
        {
            new Role("User","User"),
            new Role("Admin","Admin")
        };

        return Roles;
    }
    private IEnumerable<UserStatus> GetPredefinedUserStatus()
    {
        return new List<UserStatus>()
        {
            UserStatus.Approved,
            UserStatus.Pending,
            UserStatus.Declined,
            UserStatus.Blacklist
        };
    }
}