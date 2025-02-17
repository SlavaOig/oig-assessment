using MediatR;
using oig_assessment.Application.DTOs.Survey;
using oig_assessment.Application.Shared;
using oig_assessment.Domain.Entities;

namespace oig_assessment.Application.Surveys.Queries;

public class GetSurveyListQuery : IRequest<PagedResult<SurveyDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; } = SortDirection.None;
}
