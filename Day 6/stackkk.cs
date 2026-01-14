
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Step 1: Create a stack that can hold integer values
        Stack<int> stack = new Stack<int>();
        // Step 2: Push some values onto the stack
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        // Step 3: Pop a value from the stack and display it
        int poppedValue = stack.Pop();
        Console.WriteLine("Popped Value: " + poppedValue);

        // Step 4: Peek at the top value of the stack without removing it
        int topValue = stack.Peek();
        Console.WriteLine("Top Value:  " + topValue);

        // Step 5: Check if the stack contains a specific value
        bool contains20 = stack.Contains(20);
        Console.WriteLine("Stack contains 20: " + contains20);

        // Step 6: Display the current count of items in the stack
        int count = stack.Count;
        Console.WriteLine("Current Count: " + count);

        // Step 7: Clear the stack of all items and show the count afterwards
        stack.Clear();
        Console.WriteLine("Stack cleared.");
        Console.WriteLine("Count after clearing: " + stack.Count);


    }
}