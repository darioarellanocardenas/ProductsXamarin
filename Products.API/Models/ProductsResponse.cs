using System;

namespace Products.API.Models
{
    public class ProductsResponse
    {
        public int ProductId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastPurchase { get; set; }

        public double Stock { get; set; }
        
        public string Image { get; set; }

        public string Remarks { get; set; }
    }
}