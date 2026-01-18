using System;
using System.IO;

// Centralized logger that classifies exceptions and controls what gets written to logs.
public enum LogLevel { Trace = 0, Debug = 1, Info = 2, Warning = 3, Error = 4, Critical = 5 }

public class ValidationException : Exception { public ValidationException(string msg) : base(msg) { } }
public class BusinessRuleException : Exception { public BusinessRuleException(string msg) : base(msg) { } }
public class ExternalServiceException : Exception { public ExternalServiceException(string msg, Exception? inner = null) : base(msg, inner) { } }

public static class Logger
{
    // Configure this to control which levels are actually written to the console/log.
    // Requirement: "The system must log only critical exceptions" — default to Critical.
    public static LogLevel MinimumLogLevel { get; set; } = LogLevel.Critical;

    // Classify exceptions and decide whether to write them.
    public static void LogException(Exception ex, string? context = null)
    {
        if (ex == null) return;

        // Known, expected business exceptions should not be logged as errors.
        if (ex is ValidationException || ex is BusinessRuleException)
        {
            // Optionally write a quiet info-level message (commented out by default to reduce noise)
            // LogInfo(context ?? "", ex.Message);
            return; // do not log
        }

        // Map exception type to a log level.
        var level = Classify(ex);

        if (level < MinimumLogLevel)
            return; // skip noisy logs

        Write(level, context, ex);
    }

    public static void LogInfo(string message)
    {
        if (LogLevel.Info < MinimumLogLevel) return;
        Console.WriteLine($"[{DateTime.Now:O}] INFO: {message}");
    }

    private static LogLevel Classify(Exception ex)
    {
        // Place more specific/critical exceptions first.
        if (ex is ExternalServiceException) return LogLevel.Critical;
        if (ex is IOException || ex is UnauthorizedAccessException) return LogLevel.Error;
        if (ex is TransactionFailedException) return LogLevel.Critical;

        // Default: Error
        return LogLevel.Error;
    }

    private static void Write(LogLevel level, string? context, Exception ex)
    {
        Console.WriteLine($"[{DateTime.Now:O}] {level}: {context ?? ""} - {ex.GetType().Name}: {ex.Message}");
        if (ex.InnerException != null)
            Console.WriteLine($"  Inner: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
    }
}

// TransactionFailedException referenced by logger classification
public class TransactionFailedException : Exception
{
    public TransactionFailedException(string message, Exception? inner = null) : base(message, inner) { }
}
