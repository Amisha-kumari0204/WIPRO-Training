using System;

class ArrayProgram
{
    static void Main()
    {
        // Step 1: Declare the array of type int
        int[] numbers;

        // Step 2: Initialize the array with size 5
        numbers = new int[5];
        // Step 3: Assign values to each index of the array
        numbers[0] = 10;
        numbers[1] = 20;
        numbers[2] = 30;
        numbers[3] = 40;
        numbers[4] = 50;

        // Step 4: Print the values of the array using a for loop
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine($"Index {i}: {numbers[i]}");
        }
    }
}
