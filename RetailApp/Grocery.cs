using System;

namespace RetailApp
{
    public class Grocery : Product
    {
        public DateTime ExpirationDate { get; set; }

        public Grocery(int productID, string productName, decimal price, int stockLevel, DateTime expirationDate)
            : base(productID, productName, price, stockLevel, "Grocery")
        {
            ExpirationDate = expirationDate;
        }

        public override void ShowDetails()
        {
            Console.WriteLine($"Grocery: {ProductName}, Expiration Date: {ExpirationDate.ToShortDateString()}");
        }
    }
}
