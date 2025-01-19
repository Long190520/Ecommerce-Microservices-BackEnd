namespace UserService.Users.LogOut
{
    public record LogOutCommand(string UserName)
        : ICommand<LogOutResult>;
    public record LogOutResult(bool IsSuccess);

    public class LogOutCommandValidator : AbstractValidator<LogOutCommand>
    {
        public LogOutCommandValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName is required!");
        }
    }

    internal class LogOutCommandHandler(IDocumentSession session)
        : ICommandHandler<LogOutCommand, LogOutResult>
    {
        public async Task<LogOutResult> Handle(LogOutCommand command, CancellationToken cancellationToken)
        {
            var user = await session.Query<User>()
                .Where(x=>x.UserName.Equals(command.UserName))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserNotFoundException(command.UserName);
            }

            user.RefreshToken = null;
            user.RefreshTokenExp = null;
            session.Update(user);
            await session.SaveChangesAsync();

            return new LogOutResult(true);
        }
    }
}
