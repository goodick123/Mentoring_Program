
namespace UnitTesting
{
    public static class CalcStats
    {
        public static int[] CalcStatsData(int[] input)
        {
            var result = new int[4];
            result[0] = MinNumber(input);
            result[1] = MaxNumber(input);
            result[2] = CountNumber(input);
            result[3] = AverageNumber(input);

            return result;
        }

        private static int MinNumber(int[] input)
        {
            var list = input.ToList();

            int? result = null;

            foreach (var item in list)
            {
                if (result == null)
                {
                    result = item;
                }
                else
                {
                    if (item < result)
                    {
                        result = item;
                    }
                }
            }

            return result.Value;
        }

        private static int MaxNumber(int[] input)
        {
            var list = input.ToList();

            int? result = null;

            foreach (var item in list)
            {
                if (result == null)
                {
                    result = item;
                }
                else
                {
                    if (item > result)
                    {
                        result = item;
                    }
                }
            }

            return result.Value;
        }

        private static int CountNumber(int[] input)
        {
            var list = input.ToList();

            int result = 0;

            foreach (var item in list)
            {
                result++;
            }

            return result;
        }

        private static int AverageNumber(int[] input)
        {
            var list = input.ToList();

            int result = 0;

            foreach (var item in list)
            {
                result += item;
            }

            result /= CountNumber(input);

            return result;
        }
    }
}
