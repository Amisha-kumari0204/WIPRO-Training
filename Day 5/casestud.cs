using System;

// Case study: Exception handling for a simple banking transfer scenario
// Demonstrates: try/catch/finally, custom exceptions, input validation, rollback

public class InvalidAccountException : Exception
{
    public InvalidAccountException(string message) : base(message) { }
}

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message) { }
}

public class TransactionFailedException : Exception
{
    public TransactionFailedException(string message, Exception inner = null) : base(message, inner) { }
}

public class BankAccount
{
    public string AccountId { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string accountId, decimal initialBalance)
    {
        AccountId = accountId ?? throw new ArgumentNullException(nameof(accountId));
        Balance = initialBalance;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be greater than zero.", nameof(amount));
        if (amount > Balance) throw new InsufficientFundsException($"Account {AccountId} has insufficient funds.");
        Balance -= amount;
        Console.WriteLine($"[{AccountId}] Withdrawn {amount:C}. New balance: {Balance:C}");
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be greater than zero.", nameof(amount));
        Balance += amount;
        Console.WriteLine($"[{AccountId}] Deposited {amount:C}. New balance: {Balance:C}");
    }
}

public static class CaseStudyBank
{
    // Transfer demonstrates try/catch/finally and a simple rollback strategy
    public static void Transfer(BankAccount from, BankAccount to, decimal amount)
    {
        Console.WriteLine($"\nStarting transfer of {amount:C} from '{from?.AccountId}' to '{to?.AccountId}'...");

        if (from == null || to == null)
            throw new InvalidAccountException("Source or destination account is null.");

        if (amount <= 0)
            throw new ArgumentException("Transfer amount must be greater than zero.", nameof(amount));

        bool withdrawn = false;

        try
        {
            from.Withdraw(amount);
            withdrawn = true;

            // Simulate potential failure on deposit (in real systems this could be a network/db error)
            to.Deposit(amount);

            Console.WriteLine("Transfer completed successfully.");
        }
        catch (InsufficientFundsException ife)
        {
            // Business rule violation: handle gracefully, do NOT flood logs.
            Console.WriteLine($"Transfer failed (business rule): {ife.Message}");
            // Optionally record an informational audit (we keep log noise minimal)
            Logger.LogInfo($"Transfer blocked due to insufficient funds: {ife.Message}");
        }
        catch (Exception ex)
        {
            // Unexpected errors are critical — let logger decide whether to write them.
            Logger.LogException(ex, $"Transfer error: {from?.AccountId} -> {to?.AccountId}, amount {amount:C}");

            // Attempt a simple rollback when withdrawal succeeded but deposit failed
            if (withdrawn)
            {
                try
                {
                    from.Deposit(amount);
                    Console.WriteLine("Rollback: refunded amount to source account.");
                }
                catch (Exception rbEx)
                {
                    // Log rollback failures as errors (may indicate system instability)
                    Logger.LogException(rbEx, "Rollback failed during transfer");
                }
            }

            // Wrap and rethrow to surface the failure to higher-level handlers if needed
            throw new TransactionFailedException("Transfer could not be completed.", ex);
        }
        finally
        {
            // Will always run: logging, metrics, releasing resources, etc.
            Logger.LogInfo("Transfer attempt finished (finally block).");
        }
    }

    public static void Demo()
    {
        var a1 = new BankAccount("A-100", 1000m);
        var a2 = new BankAccount("B-200", 200m);

        // Successful transfer
        try
        {
            Transfer(a1, a2, 150m);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Demo caught: {ex.GetType().Name} - {ex.Message}");
        }

        // Transfer that will fail due to insufficient funds
        try
        {
            Transfer(a2, a1, 1000m);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Demo caught: {ex.GetType().Name} - {ex.Message}");
        }

        // Transfer with invalid account to show top-level handling
        try
        {
            Transfer(null, a1, 50m);
        }
        catch (InvalidAccountException iae)
        {
            Console.WriteLine($"Handled invalid account: {iae.Message}");
        }
        catch (TransactionFailedException tfe)
        {
            Console.WriteLine($"Handled transaction failure: {tfe.Message}");
        }
    }
}
