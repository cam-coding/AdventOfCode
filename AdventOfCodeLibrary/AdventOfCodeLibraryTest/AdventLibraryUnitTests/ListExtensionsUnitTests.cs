using AdventLibrary;
using AdventLibrary.Helpers;
using System.Collections.Generic;
using Xunit;

namespace AdventLibraryUnitTests
{
    public class ListExtensionsUnitTests
    {
        [Theory]
        [MemberData(nameof(CombinationsTestData))]
        public void GetKCombinationsTest(List<int> starting, List<List<int>> expected, int length)
        {
            var result = starting.GetKCombinations(length);
            Assert.Equal(expected, result);
        }
        [Theory]
        [MemberData(nameof(PermutationsTestData))]
        public void GetPermutationsTest(List<int> starting, List<List<int>> expected)
        {
            var result = starting.GetPermutations();
            Assert.Equal(expected, result);
        }

        #region TestData
        public static IEnumerable<object[]> CombinationsTestData()
        {
            yield return new object[]
            {
                new List<int>() { 1, 2, 3},
                new List<List<int>>()
                {
                    new List<int>() { 1, 2},
                    new List<int>() { 1, 3},
                    new List<int>() { 2, 3},
                },
                2
            };
            yield return new object[]
            {
                new List<int>() { 1, 2},
                new List<List<int>>()
                {
                    new List<int>() { 1, 2},
                },
                2
            };
            yield return new object[]
            {
                new List<int>() { 1, 2, 3, 4},
                new List<List<int>>()
                {
                    new List<int>() { 1, 2, 3},
                    new List<int>() { 1, 2, 4},
                    new List<int>() { 1, 3, 4},
                    new List<int>() { 2, 3, 4},
                },
                3
            };
            yield return new object[]
            {
                new List<int>() { 1},
                new List<List<int>>()
                {
                    new List<int>() { 1},
                },
                1
            };
        }
        public static IEnumerable<object[]> PermutationsTestData()
        {
            yield return new object[]
            {
                new List<int>() { 1, 2, 3},
                new List<List<int>>()
                {
                    new List<int>() { 1, 2, 3},
                    new List<int>() { 1, 3, 2},
                    new List<int>() { 2, 1, 3},
                    new List<int>() { 2, 3, 1},
                    new List<int>() { 3, 1, 2},
                    new List<int>() { 3, 2, 1},
                },
            };
            yield return new object[]
            {
                new List<int>() { 1},
                new List<List<int>>()
                {
                    new List<int>() { 1},
                },
            };
        }
        #endregion TestData
    }
}
