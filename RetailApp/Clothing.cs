using System;

namespace RetailApp
{
    public class Clothing : Product
    {
        public string Size { get; set; }
        public string Material { get; set; }

        public Clothing(int productID, string productName, decimal price, int stockLevel, string size, string material)
            : base(productID, productName, price, stockLevel, "Clothing")
        {
            Size = size;
            Material = material;
        }

        public override void ShowDetails()
        {
            Console.WriteLine($"Clothing: {ProductName}, Size: {Size}, Material: {Material}");
        }
    }
}
