using System;
using System.IO;

// Day 5 - Exception Handling Demos
// This file demonstrates common exception handling patterns in C#:
// - try/catch/finally
// - multiple catch blocks and exception filters
// - custom exceptions
// - argument validation and rethrowing
// - File I/O exception handling

public class ResourceNotAvailableException : Exception
{
    public ResourceNotAvailableException(string message) : base(message) { }
}

public static class Program
{
    public static void Main()
    {
        try
        {
            DemoDivision();
            DemoParsing();
            DemoCustomException();
            DemoFileRead();
            DemoArgumentValidation();
            DemoRethrowBehavior();

            Console.WriteLine("\nAll demos finished.");
        }
        catch (Exception ex)
        {
            // Top-level catch to ensure a friendly message for unexpected errors
            Console.WriteLine($"Fatal error: {ex.GetType().Name} - {ex.Message}");
        }
    }

    // Example 1: Division with DivideByZeroException handling and finally
    private static void DemoDivision()
    {
        Console.WriteLine("\nDemoDivision: divide 100 by provided values");
        int[] divisors = { 5, 0, -2 };
        foreach (var d in divisors)
        {
            try
            {
                Console.WriteLine($"Attempting to divide by {d}...");
                int result = 100 / d; // may throw DivideByZeroException
                Console.WriteLine($"Result: {result}");
            }
            catch (DivideByZeroException dbz)
            {
                Console.WriteLine($"Caught divide-by-zero: {dbz.Message}");
            }
            finally
            {
                Console.WriteLine("Cleaning up after division (finally block).");
            }
        }
    }

    // Example 2: Parsing with FormatException and exception filter
    private static void DemoParsing()
    {
        Console.WriteLine("\nDemoParsing: parsing different inputs");
        string[] inputs = { "42", "abc", "100" };

        foreach (var input in inputs)
        {
            try
            {
                Console.WriteLine($"Parsing '{input}'...");
                int value = int.Parse(input); // may throw FormatException
                Console.WriteLine($"Parsed value: {value}");
            }
            catch (FormatException fex) when (input == "abc")
            {
                // Exception filter demonstrates handling a specific case differently
                Console.WriteLine($"Filtered handler: Input '{input}' is not a number.");
            }
            catch (FormatException fex)
            {
                Console.WriteLine($"General format error: {fex.Message}");
            }
        }
    }

    // Example 3: Throwing and catching a custom exception
    private static void DemoCustomException()
    {
        Console.WriteLine("\nDemoCustomException: simulating missing resource");
        try
        {
            EnsureResourceAvailable(null);
        }
        catch (ResourceNotAvailableException rnax)
        {
            Console.WriteLine($"Resource error: {rnax.Message}");
        }
    }

    private static void EnsureResourceAvailable(string resource)
    {
        if (string.IsNullOrWhiteSpace(resource))
            throw new ResourceNotAvailableException("Required resource is not available or is empty.");
    }

    // Example 4: File I/O with specific FileNotFoundException handling
    private static void DemoFileRead()
    {
        Console.WriteLine("\nDemoFileRead: attempting to read a missing file");
        string path = Path.Combine(Directory.GetCurrentDirectory(), "nonexistent.txt");

        try
        {
            var text = File.ReadAllText(path);
            Console.WriteLine(text);
        }
        catch (FileNotFoundException fnf)
        {
            Console.WriteLine($"File not found: {fnf.FileName}");
        }
        catch (UnauthorizedAccessException ua)
        {
            Console.WriteLine("Permission denied when reading file.");
        }
        catch (IOException io)
        {
            Console.WriteLine($"I/O error: {io.Message}");
        }
        finally
        {
            Console.WriteLine("Finished file read attempt.");
        }
    }

    // Example 5: Argument validation and throwing ArgumentException
    private static void DemoArgumentValidation()
    {
        Console.WriteLine("\nDemoArgumentValidation: validating inputs");

        try
        {
            Greet(null);
        }
        catch (ArgumentNullException anex)
        {
            Console.WriteLine($"Argument error: {anex.ParamName} - {anex.Message}");
        }

        try
        {
            SetAge(-5);
        }
        catch (ArgumentOutOfRangeException aor)
        {
            Console.WriteLine($"Age error: {aor.ParamName} - {aor.Message}");
        }
    }

    private static void Greet(string name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name), "Name cannot be null.");
        Console.WriteLine($"Hello, {name}!");
    }

    private static void SetAge(int age)
    {
        if (age < 0 || age > 130) throw new ArgumentOutOfRangeException(nameof(age), "Age must be between 0 and 130.");
        Console.WriteLine($"Age set to {age}.");
    }

    // Example 6: Demonstrate rethrowing vs wrapping
    private static void DemoRethrowBehavior()
    {
        Console.WriteLine("\nDemoRethrowBehavior: show throw vs throw ex");

        try
        {
            MethodThatWrapsException();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught wrapped exception: {ex.GetType().Name} - {ex.Message}");
        }

        try
        {
            MethodThatRethrows();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught rethrown exception (preserves stack): {ex.GetType().Name} - {ex.Message}");
        }
    }

    private static void MethodThatWrapsException()
    {
        try
        {
            ThrowSample();
        }
        catch (Exception ex)
        {
            // Wrapping loses original stack if not careful; include original as InnerException
            throw new ApplicationException("Failed in MethodThatWrapsException", ex);
        }
    }

    private static void MethodThatRethrows()
    {
        try
        {
            ThrowSample();
        }
        catch (Exception)
        {
            // Rethrow using 'throw;' preserves original stack trace
            throw;
        }
    }

    private static void ThrowSample()
    {
        throw new InvalidOperationException("Sample failure for demo purposes.");
    }
}
