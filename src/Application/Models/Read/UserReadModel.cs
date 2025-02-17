namespace oig_assessment.Application.Models.Read;
public sealed class UserReadModel
{
    public Guid Id { get; set; }
    public string LoginName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Role { get; set; }
}
