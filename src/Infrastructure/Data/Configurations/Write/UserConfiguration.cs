using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Domain.Entities;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Infrastructure.Data.Configurations.Write;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .HasDefaultValueSql("NEWID()")
            .ValueGeneratedNever();

        builder.Property(_ => _.LoginName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(_ => _.Role)
            .HasConversion(
            x => x.Value,
            v => UserRole.FromValue(v)
            )
            .IsRequired();
        
        builder.ToTable("Users");
    }
}
