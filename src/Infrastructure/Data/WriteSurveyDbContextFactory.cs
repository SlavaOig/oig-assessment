using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace oig_assessment.Infrastructure.Data;
public class WriteSurveyDbContextFactory : IDesignTimeDbContextFactory<WriteSurveyDbContext>
{
    public WriteSurveyDbContext CreateDbContext(string[] args)
    {
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Api");

        if (!Directory.Exists(basePath))
        {
            throw new Exception($"Configuration directory not found: {basePath}");
        }

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("dbConnString");

        var optionsBuilder = new DbContextOptionsBuilder<WriteSurveyDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new WriteSurveyDbContext(optionsBuilder.Options);
    }
}
