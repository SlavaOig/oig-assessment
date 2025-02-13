
using FluentValidation;

namespace BlazorWeb.Shared.Models.Survey;

public class PublicSurveyQuestionsDto
{
    public Guid Id { get; set; }
    public List<PublicQuestionDto> Questions { get; set; } = new();

    public class SurveyAnswersValidator : AbstractValidator<PublicSurveyQuestionsDto>
    {
        public SurveyAnswersValidator()
        {
            RuleForEach(x => x.Questions).ChildRules(answers =>
            {
                answers.RuleFor(q => q.QuestionAnswer)
                    .NotEmpty().WithMessage("Answer is required.")
                    .MinimumLength(1).WithMessage("Answer must be at least 1 characters long.");
            });
        }
    }
}
