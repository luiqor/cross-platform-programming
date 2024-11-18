using System;

using Lab3;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab3.Runner is running successfully...");

            string inputFilePath = Path.Combine("../input.txt");
            string outputFilePath = Path.Combine("../output.txt");

            int bestTimeToPrincess = Lab3.Program.ProcessData(inputFilePath);
            File.WriteAllText(outputFilePath, bestTimeToPrincess.ToString());
            Console.WriteLine("Lab3.Runner has finished successfully...");
            Console.WriteLine($"Best time to princess: {bestTimeToPrincess}");
        }
    }
}