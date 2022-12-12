
namespace UnitTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select value from list");
            Console.WriteLine("1. Circular primes");
            Console.WriteLine("2. Calc Stats");
            Console.WriteLine("3. Leap Year");
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
                    Console.WriteLine("Sum of all prime numbers: " + CircularPrimes.GetCircularPrimes(parseNum));
                }
                else
                {
                    Console.WriteLine("Invalid number");
                }

            }
            else if (numberKata == "2")
            {

                Console.WriteLine("Calc Stats Kata");
                Console.WriteLine("Enter number of digits");

                var countDigits = Console.ReadLine();

                if (int.TryParse(countDigits, out int parseCount))
                {
                    int[] numbers = new int[parseCount];
                    for (int j = 0; j < parseCount; j++)
                    {
                        Console.WriteLine("Enter number");
                        numbers[j] = Convert.ToInt32(Console.ReadLine());
                    }

                    Console.WriteLine("Minimum value: " + CalcStats.CalcStatsData(numbers)[0]);
                    Console.WriteLine("Maximum value: " + CalcStats.CalcStatsData(numbers)[1]);
                    Console.WriteLine("Number of elements in the sequence: " + CalcStats.CalcStatsData(numbers)[2]);
                    Console.WriteLine("Average value: " + CalcStats.CalcStatsData(numbers)[3]);

                }
                else
                {
                    Console.WriteLine("Invalid number");
                }
            }
            else if (numberKata == "3")
            {
                Console.WriteLine("Leap Year Kata");
                Console.WriteLine("Enter Year");
                var year = Console.ReadLine();
                int parseYear;
                if (int.TryParse(year, out parseYear))
                {
                    var result = LeapYears.Leap(parseYear) ? "Leap year" : "Not a leap year";

                    Console.WriteLine(result);
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