using oig_assessment.Application.Surveys.Queries;
using MediatR;
using oig_assessment.Application.Surveys.Commands;

namespace oig_assessment.Api.Endpoints;

public static class AnswerEndpoints
{
    public static void MapSurveyAnswersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/answers")
                        .WithTags("Answer")
                        .WithOpenApi();


        group.MapPost("/", async (IMediator mediator, CreateSurveyAnswersCommand command) =>
        {
            var result = await mediator.Send(command);
            return result.Success ? Results.NoContent() : Results.BadRequest(result.Error);
        })
        .WithName("CreateSurveyAnswers");



        group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
        {
            var surveyQuestions = await mediator.Send(new GetSurveyQuestionsByIdQuery(id));
            return Results.Ok(surveyQuestions.Value);
        })
            .WithName("GetSurveyQuestionsById");
    }
}
