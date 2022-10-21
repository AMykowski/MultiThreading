/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        const int arrayLength = 10;

        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code
            Task<int[]> task1 = Task.Run(() => CreateArray());
            Task<int[]> task2 = task1.ContinueWith(x => MultiplyArray(task1.Result), TaskContinuationOptions.ExecuteSynchronously);
            Task<int[]> task3 = task2.ContinueWith(x => SortArray(x.Result), TaskContinuationOptions.AttachedToParent);
            task3.ContinueWith(x => ArrayAverageValue(x.Result), TaskContinuationOptions.RunContinuationsAsynchronously);

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
