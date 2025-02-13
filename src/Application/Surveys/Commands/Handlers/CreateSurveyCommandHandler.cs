using MediatR;
using oig_assessment.Domain.Entities;
using oig_assessment.Application.Surveys.Commands;
using oig_assessment.Application.Shared;
using oig_assessment.Application.Common.Interfaces;
using oig_assessment.Application.Interfaces;

namespace oig_assessment.Application.Surveys.Handlers;

public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, Result<Guid>>
{
    private readonly IWriteSurveyDbContext _writeSurveyDbContext;
    private readonly IUserContext _userContext;
    public CreateSurveyCommandHandler(
        IWriteSurveyDbContext writeSurveyDbContext,
        IUserContext userContext
        )
    {
        _writeSurveyDbContext = writeSurveyDbContext;
        _userContext = userContext;
    }

    public async Task<Result<Guid>> Handle(CreateSurveyCommand command, CancellationToken cancellationToken)
    {
        var survey = Survey.Create(
            command.Name,
            command.StartDate,
            command.EndDate,
            _userContext.UserId);

        foreach (var questionToCreate in command.Questions) {
            survey.CreateRangeQuestions(questionToCreate.QuestionText, survey.Id, Guid.NewGuid());
        }

        survey.CheckDateRule(survey.StartDate);
        survey.CheckDateRule(survey.EndDate);
        survey.CheckEndDateRule(
            TimeOnly.FromDateTime(survey.StartDate),
            TimeOnly.FromDateTime(survey.EndDate));

        await _writeSurveyDbContext.Surveys.AddAsync(survey);

        return Result<Guid>.SuccessResult(survey.Id.Value);
    }
}
