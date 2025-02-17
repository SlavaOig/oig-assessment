
using FluentValidation;

namespace BlazorWeb.Shared.Models.Survey;

public class UpdateSurveyDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public TimeOnly StartDateTime { get; set; }
    public TimeOnly EndDateTime { get; set; }
    public SurveyStatus Status { get; set; } = new();
    public List<UpdateQuestionDto> Questions { get; set; }

    public class UpdateSurveyDetailsDtoValidator : AbstractValidator<UpdateSurveyDetailsDto>
    {
        public UpdateSurveyDetailsDtoValidator()
        {
            RuleFor(s => s.Name).NotNull().NotEmpty()
            .WithMessage("Name is required.");

            RuleFor(s => s.Name).MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long.");

            RuleFor(s => s.StartDate).GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Start date cannot be in the past.");

            RuleFor(s => s)
                .Must(s => (s.EndDate + s.EndDateTime.ToTimeSpan()) >= (s.StartDate + s.StartDateTime.ToTimeSpan()).AddHours(1))
                .WithMessage("End time must be at least 1 hour after the start date.");

            RuleFor(s => s.Questions)
                .NotEmpty().WithMessage("Survey must contain at least one question.")
                .Must(q => q.All(question => !string.IsNullOrWhiteSpace(question.QuestionText) && question.QuestionText.Length >= 5))
                .WithMessage("Each question must have at least 5 characters.");
        }
    }
}
