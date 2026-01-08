using System;

class Program
{
    static void Main()
    {
        Console.Write("Please enter your age: ");
        string ageInput = Console.ReadLine() ?? string.Empty;

        if (int.TryParse(ageInput, out int age))
        {
            Console.WriteLine();
            if (age >= 18)
                Console.WriteLine("You are eligible to vote.");
            else
                Console.WriteLine("You are not eligible to vote.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid age.");
        }
    }
}