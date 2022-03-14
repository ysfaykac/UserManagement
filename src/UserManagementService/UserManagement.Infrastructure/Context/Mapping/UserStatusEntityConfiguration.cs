using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.AggregateModels;

namespace UserManagement.Infrastructure.Context.Mapping;

public class UserStatusEntityConfiguration:IEntityTypeConfiguration<UserStatus>
{
    public void Configure(EntityTypeBuilder<UserStatus> builder)
    {
        builder.ToTable("USERSTATUS", UserManagementDbContext.DefaultSchema);
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Id).HasDefaultValue(1).ValueGeneratedNever().IsRequired().HasColumnName("ID");
        builder.Property(t => t.Name).IsRequired().HasColumnName("NAME");
    }
}