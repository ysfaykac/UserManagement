using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Abstract;

namespace UserManagement.Infrastructure.Extensions;

public static class ModelBuilderExt
{
    public static void AddGlobalDeletedFilter(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletedEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.SetDeleteFilter(entityType.ClrType);
            }
        }
    }
    public static void SetDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
    {
        SetDeleteFilterMethod.MakeGenericMethod(entityType)
            .Invoke(null, new object[] { modelBuilder });
    }

    static readonly MethodInfo SetDeleteFilterMethod = typeof(ModelBuilderExt)
        .GetMethods(BindingFlags.Public | BindingFlags.Static)
        .Single(t => t.IsGenericMethod && t.Name == "SetDeleteFilter");

    public static void SetDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
        where TEntity : class, ISoftDeletedEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
    }
}