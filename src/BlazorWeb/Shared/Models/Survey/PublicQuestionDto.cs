namespace BlazorWeb.Shared.Models.Survey;

public class PublicQuestionDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string QuestionAnswer { get; set; } = string.Empty;
}
