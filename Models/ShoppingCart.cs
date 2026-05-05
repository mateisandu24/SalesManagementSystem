using System.Collections.Generic;

namespace SalesManagementSystem.Models
{
    public static class ShoppingCart
    {
        public static List<Product> Products { get; set; } = new List<Product>();

        public static void Add(Product product)
        {
            Products.Add(product);
        }

        public static void Clear()
        {
            Products.Clear();
        }
    }
}
