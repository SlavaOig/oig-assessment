using MediatR;
using oig_assessment.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Domain.ValueObjects;
using oig_assessment.Application.Shared;
using oig_assessment.Application.DTOs.Question;

namespace oig_assessment.Application.Surveys.Queries
{
    public class GetSurveyQuestionsByIdQueryHandler : IRequestHandler<GetSurveyQuestionsByIdQuery, Result<List<QuestionDto>>>
    {
        private readonly IReadSurveyDbContext _readSurveyDbContext;
        public GetSurveyQuestionsByIdQueryHandler(
            IReadSurveyDbContext readSurveyDbContext
            )
        {
            _readSurveyDbContext = readSurveyDbContext;
        }

        public async Task<Result<List<QuestionDto>>> Handle(GetSurveyQuestionsByIdQuery request, CancellationToken cancellationToken)
        {
            var survey = await _readSurveyDbContext.Surveys
                    .AsNoTracking()
                    .Include(s => s.Questions.OrderBy(q => q.CreatedOn))
                    .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (survey == null)
            {
                throw new KeyNotFoundException("Survey not found.");
            }

            var listQuestionToReturn = survey.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                SurveyId = new SurveyId(q.SurveyId),
                QuestionText = q.QuestionText,
                CreatedBy = new UserId(q.CreatedBy),
                CreatedOn = q.CreatedOn
            }).ToList();

            return Result<List<QuestionDto>>.SuccessResult(listQuestionToReturn);
        }
    }
}
