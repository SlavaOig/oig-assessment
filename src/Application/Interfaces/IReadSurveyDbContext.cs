using Microsoft.EntityFrameworkCore;
using oig_assessment.Application.Models.Read;

namespace oig_assessment.Application.Common.Interfaces;
public interface IReadSurveyDbContext
{
    DbSet<UserReadModel> Users { get; }
    DbSet<SurveyReadModel> Surveys { get; }
}
