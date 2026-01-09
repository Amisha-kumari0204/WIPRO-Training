using System;

// Internal class CarParking - for variable names we use lower camel casing
internal class CarParking
{
    // Variables
    private int totalSlots = 10;
    private bool[] parkingSlots; // array to represent parking slots
    private string[] vehicleTypes; // store vehicle types
    private string[] vehicleNumbers; // store vehicle numbers
    private int choice;

    // Step 1: Constructor that initializes parking slots
    public CarParking() // constructor that initializes parking slots
    {
        parkingSlots = new bool[totalSlots];
        vehicleTypes = new string[totalSlots];
        vehicleNumbers = new string[totalSlots];
        
        // Initialize parking slots using for loop
        for (int i = 0; i < totalSlots; i++)
        {
            parkingSlots[i] = false; // false indicates slot is empty
            vehicleTypes[i] = "";
            vehicleNumbers[i] = "";
        }

        Console.WriteLine("Parking initialized with " + totalSlots + " slots.\n");
    }

    // Step 2: Method to park vehicles
    public void ParkVehicle()
    {
        Console.Write("Enter Vehicle Number: ");
        string vehicleNumber = Console.ReadLine();

        Console.WriteLine("Select Vehicle Type: ");
        Console.WriteLine("1. Car ($5/hour)");
        Console.WriteLine("2. Bike ($2/hour)");
        Console.WriteLine("3. Truck ($10/hour)");
        Console.Write("Enter choice (1-3): ");

        int typeChoice = int.Parse(Console.ReadLine());
        string vehicleType = "";

        switch (typeChoice)
        {
            case 1:
                vehicleType = "Car";
                break;
            case 2:
                vehicleType = "Bike";
                break;
            case 3:
                vehicleType = "Truck";
                break;
            default:
                Console.WriteLine("Invalid choice!\n");
                return;
        }

        // Find empty slot
        for (int i = 0; i < totalSlots; i++)
        {
            if (!parkingSlots[i]) // if slot is empty
            {
                parkingSlots[i] = true; // mark as occupied
                vehicleNumbers[i] = vehicleNumber;
                vehicleTypes[i] = vehicleType;
                Console.WriteLine($"\n✓ Vehicle parked successfully at Slot {i + 1}!\n");
                return;
            }
        }

        Console.WriteLine("\n✗ No available slots!\n");
    }

    // Step 3: Method to exit vehicles
    public void ExitVehicle()
    {
        Console.Write("Enter Vehicle Number to exit: ");
        string vehicleNumber = Console.ReadLine();

        // Find the vehicle
        for (int i = 0; i < totalSlots; i++)
        {
            if (parkingSlots[i] && vehicleNumbers[i] == vehicleNumber)
            {
                string vehicleType = vehicleTypes[i];
                
                // Step 4: Calculate charges based on vehicle type
                double charge = CalculateCharge(vehicleType);

                Console.WriteLine($"\n✓ Vehicle exited successfully!");
                Console.WriteLine($"Vehicle: {vehicleType} ({vehicleNumber})");
                Console.WriteLine($"Parking Charge: ${charge}\n");

                // Free the slot
                parkingSlots[i] = false;
                vehicleNumbers[i] = "";
                vehicleTypes[i] = "";
                return;
            }
        }

        Console.WriteLine("\n✗ Vehicle not found!\n");
    }

    // Step 4: Method to calculate charges based on vehicle type
    public double CalculateCharge(string vehicleType)
    {
        double charge = 0;

        switch (vehicleType)
        {
            case "Car":
                charge = 5.0; // $5 per hour
                break;
            case "Bike":
                charge = 2.0; // $2 per hour
                break;
            case "Truck":
                charge = 10.0; // $10 per hour
                break;
        }

        return charge;
    }

    // Method to display available slots
    public void DisplayAvailableSlots()
    {
        Console.WriteLine("\n===== Available Slots =====");
        int count = 0;
        for (int i = 0; i < totalSlots; i++)
        {
            if (!parkingSlots[i])
            {
                Console.WriteLine($"Slot {i + 1}: Available");
                count++;
            }
        }
        Console.WriteLine($"Total Available: {count}\n");
    }

    // Method to display occupied slots
    public void DisplayOccupiedSlots()
    {
        Console.WriteLine("\n===== Occupied Slots =====");
        int count = 0;
        for (int i = 0; i < totalSlots; i++)
        {
            if (parkingSlots[i])
            {
                Console.WriteLine($"Slot {i + 1}: {vehicleTypes[i]} ({vehicleNumbers[i]})");
                count++;
            }
        }
        Console.WriteLine($"Total Occupied: {count}\n");
    }

    // Method to display menu
    public void DisplayMenu()
    {
        Console.WriteLine("===== Car Parking System =====");
        Console.WriteLine("1. Park Vehicle");
        Console.WriteLine("2. Exit Vehicle");
        Console.WriteLine("3. View Available Slots");
        Console.WriteLine("4. View Occupied Slots");
        Console.WriteLine("5. Exit Application");
        Console.Write("Enter your choice: ");
    }
}

// Main Program class
class Program
{
    static void Main()
    {
        // Create instance of CarParking
        CarParking parking = new CarParking();

        // Step 5: Keep application running until user exits
        bool running = true;
        while (running)
        {
            parking.DisplayMenu();
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Step 2: Park vehicles
                    parking.ParkVehicle();
                    break;

                case 2:
                    // Step 3: Exit vehicles
                    parking.ExitVehicle();
                    break;

                case 3:
                    parking.DisplayAvailableSlots();
                    break;

                case 4:
                    parking.DisplayOccupiedSlots();
                    break;

                case 5:
                    running = false;
                    Console.WriteLine("\nThank you for using Car Parking System!");
                    break;

                default:
                    Console.WriteLine("Invalid choice! Please try again.\n");
                    break;
            }
        }
    }
}
