using System;

class ArrayProgram
{
   public static void Main()
    {
       //staic array
        int[] a={1,2,3,4,5};
        for(int i=0;i<a.Length;i++)
        {
            Console.WriteLine(i+":"+a[i]);
        }
        //dynamic array
        int n;
        Console.WriteLine("Enter the size of array:");
        n=Convert.ToInt32(Console.ReadLine());
        int[] b=new int[n];
        for(int j=0;j<b.Length;j++)
        {
            Console.WriteLine("Enter the element at index "+j+":");
            b[j]=Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine("The elements in the array are:");
        for(int k=0;k<b.Length;k++)
        {
            Console.WriteLine(k+":"+b[k]);
        }

    }
}
