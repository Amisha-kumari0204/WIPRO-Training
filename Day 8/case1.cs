using System;
using System.Collections.Generic;


// Employee Case - Indexer for Monthly Attendance
class Employee
{
    private string employeeName;
    private int employeeId;
    private Dictionary<int, int> monthlyAttendance = new Dictionary<int, int>();

    public Employee(string name, int id)
    {
        employeeName = name;
        employeeId = id;
    }

    
    public int this[int month]
    {
        get
        {
            if (monthlyAttendance.ContainsKey(month))
                return monthlyAttendance[month];
            return 0;
        }
        set
        {
            monthlyAttendance[month] = value;
        }
    }

    public void DisplayAttendance()
    {
        Console.WriteLine($"\nEmployee: {employeeName} (ID: {employeeId})");
        Console.WriteLine("Monthly Attendance:");
        for (int month = 1; month <= 12; month++)
        {
            Console.WriteLine($"  Month {month}: {this[month]} days");
        }
    }
}

// E-Commerce Case - Indexer for Customer Ratings
class Product
{
    private string productName;
    private decimal price;
    private Dictionary<int, int> customerRatings = new Dictionary<int, int>();
    private int ratingCount = 0;

    public Product(string name, decimal productPrice)
    {
        productName = name;
        price = productPrice;
    }

    // Indexer for customer ratings
    public int this[int customerIndex]
    {
        get
        {
            if (customerRatings.ContainsKey(customerIndex))
                return customerRatings[customerIndex];
            return 0;
        }
        set
        {
            customerRatings[customerIndex] = value;
            if (customerIndex > ratingCount)
                ratingCount = customerIndex;
        }
    }

    public void DisplayProductInfo()
    {
        Console.WriteLine($"\nProduct: {productName}");
        Console.WriteLine($"Price: ${price}");
        Console.WriteLine("Customer Ratings:");
        for (int i = 1; i <= ratingCount; i++)
        {
            if (customerRatings.ContainsKey(i))
                Console.WriteLine($"  Customer {i}: {this[i]} stars");
        }
    }
}

class Program
{
    static void Main()
    {
        // Employee Case Example
        Console.WriteLine("EMPLOYEE CASE");
        Employee emp = new Employee("John Doe", 101);
        
    
        emp[1] = 20;   // January
        emp[2] = 19;  
        emp[3] = 21;   
        emp[4] = 18;  
        emp[5] = 22;   

        emp.DisplayAttendance();

        // E-Commerce Case Example
        Console.WriteLine("\nE-COMMERCE CASE");
        Product product = new Product("Laptop", 999.99m);
        
        // Using indexer to set customer ratings
        product[1] = 5;  // Customer 1 rating
        product[2] = 4;  // Customer 2 rating
        product[3] = 5;  // Customer 3 rating
        product[4] = 3;  // Customer 4 rating
        product[5] = 4;  // Customer 5 rating

        product.DisplayProductInfo();

        // Accessing via indexer
        Console.WriteLine($"\nAccessing Laptop rating from customer 3: {product[3]} stars");
        Console.WriteLine($"Accessing John's attendance in March (month 3): {emp[3]} days");
    }
}
