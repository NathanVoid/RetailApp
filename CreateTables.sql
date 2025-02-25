use OnlineRetail
go

CREATE TABLE DiscountCategories (
    DiscountCategoryID INT IDENTITY(1,1) PRIMARY KEY,
    Category NVARCHAR(255) NOT NULL,
    DiscountPercentage DECIMAL(5,2) NOT NULL,
    QuantityThreshold INT NOT NULL
);

CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Address NVARCHAR(500),
    TelNo NVARCHAR(20)
);

CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    DeliveryDate DATETIME NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(255) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    StockLevel INT NOT NULL,
    Category NVARCHAR(100) NOT NULL
);

CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Additional attributes for specific product types
ALTER TABLE Products ADD ExpirationDate DATETIME NULL;
ALTER TABLE Products ADD Size NVARCHAR(50) NULL;
ALTER TABLE Products ADD Material NVARCHAR(255) NULL;
ALTER TABLE Products ADD WarrantyPeriod INT NULL;
