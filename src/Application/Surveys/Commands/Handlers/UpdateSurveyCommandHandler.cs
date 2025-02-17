using MediatR;
using oig_assessment.Application.Surveys.Commands;
using oig_assessment.Application.Interfaces;
using oig_assessment.Application.Shared;
using oig_assessment.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace oig_assessment.Application.Surveys.Handlers;

public class UpdateSurveyCommandHandler : IRequestHandler<UpdateSurveyCommand, Result>
{
    private readonly IWriteSurveyDbContext _writeSurveyDbContext;
    private readonly IUserContext _userContext;
    public UpdateSurveyCommandHandler(
        IWriteSurveyDbContext writeSurveyDbContext,
        IUserContext userContext)
    {
        _writeSurveyDbContext = writeSurveyDbContext;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UpdateSurveyCommand command, CancellationToken cancellationToken)
    {
        var survey = await _writeSurveyDbContext.Surveys
            .FirstOrDefaultAsync(s => s.Id == command.Id, cancellationToken);

        if (survey == null || survey.CreatedBy.Value != _userContext.UserId)
        {
            return Result.Failure("Survey not found");
        }

        var questionData = command.Questions.Select(q => (q.Id, q.QuestionText)).ToList();

        survey.Update(command.Name, command.StartDate, command.EndDate, _userContext.UserId);

        survey.UpdateQuestions(questionData, _userContext.UserId);

        return Result.SuccessResult();
    }
}
