using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Abstract;
using UserService.Domain.AggregateModels;
using UserService.Infrastructure.Context.Mapping;
using UserService.Infrastructure.Extensions;

namespace UserService.Infrastructure.Context;

public class UserDbContext : DbContext, IUnitOfWork
{
    public const string DefaultSchema = "user";
    private readonly IMediator _mediator;

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserStatus> UserStatus { get; set; }
    public UserDbContext() : base()
    {

    }

    public UserDbContext(DbContextOptions<UserDbContext> options, IMediator mediator) : base(options)
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
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
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