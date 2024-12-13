using AdventLibrary;
using AdventLibrary.Helpers.Grids;
using AdventLibrary.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventLibraryUnitTests.Helpers.Grids
{
    public class GridObjectUnitTests
    {
        private GridObject<int> _grid;

        public GridObjectUnitTests()
        {
            _grid = new GridObject<int>(GenerateEmptyGrid());
        }

        private List<List<int>> GenerateEmptyGrid()
        {
            var listy = new List<List<int>>();
            listy.Add(new List<int> { 1, 1, 1, 1 });
            listy.Add(new List<int> { 1, 1, 1, 1 });
            listy.Add(new List<int> { 1, 1, 1, 1 });
            listy.Add(new List<int> { 1, 1, 1, 1 });

            return listy;
        }

        [Theory]
        [InlineData(0, 0, true, 1)]
        [InlineData(3, 3, true, 1)]
        [InlineData(-1, 0, false, 0)]
        [InlineData(0, -1, false, 0)]
        [InlineData(-1, -1, false, 0)]
        [InlineData(4, 3, false, 0)]
        [InlineData(3, 4, false, 0)]
        [InlineData(4, 4, false, 0)]
        public void TryGet_ReturnsExpected(int x, int y, bool expectedReturn, int expectedValue)
        {
            int actualValue;
            var location = new GridLocation<int>(x, y);
            bool actualReturn = _grid.TryGet(out actualValue, location);
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void GridObject_RotateRight_GridAsExpected()
        {
            var input = new List<List<int>>()
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 4, 5, 6 },
                new List<int> { 7, 8, 9 }
            };
            var expected = new List<List<int>>()
            {
                new List<int> { 3, 1, 2 },
                new List<int> { 6, 4, 5 },
                new List<int> { 9, 7, 8 }
            };
            var grid = new GridObject<int>(input);
            grid.RotateGridRightWithWrap();
            Assert.True(GridsEqual(expected, grid.Grid));
        }

        [Fact]
        public void GridObject_RotateLeft_GridAsExpected()
        {
            var input = new List<List<int>>()
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 4, 5, 6 },
                new List<int> { 7, 8, 9 }
            };
            var expected = new List<List<int>>()
            {
                new List<int> { 2, 3, 1 },
                new List<int> { 5, 6, 4 },
                new List<int> { 8, 9, 7 }
            };
            var grid = new GridObject<int>(input);
            grid.RotateGridLeftWithWrap();
            Assert.True(GridsEqual(expected, grid.Grid));
        }

        [Fact]
        public void GridObject_RotateUp_GridAsExpected()
        {
            var input = new List<List<int>>()
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 4, 5, 6 },
                new List<int> { 7, 8, 9 }
            };
            var expected = new List<List<int>>()
            {
                new List<int> { 4, 5, 6 },
                new List<int> { 7, 8, 9 },
                new List<int> { 1, 2, 3 }
            };
            var grid = new GridObject<int>(input);
            grid.RotateGridUpWithWrap();
            Assert.True(GridsEqual(expected, grid.Grid));
        }

        [Fact]
        public void GridObject_RotateDown_GridAsExpected()
        {
            var input = new List<List<int>>()
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 4, 5, 6 },
                new List<int> { 7, 8, 9 }
            };
            var expected = new List<List<int>>()
            {
                new List<int> { 7, 8, 9 },
                new List<int> { 1, 2, 3 },
                new List<int> { 4, 5, 6 }
            };
            var grid = new GridObject<int>(input);
            grid.RotateGridDownWithWrap();
            Assert.True(GridsEqual(expected, grid.Grid));
        }

        private bool GridsEqual<T>(List<List<T>> gridA, List<List<T>> gridB)
        {
            if (gridA.Count != gridB.Count)
            {
                return false;
            }
            for (int i = 0; i < gridA.Count; i++)
            {
                if (!gridA[i].SequenceEqual(gridB[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}