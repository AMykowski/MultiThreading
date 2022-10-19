/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {

        static readonly object lockFinished = new object();

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            List<int> numberList = new List<int>();
            
            for (int i = 0; i < 5; i++)
            {
                //List<int> numberList = new List<int>();
                Console.WriteLine($"Iteration: {i + 1}");
                Task<List<int>> addingTask = new Task<List<int>>(() => AddNumbers(numberList));
                Task printingTask = new Task(() => PrintCollection(numberList));
                addingTask.Start();
                addingTask.Wait();
                printingTask.Start();
                printingTask.Wait();
            }


            Console.ReadLine();
        }

        private static void PrintCollection(List<int> numberList)
        {
            foreach (var number in numberList)
            {
                Console.WriteLine(number);
            }
        }

        private static List<int> AddNumbers(List<int> numberList)
        {
            lock (lockFinished)
            {
                for (int i = 0; i < 10; i++)
                {
                    numberList.Add(i + 1);
                }
            }

            return numberList;
        }
    }
}
