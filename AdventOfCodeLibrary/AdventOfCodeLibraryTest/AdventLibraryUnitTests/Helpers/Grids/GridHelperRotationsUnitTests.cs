using AdventLibrary.Extensions;
using AdventLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventLibraryUnitTests.Helpers.Grids
{
    public class GridHelperRotationsUnitTests
    {

        [Fact]
        public void Grid_RotateAllRowsRight_GridAsExpected()
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
            GridHelper.RotateAllRowsRightWithWrap(input);
            Assert.Equal(expected, input);
        }

        [Fact]
        public void Grid_RotateAllRowsLeft_GridAsExpected()
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
            GridHelper.RotateAllRowsLeftWithWrap(input);
            Assert.Equal(expected, input);
        }

        [Fact]
        public void Grid_RotateUp_GridAsExpected()
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
            GridHelper.RotateAllColumnsUpWithWrap(input);
            Assert.Equal(expected, input);
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
            GridHelper.RotateAllColumnsDownWithWrap(input);
            Assert.Equal(expected, input);
        }

        [Fact]
        public void GridHelper_Transpose_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1, 2, 3},
                new List<int>(){ 4, 5, 6},
                new List<int>(){ 7, 8, 9}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 1, 4, 7},
                new List<int>(){ 2, 5, 8},
                new List<int>(){ 3, 6, 9}
            };

            var actual = GridHelper.TransposeGrid<int>(start);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_Transpose_RectangleGrid_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1,  2,  3,  4},
                new List<int>(){ 5,  6,  7,  8},
                new List<int>(){ 9, 10, 11, 12}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 1, 5, 9},
                new List<int>(){ 2, 6, 10},
                new List<int>(){ 3, 7, 11},
                new List<int>(){ 4, 8, 12}
            };

            var actual = GridHelper.TransposeGrid<int>(start);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateRight_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1, 2, 3},
                new List<int>(){ 4, 5, 6},
                new List<int>(){ 7, 8, 9}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 7, 4, 1},
                new List<int>(){ 8, 5, 2},
                new List<int>(){ 9, 6, 3}
            };

            var actual = GridHelper.RotateGridRight<int>(start);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateRight_RectangleGrid_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1,  2,  3,  4},
                new List<int>(){ 5,  6,  7,  8},
                new List<int>(){ 9, 10, 11, 12}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 9, 5, 1},
                new List<int>(){ 10, 6, 2},
                new List<int>(){ 11, 7, 3},
                new List<int>(){ 12, 8, 4}
            };

            var actual = GridHelper.RotateGridRight<int>(start);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateLeft_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1, 2, 3},
                new List<int>(){ 4, 5, 6},
                new List<int>(){ 7, 8, 9}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 3, 6, 9},
                new List<int>(){ 2, 5, 8},
                new List<int>(){ 1, 4, 7}
            };

            var actual = GridHelper.RotateGridLeft<int>(start);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateLeft_RectangleGrid_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1,  2,  3,  4},
                new List<int>(){ 5,  6,  7,  8},
                new List<int>(){ 9, 10, 11, 12}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 4, 8, 12},
                new List<int>(){ 3, 7, 11},
                new List<int>(){ 2, 6, 10},
                new List<int>(){ 1, 5, 9}
            };

            var actual = GridHelper.RotateGridLeft<int>(start);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_RotateUpsideDown_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1, 2, 3},
                new List<int>(){ 4, 5, 6},
                new List<int>(){ 7, 8, 9}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 9, 8, 7},
                new List<int>(){ 6, 5, 4},
                new List<int>(){ 3, 2, 1}
            };

            var test1 = start.Clone2dList();
            test1 = GridHelper.RotateGrid180<int>(test1);
            Assert.Equal(expected, test1);

            var test2 = start.Clone2dList();
            test2 = GridHelper.RotateGridLeft<int>(test2);
            test2 = GridHelper.RotateGridLeft<int>(test2);
            Assert.Equal(expected, test2);

            var test3 = start.Clone2dList();
            test3 = GridHelper.RotateGridRight<int>(test3);
            test3 = GridHelper.RotateGridRight<int>(test3);
            Assert.Equal(expected, test3);
        }

        [Fact]
        public void GridHelper_RotateUpsideDown_RectangleGrid_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1,  2,  3,  4},
                new List<int>(){ 5,  6,  7,  8},
                new List<int>(){ 9, 10, 11, 12}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 4, 8, 12},
                new List<int>(){ 3, 7, 11},
                new List<int>(){ 2, 6, 10},
                new List<int>(){ 1, 5, 9}
            };

            var actual = GridHelper.RotateGridLeft<int>(start);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridHelper_FlipVertical_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1, 2, 3},
                new List<int>(){ 4, 5, 6},
                new List<int>(){ 7, 8, 9}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 3, 2, 1},
                new List<int>(){ 6, 5, 4},
                new List<int>(){ 9, 8, 7}
            };

            GridHelper.FlipAboutVertical<int>(start);
            Assert.Equal(expected, start);
        }

        [Fact]
        public void GridHelper_FlipVertical_RectangleGrid_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1,  2,  3,  4},
                new List<int>(){ 5,  6,  7,  8},
                new List<int>(){ 9, 10, 11, 12}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){  4,  3,  2, 1},
                new List<int>(){  8,  7,  6, 5},
                new List<int>(){ 12, 11, 10, 9}
            };

            GridHelper.FlipAboutVertical<int>(start);
            Assert.Equal(expected, start);
        }

        [Fact]
        public void GridHelper_FlipHorizontal_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1, 2, 3},
                new List<int>(){ 4, 5, 6},
                new List<int>(){ 7, 8, 9}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 7, 8, 9},
                new List<int>(){ 4, 5, 6},
                new List<int>(){ 1, 2, 3}
            };

            GridHelper.FlipAboutHorizontal<int>(start);
            Assert.Equal(expected, start);
        }

        [Fact]
        public void GridHelper_FlipHorizontal_RectangleGrid_ReturnsExpected()
        {
            var start = new List<List<int>>
            {
                new List<int>(){ 1,  2,  3,  4},
                new List<int>(){ 5,  6,  7,  8},
                new List<int>(){ 9, 10, 11, 12}
            };

            var expected = new List<List<int>>
            {
                new List<int>(){ 9, 10, 11, 12},
                new List<int>(){ 5,  6,  7,  8},
                new List<int>(){ 1,  2,  3,  4}
            };

            GridHelper.FlipAboutHorizontal<int>(start);
            Assert.Equal(expected, start);
        }
    }
}
