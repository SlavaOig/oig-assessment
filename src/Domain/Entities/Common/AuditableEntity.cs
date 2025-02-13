using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Domain.Entities.Common;
public abstract class AuditableEntity : Entity
{
    public UserId CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; private set; } = DateTime.Now;

    public UserId? UpdatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; private set; } = DateTime.Now;
}
