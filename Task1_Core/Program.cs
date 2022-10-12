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
            Console.WriteLine(Lib.OutPutLogic(userName));
        }
    }
}
