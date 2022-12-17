using UnitTesting;
namespace LeapYears.Test
{
    public class LeapYearsTest
    {
        [Theory]
        [InlineData(true, 2020)]
        [InlineData(false, 2021)]
        public void LeapYearTest(bool expectedResult, int parametr)
        {
            var leapYears = new LeapYear();
            bool result = leapYears.Leap(parametr);

            Assert.True(result == expectedResult);
        }

        [Fact]
        public void LeapYearShouldThrowExceptionIfYearIsNegative()
        {
            var leapYears = new LeapYear();

            Assert.Throws<ArgumentException>(() => leapYears.Leap(-5));
        }
    }
}