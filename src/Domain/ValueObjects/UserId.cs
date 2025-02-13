using Microsoft.EntityFrameworkCore;

namespace oig_assessment.Domain.ValueObjects;

[Owned]
public record UserId(Guid Value);
