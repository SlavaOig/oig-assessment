using Microsoft.EntityFrameworkCore;

namespace oig_assessment.Domain.ValueObjects;

[Owned]
public record SurveyId(Guid Value)
{
    public static SurveyId New() => new SurveyId(Guid.NewGuid());

    public static implicit operator Guid(SurveyId surveyId) => surveyId.Value;
    public static explicit operator SurveyId(Guid value) => new SurveyId(value);
}
