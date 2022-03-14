using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.AggregateModels;

namespace UserService.Infrastructure.Context.Mapping;

public class UserRoleEntityConfiguration:BaseEntityMap<UserRole>
{
    public override void Configure(EntityTypeBuilder<UserRole> builder)
    {
        base.Configure(builder);
        builder.ToTable("USER_ROLES", UserDbContext.DefaultSchema);
        builder.Property(t => t.UserId).IsRequired().HasColumnName("USER_ID");
        builder.Property(t => t.RoleId).IsRequired().HasColumnName("ROLE_ID");
        builder.HasOne(t => t.Role).WithMany().HasForeignKey(t => t.RoleId);
    }
}