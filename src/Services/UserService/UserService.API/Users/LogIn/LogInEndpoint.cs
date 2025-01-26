namespace UserService.Users.LogIn
{
    public record LogInRequest(string UserName, string Password);

    public record LogInResponse(TokenDto TokenDto);

    public class LogInEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LogInRequest request, ISender sender) =>
            {
                var command = request.Adapt<LogInCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<LogInResponse>();

                return Results.Created("/login", response);
            })
            .WithName("LogIn")
            .Produces<LogInResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Log In")
            .WithDescription("Log In");
        }
    }
}
