namespace UserService.Users.SignUp
{
    public record SignUpCommand(string FirstName, string LastName, string UserName, string Email, string Password)
        : ICommand<SignUpResult>;
    public record SignUpResult(Guid Id);

    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required!");
        }
    }

    internal class SignUpCommandHandler(IDocumentSession session)
        : ICommandHandler<SignUpCommand, SignUpResult>
    {
        public async Task<SignUpResult> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var user = await CreateNewUser(command, cancellationToken);

            return new SignUpResult(user.Id);
        }

        private async Task<User> CreateNewUser(SignUpCommand command, CancellationToken cancellationToken)
        {
            var duplicatedUserName = await session.Query<User>().FirstOrDefaultAsync(x => x.UserName == command.UserName);
            var duplicatedEmail = await session.Query<User>().FirstOrDefaultAsync(x => x.Email == command.Email);

            if(duplicatedUserName != null)
            {
                throw new BadRequestException("Duplicated UserName");
            }
            if(duplicatedEmail != null)
            {
                throw new BadRequestException("Duplicated Email");
            }

            var user = new User();

            user.PasswordSalt = UserUtils.GenerateRandomToken();
            var randomPassword = command.Password.IsNullOrEmpty() ? UserUtils.GenerateRandomToken(10) : command.Password;
            user.PasswordHash = UserUtils.GetHash(UserUtils.sHA256, randomPassword + user.PasswordSalt);
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Email = command.Email;
            user.NormalizedEmail = command.Email.ToUpper();
            user.UserName = command.UserName;
            user.NormalizedUserName = command.UserName.ToUpper();

            session.Store(user);
            await session.SaveChangesAsync(cancellationToken);

            return user;
        }

        
    }
}
