using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using System.Collections.Generic;
using Xunit;

namespace AdventLibraryUnitTests.Extensions
{
    public class ListExtensionsUnitTests
    {
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t', '(', ')', '=' };

        [Theory]
        [MemberData(nameof(GetKCombinationsTestData))]
        public void GetKCombinationsTest(List<int> starting, List<List<int>> expected, int length)
        {
            var result = starting.GetCombinationsSizeN(length);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(Get0ToKCombinationsTestData))]
        public void Get0ToKCombinationsTest(List<int> starting, List<List<int>> expected, int length)
        {
            var result = starting.GetCombinationsSize0toN(length);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(PermutationsTestData))]
        public void GetPermutationsTest(List<int> starting, List<List<int>> expected)
        {
            var result = starting.GetPermutations();
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(PermutationsSizedTestData))]
        public void GetPermutationsSizedTest(List<int> starting, int size, List<List<int>> expected)
        {
            var result = starting.GetPermutationsOfSize(size);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(CombinationsWithRepetitionsData))]
        public void GetCombinationsWithRepetitionTest(List<int> list, List<List<int>> expected, int length)
        {
            var starting = list.Clone();
            var result = list.GetCombinationsSizeNWithRepetition(length);
            Assert.Equal(expected, result);
            Assert.Equal(starting, list);
        }

        [Theory]
        [MemberData(nameof(RealStringsTestData))]
        public void GetRealStringsTest(List<string> starting, List<string> expected)
        {
            var result = starting.GetRealStrings(delimiterChars);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(StringifyTestData))]
        public void StringifyTest<T>(List<T> starting, string expected)
        {
            var result = starting.Stringify();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void StringifyEdgeCasesTest()
        {
            var list = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 123 };
            var array = new int[] { 1, 2, 3 };
            Assert.Equal(list.Stringify(), array.Stringify());

            Assert.NotEqual(list.Stringify(), list2.Stringify());
        }

        [Theory]
        [MemberData(nameof(CountDifferencesTestData))]
        public void CountDifferencesTest<T>(List<T> list1, List<T> list2, int expected)
        {
            var result = list1.CountDifferences(list2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(WrappedIndexTestData))]
        public void WrappedIndex_ReturnsExpectedValue(List<int> list, int index, int expected)
        {
            Assert.Equal(expected, list.GetWrappedIndex(index));
        }

        [Theory]
        [MemberData(nameof(GetGroupsOfSizeN_TestData))]
        public void GetGroupsTest(List<int> input, List<List<int>> expected, int length, bool overlap)
        {
            var result = input.GetGroupsOfSizeN_Overlapping(length, overlap);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetKCombinationsTestData()
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

        public static IEnumerable<object[]> Get0ToKCombinationsTestData()
        {
            yield return new object[]
            {
                new List<int>() { 1, 2, 3},
                new List<List<int>>()
                {
                    new List<int>(),
                    new List<int>() { 1 },
                    new List<int>() { 2 },
                    new List<int>() { 3 },
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
                    new List<int>(),
                    new List<int>() { 1 },
                    new List<int>() { 2 },
                    new List<int>() { 1, 2},
                },
                2
            };
        }

        public static IEnumerable<object[]> GetGroupsOfSizeN_TestData()
        {
            yield return new object[]
            {
                new List<int>() { 1, 2, 3, 4, 5, 6},
                new List<List<int>>()
                {
                    new List<int>() { 1, 2},
                    new List<int>() { 3, 4},
                    new List<int>() { 5, 6},
                },
                2,
                false
            };
            yield return new object[]
            {
                new List<int>() { 1, 2, 3, 4, 5, 6, 7},
                new List<List<int>>()
                {
                    new List<int>() { 1, 2},
                    new List<int>() { 3, 4},
                    new List<int>() { 5, 6},
                },
                2,
                false
            };
            yield return new object[]
            {
                new List<int>() { 1, 2, 3, 4, 5, 6},
                new List<List<int>>()
                {
                    new List<int>() { 1, 2, 3},
                    new List<int>() { 2, 3, 4},
                    new List<int>() { 3, 4, 5},
                    new List<int>() { 4, 5, 6},
                },
                3,
                true
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

        public static IEnumerable<object[]> PermutationsSizedTestData()
        {
            yield return new object[]
            {
                new List<int>() { 1, 2, 3},
                2,
                new List<List<int>>()
                {
                    new List<int>() { 1, 2},
                    new List<int>() { 1, 3},
                    new List<int>() { 2, 1},
                    new List<int>() { 2, 3},
                    new List<int>() { 3, 1},
                    new List<int>() { 3, 2},
                },
            };
            yield return new object[]
            {
                new List<int>() { 1},
                1,
                new List<List<int>>()
                {
                    new List<int>() { 1},
                },
            };
        }

        public static IEnumerable<object[]> CombinationsWithRepetitionsData()
        {
            yield return new object[]
            {
                new List<int>() { 1, 2},
                new List<List<int>>()
                {
                    new List<int>() { 1, 1},
                    new List<int>() { 1, 2},
                    new List<int>() { 2, 2},
                },
                2
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
            yield return new object[]
            {
                new List<int>() { 1},
                new List<List<int>>()
                {
                    new List<int>() { 1, 1},
                },
                2
            };
        }

        public static IEnumerable<object[]> RealStringsTestData()
        {
            yield return new object[]
            {
                new List<string>() { "a", "a ", "", " ", "\n"},
                new List<string>() { "a", "a "},
            };

            yield return new object[]
            {
                new List<string>() { "(", ",", ":", "a"},
                new List<string>() { "a"},
            };
        }

        public static IEnumerable<object[]> StringifyTestData()
        {
            yield return new object[]
            {
                new List<int>() { 1,2,3},
                "1:2:3:",
            };

            yield return new object[]
            {
                new List<int>() { 123},
                "123:",
            };

            yield return new object[]
            {
                new List<string>() { "a", "b", "c"},
                "a:b:c:",
            };
        }

        public static IEnumerable<object[]> CountDifferencesTestData()
        {
            yield return new object[]
            {
                new List<string>() { "a", "b ", "c"},
                new List<string>() { "a", "b ", "c"},
                0,
            };

            yield return new object[]
            {
                new List<int>() { 1, 2, 3},
                new List<int>() { 1, 2, 3},
                0,
            };

            yield return new object[]
            {
                new List<int>() { 1, 2, 3},
                new List<int>() { 1, 2, 3, 4},
                -1,
            };

            yield return new object[]
            {
                new List<int>() { 1, 2, 3},
                new List<int>() { 1, 2, 4},
                1,
            };

            yield return new object[]
            {
                new List<int>() { 1, 2, 3},
                new List<int>() { 1, 5, 4},
                2,
            };

            yield return new object[]
            {
                new List<int>() { 1, 2, 3},
                new List<int>() { 6, 5, 4},
                3,
            };

            yield return new object[]
            {
                new List<int>(),
                new List<int>(),
                0,
            };
        }

        public static IEnumerable<object[]> WrappedIndexTestData()
        {
            yield return new object[]
            {
                new List<int>() { 1,2,3},
                3,
                0,
            };

            yield return new object[]
            {
                new List<int>() { 1,2,3},
                0,
                0,
            };

            yield return new object[]
            {
                new List<int>() { 1,2,3},
                4,
                1,
            };

            yield return new object[]
            {
                new List<int>() { 1,2,3},
                6,
                0,
            };

            yield return new object[]
            {
                new List<int>() { 1,2,3},
                -1,
                2,
            };

            yield return new object[]
            {
                new List<int>() { 1,2,3},
                -2,
                1,
            };

            yield return new object[]
            {
                new List<int>() { 1,2,3},
                -4,
                2,
            };
        }
    }
}