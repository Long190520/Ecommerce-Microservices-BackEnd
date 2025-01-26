using System.Security.Claims;
using UserService.Service;

namespace UserService.Users.Refresh
{
    public record RefreshCommand(string AccessToken, string RefreshToken)
        : ICommand<RefreshResult>;
    public record RefreshResult(string AccessToken);

    internal class RefreshHandler(IDocumentSession session, TokenService tokenService, IConfiguration configuration)
        : ICommandHandler<RefreshCommand, RefreshResult>
    {
        public async Task<RefreshResult> Handle(RefreshCommand command, CancellationToken cancellationToken)
        {
            var principal = UserUtils.GetPrincipalFromAccessToken(command.AccessToken, configuration);

            var userName = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if(userName != null)
            {
                var user = await session.Query<User>()
                .FirstOrDefaultAsync(x => x.UserName == userName.Value
                    && x.RefreshToken == command.RefreshToken
                    && x.RefreshTokenExp > DateTime.Now);

                if (user == null)
                {
                    throw new UserNotFoundException(userName.Value);
                }

                var res = tokenService.GetUserToken(user, command.RefreshToken);

                return new RefreshResult(res.AccessToken ?? "");
            }
            else
            {
                throw new BadRequestException("Invalid Token!");
            }
        }
    }
}
