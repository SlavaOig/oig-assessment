using Microsoft.EntityFrameworkCore;
using oig_assessment.Domain.Entities;

namespace oig_assessment.Application.Common.Interfaces;
public interface IWriteSurveyDbContext
{
    DbSet<User> Users { get; }
    DbSet<Survey> Surveys { get; }
    DbSet<Answer> Answers { get; }
}
