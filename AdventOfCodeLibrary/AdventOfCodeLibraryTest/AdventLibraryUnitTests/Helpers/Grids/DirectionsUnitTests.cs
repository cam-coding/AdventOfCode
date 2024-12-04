using AdventLibrary.Helpers.Grids;
using System.Collections.Generic;
using Xunit;

namespace AdventLibraryUnitTests.Helpers.Grids
{
    public class DirectionsUnitTests
    {
        [Theory]
        [MemberData(nameof(DirectionStrings))]
        public void GetDirectionByString_ReturnsExpected(string[] array, GridLocation<int> expected)
        {
            foreach (var item in array)
            {
                var result = Directions.GetDirectionByString(item);
                Assert.Equal(expected, result);
            }

        }
        public static IEnumerable<object[]> DirectionStrings()
        {
            yield return new object[]
            {
                new[] { "U", "UP", "^"},
                Directions.Up,
            };
            yield return new object[]
            {
                new[] { "R", "RIGHT", ">"},
                Directions.Right,
            };
            yield return new object[]
            {
                new[] { "D", "DOWN", "V"},
                Directions.Down,
            };
            yield return new object[]
            {
                new[] { "L", "LEFT", "<"},
                Directions.Left,
            };
            yield return new object[]
            {
                new[] { "", "BLAHBLAH"},
                null,
            };
        }
    }
}
