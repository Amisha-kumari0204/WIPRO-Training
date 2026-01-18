using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Creating a file using File.Create() method in default directory
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
            sw.WriteLine("Hello, this is a demo file created today.");
            sw.WriteLine("This file is created to demonstrate file handling in C#.");
        }
        Console.WriteLine("Data written to file successfully.");
    }
}
