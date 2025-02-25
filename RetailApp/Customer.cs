public class Customer
{
    public int CustomerID { get; set; } // Primary Key
    public string FirstName { get; set; } // More flexible than a single CustomerName
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string TelNo { get; set; }

    // Parameterless constructor for EF Core
    public Customer()
    {
    }

    // Constructor to initialize the customer with necessary details
    public Customer(int customerID, string firstName, string lastName, string email, string address, string telNo)
    {
        CustomerID = customerID;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        TelNo = telNo;
    }

    // Optionally, you could add validation methods if needed
    public bool IsValidEmail()
    {
        return Email.Contains("@"); // A simple example validation
    }
}
