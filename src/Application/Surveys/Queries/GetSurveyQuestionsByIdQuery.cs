using oig_assessment.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using oig_assessment.Application.Common.Interfaces;
using oig_assessment.Application.DTOs.Question;
using oig_assessment.Application.DTOs.Survey;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Application.Surveys.Queries
{
    public class GetSurveyQuestionsByIdQuery : IRequest<Result<List<QuestionDto>>>
    {
        public Guid Id { get; }

        public GetSurveyQuestionsByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
