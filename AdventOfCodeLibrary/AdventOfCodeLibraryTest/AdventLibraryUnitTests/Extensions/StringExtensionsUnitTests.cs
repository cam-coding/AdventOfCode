using AdventLibrary.Extensions;
using Xunit;

namespace AdventLibraryUnitTests.Extensions
{
    public class StringExtensionsUnitTests
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
        [InlineData("12345", "54321")]
        [InlineData("racecar", "racecar")]
        [InlineData("a", "a")]
        [InlineData("", "")]
        public void ReverseString_ReturnsExpected(string expected, string input)
        {
            var result = input.ReverseString();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("12345", "32145", 0, 2)]
        [InlineData("12345", "12345", 1, 1)]
        [InlineData("12345", "52341", 0, 4)]
        public void SwapCharacters_ReturnsExpected(string expected, string input, int left, int right)
        {
            var result = input.SwapCharactersAtIndexes(left, right);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('3', "12345")]
        [InlineData('3', "123456")]
        [InlineData('1', "1")]
        [InlineData('\0', "")]
        public void GetMiddleCharacter_ReturnsExpected(char expected, string input)
        {
            var result = input.GetMiddleCharacter();
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(SubStringIndexesData))]
        public void GetIndexesOfSubstringTest(string input, string substring, List<int> expected)
        {
            var result = input.GetIndexesOfSubstring(substring);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(GetIndexesOfSubstringNonOverlappingData))]
        public void GetIndexesOfSubstringNonOverlappingTest(string input, string substring, List<int> expected)
        {
            var result = input.GetIndexesOfSubstringNonOverlapping(substring);
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

        public static IEnumerable<object[]> GetIndexesOfSubstringNonOverlappingData =>
        new List<object[]>
        {
            new object[] { "aaaaaa", "aa", new List<int>() { 0, 2, 4} },
            new object[] { "aaaaaaa", "aa", new List<int>() { 0, 2, 4} },
            new object[] { "aa", "aa", new List<int>() { 0} },
            new object[] { "aabaabaa", "aa", new List<int>() { 0, 3, 6} },
            new object[] { "bcb", "aa", new List<int>() },
            new object[] { "aa", "aaa", new List<int>() },
        };
    }
}