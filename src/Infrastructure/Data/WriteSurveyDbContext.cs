using Microsoft.EntityFrameworkCore;
using oig_assessment.Application.Common.Interfaces;
using oig_assessment.Domain.Entities;

namespace oig_assessment.Infrastructure.Data;
public class WriteSurveyDbContext : DbContext, IWriteSurveyDbContext
{
    public WriteSurveyDbContext(DbContextOptions<WriteSurveyDbContext> options)
        : base(options) { }
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Survey> Surveys { get; set; } = null!;

    public DbSet<Answer> Answers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteSurveyDbContext).Assembly,
            WriteConfigurationFilter);
    }

    private static bool WriteConfigurationFilter(Type type) =>
        type.FullName?.Contains("Configurations.Write") ?? false;
}
