using oig_assessment.Application.Common;
using oig_assessment.Infrastructure.Data;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly WriteSurveyDbContext _writeDbcontext;

    public UnitOfWork(WriteSurveyDbContext writeDbcontext)
    {
        _writeDbcontext = writeDbcontext;
    }

    public async Task CommitAsync()
    {
        await _writeDbcontext.SaveChangesAsync();
    }
}
