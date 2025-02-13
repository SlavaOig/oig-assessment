namespace oig_assessment.Domain.Entities;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}
