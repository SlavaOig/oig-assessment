namespace oig_assessment.Application.Common;
public interface IUnitOfWork
{
    Task CommitAsync();
}
