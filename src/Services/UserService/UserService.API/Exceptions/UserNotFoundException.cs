using BuildingBlocks.Exceptions;

namespace UserService.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userName) : base("user", userName)
        {
        }
    }
}
