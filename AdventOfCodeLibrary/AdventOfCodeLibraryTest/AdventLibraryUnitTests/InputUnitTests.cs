using System;
using Xunit;
using System.Collections.Generic;

namespace AdventLibraryUnitTests
{
    public class InputUnitTests
    {
        [Theory]
        [InlineData("1,2,3", ",")]
        public void TokenizeAndParse_HandlesCommas(string input, string seperator)
        {
            var expected = new List<int> { 1, 2, 3 };
            var result = AdventLibrary.ParseInput.TokenizeAndParseIntoList<int>(input, seperator);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TokenizeAndParse_HandlesNewLine()
        {
            var input = "1\r\n2\r\n3";
            var expected = new List<int> { 1, 2, 3 };
            var result = AdventLibrary.ParseInput.TokenizeAndParseIntoList<int>(input, Environment.NewLine);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TokenizeAndParse_HandleNewlineFile()
        {
            var input = AdventLibrary.ParseInput.GetText("..\\..\\..\\TestData\\2020day1short.txt");
            var expected = new List<int> { 1721, 979, 366, 299, 675, 1456 };
            var result = AdventLibrary.ParseInput.TokenizeAndParseIntoList<int>(input, Environment.NewLine);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void HandleNewlineFile()
        {
            var input = AdventLibrary.ParseInput.GetText("..\\..\\..\\TestData\\2020day1short.txt");
            var expected = new List<int> { 1721, 979, 366, 299, 675, 1456 };
            var result = AdventLibrary.ParseInput.ParseLinesAsType<int>(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void HandleCommaFile()
        {
            var input = AdventLibrary.ParseInput.GetText("..\\..\\..\\TestData\\2020day1shortCommas.txt");
            var expected = new List<int> { 1721, 979, 366, 299, 675, 1456 };
            var result = AdventLibrary.ParseInput.ParseCommaSeperatedAsType<int>(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void HandleCommasAndNewLinesFile()
        {
            var input = AdventLibrary.ParseInput.GetLines("..\\..\\..\\TestData\\2020day1shortCommasNewLines.txt");
            var expected = new List<int> { 1721, 979, 366, 299, 675, 1456 };

            List<int> result = new List<int>();
            foreach (var line in input)
            {
                result.AddRange(AdventLibrary.ParseInput.ParseCommaSeperatedAsType<int>(line));
            }

            Assert.Equal(expected, result);
        }
    }
}
