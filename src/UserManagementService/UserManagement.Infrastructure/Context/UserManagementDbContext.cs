using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Abstract;
using UserManagement.Domain.AggregateModels;
using UserManagement.Infrastructure.Context.Mapping;
using UserManagement.Infrastructure.Extensions;

namespace UserManagement.Infrastructure.Context;

public class UserManagementDbContext : DbContext, IUnitOfWork
{
    public const string DefaultSchema = "UserInfo";
    private readonly IMediator _mediator;

    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<UserStatus> UserStatus { get; set; }
   
    public UserManagementDbContext() : base()
    {

    }

    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options, IMediator mediator) : base(options)
    {
        this._mediator = mediator;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ModifyBaseEntities();
        await _mediator.DispatchDomainEventsAsync(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserInfoEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserStatusEntityConfiguration());
        modelBuilder.AddGlobalDeletedFilter();

    }


    private void ModifyBaseEntities()
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(x => (x.Entity is ISoftDeletedEntity)
                        && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

        foreach (var entry in modifiedEntries)
        {
            if (entry.Entity is ISoftDeletedEntity entity)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is ISoftDeletedEntity)
                    {
                        entity.IsDeleted = false;
                    }
                }
                else
                {
                    if (entry.Entity is ISoftDeletedEntity deletedEntity && entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        deletedEntity.IsDeleted = true;
                    }
                }
            }
        }

        var modifiedEntites = ChangeTracker.Entries()
            .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)).ToList();
        foreach (var entry in modifiedEntites)
        {
            foreach (var prop in entry.Properties)
            {
                if (prop.Metadata.ClrType == typeof(DateTime))
                {
                    prop.Metadata.FieldInfo.SetValue(entry.Entity, DateTime.SpecifyKind((DateTime)prop.CurrentValue, DateTimeKind.Utc));
                }
                else if (prop.Metadata.ClrType == typeof(DateTime?) && prop.CurrentValue != null)
                {
                    prop.Metadata.FieldInfo.SetValue(entry.Entity, DateTime.SpecifyKind(((DateTime?)prop.CurrentValue).Value, DateTimeKind.Utc));
                }
            }
        }
    }

}