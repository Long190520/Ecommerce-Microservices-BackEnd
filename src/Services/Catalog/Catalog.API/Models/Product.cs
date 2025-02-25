﻿namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid CategoryId { get; set; }
        //public Guid ShopId { get; set; }
    }
}
