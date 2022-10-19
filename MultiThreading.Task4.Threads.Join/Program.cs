/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{

    class Program
    {
        static SemaphoreSlim semaphore = new SemaphoreSlim(1);
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code

            var numberOfThreads = 10;

            Thread thread = new Thread(CreateThread);
            thread.Start(numberOfThreads);
            thread.Join();

            numberOfThreads = 10;

            ThreadPool.QueueUserWorkItem<int>(CreateThreadPool, numberOfThreads, false);


            Console.ReadLine();
        }

        private static void CreateThreadPool(int number)
        {
            Console.WriteLine(number);
            number -= 1;
            semaphore.Wait();
            if (number > 0)
            {
                ThreadPool.QueueUserWorkItem<int>(CreateThreadPool, number, false);
                semaphore.Release();
            }

        }

        private static void CreateThread(object number)
        {
            int passedNumber = (int)number;
            Console.WriteLine(passedNumber);
            passedNumber -= 1;
            if (passedNumber > 0)
            {
                var newThread = new Thread(CreateThread);
                newThread.Start(passedNumber);
                newThread.Join();
            }

        }
    }
}

