using oig_assessment.Application.Surveys.Queries;
using MediatR;
using oig_assessment.Application.Surveys.Commands;

namespace oig_assessment.Api.Endpoints;

public static class SurveyEndpoints
{
    public static void MapSurveyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/surveys")
                        .WithTags("Surveys")
                        .WithOpenApi();

        
        group.MapPost("/", async (IMediator mediator, CreateSurveyCommand command) =>
        {
            var surveyId = await mediator.Send(command);
            return Results.Ok(surveyId.Value);
        })
            .WithName("CreateSurvey")
            .RequireAuthorization();


        group.MapPut("/{id:guid}", async (IMediator mediator, Guid id, UpdateSurveyCommand command) =>
        {
            if (id != command.Id)
            {
                return Results.BadRequest("Mismatched ID");
            }

            var result = await mediator.Send(command);
            return result.Success ? Results.NoContent() : Results.NotFound();
        })
             .WithName("UpdateSurvey")
             .RequireAuthorization();

        group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new DeleteSurveyCommand { Id = id });
            return result.Success ? Results.NoContent() : Results.NotFound();
        })
            .WithName("DeleteSurvey")
            .RequireAuthorization();


        group.MapGet("/", async (IMediator mediator, [AsParameters] GetSurveyListQuery query) =>
        {
            var surveys = await mediator.Send(query);
            return Results.Ok(surveys);
        })
            .WithName("GetSurveyList")
            .RequireAuthorization();


        group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
        {
            var survey = await mediator.Send(new GetSurveyByIdQuery(id));
            return survey is not null ? Results.Ok(survey.Value) : Results.NotFound();
        })
            .WithName("GetSurveyById")
            .RequireAuthorization();


        group.MapPut("/{id:guid}/statusChange", async (IMediator mediator, Guid id, UpdateSurveyStatusCommand command) =>
        {
            if (id != command.SurveyId)
            {
                return Results.BadRequest("Mismatched ID");
            }

            var result = await mediator.Send(command);
            return result ? Results.NoContent() : Results.BadRequest("Invalid status transition.");
        })
        .WithName("UpdateSurveyStatusFromConceptToAvailable")
        .RequireAuthorization();
    }
}
