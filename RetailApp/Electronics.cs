using System;

namespace RetailApp
{
    public class Electronics : Product
    {
        public int WarrantyPeriod { get; set; }

        public Electronics(int productID, string productName, decimal price, int stockLevel, int warrantyPeriod)
            : base(productID, productName, price, stockLevel, "Electronics")
        {
            WarrantyPeriod = warrantyPeriod;
        }

        public override void ShowDetails()
        {
            Console.WriteLine($"Electronics: {ProductName}, Price: {Price}, Warranty: {WarrantyPeriod} months");
        }
    }
}
