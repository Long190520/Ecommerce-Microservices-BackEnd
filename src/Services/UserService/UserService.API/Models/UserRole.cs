namespace UserService.Models
{
    public class UserRole : Entity<Guid>
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
