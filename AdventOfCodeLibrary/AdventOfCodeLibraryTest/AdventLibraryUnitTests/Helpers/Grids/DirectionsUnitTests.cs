using AdventLibrary.Helpers.Grids;
using System.Collections.Generic;
using Xunit;

namespace AdventLibraryUnitTests.Helpers.Grids
{
    public class DirectionsUnitTests
    {
        [Theory]
        [MemberData(nameof(DirectionStrings_TestData))]
        public void GetDirectionByString_ReturnsExpected(string[] array, GridLocation<int> expected)
        {
            foreach (var item in array)
            {
                var result = Directions.GetDirectionByString(item);
                Assert.Equal(expected, result);
            }
        }

        [Fact]
        public void Turn_ReturnsExpected()
        {
            var current = Directions.Up;
            Assert.Equal(Directions.Left, Directions.TurnleftOrthogonal(current));
            Assert.Equal(Directions.UpLeft, Directions.TurnleftAll(current));
            current = Directions.UpRight;
            Assert.Equal(Directions.UpLeft, Directions.TurnleftDiagonal(current));

            current = Directions.Left;
            Assert.Equal(Directions.Up, Directions.TurnRightOrthogonal(current));
            current = Directions.UpLeft;
            Assert.Equal(Directions.UpRight, Directions.TurnRightDiagonal(current));
            Assert.Equal(Directions.Up, Directions.TurnRightAll(current));
        }

        public static IEnumerable<object[]> DirectionStrings_TestData()
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
