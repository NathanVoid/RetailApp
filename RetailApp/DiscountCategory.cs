namespace RetailApp
{
    public class DiscountCategory
    {
        public int DiscountCategoryID { get; set; } // Primary Key
        public string Category { get; set; } // 'Electronics', 'Clothing', 'Groceries'
        public decimal DiscountPercentage { get; set; } // Discount percentage for the category
        public int QuantityThreshold { get; set; } // Minimum quantity to trigger discount

        // Parameterless constructor for EF Core
        public DiscountCategory() { }

        // Constructor to initialize the discount category with necessary details
        public DiscountCategory(string category, decimal discountPercentage, int quantityThreshold)
        {
            Category = category;
            DiscountPercentage = discountPercentage;
            QuantityThreshold = quantityThreshold;
        }
    }
}
