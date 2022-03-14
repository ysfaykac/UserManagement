using UserManagement.Application.Abstract;
using UserManagement.Domain.AggregateModels;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Repositories;

public class UserInfoRepository:EfGenericRepository<UserInfo>,IUserInfoRepository
{
    public UserInfoRepository(UserManagementDbContext shopDbContext) : base(shopDbContext)
    {
    }
}