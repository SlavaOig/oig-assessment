using oig_assessment.Application.Shared;
using MediatR;
using oig_assessment.Application.Interfaces;

namespace oig_assessment.Application.Surveys.Commands;

public class DeleteSurveyCommand : IRequest<Result>, IUnitOfWorkRequired
{
    public Guid Id { get; set; }
}
