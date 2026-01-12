using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Static and Non-Static Members in C#");

        Counter counter1 = new Counter();
        Counter counter2 = new Counter();
        Counter counter3 = new Counter();

        // Accessing non-static method using object
        counter1.DisplayCount(); // Output: Count: 3

        // Accessing static method using class name
        Counter.StaticDisplayCount(); // Output: Static Count: 3
    }
}

// Counter class moved to Counter.cs
