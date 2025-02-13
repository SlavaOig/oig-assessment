namespace oig_assessment.Domain.Entities.Rules;
public class SurveyTimeRule: IBusinessRule
{
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;

    public SurveyTimeRule(TimeOnly startTime, TimeOnly endTime)
    {
        _startTime = startTime;
        _endTime = endTime;
    }

    public bool IsBroken() => _endTime < _startTime.AddHours(1);

    public string Message => "End time should be at least 1 hour later than start time of the survey.";
}
