using AdventLibrary;
using AdventLibrary.Helpers;
using System.Collections.Generic;
using Xunit;

namespace AdventLibraryUnitTests
{
    public class ListExtensionsUnitTests
    {
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t', '(', ')', '=' };
        [Theory]
        [MemberData(nameof(GetKCombinationsTestData))]
        public void GetKCombinationsTest(List<int> starting, List<List<int>> expected, int length)
        {
            var result = starting.GetKCombinations(length);
            Assert.Equal(expected, result);
        }
        [Theory]
        [MemberData(nameof(Get0ToKCombinationsTestData))]
        public void Get0ToKCombinationsTest(List<int> starting, List<List<int>> expected, int length)
        {
            var result = starting.Get0toKCombinations(length);
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
        [MemberData(nameof(CombinationsWithRepetitionsData))]
        public void GetCombinationsWithRepetitionTest(List<int> starting, List<List<int>> expected, int length)
        {
            var result = starting.GenerateCombinationsWithRepetition(length);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(RealStringsTestData))]
        public void GetRealStringsTest(List<string> starting, List<string> expected)
        {
            var result = starting.OnlyRealStrings(delimiterChars);
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
            var array = new int[] { 1 ,2, 3 };
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

        #region TestData
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
        #endregion TestData
    }
}
