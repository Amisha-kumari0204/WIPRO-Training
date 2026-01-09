using System;

class TwoDArrayProgram
{
    static void Main()
    {
        int[,] matrix = new int[3, 3]; 

        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 3;

        matrix[1, 0] = 4;
        matrix[1, 1] = 5;
        matrix[1, 2] = 6;

        matrix[2, 0] = 7;
        matrix[2, 1] = 8;
        matrix[2, 2] = 9;

        Console.WriteLine("2D Array (Matrix):");
        for (int i = 0; i < 3; i++) //Iterate through rows
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(matrix[i, j] + " "); 
            }
            Console.WriteLine(); 
        }

     }
}