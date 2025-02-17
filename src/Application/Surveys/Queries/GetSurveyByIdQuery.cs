using oig_assessment.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using oig_assessment.Application.Common.Interfaces;
using oig_assessment.Application.DTOs.Survey;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Application.Surveys.Queries
{
    public class GetSurveyByIdQuery : IRequest<Result<SurveyDetailsDto>?>
    {
        public Guid Id { get; }

        public GetSurveyByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
