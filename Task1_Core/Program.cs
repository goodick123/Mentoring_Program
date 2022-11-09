using System;
using Task1_Lib;

namespace Task1_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter username:");
            string userName = Console.ReadLine();
            try
            {
                Console.WriteLine($"First letter of name is : {userName[0]}");
                Console.WriteLine(Lib.OutPutLogic(userName));
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("String is empty", e.Message);
            }
        }
    }
}
