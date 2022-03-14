using UserService.Application.Abstract;
using UserService.Domain.AggregateModels;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Repositories;

public class UserRepository:EfGenericRepository<User>,IUserRepository
{
    public UserRepository(UserDbContext shopDbContext) : base(shopDbContext)
    {
    }
}