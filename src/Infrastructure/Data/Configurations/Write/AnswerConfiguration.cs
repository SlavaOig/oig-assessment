using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Domain.Entities;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Infrastructure.Data.Configurations.Write;
public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Id)
            .HasConversion(
                id => id.Value,
                value => new AnswerId(value))
            .HasDefaultValueSql("NEWID()")
            .ValueGeneratedNever();

        builder.Property(_ => _.QuestionAnswer)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.SurveyId)
           .HasConversion(
               id => id.Value,
               value => new SurveyId(value))
           .HasDefaultValueSql("NEWID()")
           .ValueGeneratedNever();

        builder.Property(_ => _.QuestionId)
           .HasConversion(
               id => id.Value,
               value => new QuestionId(value))
           .HasDefaultValueSql("NEWID()")
           .ValueGeneratedNever();

        builder.Property(_ => _.AnsweredBy)
          .HasConversion(
              id => id.Value,
              value => new UserId(value))
          .HasDefaultValueSql("NEWID()")
          .ValueGeneratedNever();

        builder.ToTable("Answers");
    }
}
