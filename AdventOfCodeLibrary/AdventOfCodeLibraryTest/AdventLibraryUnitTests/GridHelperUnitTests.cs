using System;
using Xunit;
using System.Collections.Generic;

namespace AdventLibraryUnitTests
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndInclusive(0, 0, 3, 3);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndInclusive(3, 3, 0, 0);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndInclusive(0, 0, 0, 3);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndInclusive(0, 3, 0, 0);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndInclusive(0, 0, 3, 3);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndInclusive(0, 3, 3, 0);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndInclusive(3, 0, 0, 3);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndInclusive(3, 3, 0, 0);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndExclusive(0, 0, 3, 0);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndExclusive(3, 0, 0, 0);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndExclusive(0, 0, 0, 3);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndExclusive(0, 0, 0, 3);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndExclusive(0, 0, 3, 3);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndExclusive(0, 3, 3, 0);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndExclusive(3, 0, 0, 3);
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

            var actual = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndExclusive(3, 3, 0, 0);
            Assert.Equal(expected, actual);
        }
        #endregion ExclusiveLine

        /*
        
        [Fact]
        public void GridHelper_RotateColumnDown_ReturnsExpected()
        {
            var expected = new int[,]{ { 7, 2, 3, 4 }, { 5, 6, 1, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } };
              
            var start = new int[,]{ { 13, 2, 3, 4 }, { 7, 6, 1, 8 }, { 5, 10, 11, 12 }, { 9, 14, 15, 16 } };

            var actual = AdventLibrary.GridHelper.RotateColumnDownWithWrap(start, 0);
            Assert.Equal(expected, actual);
        } */
    }
}