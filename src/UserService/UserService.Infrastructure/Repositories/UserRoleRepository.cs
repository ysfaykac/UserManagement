using UserService.Application.Abstract;
using UserService.Domain.AggregateModels;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Repositories;

public class UserRoleRepository:EfGenericRepository<UserRole>,IUserRoleRepository
{
    public UserRoleRepository(UserDbContext shopDbContext) : base(shopDbContext)
    {
    }
}