namespace UserService.Abstractions
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }

    }

    public interface IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
