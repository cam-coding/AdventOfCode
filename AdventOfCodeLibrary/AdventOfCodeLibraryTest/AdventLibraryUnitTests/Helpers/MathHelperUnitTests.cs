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
    }
}