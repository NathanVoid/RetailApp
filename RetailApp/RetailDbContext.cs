using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RetailApp;

public class RetailDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<DiscountCategory> DiscountCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Change to match MSSQL server and the Same Database
        optionsBuilder.UseSqlServer("Server=DESKTOP-AG2G1CE\\MSSQL2022;Database=OnlineRetail;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure inheritance for Product types
        modelBuilder.Entity<Product>()
            .HasDiscriminator<string>("Category")
            .HasValue<Electronics>("Electronics")
            .HasValue<Clothing>("Clothing")
            .HasValue<Grocery>("Grocery");

        // Configure Price precision for Product and OrderDetail
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)"); // Specify precision and scale

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.Price)
            .HasColumnType("decimal(18,2)"); // Specify precision and scale

        // Configure relationships for OrderDetail
        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => od.OrderDetailID);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderID);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany()
            .HasForeignKey(od => od.ProductID);

        // Configure DiscountCategory
        modelBuilder.Entity<DiscountCategory>()
            .HasKey(dc => dc.DiscountCategoryID);

        modelBuilder.Entity<DiscountCategory>()
            .Property(dc => dc.DiscountPercentage)
            .HasColumnType("decimal(5,2)"); // Specify precision and scale for discount

        modelBuilder.Entity<DiscountCategory>()
            .Property(dc => dc.QuantityThreshold) // Add configuration for QuantityThreshold
            .HasDefaultValue(0); // Set default value for QuantityThreshold

        // Configure Order
        modelBuilder.Entity<Order>()
            .HasKey(o => o.OrderID);

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasDefaultValue("Pending");

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerID);
    }
}
