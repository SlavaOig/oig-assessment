using Microsoft.EntityFrameworkCore;

namespace oig_assessment.Domain.ValueObjects;

[Owned]
public record AnswerId(Guid Value)
{
    public static AnswerId New() => new AnswerId(Guid.NewGuid());

    public static implicit operator Guid(AnswerId answerId) => answerId.Value;
    public static explicit operator AnswerId(Guid value) => new AnswerId(value);
}
