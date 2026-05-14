using System.Collections.Generic;

namespace SalesManagementSystem.Models
{
    public static class ShoppingCart
    {
        public static List<Product> Products { get; set; } = new List<Product>();

        public static void Add(Product product, int quantity = 1)
        {
            for (int i = 0; i < quantity; i++)
            {
                Products.Add(product);
            }
        }

        public static void Clear()
        {
            Products.Clear();
        }

        public static int GetQuantity(System.Guid productId)
        {
            int count = 0;
            foreach (var p in Products)
            {
                if (p.Id == productId) count++;
            }
            return count;
        }

        public static void SetQuantity(Product product, int quantity)
        {
            Products.RemoveAll(p => p.Id == product.Id);
            for (int i = 0; i < quantity; i++)
            {
                Products.Add(product);
            }
        }

        public static void Remove(System.Guid productId)
        {
            Products.RemoveAll(p => p.Id == productId);
        }
    }
}
