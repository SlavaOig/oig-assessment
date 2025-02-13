using MediatR;
using oig_assessment.Application.Interfaces;

namespace oig_assessment.Application.Surveys.Commands;
public class UpdateSurveyStatusCommand : IRequest<bool>, IUnitOfWorkRequired
{
    public Guid SurveyId { get; set; }
    public int NewStatusValue { get; set; }
}
