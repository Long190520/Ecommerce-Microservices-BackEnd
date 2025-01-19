using UserService.Users.SignUp;

namespace UserService.Users.SignUp
{
    public record SignUpRequest(string FirstName, string LastName, string UserName, string Email, string Password);

    public record SignUpResponse(Guid Id);

    public class SignUpEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/signup", async (SignUpRequest request, ISender sender) =>
            {
                var command = request.Adapt<SignUpCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<SignUpResponse>();

                return Results.Created($"/signup/{response.Id}", response);
            })
            .WithName("SignUp")
            .Produces<SignUpResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Sign Up")
            .WithDescription("Sign Up");
        }
    }
}
