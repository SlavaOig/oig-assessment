using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using oig_assessment.Domain.ValueObjects;

public class SurveyStatusConverter : ValueConverter<SurveyStatus, int>
{
    public SurveyStatusConverter()
        : base(
            status => status.Value,  
            value => SurveyStatus.FromValue(value)
        )
    { }
}
