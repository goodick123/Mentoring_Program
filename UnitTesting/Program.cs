
namespace UnitTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select value from list");
            Console.WriteLine("1. Circular primes");
            Console.WriteLine("Enter number you want to select");

            var numberKata = Console.ReadLine();

            if (numberKata == "1")
            {
                Console.WriteLine("Circular primes kata");
                Console.WriteLine("Enter number");

                var num = Console.ReadLine();
                int parseNum;

                if (int.TryParse(num, out parseNum))
                {
                    CircularPrimes primes = new();
                    Console.WriteLine("Sum of all prime numbers: " + primes.GetCircularPrimes(parseNum));
                }
                else
                {
                    Console.WriteLine("Invalid number");
                }

            }
            else
            {
                Console.WriteLine("Invalid number");

            }
        }
    }
}