using UnitTesting;
using Xunit;

namespace xUnitTests
{
    public class KataTests
    {
        public static IEnumerable<object[]> GetDataForCircularPrimesTest()
        {
            yield return new object[] { 0, 1 };
            yield return new object[] { 4, 10 };
            yield return new object[] { 13, 100 };
            yield return new object[] { 25, 1000 };
            yield return new object[] { 33, 10000 };
            yield return new object[] { 43, 100000 };
            yield return new object[] { 55, 1000000 };
        }

        public static IEnumerable<object[]> GetDataForLeapYearsTest()
        {
            yield return new object[] { true, 2020 };
            yield return new object[] { false, 2021 };
        }

        public static IEnumerable<object[]> GetDataForCalcStatsTest()
        {
            yield return new object[] { new int[] {10,40,4,25}, new int[] { 10, 20, 30, 40 } };
            yield return new object[] { new int[] { 100, 500, 5, 300 }, new int[] { 100, 200, 300, 400, 500 } };
        }

        [Theory]
        [MemberData(nameof(GetDataForCircularPrimesTest))]
        public void CircularPrimesTest(int expectedResult, int parametr)
        {
            int result = CircularPrimes.GetCircularPrimes(parametr);

            Assert.True(result == expectedResult);
        }

        [Theory]
        [MemberData(nameof(GetDataForLeapYearsTest))]
        public void LeapYearTest(bool expectedResult, int parametr)
        {
            bool result = LeapYears.Leap(parametr);

            Assert.True(result == expectedResult);
        }

        [Theory]
        [MemberData(nameof(GetDataForCalcStatsTest))]
        public void CalcStatTest(int[] expectedResult, int[] parametr)
        {
            var result = CalcStats.CalcStatsData(parametr);
            
            Assert.True(result.SequenceEqual(expectedResult));
        }
    }
}