using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Abstract;

namespace UserManagement.Infrastructure.Context.Mapping;

public class BaseEntityMap<T> :IEntityTypeConfiguration<T> where T:BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("ID").ValueGeneratedOnAdd();
        builder.Property(t => t.CreateDate).HasColumnName("CREATE_DATE").IsRequired()
            .ValueGeneratedOnAdd().HasDefaultValueSql("NOW()");
    }
}