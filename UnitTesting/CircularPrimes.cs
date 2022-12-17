
using UnitTesting.Interface;

namespace UnitTesting
{
    public class CircularPrime : ICircularPrimes
    {
        public int GetCircularPrimes(int num)
        {
            var count = 0;

                for (int i = 0; i <= num; i++)
                {
                    if (CheckCircular(i))
                        count++;
                }

                return count;
        }

        public bool IsPrime(int n)
        {
            if (n <= 1)
            {
                return false;
            }
            if (n <= 3)
            {
                return true;
            }

            if (n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }

            for (int i = 5; i * i <= n; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckCircular(int n)
        {
            int count = 0, temp = n;
            while (temp > 0)
            {
                count++;
                temp /= 10;
            }

            int num = n;
            while (IsPrime(num))
            {
                int rem = num % 10;
                int div = num / 10;
                num = (int)(Math.Pow(10, count -1) * rem) + div;
                if (num == n)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
