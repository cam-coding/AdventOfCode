using AdventLibrary.Helpers;
using System.Numerics;
using Xunit;

namespace AdventLibraryUnitTests.Helpers
{
    public class MathHelperUnitTests
    {
        [Fact]
        public void GetDigitsCounts_ReturnsExpected()
        {
            for (var i = 0; i <= 10; i++)
            {
                Assert.Equal(i + 1, MathHelper.GetNumberOfDigits(Math.Pow(10, i)));
            }
        }

        [Fact]
        public void ChineseRemainderTheorem_ReturnsExpected()
        {
            var values = new List<int>() { 3, 4, 5 };
            var remainders = new List<int>() { 2, 3, 1 };
            var values2 = new List<long>() { 3, 5, 7 };
            var remainders2 = new List<long>() { 2, 3, 2 };
            Assert.Equal(11, MathHelper.ChineseRemainderTheorem(values, remainders));
            Assert.Equal(23, MathHelper.ChineseRemainderTheorem(values2, remainders2));
        }

        [Theory]
        [InlineData(0, 2, 1)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 5, 3)]
        [InlineData(1, 6, 3)]
        [InlineData(5, 1, 3)]
        [InlineData(-1, -5, -3)]
        [InlineData(-1, -6, -4)]
        [InlineData(-5, -1, -3)]
        [InlineData(-6, -1, -4)]
        [InlineData(0, -2, -1)]
        [InlineData(0, -1, -1)]
        [InlineData(-1, 0, -1)]
        public void GetMiddle_ReturnsExpected(int num1, int num2, int expected)
        {
            var result = MathHelper.GetMiddle(num1, num2);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMiddle_AllTypes_ReturnsExpected()
        {
            Assert.Equal((long)3, MathHelper.GetMiddle((long)1, (long)5));
            Assert.Equal((ulong)3, MathHelper.GetMiddle((ulong)1, (ulong)5));
            Assert.Equal((BigInteger)3, MathHelper.GetMiddle((BigInteger)1, (BigInteger)5));
            Assert.Equal((float)3.0, MathHelper.GetMiddle((float)1.0, (float)5.0));
            Assert.Equal(3.0, MathHelper.GetMiddle(1.0, 5.0));
            Assert.Equal(2.5, MathHelper.GetMiddle(1.0, 4.0));
        }

        [Theory]
        [InlineData(0, 2, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 5, 3)]
        [InlineData(1, 6, 4)]
        [InlineData(5, 1, 3)]
        [InlineData(-1, -5, -3)]
        [InlineData(-1, -6, -3)]
        [InlineData(-5, -1, -3)]
        [InlineData(-6, -1, -3)]
        [InlineData(0, -2, -1)]
        [InlineData(0, -1, 0)]
        [InlineData(-1, 0, 0)]
        public void GetMiddleRoundedUp_ReturnsExpected(int num1, int num2, int expected)
        {
            var result = MathHelper.GetMiddleRoundedUp(num1, num2);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMiddleRoundedUp_WholeNumberTypes_ReturnsExpected()
        {
            Assert.Equal((long)3, MathHelper.GetMiddleRoundedUp((long)1, (long)5));
            Assert.Equal((ulong)3, MathHelper.GetMiddleRoundedUp((ulong)1, (ulong)5));
            Assert.Equal((BigInteger)3, MathHelper.GetMiddleRoundedUp((BigInteger)1, (BigInteger)5));
        }

        [Fact]
        public void GetMiddleRoundedUp_PrecisionTypes_Throws()
        {
            Assert.Throws<NotSupportedException>(() => MathHelper.GetMiddleRoundedUp((float)1.0, (float)5.0));
            Assert.Throws<NotSupportedException>(() => MathHelper.GetMiddleRoundedUp(1.0, 5.0));
        }
    }
}