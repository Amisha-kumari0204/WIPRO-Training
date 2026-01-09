using System;

class Program
{
    // Function 1: Calculate Total Marks
    static int CalculateTotal(int[] marks)
    {
        int total = 0;
        foreach (int mark in marks)
        {
            total += mark;
        }
        return total;
    }

    // Function 2: Calculate Average Marks
    static double CalculateAverage(int[] marks)
    {
        int total = CalculateTotal(marks);
        double average = (double)total / marks.Length;
        return average;
    }

    // Function 3: Determine Pass or Fail
    static string DetermineResult(double average)
    {
        if (average >= 40)
            return "PASS";
        else
            return "FAIL";
    }

    // Main Function - calls all three functions
    static void Main()
    {
        // Sample marks for 5 students
        int[] marks = { 78, 65, 49, 90, 55 };

        Console.WriteLine("=== School Marks System ===\n");
        
        // Call Function 1: CalculateTotal
        int totalMarks = CalculateTotal(marks);
        Console.WriteLine($"Total Marks: {totalMarks}");

        // Call Function 2: CalculateAverage
        double avgMarks = CalculateAverage(marks);
        Console.WriteLine($"Average Marks: {avgMarks:F2}");

        // Call Function 3: DetermineResult
        string result = DetermineResult(avgMarks);
        Console.WriteLine($"Result: {result}");
    }
}
