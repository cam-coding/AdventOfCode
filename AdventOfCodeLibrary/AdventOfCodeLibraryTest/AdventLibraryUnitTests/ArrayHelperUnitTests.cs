using AdventLibrary.Helpers;
using System.Collections.Generic;
using Xunit;

namespace AdventLibraryUnitTests
{
    public class ArrayHelperUnitTests
    {
        [Theory]
        [MemberData(nameof(RotateLeftTestData))]
        public void RotateLeft(int[] array, int[] expected, int n)
        {
            var result = ArrayHelper.RotateArrayLeft(array, n);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(RotateRightTestData))]
        public void RotateRight(int[] array, int[] expected, int n)
        {
            var result = ArrayHelper.RotateArrayRight(array, n);

            Assert.Equal(expected, result);
        }

        #region TestData
        public static IEnumerable<object[]> RotateLeftTestData()
        {
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 2, 3, 4, 1},
                1 
            };
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 3, 4, 1, 2},
                2
            };
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 4, 1, 2, 3},
                3
            };
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 1, 2, 3, 4},
                4
            };
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 2, 3, 4, 1},
                5
            };
        }

        public static IEnumerable<object[]> RotateRightTestData()
        {
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 4, 1, 2, 3},
                1
            };
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 3, 4, 1, 2},
                2
            };
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 2, 3, 4, 1},
                3
            };
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 1, 2, 3, 4},
                4
            };
            yield return new object[]
            {
                new[] { 1, 2, 3, 4},
                new[] { 4, 1, 2, 3},
                5
            };
        }
        #endregion
    }
}
