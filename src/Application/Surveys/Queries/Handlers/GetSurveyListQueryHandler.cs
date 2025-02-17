using MediatR;
using oig_assessment.Application.DTOs.Survey;
using oig_assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Application.Shared;
using oig_assessment.Application.Common.Interfaces;
using oig_assessment.Domain.ValueObjects;
using oig_assessment.Application.Interfaces;

namespace oig_assessment.Application.Surveys.Queries;

public class GetSurveyListQueryHandler : IRequestHandler<GetSurveyListQuery, PagedResult<SurveyDto>>
{
    private readonly IReadSurveyDbContext _readSurveyDbContext;
    private readonly IUserContext _userContext;
    public GetSurveyListQueryHandler(
        IReadSurveyDbContext readSurveyDbContext,
        IUserContext userContext
        )
    {
        _readSurveyDbContext = readSurveyDbContext;
        _userContext = userContext;
    }

    public async Task<PagedResult<SurveyDto>> Handle(GetSurveyListQuery request, CancellationToken cancellationToken)
    {
        var query = _readSurveyDbContext.Surveys.AsNoTracking();

        if (_userContext?.Role == "SimpleUser")
        {
            query = query.Where(s => s.CreatedBy == _userContext.UserId);
        }

        int totalCount = await query.CountAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.SortBy) && request.SortDirection != SortDirection.None)
        {
            query = request.SortDirection switch
            {
                SortDirection.Ascending => query.OrderBy(s => EF.Property<object>(s, request.SortBy)),
                SortDirection.Descending => query.OrderByDescending(s => EF.Property<object>(s, request.SortBy)),
                _ => query
            };
        }
        else
        {
            query = query.OrderBy(s => s.Id);
        }

        var surveys = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var surveyDtos = surveys
            .Select(s => new SurveyDto
            {
                CreatedBy = s.CreatedBy,
                EndDate = s.EndDate,
                Id = s.Id,
                Name = s.Name,
                StartDate = s.StartDate,
                Status = SurveyStatus.FromValue(s.Status),
                UpdatedBy = s.UpdatedBy
            }).ToList();

        return new PagedResult<SurveyDto>(surveyDtos, totalCount, request.Page, request.PageSize);
    }
}
