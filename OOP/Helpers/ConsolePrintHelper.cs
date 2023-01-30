
namespace OOP.Helpers
{
    internal class ConsolePrintHelper
    {
        public string? PrintStart()
        {
            Console.WriteLine("Enter type:");
            var type = Console.ReadLine();
            return type;
        }

        public char PrintId()
        {
            Console.WriteLine("Id :");

            var idChoice = Console.ReadKey(true).KeyChar;

            return idChoice;
        }
        public char PrintExit(object result)
        {
            Console.WriteLine(result);

            Console.WriteLine("Enter 1 if you want to exit:");
            var exit = Console.ReadKey(true).KeyChar;
            return exit;
        }
    }
}
