using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Domain.Entities;

public class User
{
    public UserId Id { get; private set; } = new UserId(Guid.NewGuid());
    public required string LoginName { get; set; }
    public required string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.SimpleUser;

    public DateTime CreatedOn { get; set; } = DateTime.Now;
}
