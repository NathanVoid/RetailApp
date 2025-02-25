public class Order
{
    public int OrderID { get; set; }
    public int CustomerID { get; set; } // Foreign key to Customer
    public Customer Customer { get; set; } // Navigation property to Customer
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string Status { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    // Parameterless constructor (required by EF Core)
    public Order()
    {
        OrderDate = DateTime.Now;
        Status = "Pending"; // Default status
    }

    // Constructor for initializing with multiple products
    public Order(int customerID, List<OrderDetail> orderDetails)
    {
        CustomerID = customerID;
        OrderDate = DateTime.Now;
        Status = "Pending";
        OrderDetails = orderDetails;
    }

    // Method to add a single order detail (e.g., when creating an order with a single product)
    public void AddOrderDetail(int productID, int quantity, decimal price)
    {
        OrderDetails.Add(new OrderDetail(OrderID, productID, quantity, price));
    }

    // Method to calculate total order cost
    public decimal CalculateTotal()
    {
        decimal total = 0;
        foreach (var detail in OrderDetails)
        {
            total += detail.Price;
        }
        return total;
    }

    // Optional: Method to update order status
    public void UpdateStatus(string newStatus)
    {
        Status = newStatus;
    }
}
