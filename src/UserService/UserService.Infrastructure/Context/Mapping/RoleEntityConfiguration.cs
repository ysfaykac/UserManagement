using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.AggregateModels;

namespace UserService.Infrastructure.Context.Mapping;

public class RoleEntityConfiguration:BaseEntityMap<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);
        builder.ToTable("ROLES", UserDbContext.DefaultSchema);
        builder.Property(t => t.Name).IsRequired().HasColumnName("NAME");
        builder.Property(t => t.Description).IsRequired().HasColumnName("DESCRIPTION");
    }
}