/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        const int arrayLength = 10;

        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code

            Task<int[]> task1 = Task.Run(() => CreateArray());
            Task<int[]> task2 = task1.ContinueWith(x => MultiplyArray(task1.Result));
            Task<int[]> task3 = task2.ContinueWith(x => SortArray(task2.Result));
            task3.ContinueWith(x => ArrayAverageValue(task3.Result));

            Console.ReadLine();
        }

        private static int[] CreateArray()
        {
            int[] numberArray = new int[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                numberArray[i] = random.Next(20);
            }
            Console.WriteLine("Task #1 output:");
            foreach (var number in numberArray)
            {

                Console.WriteLine(number);
            }
            return numberArray;
        }

        private static int[] MultiplyArray(int[] numberArray)
        {
            for (int i = 0; i < numberArray.Length; i++)
            {
                numberArray[i] = numberArray[i] * random.Next(10);
            }

            Console.WriteLine("Task #2 output:");
            foreach (var number in numberArray)
            {

                Console.WriteLine(number);
            }
            return numberArray;
        }

        private static int[] SortArray(int[] numberArray)
        {
            Array.Sort(numberArray);
            Console.WriteLine("Task #3 output:");
            foreach (var number in numberArray)
            {

                Console.WriteLine(number);
            }
            return numberArray;
        }

        private static void ArrayAverageValue(int[] numberArray)
        {
            Console.WriteLine("Task #4 output:");
            var average = numberArray.Sum() / numberArray.Length;
            Console.WriteLine($"{average}");
        }
    }
}
