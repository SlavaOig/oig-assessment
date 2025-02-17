
using FluentValidation;

namespace BlazorWeb.Shared.Models.Survey;

public class CreateSurveyAnswerDto
{
    public Guid SurveyId { get; set; }
    public List<CreateAnswerQuestion> QuestionAnswers { get; set; }
}
