namespace UserService.Domain.Abstract;

public interface IRepository<T>
{ 
    IUnitOfWork UnitOfWork { get; }

}