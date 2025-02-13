using Microsoft.EntityFrameworkCore;
using oig_assessment.Application.Common.Interfaces;
using oig_assessment.Application.Models.Read;

namespace oig_assessment.Infrastructure.Data;
public sealed class ReadSurveyDbContext : DbContext, IReadSurveyDbContext
{
    public ReadSurveyDbContext(DbContextOptions<ReadSurveyDbContext> options)
        : base(options) { }
    public DbSet<UserReadModel> Users { get; set; }

    public DbSet<SurveyReadModel> Surveys { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadSurveyDbContext).Assembly,
            ReadConfigurationFilter);
    }

    private static bool ReadConfigurationFilter(Type type) =>
        type.FullName?.Contains("Configurations.Read") ?? false;
}
