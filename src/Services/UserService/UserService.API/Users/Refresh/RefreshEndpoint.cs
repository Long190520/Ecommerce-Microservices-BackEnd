
using Microsoft.AspNetCore.Authorization;
using UserService.Users.LogOut;

namespace UserService.Users.Refresh
{
    public record RefreshRequest(string AccessToken, string RefreshToken);
    public record RefreshResponse(string AccessToken);

    public class RefreshEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/refresh", [Authorize] async (RefreshRequest request, ISender sender) =>
            {
                var command = request.Adapt<RefreshCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<RefreshResponse>();

                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName("Refresh")
            .Produces<RefreshResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Refresh")
            .WithDescription("Refresh");
        }
    }
}
