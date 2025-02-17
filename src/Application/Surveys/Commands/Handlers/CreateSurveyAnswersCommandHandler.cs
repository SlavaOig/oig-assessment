using MediatR;
using oig_assessment.Domain.Entities;
using oig_assessment.Application.Surveys.Commands;
using oig_assessment.Application.Shared;
using oig_assessment.Application.Common.Interfaces;

namespace oig_assessment.Application.Surveys.Handlers;

public class CreateSurveyAnswersCommandHandler : IRequestHandler<CreateSurveyAnswersCommand, Result>
{
    private readonly IWriteSurveyDbContext _writeSurveyDbContext;
    public CreateSurveyAnswersCommandHandler(
        IWriteSurveyDbContext writeSurveyDbContext
        )
    {
        _writeSurveyDbContext = writeSurveyDbContext;
    }

    public async Task<Result> Handle(CreateSurveyAnswersCommand request, CancellationToken cancellationToken)
    {
        if (request.Answers == null || !request.Answers.Any())
        {
            return Result.Failure("No answers provided.");
        }

        var answeredBy = Guid.NewGuid();

        var answers = request.Answers.Select(a => Answer.Create(
            a.QuestionAnswer,
            answeredBy,
            a.SurveyId,
            a.QuestionId
            )).ToList();

        await _writeSurveyDbContext.Answers.AddRangeAsync(answers);

        return Result.SuccessResult();
    }
}
