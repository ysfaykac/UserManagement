using System.Linq.Expressions;
using UserService.Domain.Abstract;

namespace UserService.Application.Abstract;

public interface IGenericRepository<T> : IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAll();

    Task<List<T>> Get(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);

    Task<List<T>> Get(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
    Task<T?> GetById(Guid id);
    Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
    Task<T?> GetSingleAsync(Expression<Func<T?, bool>> expression, params Expression<Func<T, object>>[] includes);
    Task<T?> GetSingleAsync(Expression<Func<T?, bool>> expression);
    Task<T> AddAsync(T entity);
    Task<bool> DeleteById(Guid id);
    T Update(T entity);
}