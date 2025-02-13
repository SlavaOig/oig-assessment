using oig_assessment.Application.DTOs;
using oig_assessment.Application.Interfaces;

namespace oig_assessment.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth")
            .WithTags("Authorization");

        group.MapPost("register", async (IUserService userService, RegisterRequest request) =>
        {
            var result = await userService.Register(request);

            if (!result.Success)
            {
                return Results.BadRequest(new { Error = result.Error });
            }

            return Results.Created($"/api/users/{request.LoginName}", new { Token = result.Value });
        })
        .WithName("Register");


        group.MapPost("login", async (IUserService userService, LoginRequest request) =>
        {
            var result = await userService.Login(request);

            if (!result.Success)
            {
                return Results.BadRequest(new {Error = result.Error});
            }

            return Results.Ok(new { Token = result.Value });
        })
        .WithName("Login");

    }
}
