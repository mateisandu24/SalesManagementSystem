using System;

namespace SalesManagementSystem.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public decimal Vat { get; set; }

        public MainCategory MainCategory { get; set; }

        public SubCategory SubCategory { get; set; }

        public Brand Brand { get; set; }
    }
}
