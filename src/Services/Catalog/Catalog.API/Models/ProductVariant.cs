namespace Catalog.API.Models
{
    public class ProductVariant
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageFile { get; set; } = default!;
    }
}
