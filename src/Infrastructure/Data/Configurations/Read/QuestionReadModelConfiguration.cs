using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Application.Models.Read;

namespace oig_assessment.Infrastructure.Data.Configurations.Read;
internal sealed class QuestionReadModelConfiguration : IEntityTypeConfiguration<QuestionReadModel>
{
    public void Configure(EntityTypeBuilder<QuestionReadModel> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.QuestionText)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(q => q.SurveyId)
            .IsRequired();

        builder.Property(q => q.CreatedBy)
            .IsRequired();

        builder.Property(q => q.CreatedOn)
            .IsRequired();
    }
}

