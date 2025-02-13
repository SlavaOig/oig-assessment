using oig_assessment.Application.Interfaces;

namespace oig_assessment.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/user")
            .WithTags("Users");

        group.MapGet("/GetAll", async (IUserService userService) =>
        {
            var users = await userService.GetAllUsers();

            return users;
        })
            .WithName("GetAllUsers")
            .RequireAuthorization();
    }
}
