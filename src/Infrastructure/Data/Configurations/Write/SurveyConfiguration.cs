using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Domain.Entities;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Infrastructure.Data.Configurations.Write;
public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.ToTable("Surveys");

        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Id)
            .HasConversion(
                id => id.Value,
                value => new SurveyId(value))
            .HasDefaultValueSql("NEWID()")
            .ValueGeneratedNever();

        builder.Property(_ => _.CreatedBy)
           .HasConversion(
               id => id.Value,
               value => new UserId(value))
           .HasColumnName("CreatedBy")
           .IsRequired()
           .ValueGeneratedNever();

        builder.Property(_ => _.UpdatedBy)
           .HasConversion(
               id => id != null ? id.Value : (Guid?)null,
               value => value != null ? new UserId(value.Value) : (UserId?)null)
           .HasColumnName("UpdatedBy")
           .HasDefaultValueSql("NULL");


        builder.Property(_ => _.Name).IsRequired().HasMaxLength(255);

        builder.Property(_ => _.StartDate).IsRequired();

        builder.Property(_ => _.EndDate).IsRequired();

        builder.Property(_ => _.Status)
               .HasConversion(
                x => x.Value,
                v => SurveyStatus.FromValue(v));

        builder.Ignore(_ => _.Questions);

        builder.OwnsMany<Question>("_questions", question =>
        {
            question.HasKey(q => q.Id);

            question.WithOwner().HasForeignKey(q => q.SurveyId);

            question.Property(q => q.Id)
                .HasConversion(
                    id => id.Value,
                    value => new QuestionId(value))
                .ValueGeneratedNever();

            question.Property(q => q.SurveyId)
                .HasConversion(
                    id => id.Value,
                    value => new SurveyId(value))
                .IsRequired();

            question.Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(500);

            question.Property(q => q.CreatedBy)
                .HasConversion(
                       id => id.Value,
                       value => new UserId(value))
                   .HasColumnName("CreatedBy")
                   .IsRequired()
                   .ValueGeneratedNever();

            question.Property(q => q.UpdatedBy)
                   .HasConversion(
                       id => id != null ? id.Value : (Guid?)null,
                       value => value != null ? new UserId(value.Value) : (UserId?)null)
                   .HasColumnName("UpdatedBy")
                   .HasDefaultValueSql("NULL");

            question.ToTable("Questions");
        });

    }
}
