using UserService.Application.Abstract;
using UserService.Domain.AggregateModels;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Repositories;

public class RoleRepository:EfGenericRepository<Role>,IRoleRepository
{
    public RoleRepository(UserDbContext shopDbContext) : base(shopDbContext)
    {
    }
}