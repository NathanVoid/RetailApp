using System;
using System.ComponentModel.DataAnnotations;

namespace RetailApp
{
    public abstract class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockLevel { get; set; }

        [Required]
        public string Category { get; set; }

        protected Product(int productID, string productName, decimal price, int stockLevel, string category)
        {
            ProductID = productID;
            ProductName = productName;
            Price = price;
            StockLevel = stockLevel;
            Category = category;
        }

        public abstract void ShowDetails();
    }
}
