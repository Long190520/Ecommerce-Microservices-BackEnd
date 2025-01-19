using UserService.Service;

namespace UserService.Users.LogIn
{
    public record LogInCommand(string UserName, string Password)
        : ICommand<LogInResult>;
    public record LogInResult(TokenDto TokenDto);

    public class LogInCommandValidator : AbstractValidator<LogInCommand>
    {
        public LogInCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required!");
        }
    }

    internal class LogInCommandHandler(IDocumentSession session, TokenService tokenService)
        : ICommandHandler<LogInCommand, LogInResult>
    {
        public async Task<LogInResult> Handle(LogInCommand command, CancellationToken cancellationToken)
        {
            var matchedUser = await session.Query<User>()
                .Where(u => u.UserName == command.UserName)
                .FirstOrDefaultAsync();

            if (matchedUser is null)
            {
                throw new UserNotFoundException(command.UserName);
            }

            var response = new TokenDto();

            if (matchedUser != null)
            {
                var hashedPassword = UserUtils.GetHash(UserUtils.sHA256, command.Password + matchedUser.PasswordSalt);
                var matchPassword = matchedUser.PasswordHash == hashedPassword;

                if (!matchPassword)
                {
                    matchedUser.AccessFailedCount += 1;
                    throw new UserNotFoundException(command.UserName);
                }
                else
                {
                    matchedUser.AccessFailedCount = 0;
                    response = tokenService.GetUserToken(matchedUser);
                    matchedUser.RefreshToken = response.RefreshToken;
                    matchedUser.RefreshTokenExp = response.RefreshTokenExp;
                    session.Update(matchedUser);
                    await session.SaveChangesAsync();
                }
            }

            return new LogInResult(response);
        }


    }
}
