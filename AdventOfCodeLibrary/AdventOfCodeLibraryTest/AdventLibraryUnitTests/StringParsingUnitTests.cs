using System;
using Xunit;
using System.Collections.Generic;

namespace AdventLibraryUnitTests
{
    public class StringParsingUnitTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void GetNumbersFromString_ReturnsExpected(List<int> expected, string input)
        {
            var result = AdventLibrary.StringParsing.GetIntsFromString(input);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { new List<int>() { 1, 2, 3, 4}, "1_2+3 4" },
            new object[] { new List<int>() { 10, 20, 30, 40}, "10_20+30 40" },
            new object[] { new List<int>() { 432, 708, 432, 160}, "432,708 -> 432,160" },
            new object[] { new List<int>() { 579, 594, 579, 448}, "579,594 -> 579,448" },
            new object[] { new List<int>() { 10, 10, 20, 20}, "10:10, 20:20" },
        };
    }
}
