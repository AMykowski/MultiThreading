/*
 * 3. Write a program, which multiplies two matrices and uses class Parallel.
 * a. Implement logic of MatricesMultiplierParallel.cs
 *    Make sure that all the tests within MultiThreading.Task3.MatrixMultiplier.Tests.csproj run successfully.
 * b. Create a test inside MultiThreading.Task3.MatrixMultiplier.Tests.csproj to check which multiplier runs faster.
 *    Find out the size which makes parallel multiplication more effective than the regular one.
 */

using System;
using System.Threading.Tasks;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3.	Write a program, which multiplies two matrices and uses class Parallel. ");
            Console.WriteLine();

            const byte matrixSize = 7; // todo: use any number you like or enter from console
            const byte matrixSizeParallel = 9;

            Task[] taskArray = new Task[2];
            taskArray[0] = new Task(() => CreateAndProcessMatrices(matrixSize));
            taskArray[1] = new Task(() => CreateAndProcessMatricesParallel(matrixSizeParallel));

            Parallel.ForEach(taskArray, task =>
            {
                task.Start();
            });

            Console.ReadLine();
        }

        private static void CreateAndProcessMatricesParallel(byte sizeOfMatrix)
        {
            Console.WriteLine("Multiplying...");
            var firstMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix);
            var secondMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix);
            IMatrix resultMatrix = new MatricesMultiplierParallel().Multiply(firstMatrix, secondMatrix);
            PrintMatrixes(firstMatrix, secondMatrix, resultMatrix);
        }

        private static void CreateAndProcessMatrices(byte sizeOfMatrix)
        {
            Console.WriteLine("Multiplying...");
            var firstMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix);
            var secondMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix);
            IMatrix resultMatrix = new MatricesMultiplier().Multiply(firstMatrix, secondMatrix);
            PrintMatrixes(firstMatrix, secondMatrix, resultMatrix);
        }

        private static void PrintMatrixes(Matrix firstMatrix, Matrix secondMatrix, IMatrix resultMatrix)
        {
            Console.WriteLine("firstMatrix:");
            firstMatrix.Print();
            Console.WriteLine("secondMatrix:");
            secondMatrix.Print();
            Console.WriteLine("resultMatrix:");
            resultMatrix.Print();
        }
    }
}
