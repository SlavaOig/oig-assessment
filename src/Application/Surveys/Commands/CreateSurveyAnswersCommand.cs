using oig_assessment.Application.Shared;
using MediatR;
using oig_assessment.Application.Interfaces;
using oig_assessment.Application.DTOs.Question;

namespace oig_assessment.Application.Surveys.Commands;

public class CreateSurveyAnswersCommand : IRequest<Result>, IUnitOfWorkRequired
{
    public List<AnswerQuestionDto> Answers { get; set; } = null!;
}
