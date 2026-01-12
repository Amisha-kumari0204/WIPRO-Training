using System;

public class Counter
{
    // non static variable
    public int instanceCount = 0; // Each object of Counter class will have its own copy of instanceCount variable

    // static variable
    public static int count = 0; // At any point, we can access this static variable using class name Counter.count

    // constructor
    public Counter()
    {
        count++; // increment - reflects across all objects
    }

    // non static method which can access static variables
    public void DisplayCount()
    {
        Console.WriteLine("Count: " + count);
    }

    // static method
    public static void StaticDisplayCount() // static method can access only static variables
    {
        Console.WriteLine("Static Count: " + count);
    }
}
