using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderProcessingSystem
{
    // Generic Class: GenericOrderProcessor
    public class GenericOrderProcessor<T>
    {
        // Generic Field: Stores generic data
        private T _orderData;

        // Event for Subscribers (Email/Log)
        public event Action<string> OnOrderProcessed;

        public GenericOrderProcessor(T orderData)
        {
            _orderData = orderData;
        }

        // Generic Method: Processes any data type
        public void DisplayOrderInfo<U>(U additionalInfo)
        {
            Console.WriteLine($"[Generic Method] Processing Order Data: {_orderData}, Extra Info: {additionalInfo}");
        }

        // Async & Await: Non-blocking operations
        public async Task ProcessOrderAsync(string orderName)
        {
            Console.WriteLine($"[Async] Starting async processing for {orderName}...");
            await Task.Delay(1000); // Simulate I/O operation
            NotifySubscribers($"Order '{orderName}' has been processed asynchronously.");
        }

        // Threading: Parallel order processing
        public void ProcessOrdersInParallel(List<string> orders)
        {
            Console.WriteLine("[Threading] Processing multiple orders in parallel...");
            Parallel.ForEach(orders, (order) =>
            {
                Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] Processing: {order}");
                Thread.Sleep(500); // Simulate work
            });
        }

        // ref & out: Modify existing value and return additional computed value
        public void UpdateOrderStatus(ref int statusCode, out string statusMessage)
        {
            if (statusCode == 0)
            {
                statusCode = 1; // Modified via ref
                statusMessage = "Order is now in progress."; // Returned via out
            }
            else
            {
                statusCode = 2;
                statusMessage = "Order completed.";
            }
        }

        private void NotifySubscribers(string message)
        {
            OnOrderProcessed?.Invoke(message);
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Generic Async Order Processing System ===\n");

            // 1. Handlers for Subscribers (Events)
            Action<string> emailSubscriber = (msg) => Console.WriteLine($"[Email Service] Notification sent: {msg}");
            Action<string> logSubscriber = (msg) => Console.WriteLine($"[Log Service] Entry added: {msg}");

            // 2. Generic Class usage (string order)
            var stringProcessor = new GenericOrderProcessor<string>("ORD-1001");
            stringProcessor.OnOrderProcessed += emailSubscriber;
            stringProcessor.OnOrderProcessed += logSubscriber;

            // 3. Generic Method usage
            stringProcessor.DisplayOrderInfo(DateTime.Now);

            // 4. ref & out usage
            int status = 0;
            string message;
            Console.WriteLine($"[Ref/Out] Initial Status: {status}");
            stringProcessor.UpdateOrderStatus(ref status, out message);
            Console.WriteLine($"[Ref/Out] Updated Status: {status}, Message: {message}");

            // 5. Async/Await usage
            await stringProcessor.ProcessOrderAsync("Premium Subscription");

            // 6. Parallel Processing (Threading)
            var orders = new List<string> { "Order_A", "Order_B", "Order_C", "Order_D" };
            stringProcessor.ProcessOrdersInParallel(orders);

            // 7. Generic Class usage with int
            Console.WriteLine("\n--- Processing Integer Orders ---");
            var intProcessor = new GenericOrderProcessor<int>(5005);
            intProcessor.DisplayOrderInfo("Urgent");

            Console.WriteLine("\nSystem Processing Complete.");
        }
    }
}
