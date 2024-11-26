using System;
using Xunit;
using System.Collections.Generic;
using AdventLibrary;

namespace AdventLibraryUnitTests.Helpers
{
    public class GridHelperUnitTests
    {
        #region InclusiveLine
        [Fact]
        public void GridHelper_InclusiveLine_StraightForward_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0, 0),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(3, 3),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndInclusive(0, 0, 3, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_StraightForward_MatchesNewMethod()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0, 0),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(3, 3),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndInclusive(0, 0, 3, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_StraightBackward_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(3, 3),
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(0, 0),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndInclusive(3, 3, 0, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_StraightDown_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0, 0),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(0, 2),
                new Tuple<int, int>(0, 3),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndInclusive(0, 0, 0, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_StraightUp_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0, 3),
                new Tuple<int, int>(0, 2),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(0, 0),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndInclusive(0, 3, 0, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_DiagonalForwardDown_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0, 0),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(3, 3),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndInclusive(0, 0, 3, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_DiagonalForwardUp_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0, 3),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(3, 0),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndInclusive(0, 3, 3, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_DiagonalBackwardDown_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(3, 0),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(0, 3),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndInclusive(3, 0, 0, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_InclusiveLine_DiagonalBackwardUp_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(3, 3),
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(0, 0),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndInclusive(3, 3, 0, 0);
            Assert.Equal(expected, actual);
        }
        #endregion InclusiveLine
        #region ExclusiveLine
        [Fact]
        public void GridHelper_ExclusiveLine_StraightForward_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(2, 0),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndExclusive(0, 0, 3, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_Straightbackward_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(2, 0),
                new Tuple<int, int>(1, 0),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndExclusive(3, 0, 0, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_StraightDown_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(0, 2),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndExclusive(0, 0, 0, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_StraightUp_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(0, 2),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndExclusive(0, 0, 0, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_DiagonalForwardDown_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(2, 2),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndExclusive(0, 0, 3, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_DiagonalForwardUp_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(2, 1),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndExclusive(0, 3, 3, 0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_DiagonalBackwardDown_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(1, 2),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndExclusive(3, 0, 0, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_ExclusiveLine_DiagonalBackwardUp_ReturnsExpected()
        {
            var expected = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(1, 1),
            };

            var actual = GridHelperWeirdTypes.GetPointsBetweenStartAndEndExclusive(3, 3, 0, 0);
            Assert.Equal(expected, actual);
        }
        #endregion ExclusiveLine

        #region Neighbours
        /* check from top right and clockwise*/
        [Fact]
        public void GridHelper_GetAdjacent_Corner_ReturnsExpected()
        {
            var grid = GridHelper.GenerateSquareGrid(5);
            var actual = GridHelperWeirdTypes.GetOrthogonalNeighboursTuple(grid, 0, 0);
            Assert.Contains(new Tuple<int, int>(0, 1), actual);
            Assert.Contains(new Tuple<int, int>(1, 0), actual);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void GridHelper_GetAdjacent_TopEdge_ReturnsExpected()
        {
            var grid = GridHelper.GenerateSquareGrid(5);
            var actual = GridHelperWeirdTypes.GetOrthogonalNeighboursTuple(grid, 1, 0);
            Assert.Contains(new Tuple<int, int>(1, 1), actual);
            Assert.Contains(new Tuple<int, int>(2, 0), actual);
            Assert.Contains(new Tuple<int, int>(0, 0), actual);
            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public void GridHelper_GetAdjacent_SideEdge_ReturnsExpected()
        {
            var grid = GridHelper.GenerateSquareGrid(5);
            var actual = GridHelperWeirdTypes.GetOrthogonalNeighboursTuple(grid, 0, 1);
            Assert.Contains(new Tuple<int, int>(1, 1), actual);
            Assert.Contains(new Tuple<int, int>(0, 0), actual);
            Assert.Contains(new Tuple<int, int>(0, 2), actual);
            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public void GridHelper_GetAdjacent_Middle_ReturnsExpected()
        {
            var grid = GridHelper.GenerateSquareGrid(5);
            var actual = GridHelperWeirdTypes.GetOrthogonalNeighboursTuple(grid, 1, 1);
            Assert.Contains(new Tuple<int, int>(0, 1), actual);
            Assert.Contains(new Tuple<int, int>(2, 1), actual);
            Assert.Contains(new Tuple<int, int>(1, 0), actual);
            Assert.Contains(new Tuple<int, int>(1, 2), actual);
            Assert.Equal(4, actual.Count);
        }

        [Fact]
        public void GridHelper_GetOrthoginal_Corner_ReturnsExpected()
        {
            var grid = GridHelper.GenerateSquareGrid(5);
            var actual = GridHelperWeirdTypes.GetAllNeighbours(grid, 0, 0);
            Assert.Contains(new Tuple<int, int>(0, 1), actual);
            Assert.Contains(new Tuple<int, int>(1, 1), actual);
            Assert.Contains(new Tuple<int, int>(1, 0), actual);
            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public void GridHelper_GetOrthoginal_TopEdge_ReturnsExpected()
        {
            var grid = GridHelper.GenerateSquareGrid(5);
            var actual = GridHelperWeirdTypes.GetAllNeighbours(grid, 1, 0);
            Assert.Contains(new Tuple<int, int>(2, 0), actual);
            Assert.Contains(new Tuple<int, int>(2, 1), actual);
            Assert.Contains(new Tuple<int, int>(1, 1), actual);
            Assert.Contains(new Tuple<int, int>(0, 1), actual);
            Assert.Contains(new Tuple<int, int>(0, 0), actual);
            Assert.Equal(5, actual.Count);
        }

        [Fact]
        public void GridHelper_GetOrthoginal_SideEdge_ReturnsExpected()
        {
            var grid = GridHelper.GenerateSquareGrid(5);
            var actual = GridHelperWeirdTypes.GetAllNeighbours(grid, 0, 1);
            Assert.Contains(new Tuple<int, int>(1, 0), actual);
            Assert.Contains(new Tuple<int, int>(1, 1), actual);
            Assert.Contains(new Tuple<int, int>(1, 2), actual);
            Assert.Contains(new Tuple<int, int>(0, 2), actual);
            Assert.Contains(new Tuple<int, int>(0, 0), actual);
            Assert.Equal(5, actual.Count);
        }

        [Fact]
        public void GridHelper_GetOrthoginal_Middle_ReturnsExpected()
        {
            var grid = GridHelper.GenerateSquareGrid(5);
            var actual = GridHelperWeirdTypes.GetAllNeighbours(grid, 1, 1);
            Assert.Contains(new Tuple<int, int>(2, 0), actual);
            Assert.Contains(new Tuple<int, int>(2, 1), actual);
            Assert.Contains(new Tuple<int, int>(2, 2), actual);
            Assert.Contains(new Tuple<int, int>(1, 2), actual);
            Assert.Contains(new Tuple<int, int>(0, 2), actual);
            Assert.Contains(new Tuple<int, int>(0, 1), actual);
            Assert.Contains(new Tuple<int, int>(0, 0), actual);
            Assert.Contains(new Tuple<int, int>(1, 0), actual);
            Assert.Equal(8, actual.Count);
        }
        #endregion Neighbours

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
    }
}