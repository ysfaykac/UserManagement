using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Abstract;
using UserManagement.Domain.Abstract;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Repositories;

public class EfGenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity:BaseEntity
{
    protected readonly UserManagementDbContext UserDbContext;

    public EfGenericRepository(UserManagementDbContext shopDbContext)
    {
        UserDbContext = shopDbContext ?? throw new ArgumentNullException(nameof(shopDbContext));
    }

    public IUnitOfWork UnitOfWork => UserDbContext;

    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity?, bool>> expression)
    {
        IQueryable<TEntity?> query = UserDbContext.Set<TEntity>();

        return await query.SingleOrDefaultAsync(expression);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await UserDbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var entity = await GetById(id);
        UserDbContext.Set<TEntity>().Remove(entity);
        return true;
    }

    public virtual Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        return Get(filter, null, includes);
    }

    public virtual async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = UserDbContext.Set<TEntity>();
        query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ToListAsync();
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await UserDbContext.Set<TEntity>().ToListAsync();

    }


    public virtual async Task<TEntity?> GetById(Guid id)
    {
        return await UserDbContext.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity?> query = UserDbContext.Set<TEntity>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(t => t.Id == id);
    }

    public virtual async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity?, bool>> expression,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity?> query = UserDbContext.Set<TEntity>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.SingleOrDefaultAsync(expression);
    }


    public TEntity Update(TEntity entity)
    {
        UserDbContext.Set<TEntity>().Update(entity);
        return entity;
    }
}