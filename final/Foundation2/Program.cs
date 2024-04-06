using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        var customer1 = new Customer("Andres  Montero", new Address("123 Main St", "Los Angeles", "CA", "USA"));
        var customer2 = new Customer("Stivaliz Trejo", new Address("456 Central Ave", "Toronto", "ON", "Canada"));

        var order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "1001", 999.99m, 1));
        order1.AddProduct(new Product("USB-C Cable", "1002", 19.99m, 2));

        var order2 = new Order(customer2);
        order2.AddProduct(new Product("Book: Personal Growth", "2001", 39.99m, 3));
        order2.AddProduct(new Product("Sticky Notes", "2002", 5.99m, 5));

        DisplayOrderDetails(order1);
        DisplayOrderDetails(order2);
    }

    private static void DisplayOrderDetails(Order order)
    {
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine("\nShipping Label:");
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"\nTotal Price: {order.GetTotalPrice():C}\n");
        Console.WriteLine("----------------------------------------\n");
    }
}

public class Product
{
    public string Name { get; private set; }
    public string ProductId { get; private set; }
    public decimal PricePerUnit { get; private set; }
    public int Quantity { get; private set; }

    public Product(string name, string productId, decimal pricePerUnit, int quantity)
    {
        Name = name;
        ProductId = productId;
        PricePerUnit = pricePerUnit;
        Quantity = quantity;
    }

    public decimal GetTotalCost() => PricePerUnit * Quantity;
}

public class Customer
{
    public string Name { get; private set; }
    public Address Address { get; private set; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public bool LivesInUSA() => Address.IsInUSA();
}

public class Address
{
    public string StreetAddress { get; private set; }
    public string City { get; private set; }
    public string StateOrProvince { get; private set; }
    public string Country { get; private set; }

    public Address(string streetAddress, string city, string stateOrProvince, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        StateOrProvince = stateOrProvince;
        Country = country;
    }

    public bool IsInUSA() => Country == "USA";

    public override string ToString() => $"{StreetAddress}, {City}, {StateOrProvince}, {Country}";
}

public class Order
{
    private List<Product> Products = new List<Product>();
    private Customer Customer;
    private const decimal ShippingCostUSA = 5.0m;
    private const decimal ShippingCostInternational = 35.0m;

    public Order(Customer customer)
    {
        Customer = customer;
    }

    public void AddProduct(Product product) => Products.Add(product);

    public string GetPackingLabel() => string.Join("\n", Products.Select(p => $"{p.Name} - {p.ProductId}"));

    public string GetShippingLabel() => $"{Customer.Name}\n{Customer.Address}";

    public decimal GetTotalPrice()
    {
        var productsTotal = Products.Sum(p => p.GetTotalCost());
        var shippingCost = Customer.LivesInUSA() ? ShippingCostUSA : ShippingCostInternational;
        return productsTotal + shippingCost;
    }
}
