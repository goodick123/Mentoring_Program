using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConsoleKey input = ConsoleKey.Enter;
            while (input != ConsoleKey.Escape)
            {
                string userInput = Console.ReadLine();
                try
                {
                    char firstLetter = userInput[0];
                    Console.WriteLine(firstLetter);
                }
                catch
                {
                    Console.WriteLine("Empty string is to allowed");
                }
                Console.WriteLine("press Escape if you want to finish");
                input = Console.ReadKey().Key;
            }
        }
    }
}