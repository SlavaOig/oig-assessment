using oig_assessment.Application.Shared;
using MediatR;
using oig_assessment.Application.Interfaces;
using oig_assessment.Application.DTOs.Question;

namespace oig_assessment.Application.Surveys.Commands;

public class CreateSurveyCommand : IRequest<Result<Guid>>, IUnitOfWorkRequired
{
    public string Name { get; set; } = string.Empty;
    public List<CreateQuestionDto> Questions { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
