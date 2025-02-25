using System;
using System.Collections.Generic;
using System.Linq;

namespace RetailApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Retail Management System");
            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Add New Product");
            Console.WriteLine("2. Add New Customer");
            Console.WriteLine("3. Check Inventory / Apply Discount");
            Console.WriteLine("4. Check / Place Orders");
            Console.WriteLine("5. Exit");

            Console.Write("Select an option (1-5): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddNewProductMenu();
                    break;
                case "2":
                    AddNewCustomerMenu();
                    break;
                case "3":
                    CheckInventoryMenu();
                    break;
                case "4":
                    CheckPlaceOrdersMenu();
                    break;
                case "5":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    ShowMainMenu();
                    break;
            }
        }

        static void AddNewProductMenu()
        {
            Console.WriteLine("\nAdd New Product:");
            Console.WriteLine("Select Product Category:");
            Console.WriteLine("1. Electronics");
            Console.WriteLine("2. Clothing");
            Console.WriteLine("3. Grocery");
            Console.Write("Enter your choice (1-3): ");
            string categoryChoice = Console.ReadLine();

            string productName;
            decimal price;
            int stockLevel;

            Console.Write("\nEnter Product Name: ");
            productName = Console.ReadLine();

            Console.Write("Enter Price: ");
            price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Stock Level: ");
            stockLevel = int.Parse(Console.ReadLine());

            switch (categoryChoice)
            {
                case "1":
                    AddElectronics(productName, price, stockLevel);
                    break;
                case "2":
                    AddClothing(productName, price, stockLevel);
                    break;
                case "3":
                    AddGrocery(productName, price, stockLevel);
                    break;
                default:
                    Console.WriteLine("Invalid category choice.");
                    break;
            }

            ShowMainMenu();
        }

        static void AddElectronics(string productName, decimal price, int stockLevel)
        {
            Console.Write("Enter Warranty Period (months): ");
            int warrantyPeriod = int.Parse(Console.ReadLine());

            var electronics = new Electronics(0, productName, price, stockLevel, warrantyPeriod);
            SaveProduct(electronics);

            Console.WriteLine("Electronics product added successfully!");
        }

        static void AddClothing(string productName, decimal price, int stockLevel)
        {
            Console.Write("Enter Size: ");
            string size = Console.ReadLine();

            Console.Write("Enter Material: ");
            string material = Console.ReadLine();

            var clothing = new Clothing(0, productName, price, stockLevel, size, material);
            SaveProduct(clothing);

            Console.WriteLine("Clothing product added successfully!");
        }

        static void AddGrocery(string productName, decimal price, int stockLevel)
        {
            Console.Write("Enter Expiration Date (YYYY-MM-DD): ");
            DateTime expirationDate = DateTime.Parse(Console.ReadLine());

            var grocery = new Grocery(0, productName, price, stockLevel, expirationDate);
            SaveProduct(grocery);

            Console.WriteLine("Grocery product added successfully!");
        }

        static void SaveProduct(Product product)
        {
            Database.SaveProductToDatabase(product);
            Console.WriteLine($"{product.ProductName} has been saved to the database.");
        }

        static void AddNewCustomerMenu()
        {
            Console.WriteLine("\nAdd New Customer:");

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Tel/Cell No: ");
            string telNo = Console.ReadLine();

            // Create a new Customer instance
            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Address = address,
                TelNo = telNo
            };

            // Save to database
            SaveCustomer(customer);

            Console.WriteLine("Customer added successfully!");
            ShowMainMenu();
        }

        static void SaveCustomer(Customer customer)
        {
            Database.SaveCustomerToDatabase(customer);
        }

        static void CheckInventoryMenu()
        {
            Console.WriteLine("\nInventory Menu:");
            Console.WriteLine("1. Check Inventory");
            Console.WriteLine("2. Update Products");
            Console.WriteLine("3. Apply Discount");
            Console.Write("Select an option (1-3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CheckInventory();
                    break;
                case "2":
                    UpdateProduct();
                    break;
                case "3":
                    ApplyDiscount();
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    CheckInventoryMenu();
                    break;
            }
        }

        static void CheckInventory()
        {
            Console.WriteLine("\nChecking Inventory:");
            var products = Database.GetAllProducts(); // Retrieve products from DB
            if (products.Count == 0)
            {
                Console.WriteLine("No products found in the inventory.");
            }
            else
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}, Stock Level: {product.StockLevel}");
                }
            }
            ShowMainMenu();
        }

        static void UpdateProduct()
        {
            Console.WriteLine("\nUpdate Product:");
            var products = Database.GetAllProducts(); // Retrieve products from DB

            if (products.Count == 0)
            {
                Console.WriteLine("No products available to update.");
                ShowMainMenu();
                return;
            }

            Console.WriteLine("Select the product ID to update:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}, Stock Level: {product.StockLevel}");
            }

            Console.Write("Enter the Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            var selectedProduct = products.FirstOrDefault(p => p.ProductID == productId);
            if (selectedProduct != null)
            {
                Console.Write("Enter new price (or leave blank to keep current price): ");
                string newPriceInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(newPriceInput))
                {
                    selectedProduct.Price = decimal.Parse(newPriceInput);
                }

                Console.Write("Enter new stock level (or leave blank to keep current stock): ");
                string newStockLevelInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(newStockLevelInput))
                {
                    selectedProduct.StockLevel = int.Parse(newStockLevelInput);
                }

                Database.UpdateProduct(selectedProduct); // Save the updated product to DB
                Console.WriteLine("Product updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Product ID.");
            }

            ShowMainMenu();
        }

        static void ApplyDiscount()
        {
            Console.WriteLine("\nApply Discount:");
            var discountCategories = Database.GetDiscountCategories(); // Fetch categories with discount

            if (discountCategories.Count == 0)
            {
                Console.WriteLine("No discount categories available.");
                ShowMainMenu();
                return;
            }

            foreach (var discountCategory in discountCategories)
            {
                Console.WriteLine($"Category: {discountCategory.Category}, Current Discount: {discountCategory.DiscountPercentage}%");

                Console.Write($"Enter new discount percentage for {discountCategory.Category} (or leave blank to keep current): ");
                string discountInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(discountInput))
                {
                    discountCategory.DiscountPercentage = decimal.Parse(discountInput);
                }

                Console.Write($"Enter new quantity threshold for {discountCategory.Category} (or leave blank to keep current): ");
                string thresholdInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(thresholdInput))
                {
                    discountCategory.QuantityThreshold = int.Parse(thresholdInput);
                }
            }

            Database.UpdateDiscountCategories(discountCategories); // Save discount changes to DB
            Console.WriteLine("Discounts applied successfully!");

            ShowMainMenu();
        }

        static void CheckPlaceOrdersMenu()
        {
            Console.WriteLine("\nOrder Menu:");
            Console.WriteLine("1. Place Order");
            Console.WriteLine("2. Complete/Return Order");
            Console.WriteLine("3. List Orders");
            Console.Write("Select an option (1-3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlaceOrder();
                    break;
                case "2":
                    CompleteOrReturnOrder();
                    break;
                case "3":
                    ListOrders();
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    CheckPlaceOrdersMenu();
                    break;
            }
        }

        static void PlaceOrder()
        {
            Console.WriteLine("\nPlace Order:");

            var products = Database.GetAllProducts();
            if (products.Count == 0)
            {
                Console.WriteLine("No products available.");
                ShowMainMenu();
                return;
            }

            var cart = new List<OrderDetail>();

            while (true)
            {
                Console.WriteLine("Available Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}, Stock Level: {product.StockLevel}");
                }

                Console.Write("Enter Product ID to add to cart (0 to finish): ");
                int productId = int.Parse(Console.ReadLine());

                if (productId == 0)
                {
                    break;
                }

                var selectedProduct = products.FirstOrDefault(p => p.ProductID == productId);
                if (selectedProduct != null)
                {
                    Console.Write("Enter Quantity: ");
                    int quantity = int.Parse(Console.ReadLine());

                    if (quantity > selectedProduct.StockLevel)
                    {
                        Console.WriteLine("Quantity exceeds stock level. Try again.");
                        continue;
                    }

                    cart.Add(new OrderDetail(0, productId, quantity, selectedProduct.Price));
                }
                else
                {
                    Console.WriteLine("Invalid Product ID. Try again.");
                }
            }

            var customer = GetCustomer();
            var order = new Order(customer.CustomerID, cart);
            ApplyDiscountsToOrder(order);

            // Display the corrected total per product
            Console.WriteLine("Order Summary:");
            foreach (var detail in order.OrderDetails)
            {
                var product = Database.GetProductByID(detail.ProductID);
                Console.WriteLine($"Product ID: {product.ProductID}, Final Price (after discount): {detail.Price:C}, Quantity: {detail.Quantity}");
            }

            // Correct total order cost calculation
            Console.WriteLine($"Total Order Cost: {order.CalculateTotal():C}");

            Console.WriteLine("Confirm Order? (Y/N): ");
            if (Console.ReadLine().Trim().ToUpper() == "Y")
            {
                Database.SaveOrderToDatabase(order);

                foreach (var detail in cart)
                {
                    var product = products.First(p => p.ProductID == detail.ProductID);
                    product.StockLevel -= detail.Quantity;
                    Database.UpdateProduct(product);
                }

                Console.WriteLine("Order placed successfully!");
            }
            else
            {
                Console.WriteLine("Order canceled.");
            }

            ShowMainMenu();
        }

        static Customer GetCustomer()
        {
            Console.WriteLine("\nEnter Customer Details:");

            Console.Write("Enter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            var customer = Database.GetCustomerByID(customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return GetCustomer();
            }

            return customer;
        }

        static void ApplyDiscountsToOrder(Order order)
        {
            var discountCategories = Database.GetDiscountCategories();

            foreach (var detail in order.OrderDetails)
            {
                var product = Database.GetProductByID(detail.ProductID);
                var discountCategory = discountCategories.FirstOrDefault(dc => dc.Category == product.Category);

                // Apply discount if the product quantity meets or exceeds the threshold
                if (discountCategory != null && detail.Quantity >= discountCategory.QuantityThreshold)
                {
                    decimal discountAmount = product.Price * (discountCategory.DiscountPercentage / 100);
                    decimal discountedUnitPrice = product.Price - discountAmount;

                    // Calculate the total price after discount for the given quantity
                    detail.Price = discountedUnitPrice * detail.Quantity;
                }
                else
                {
                    // No discount applied, retain original total price for the given quantity
                    detail.Price = product.Price * detail.Quantity;
                }
            }
        }

        static void CompleteOrReturnOrder()
        {
            Console.WriteLine("\nComplete/Return Order:");

            var orders = Database.GetPendingOrders(); // Retrieve pending orders from DB
            if (orders.Count == 0)
            {
                Console.WriteLine("No pending orders.");
                ShowMainMenu();
                return;
            }

            Console.WriteLine("Select the Order ID to complete/return:");
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.OrderID}, Customer: {order.Customer.FirstName} {order.Customer.LastName}, Date: {order.OrderDate}");
            }

            Console.Write("Enter the Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            var selectedOrder = orders.FirstOrDefault(o => o.OrderID == orderId);
            if (selectedOrder != null)
            {
                Console.WriteLine("Select action: 1. Complete 2. Return");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    selectedOrder.UpdateStatus("Completed");
                    Database.UpdateOrderStatus(selectedOrder);
                    Console.WriteLine("Order completed successfully!");
                }
                else if (action == "2")
                {
                    selectedOrder.UpdateStatus("Returned");
                    Database.UpdateOrderStatus(selectedOrder);

                    // Update stock levels
                    foreach (var detail in selectedOrder.OrderDetails)
                    {
                        var product = Database.GetProductByID(detail.ProductID);
                        product.StockLevel += detail.Quantity;
                        Database.UpdateProduct(product);
                    }

                    Console.WriteLine("Order returned and stock levels updated.");
                }
                else
                {
                    Console.WriteLine("Invalid action.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Order ID.");
            }

            ShowMainMenu();
        }

        static void ListOrders()
        {
            Console.WriteLine("\nList Orders:");

            var orders = Database.GetAllOrders(); // Retrieve all orders from DB
            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                ShowMainMenu();
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.OrderID}, Status: {order.Status}, Date: {order.OrderDate}");
                Console.WriteLine($"Customer: {order.Customer.FirstName} {order.Customer.LastName}, Address: {order.Customer.Address}");
                foreach (var detail in order.OrderDetails)
                {
                    Console.WriteLine($"  Product ID: {detail.ProductID}, Quantity: {detail.Quantity}, Price: {detail.Price:C}");
                }
                Console.WriteLine();
            }

            ShowMainMenu();
        }
    }
}
