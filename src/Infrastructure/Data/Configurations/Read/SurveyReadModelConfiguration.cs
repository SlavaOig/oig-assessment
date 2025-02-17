using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Application.Models.Read;

namespace oig_assessment.Infrastructure.Data.Configurations.Read;
internal sealed class SurveyReadModelConfiguration : IEntityTypeConfiguration<SurveyReadModel>
{
    public void Configure(EntityTypeBuilder<SurveyReadModel> builder)
    {
        builder.ToTable("Surveys");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(s => s.StartDate)
            .IsRequired();

        builder.Property(s => s.EndDate)
            .IsRequired();

        builder.Property(s => s.Status)
            .IsRequired();

        builder.Property(s => s.CreatedBy)
            .IsRequired();

        builder.Property(s => s.UpdatedBy)
            .IsRequired(false);

        builder.HasMany(s => s.Questions)
            .WithOne()
            .HasForeignKey(q => q.SurveyId);
    }
}

