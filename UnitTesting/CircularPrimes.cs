
namespace UnitTesting
{
    public class CircularPrimes
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

        private bool IsPrime(int n)
        {
            if (n <= 1)
                return false;
            if (n <= 3)
                return true;

            if (n % 2 == 0 || n % 3 == 0)
                return false;

            for (int i = 5; i * i <= n; i = i + 6)
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;

            return true;
        }
        
        private bool CheckCircular(int N)
        {
            int count = 0, temp = N;
            while (temp > 0)
            {
                count++;
                temp /= 10;
            }

            int num = N;
            while (IsPrime(num))
            {
                int rem = num % 10;
                int div = num / 10;
                num = (int)(Math.Pow(10, count -1) * rem) + div;
                if (num == N)
                    return true;
            }

            return false;
        }
    }
}
