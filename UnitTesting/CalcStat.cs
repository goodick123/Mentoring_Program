
using UnitTesting.Dto;
using UnitTesting.Interface;

namespace UnitTesting
{
    public class CalcStat : ICalcStats
    {
        public CalcStatsDto CalcStatsData(int[] input)
        {
            if(input.Length == 0)
            {
                throw new ArgumentException();
            }

            var result = new CalcStatsDto() { MinNumber = MinNumber(input), MaxNumber = MaxNumber(input), 
                CountNumber = CountNumber(input), AverageNumber = AverageNumber(input) };

            return result;
        }

        public int MinNumber(int[] input)
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

        public int MaxNumber(int[] input)
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

        public int CountNumber(int[] input)
        {
            var list = input.ToList();

            int result = 0;

            foreach (var item in list)
            {
                result++;
            }

            return result;
        }

        public int AverageNumber(int[] input)
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
