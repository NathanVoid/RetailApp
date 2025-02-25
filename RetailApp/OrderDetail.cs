using RetailApp;

public class OrderDetail
{
    public int OrderDetailID { get; set; } // Primary key
    public int OrderID { get; set; } // Foreign Key to Order
    public int ProductID { get; set; } // Foreign Key to Product
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // Parameterless constructor for EF Core
    public OrderDetail()
    {
    }

    // Constructor for initializing the order details
    public OrderDetail(int orderID, int productID, int quantity, decimal price)
    {
        OrderID = orderID;
        ProductID = productID;
        Quantity = quantity;
        Price = price;
    }

    // Navigation properties
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}
