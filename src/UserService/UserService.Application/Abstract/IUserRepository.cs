using UserService.Domain.AggregateModels;

namespace UserService.Application.Abstract;

public interface IUserRepository:IGenericRepository<User>
{
    
}