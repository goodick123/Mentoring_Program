
using UnitTesting.Dto;

namespace UnitTesting.Interface
{
    public interface ICalcStats
    {
        CalcStatsDto CalcStatsData(int[] input);

        int MinNumber(int[] input);

        int MaxNumber(int[] input);

        int CountNumber(int[] input);

        int AverageNumber(int[] input);

    }
}
