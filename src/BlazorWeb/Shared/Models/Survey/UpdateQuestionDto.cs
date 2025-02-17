namespace BlazorWeb.Shared.Models.Survey;

public class UpdateQuestionDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
}
