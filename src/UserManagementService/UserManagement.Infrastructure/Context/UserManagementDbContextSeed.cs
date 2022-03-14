using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.AggregateModels;

namespace UserManagement.Infrastructure.Context;

public class UserManagementDbContextSeed
{
    public async Task SeedAsync(UserManagementDbContext context, IServiceProvider serviceProvider)
    {
        await using (context)
        {
            context.Database.Migrate();
           
            if (!context.UserStatus.Any())
            {
                var datas = GetPredefinedUserStatus();
                context.UserStatus.AddRange(datas);
            }
            await context.SaveChangesAsync();
        }
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