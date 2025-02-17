using MediatR;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Application.Common.Interfaces;
using oig_assessment.Application.Surveys.Commands;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Application.Surveys.Handlers;
public class UpdateSurveyStatusCommandHandler : IRequestHandler<UpdateSurveyStatusCommand, bool> 
{
    private readonly IWriteSurveyDbContext _writeSurveyDbContext;

    public UpdateSurveyStatusCommandHandler(IWriteSurveyDbContext writeSurveyDbContext)
    {
        _writeSurveyDbContext = writeSurveyDbContext;
    }

    public async Task<bool> Handle(UpdateSurveyStatusCommand command, CancellationToken cancellationToken)
    {
        var survey = await _writeSurveyDbContext.Surveys.FirstOrDefaultAsync(s => s.Id == command.SurveyId, cancellationToken);
        if (survey is null)
        {
            return false;
        }

        var newStatus = SurveyStatus.FromValue(command.NewStatusValue);
        var allowedStatuses = SurveyStatus.GetAllowedStatuses(survey.Status.Value);

        if (!allowedStatuses.Contains(newStatus))
        {
            return false;
        }

        survey.ChangeStatus(newStatus);

        return true;
    }

}
