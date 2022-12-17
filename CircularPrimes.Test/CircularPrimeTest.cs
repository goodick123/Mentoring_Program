using UnitTesting;

namespace CircularPrimes.Test
{
    public class CircularPrimeTest
    {
        [Theory]
        [InlineData(0,1)]
        [InlineData(4, 10)]
        [InlineData(13, 100)]
        public void CircularPrimesTest(int expectedResult, int parametr)
        {
            var circularPrimes = new CircularPrime();
            int result = circularPrimes.GetCircularPrimes(parametr);

            Assert.True(result == expectedResult);
        }
    }
}