namespace Catalog.API.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string URL { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int SortOrder { get; set; }
    }
}
