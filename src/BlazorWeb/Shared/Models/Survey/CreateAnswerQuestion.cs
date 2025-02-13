namespace BlazorWeb.Shared.Models.Survey;

public class CreateAnswerQuestion
{
    public Guid QuestionId { get; set; }
    public Guid SurveyId { get; set; }
    public string QuestionAnswer { get; set; } = string.Empty;
}
