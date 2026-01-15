using System;
using System.Collections.Generic;

namespace DReg_Sorting_techniques
{
       internal class Program
    {
        static void Main(string[] args)
        {
            // Student data
            int[] marks = { 78, 95, 45, 62, 78, 98, 88 };
            int[] registrationNumbers = { 10234, 904321, 345678, 123456, 56780 };

            Console.WriteLine("Original Data");
            Console.WriteLine("Student Marks: " + string.Join(", ", marks));
            Console.WriteLine("Registration Numbers: " + string.Join(", ", registrationNumbers));

            // Sort marks using Counting Sort
            int[] sortedMarks = CountingSort(marks);
            
            // Sort registration numbers using Radix Sort
            int[] sortedRegistrationNumbers = RadixSort(registrationNumbers);

            Console.WriteLine("\nSorted Data");
            Console.WriteLine("Sorted Student Marks (Counting Sort): " + string.Join(", ", sortedMarks));
            Console.WriteLine("Sorted Registration Numbers (Radix Sort): " + string.Join(", ", sortedRegistrationNumbers));
        }

        // Counting Sort - O(n + k) 
        static int[] CountingSort(int[] arr)
        {
            int max = arr[0];
            int min = arr[0];
            
            // Find max and min
            foreach (int num in arr)
            {
                if (num > max) max = num;
                
                if (num < min) min = num;
            }

            int range = max - min + 1;
            int[] count = new int[range];
            int[] output = new int[arr.Length];

            foreach (int num in arr)
            {
                count[num - min]++;
            }

            // Change count[i] so that it contains actual position
            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }

            // Build the output array
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                output[count[arr[i] - min] - 1] = arr[i];
                count[arr[i] - min]--;
            }

            return output;
        }

        // Radix Sort - O(d * (n+k))
        static int[] RadixSort(int[] arr)
        {
            int max = arr[0];
            
            // Find the maximum number
            foreach (int num in arr)
            {
                if (num > max) max = num;
            }

            // Do counting sort for each digit (LSD - Least Significant Digit)
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                arr = CountingSortByDigit(arr, exp);
            }

            return arr;
        }

        // Helper method for Radix Sort
        static int[] CountingSortByDigit(int[] arr, int exp)
        {
            int[] output = new int[arr.Length];
            int[] count = new int[10];

            // Store count of occurrences
            foreach (int num in arr)
            {
                count[(num / exp) % 10]++;
            }

            // Change count[i] so that it contains actual position
            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            // Build the output array
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                output[count[(arr[i] / exp) % 10] - 1] = arr[i];
                count[(arr[i] / exp) % 10]--;
            }

            return output;
        }
    }
}
