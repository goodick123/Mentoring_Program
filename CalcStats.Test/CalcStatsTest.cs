using UnitTesting;
using UnitTesting.Dto;

namespace CalcStats.Test
{
    public class CalcStatsTest
    {
        public static IEnumerable<object[]> GetDataForCalcStatsTest()
        {
            yield return new object[] { new CalcStatsDto { MinNumber = 10, MaxNumber = 40, CountNumber = 4, AverageNumber = 25 }, new int[] { 10, 20, 30, 40 } };
            yield return new object[] { new CalcStatsDto { MinNumber = 100, MaxNumber = 500, CountNumber = 5, AverageNumber = 300 }, new int[] { 100, 200, 300, 400, 500 } };
        }

        [Theory]
        [MemberData(nameof(GetDataForCalcStatsTest))]
        public void CalcStatTest(CalcStatsDto expectedResult, int[] parametr)
        {
            var calcStats = new CalcStat();
            var result = calcStats.CalcStatsData(parametr);

            Assert.Equal(result.MinNumber , expectedResult.MinNumber);
            Assert.Equal(result.MaxNumber, expectedResult.MaxNumber);
            Assert.Equal(result.CountNumber, expectedResult.CountNumber);
            Assert.Equal(result.AverageNumber, expectedResult.AverageNumber);
        }

        [Fact]
        public void CalcStatShouldThrowExceptionWithEmptyArray()
        {
            var arrange = new int[] { };

            var calcStats = new CalcStat();

            Assert.Throws<ArgumentException>(() => calcStats.CalcStatsData(arrange));

        }

    }
}