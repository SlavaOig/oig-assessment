namespace oig_assessment.Domain.Entities.Rules;
public class SurveyDateRule: IBusinessRule
{
    private readonly DateTime _date;

    public SurveyDateRule(DateTime date)
    {
        _date = date;
    }

    public bool IsBroken() => _date < DateTime.Today;

    public string Message => "Date can not be in the past";
}
