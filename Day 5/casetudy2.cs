using System;

//Creating a account class that act as a busineslogic class for implementing user define exception 
public class BankAccount //business logic class
 {
     private decimal dailyLimit = 1000; //daily limit for transactions
     private decimal totalTransactionsToday = 0; //to keep track of total transactions made today

     // Expose total transactions for reporting (read-only)
     public decimal TotalTransactionsToday => totalTransactionsToday;

     public void MakeTransaction(decimal amount)
     {
         if (totalTransactionsToday + amount > dailyLimit)
         {
             //throwing user define exception when daily limit is exceeded.
             throw new DailyLimitExceededException("Daily transaction limit exceeded.");
         }
         totalTransactionsToday += amount;
         Console.WriteLine($"Transaction of {amount} completed successfully.");
     }
 }

public class DailyLimitExceededException : Exception
{
    public DailyLimitExceededException(string message) : base(message) { }
}

public static class Program
{
    public static void Main()
    {
        var account = new BankAccount();

        try
        {
            Console.WriteLine("Starting transaction demo...");
            account.MakeTransaction(500m);
            account.MakeTransaction(600m); // this should exceed daily limit (500 + 600 > 1000)
            Console.WriteLine("Transactions completed.");
        }
        catch (DailyLimitExceededException dlex)
        {
            // Business rule exception — do not flood logs. Handle gracefully and inform the user.
            Console.WriteLine($"Business rule: {dlex.Message}");
            // Record minimal info via logger (Info-level only; will not be output with MinimumLogLevel=Critical)
            Logger.LogInfo($"Daily limit exceeded while making a transaction: {dlex.Message}");
        }
        catch (ArgumentException aex)
        {
            Console.WriteLine($"Invalid input: {aex.Message}");
            Logger.LogInfo($"Invalid input during transaction: {aex.Message}");
        }
        catch (Exception ex)
        {
            // Unexpected/critical exceptions should be logged so they can be investigated
            Logger.LogException(ex, "Unexpected error in transaction demo");
            Console.WriteLine($"Unexpected error: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"Total transactions recorded today: {account.TotalTransactionsToday:C}");
            Console.WriteLine("Demo finished. Resources cleaned up (if any). (finally block)");
        }
    }
}