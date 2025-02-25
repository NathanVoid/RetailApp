using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RetailApp
{
    public static class Database
    {
        // Method to save a new product to the database (used when adding a new product)
        public static void SaveProductToDatabase(Product product)
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    dbContext.Products.Add(product);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving product: " + ex.Message);
            }
        }

        // Method to retrieve all products from the database
        public static List<Product> GetAllProducts()
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    return dbContext.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving products: " + ex.Message);
                return new List<Product>();
            }
        }

        // Method to update a product in the database
        public static void UpdateProduct(Product product)
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    var existingProduct = dbContext.Products.Find(product.ProductID);
                    if (existingProduct != null)
                    {
                        // Update properties if they have new values
                        if (product.Price > 0)
                            existingProduct.Price = product.Price;

                        if (product.StockLevel >= 0)
                            existingProduct.StockLevel = product.StockLevel;

                        dbContext.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Product not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating product: " + ex.Message);
            }
        }

        // Method to save a new customer to the database
        public static void SaveCustomerToDatabase(Customer customer)
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    dbContext.Customers.Add(customer);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving customer: " + ex.Message);
            }
        }

        // Method to retrieve all customers from the database
        public static List<Customer> GetAllCustomers()
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    return dbContext.Customers.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving customers: " + ex.Message);
                return new List<Customer>();
            }
        }

        // Method to retrieve all discount categories from the DiscountCategories table
        public static List<DiscountCategory> GetDiscountCategories()
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    return dbContext.DiscountCategories.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving discount categories: " + ex.Message);
                return new List<DiscountCategory>();
            }
        }

        // Method to update discount categories in the database
        public static void UpdateDiscountCategories(List<DiscountCategory> discountCategories)
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    foreach (var discountCategory in discountCategories)
                    {
                        var existingCategory = dbContext.DiscountCategories.Find(discountCategory.DiscountCategoryID);
                        if (existingCategory != null)
                        {
                            // Update properties if they have new values
                            if (discountCategory.DiscountPercentage >= 0)
                                existingCategory.DiscountPercentage = discountCategory.DiscountPercentage;

                            if (discountCategory.QuantityThreshold >= 0)
                                existingCategory.QuantityThreshold = discountCategory.QuantityThreshold;
                        }
                        else
                        {
                            Console.WriteLine($"Discount category with ID {discountCategory.DiscountCategoryID} not found.");
                        }
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating discount categories: " + ex.Message);
            }
        }

        // Method to save a new order to the database
        public static void SaveOrderToDatabase(Order order)
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    dbContext.Orders.Add(order);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving order: " + ex.Message);
            }
        }

        // Method to retrieve all pending orders from the database
        public static List<Order> GetPendingOrders()
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    return dbContext.Orders
                        .Where(o => o.Status == "Pending")
                        .Include(o => o.Customer) // Ensure Customer is included
                        .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving pending orders: " + ex.Message);
                return new List<Order>();
            }
        }

        // Method to update the status of an order in the database
        public static void UpdateOrderStatus(Order order)
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    var existingOrder = dbContext.Orders.Find(order.OrderID);
                    if (existingOrder != null)
                    {
                        existingOrder.Status = order.Status;
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Order not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating order status: " + ex.Message);
            }
        }

        // Method to retrieve all orders from the database
        public static List<Order> GetAllOrders()
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    return dbContext.Orders
                        .Include(o => o.Customer) // Ensure Customer is included
                        .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving all orders: " + ex.Message);
                return new List<Order>();
            }
        }

        // Method to retrieve a customer by ID
        public static Customer GetCustomerByID(int customerId)
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    return dbContext.Customers.Find(customerId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving customer: " + ex.Message);
                return null;
            }
        }

        // Method to retrieve a product by ID
        public static Product GetProductByID(int productId)
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    return dbContext.Products.Find(productId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving product: " + ex.Message);
                return null;
            }
        }

        // Method to retrieve an order by ID
        public static Order? GetOrderByID(int orderId)
        {
            try
            {
                using (var dbContext = new RetailDbContext())
                {
                    return dbContext.Orders
                        .Include(o => o.Customer) // Ensure Customer is included
                        .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                        .FirstOrDefault(o => o.OrderID == orderId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving order: " + ex.Message);
                return null;
            }
        }
    }
}
