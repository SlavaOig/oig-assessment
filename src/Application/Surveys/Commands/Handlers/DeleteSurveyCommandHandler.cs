using MediatR;
using oig_assessment.Application.Surveys.Commands;
using oig_assessment.Application.Shared;
using oig_assessment.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Application.Interfaces;

namespace oig_assessment.Application.Surveys.Handlers;

public class DeleteSurveyCommandHandler : IRequestHandler<DeleteSurveyCommand, Result>
{
    private readonly IWriteSurveyDbContext _writeSurveyDbContext;
    private readonly IUserContext _userContext;
    public DeleteSurveyCommandHandler(
        IWriteSurveyDbContext writeSurveyDbContext,
        IUserContext userContext
        )
    {
        _writeSurveyDbContext = writeSurveyDbContext;
        _userContext = userContext;
    }

    public async Task<Result> Handle(DeleteSurveyCommand command, CancellationToken cancellationToken)
    {
        var survey = await _writeSurveyDbContext.Surveys.FirstOrDefaultAsync(s => s.Id == command.Id, cancellationToken);
        
        if (survey == null || survey.CreatedBy.Value != _userContext.UserId)
        {
            return Result.Failure("Survey not found");
        }

        _writeSurveyDbContext.Surveys.Remove(survey);

        return Result.SuccessResult();
    }
}
