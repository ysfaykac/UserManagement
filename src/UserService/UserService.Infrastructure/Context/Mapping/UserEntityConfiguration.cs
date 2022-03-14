using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.AggregateModels;

namespace UserService.Infrastructure.Context.Mapping;

public class UserEntityConfiguration: BaseEntityMap<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.ToTable("USERS", UserDbContext.DefaultSchema);

        builder.Property(t => t.UserName).IsRequired().HasColumnName("USERNAME");
        builder.Property(t => t.Password).IsRequired().HasColumnName("PASSWORD");

        builder
            .Property<int>("userStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UserStatusId")
            .IsRequired();

        builder.Property(t => t.IsEnabled).IsRequired().HasColumnName("IS_ENABLED").HasDefaultValue(true);
        builder.HasMany(t => t.UserRoles).WithOne().HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
        
        var navigation = builder.Metadata.FindNavigation(nameof(User.UserRoles));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
      

        builder.HasOne(o => o.UserStatus)
            .WithMany()
            .HasForeignKey("userStatusId");
    }
}