using System;
using System.Collections.Generic;

namespace Demo_Collection_Stack_Queue
{
    // In C# we have Lambda Expressions which are used for short, inline functions
    // (parameters) => expression or blocks
    // Example: Func<int, bool> IsEven = number => number % 2 == 0;

    internal class DemoLambda
    {
        // Normal method (expression-bodied)
        static bool IsEvenMethod(int number) => number % 2 == 0;

        static void Main(string[] args)
        {
            // Call the normal method
            Console.WriteLine($"Is 4 even (method)? {IsEvenMethod(4)}");

            // Lambda assigned to a Func delegate
            Func<int, bool> IsEven = number => number % 2 == 0;
            Console.WriteLine($"Is 5 even (lambda)? {IsEven(5)}");

            // Use lambda with List.FindAll to filter values
            var nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var evens = nums.FindAll(n => n % 2 == 0);
            Console.WriteLine("Evens: " + string.Join(", ", evens));

            // Lambda with Action
            Action<string> greet = name => Console.WriteLine($"Hello, {name}!");
            greet("Alice");

            // Predicate delegate example
            Predicate<int> isOdd = x => x % 2 != 0;
            Console.WriteLine($"Is 3 odd? {isOdd(3)}");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
