using UserService.Users.LogIn;

namespace UserService.Users.LogOut
{
    public record LogOutRequest(string UserName);
    public record LogOutResponse(bool IsSuccess);

    public class LogOutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/logout", async (LogOutRequest request, ISender sender) => { 
                var command = request.Adapt<LogOutCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<LogOutResponse>();

                return Results.Ok(response);
            })
            .WithName("LogOut")
            .Produces<LogOutResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Log Out")
            .WithDescription("Log Out");
        }
    }
}
