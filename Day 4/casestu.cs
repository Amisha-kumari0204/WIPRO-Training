using System;

// Step 1: Enum to represent order status
public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

// Step 2: Struct to represent a location (latitude, longitude)
public struct Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public override string ToString() => $"({Latitude}, {Longitude})";
}

// Step 3: Payment contract
public interface IPayment
{
    void ProcessPayment(double amount);
    void RefundPayment(double amount);
    bool MakePayment(double amount);
}

// Example implementations of IPayment
public class CreditCardPayment : IPayment
{
    public string CardNumber { get; set; }

    public CreditCardPayment(string cardNumber)
    {
        CardNumber = cardNumber;
    }

    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"[CreditCard] Processing payment of {amount:C} using card {CardNumber}.");
    }

    public void RefundPayment(double amount)
    {
        Console.WriteLine($"[CreditCard] Refunding {amount:C} to card {CardNumber}.");
    }

    public bool MakePayment(double amount)
    {
        ProcessPayment(amount);
        return true; // stubbed success
    }
}

public class DebitCardPayment : IPayment
{
    public string CardNumber { get; set; }

    public DebitCardPayment(string cardNumber)
    {
        CardNumber = cardNumber;
    }

    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"[DebitCard] Processing payment of {amount:C} using card {CardNumber}.");
    }

    public void RefundPayment(double amount)
    {
        Console.WriteLine($"[DebitCard] Refunding {amount:C} to card {CardNumber}.");
    }

    public bool MakePayment(double amount)
    {
        ProcessPayment(amount);
        return true; // stubbed success
    }
}

// Step 4: Order class which uses the enum and struct and implements IPayment by delegating to a payment method
public class Order : IPayment
{
    public int OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Location ShippingLocation { get; set; }

    private IPayment _paymentMethod;

    public Order(int orderId, OrderStatus status, Location shippingLocation, IPayment paymentMethod)
    {
        OrderId = orderId;
        OrderStatus = status;
        ShippingLocation = shippingLocation;
        _paymentMethod = paymentMethod;
    }

    public void ProcessPayment(double amount) => _paymentMethod.ProcessPayment(amount);
    public void RefundPayment(double amount) => _paymentMethod.RefundPayment(amount);
    public bool MakePayment(double amount) => _paymentMethod.MakePayment(amount);

    public override string ToString() => $"Order {OrderId}: {OrderStatus} (Ship to: {ShippingLocation})";
}

// Small demo helper — call CaseStudy.Demo() from your `Main` if you'd like to see this in action.
public static class CaseStudy
{
    public static void Demo()
    {
        var payment = new CreditCardPayment("1234-5678-9012-3456");
        var order = new Order(1, OrderStatus.Pending, new Location(12.345, 67.890), payment);

        Console.WriteLine(order);
        order.MakePayment(99.99);
        order.OrderStatus = OrderStatus.Processing;
        Console.WriteLine(order);

        // Demo refund
        order.RefundPayment(99.99);
    }
}

// Minimal entry point so this file can be run directly with `dotnet casestu.cs`
public static class CasestuEntry
{
    public static void Main()
    {
        CaseStudy.Demo();
    }
}
