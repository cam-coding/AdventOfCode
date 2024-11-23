using AdventLibrary.Extensions;
using System.Collections.Generic;
using Xunit;

namespace AdventLibraryUnitTests
{
    public class StringHelperUnitTests
    {
        [Theory]
        [InlineData(3, 2, false, "aabbcc")]
        [InlineData(3, 2, false, "aabbaa")]
        [InlineData(6, 1, false, "aaaaaa")]
        [InlineData(3, 2, false, "aaaaaa")]
        [InlineData(2, 3, false, "aaaaaa")]
        [InlineData(1, 4, false, "aaaaaa")]
        [InlineData(1, 2, true, "aaaa")]
        [InlineData(1, 2, true, "abaa")]
        public void GetGroups_ReturnsExpected(int expected, int size, bool unique, string input)
        {
            var result = StringExtensions.CountGroups_NonOverlapping(input, size, unique);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(SubStringIndexesData))]
        public void GetIndexesOfSubstringTest(string input, string substring, List<int> expected)
        {
            var result = input.GetIndexesOfSubstring(substring);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> SubStringIndexesData =>
        new List<object[]>
        {
            new object[] { "aabaa", "aa", new List<int>() { 0, 3} },
            new object[] { "aa", "aa", new List<int>() { 0} },
            new object[] { "aabaabaa", "aa", new List<int>() { 0, 3, 6} },
            new object[] { "bcb", "aa", new List<int>() },
            new object[] { "aaaaaa", "aa", new List<int>() { 0, 1, 2, 3, 4} },
        };
    }
}
