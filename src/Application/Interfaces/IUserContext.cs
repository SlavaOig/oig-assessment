namespace oig_assessment.Application.Interfaces;
public interface IUserContext
{
    Guid UserId { get; }
    string? Role { get; }
}
