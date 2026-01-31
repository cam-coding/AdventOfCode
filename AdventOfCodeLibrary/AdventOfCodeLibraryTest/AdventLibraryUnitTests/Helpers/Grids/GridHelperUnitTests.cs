using AdventLibrary;
using AdventLibrary.Helpers.Grids;
using Xunit;

namespace AdventLibraryUnitTests.Helpers.Grids
{
    public class GridHelperUnitTests
    {
        [Fact]
        public void GridHelper_InclusiveLine_StraightForward_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(0, 0),
                new GridLocation<int>(1, 1),
                new GridLocation<int>(2, 2),
                new GridLocation<int>(3, 3),
            };

            var actual = GridHelper.GetPointsBetween(0, 0, 3, 3, true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_StraightForward_MatchesNewMethod()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(0, 0),
                new GridLocation<int>(1, 1),
                new GridLocation<int>(2, 2),
                new GridLocation<int>(3, 3),
            };

            var actual = GridHelper.GetPointsBetween(0, 0, 3, 3, true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_StraightBackward_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(3, 3),
                new GridLocation<int>(2, 2),
                new GridLocation<int>(1, 1),
                new GridLocation<int>(0, 0),
            };

            var actual = GridHelper.GetPointsBetween(3, 3, 0, 0, true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_StraightDown_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(0, 0),
                new GridLocation<int>(0, 1),
                new GridLocation<int>(0, 2),
                new GridLocation<int>(0, 3),
            };

            var actual = GridHelper.GetPointsBetween(0, 0, 0, 3, true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_StraightUp_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(0, 3),
                new GridLocation<int>(0, 2),
                new GridLocation<int>(0, 1),
                new GridLocation<int>(0, 0),
            };

            var actual = GridHelper.GetPointsBetween(0, 3, 0, 0, true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_DiagonalForwardDown_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(0, 0),
                new GridLocation<int>(1, 1),
                new GridLocation<int>(2, 2),
                new GridLocation<int>(3, 3),
            };

            var actual = GridHelper.GetPointsBetween(0, 0, 3, 3, true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_DiagonalForwardUp_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(0, 3),
                new GridLocation<int>(1, 2),
                new GridLocation<int>(2, 1),
                new GridLocation<int>(3, 0),
            };

            var actual = GridHelper.GetPointsBetween(0, 3, 3, 0, true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_DiagonalBackwardDown_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(3, 0),
                new GridLocation<int>(2, 1),
                new GridLocation<int>(1, 2),
                new GridLocation<int>(0, 3),
            };

            var actual = GridHelper.GetPointsBetween(3, 0, 0, 3, true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_DiagonalBackwardUp_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(3, 3),
                new GridLocation<int>(2, 2),
                new GridLocation<int>(1, 1),
                new GridLocation<int>(0, 0),
            };

            var actual = GridHelper.GetPointsBetween(3, 3, 0, 0, true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_StraightForward_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(1, 0),
                new GridLocation<int>(2, 0),
            };

            var actual = GridHelper.GetPointsBetween(0, 0, 3, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_Straightbackward_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(2, 0),
                new GridLocation<int>(1, 0),
            };

            var actual = GridHelper.GetPointsBetween(3, 0, 0, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_StraightDown_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(0, 1),
                new GridLocation<int>(0, 2),
            };

            var actual = GridHelper.GetPointsBetween(0, 0, 0, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_StraightUp_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(0, 1),
                new GridLocation<int>(0, 2),
            };

            var actual = GridHelper.GetPointsBetween(0, 0, 0, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_DiagonalForwardDown_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(1, 1),
                new GridLocation<int>(2, 2),
            };

            var actual = GridHelper.GetPointsBetween(0, 0, 3, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_DiagonalForwardUp_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(1, 2),
                new GridLocation<int>(2, 1),
            };

            var actual = GridHelper.GetPointsBetween(0, 3, 3, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_DiagonalBackwardDown_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(2, 1),
                new GridLocation<int>(1, 2),
            };

            var actual = GridHelper.GetPointsBetween(3, 0, 0, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_DiagonalBackwardUp_ReturnsExpected()
        {
            var expected = new List<GridLocation<int>>()
            {
                new GridLocation<int>(2, 2),
                new GridLocation<int>(1, 1),
            };

            var actual = GridHelper.GetPointsBetween(3, 3, 0, 0);
            Assert.Equal(expected, actual);
        }

        private List<List<int>> GenerateEmptyGrid()
        {
            var listy = new List<List<int>>();
            listy.Add(new List<int> { 0, 0, 0, 0 });
            listy.Add(new List<int> { 0, 0, 0, 0 });
            listy.Add(new List<int> { 0, 0, 0, 0 });
            listy.Add(new List<int> { 0, 0, 0, 0 });

            return listy;
        }

        [Fact]
        public void GridHelper_RotateColumnDown_ReturnsExpected()
        {
            var start = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };

            var expected = new int[,] { { 9, 2, 3, 4 }, { 1, 6, 7, 8 }, { 5, 10, 11, 12 } };

            var actual = GridHelperWeirdTypes.RotateColumnDownWithWrap(start, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateColumnUp_ReturnsExpected()
        {
            var start = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };

            var expected = new int[,] { { 5, 2, 3, 4 }, { 9, 6, 7, 8 }, { 1, 10, 11, 12 } };

            var actual = GridHelperWeirdTypes.RotateColumnUpWithWrap(start, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateGridDown_ReturnsExpected()
        {
            var start = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };

            var expected = new int[,] { { 9, 10, 11, 12 }, { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };

            var actual = GridHelperWeirdTypes.RotateGridDownWithWrap(start);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateGridUp_ReturnsExpected()
        {
            var start = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };

            var expected = new int[,] { { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 1, 2, 3, 4 } };

            var actual = GridHelperWeirdTypes.RotateGridUpWithWrap(start);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateRowRight_ReturnsExpected()
        {
            var start = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };

            var expected = new int[,] { { 4, 1, 2, 3 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };

            var actual = GridHelperWeirdTypes.RotateRowRightWithWrap(start, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateRowLeft_ReturnsExpected()
        {
            var start = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };

            var expected = new int[,] { { 2, 3, 4, 1 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };

            var actual = GridHelperWeirdTypes.RotateRowLeftWithWrap(start, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_GetColumnsWithInts_ReturnsExpected()
        {
            var start = new List<List<int>>()
            {
                new List<int>(){ 1,2,3 },
                new List<int>(){ 1,2,3 },
                new List<int>(){ 1,2,3 },
            };
            var expected = new List<List<int>>()
            {
                new List<int>(){ 1,1,1 },
                new List<int>(){ 2,2,2 },
                new List<int>(){ 3,3,3 },
            };
            var actual = start.GetColumns();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_GetColumnsWithChars_ReturnsExpected()
        {
            var start = new List<List<char>>()
            {
                new List<char>(){ '1','2','3' },
                new List<char>(){ '1','2','3' },
                new List<char>(){ '1','2','3' },
            };
            var expected = new List<List<char>>()
            {
                new List<char>(){ '1','1','1' },
                new List<char>(){ '2','2','2' },
                new List<char>(){ '3','3','3' },
            };
            var actual = start.GetColumns();
            Assert.Equal(expected, actual);
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