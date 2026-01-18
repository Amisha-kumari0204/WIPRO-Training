using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "D:\\demo.txt";
        using (FileStream fs = File.Create(filePath))
        {
            // File created successfully
            if (File.Exists(filePath))
            {
                Console.WriteLine("File created successfully: " + filePath);
            }
        }

        // Writing to the file using StreamWriter class
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("Hello, this is a demo file created at 13/01/2026.");
            sw.WriteLine("This file is created to demonstrate file handling in C#.");
        }

        // 2. Writing to the file using File.WriteAllText() method
        string fileContent = "This is written using File.WriteAllText at 13/01/2026.";
        File.WriteAllText(filePath, fileContent);
        Console.WriteLine("Data written to file using File.WriteAllText().");

        // Step 4: Read from the file using StreamReader class
        Console.WriteLine("Reading file content using StreamReader:");
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        // Step 5: Delete the file using File.Delete() method
        File.Delete(filePath);
        Console.WriteLine("File deleted successfully.");

        // Step 6: Check if the file exists using File.Exists() method
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist (confirmed after deletion).");
        }
        else
        {
            Console.WriteLine("File still exists.");
        }
    }
}
