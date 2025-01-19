namespace UserService.Models
{
    public class Role : Entity<Guid>
    {
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the normalized name for this role.
        /// </summary>
        public string? NormalizedName { get; set; }

        /// <summary>
        /// A random value that must change whenever a role is persisted to the store
        /// </summary>
        public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
