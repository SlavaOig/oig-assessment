using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace BlazorWeb.Shared.Models;

public class CreateSurveyDto
{
    public string Name { get; set; } = string.Empty;

    public DateTime StartDate { get; set; } = DateTime.Today;

    public TimeOnly StartDateTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

    public DateTime EndDate { get; set; } = DateTime.Today;

    public TimeOnly EndDateTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

    public List<CreateQuestionValueDto> Questions { get; set; } = new();


    public class SurveyValidator: AbstractValidator<CreateSurveyDto>
    {
        public SurveyValidator ()
        {
            RuleFor(s => s.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name cannot be empty.");

            RuleFor(s => s.StartDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Start date cannot be in the past.");

            RuleFor(s => s.EndDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("End date cannot be in the past.");

            RuleFor(s => s)
                .Must(s => s.StartDate != s.EndDate || s.StartDateTime <= s.EndDateTime.AddHours(-1))
                .WithMessage("If Start Date equals End Date, Start Time must be at least 1 hour earlier than End Time.");

            RuleFor(s => s.Questions)
                .NotNull().WithMessage("Survey must have at least one question.")
                .Must(q => q.Count > 0).WithMessage("Survey must have at least one question.");

            RuleForEach(s => s.Questions)
                .ChildRules(q =>
                {
                    q.RuleFor(q => q.QuestionText)
                        .NotNull().WithMessage("Question text is required.")
                        .NotEmpty().WithMessage("Question text cannot be empty.");
                });
        }
    }
}
