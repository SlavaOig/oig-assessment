using oig_assessment.Application.Shared;
using MediatR;
using oig_assessment.Application.DTOs.Question;
using oig_assessment.Application.Interfaces;

namespace oig_assessment.Application.Surveys.Commands;

public class UpdateSurveyCommand : IRequest<Result>, IUnitOfWorkRequired
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<QuestionDto> Questions { get; set; } = null!;
}
