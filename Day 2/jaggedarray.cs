using System;

class JaggedArrayProgram
{
    static void Main()
    {
        Console.WriteLine("Jagged Arrays in C#");
        
        int[][] jaggedArray = new int[3][]; 

        jaggedArray[0] = new int[] { 1, 2 }; 
        jaggedArray[1] = new int[] { 3, 4, 5 }; 
        jaggedArray[2] = new int[] { 6, 7, 8, 9 }; 

        // Print Jagged Array
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            Console.Write($"Row {i}: ");
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write(jaggedArray[i][j] + " ");
            }
            Console.WriteLine();
        }
    }
}