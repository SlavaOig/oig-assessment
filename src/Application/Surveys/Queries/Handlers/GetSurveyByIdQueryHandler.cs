using MediatR;
using oig_assessment.Application.DTOs.Survey;
using oig_assessment.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Domain.ValueObjects;
using oig_assessment.Application.Shared;
using oig_assessment.Application.DTOs.Question;
using oig_assessment.Application.Interfaces;

namespace oig_assessment.Application.Surveys.Queries;
public class GetSurveyByIdQueryHandler : IRequestHandler<GetSurveyByIdQuery, Result<SurveyDetailsDto>?>
{
    private readonly IReadSurveyDbContext _readSurveyDbContext;
    private readonly IUserContext _userContext;
    public GetSurveyByIdQueryHandler(
        IReadSurveyDbContext readSurveyDbContext,
        IUserContext userContext
        )
    {
        _readSurveyDbContext = readSurveyDbContext;
        _userContext = userContext;
    }

    public async Task<Result<SurveyDetailsDto>?> Handle(GetSurveyByIdQuery request, CancellationToken cancellationToken)
    {
        var survey = await _readSurveyDbContext.Surveys
            .Where(s => s.Id == request.Id)
            .Select(s => new SurveyDetailsDto
            {
                Id = s.Id,
                CreatedBy = s.CreatedBy,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                EndDateTime = TimeOnly.FromDateTime(s.EndDate),
                StartDateTime = TimeOnly.FromDateTime(s.StartDate),
                Name = s.Name,
                Status = SurveyStatus.FromValue(s.Status),
                UpdatedBy = s.UpdatedBy.HasValue ? s.UpdatedBy.Value : null,
                Questions = s.Questions.Select(q => new QuestionDto
                {
                    CreatedBy = new UserId(q.CreatedBy),
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    SurveyId = new SurveyId(q.SurveyId),
                    UpdatedBy = q.UpdatedBy.HasValue ? new UserId(q.UpdatedBy.Value) : null
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (survey == null)
            return Result<SurveyDetailsDto>.Failure("Survey not found.");

        if (survey.CreatedBy != _userContext.UserId)
            return Result<SurveyDetailsDto>.Failure("Logged-in user does not have access to this survey");

        return Result<SurveyDetailsDto>.SuccessResult(survey);
    }
}
