
using System;
using System.Collections;

class Program
{
    static void Main()
    {
        // Step 1: Create a non-generic stack
        Stack stack = new Stack();

        // Step 2: Push mixed values onto the stack
        stack.Push("Amisha");
        stack.Push(true);
        stack.Push(10);
        stack.Push("Hello");

        // Step 3: Pop a value (returns object) and display it
        object popped = stack.Pop();
        Console.WriteLine("Popped (object): " + popped);
        if (popped is int pi) Console.WriteLine("Popped as int: " + pi);

        // Step 4: Peek at the top value without removing it
        if (stack.Count > 0)
        {
            object top = stack.Peek();
            Console.WriteLine("Top (object): " + top);
        }

        // Step 5: Check if the stack contains a specific value
        Console.WriteLine("Contains 20: " + stack.Contains(20));

        // Step 6: Display the current count of items in the stack
        Console.WriteLine("Current Count: " + stack.Count);

        // Step 7: Clear the stack of all items and show the count afterwards
        stack.Clear();
        Console.WriteLine("Stack cleared. Count after clearing: " + stack.Count);

    }
}
