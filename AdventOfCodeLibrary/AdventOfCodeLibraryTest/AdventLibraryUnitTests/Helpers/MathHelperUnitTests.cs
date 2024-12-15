using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventLibrary.Helpers;
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
            var values = new List<int>() { 3,4,5 };
            var remainders = new List<int>() { 2,3,1 };
            var values2 = new List<long>() { 3, 5, 7 };
            var remainders2 = new List<long>() { 2, 3, 2 };
            Assert.Equal(11, MathHelper.ChineseRemainderTheorem(values, remainders));
            Assert.Equal(23, MathHelper.ChineseRemainderTheorem(values2, remainders2));
        }
    }
}