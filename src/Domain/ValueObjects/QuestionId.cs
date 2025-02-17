namespace oig_assessment.Domain.ValueObjects;

public record QuestionId(Guid Value)
{
    public static QuestionId New() => new QuestionId(Guid.NewGuid());

    public static implicit operator Guid(QuestionId questionId) => questionId.Value;
    public static explicit operator QuestionId(Guid value) => new QuestionId(value);
}
