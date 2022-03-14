using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.AggregateModels;

namespace UserManagement.Infrastructure.Context.Mapping;

public class UserInfoEntityConfiguration: BaseEntityMap<UserInfo>
{
    public override void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        base.Configure(builder);
        builder.ToTable("USERINFOS", UserManagementDbContext.DefaultSchema);
        builder.Property(t => t.UserName).IsRequired().HasColumnName("USERNAME");
        builder.Property(t => t.FirstName).IsRequired().HasColumnName("FIRST_NAME");
        builder.Property(t => t.LastName).IsRequired().HasColumnName("LAST_NAME");
        builder.Property(t => t.Email).IsRequired().HasColumnName("EMAIL");
        builder
            .Property<int>("userStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UserStatusId")
            .IsRequired();

        builder.HasOne(o => o.UserStatus)
            .WithMany()
            .HasForeignKey("userStatusId");

        builder.Property(t => t.IsEnabled).IsRequired().HasColumnName("IS_ENABLED").HasDefaultValue(true);

    }
}