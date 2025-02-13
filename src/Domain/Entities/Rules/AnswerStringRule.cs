namespace oig_assessment.Domain.Entities.Rules;
public class AnswerStringRule : IBusinessRule
{
    private readonly string _answer;

    public AnswerStringRule(string answer)
    {
        _answer = answer;
    }

    public bool IsBroken() => string.IsNullOrEmpty(_answer);

    public string Message => "Answer can't be empty";
}
