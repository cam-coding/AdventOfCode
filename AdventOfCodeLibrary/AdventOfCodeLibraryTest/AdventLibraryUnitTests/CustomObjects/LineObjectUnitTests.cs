using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers.Grids;
using Xunit;

namespace AdventLibraryUnitTests.CustomObjects
{
    public class LineObjectUnitTests
    {
        [Fact]
        public void LineObject_EqualsTest()
        {
            var start = new GridLocation<int>(0, 0);
            var end = new GridLocation<int>(5, 5);
            var lineObject1 = new LineObject<int>(start, end);
            var lineObject2 = new LineObject<int>(start, end);
            var lineObject2finite = new LineObject<int>(start, end, false);

            Assert.Equal(lineObject1, lineObject2);
            Assert.NotEqual(lineObject1, lineObject2finite);
        }

        [Fact]
        public void LineObject_HashCodeTest()
        {
            var start = new GridLocation<int>(0, 0);
            var end = new GridLocation<int>(5, 5);
            var lineObject1 = new LineObject<int>(start, end);
            var lineObject2 = new LineObject<int>(start, end);

            Assert.Equal(lineObject1.GetHashCode(), lineObject2.GetHashCode());
        }

        [Fact]
        public void LineObject_SlopeTest_Null()
        {
            var start = new GridLocation<int>(0, 0);
            var end = new GridLocation<int>(0, 5);
            var lineObject1 = new LineObject<int>(start, end);

            Assert.Null(lineObject1.Slope);
        }

        [Fact]
        public void LineObject_SlopeTest()
        {
            var start = new GridLocation<int>(0, 0);
            var end = new GridLocation<int>(2, 2);
            var lineObject1 = new LineObject<int>(start, end);

            var manualSlope = (end.X - start.X) / (end.Y - start.Y);

            Assert.Equal(manualSlope, lineObject1.Slope);
        }

        [Fact]
        public void LineObject_YIntercept_Test()
        {
            var start = new GridLocation<int>(0, 0);
            var end = new GridLocation<int>(0, 5);
            var verticalLine = new LineObject<int>(start, end);
            Assert.Null(verticalLine.GetYIntercept());

            var start2 = new GridLocation<decimal>(0, 3);
            var end2 = new GridLocation<decimal>(3, 6);
            var line = new LineObject<decimal>(start2, end2);
            Assert.Equal(start2, line.GetYIntercept());
        }

        [Fact]
        public void LineObject_XIntercept_Test()
        {
            var start = new GridLocation<int>(0, 5);
            var end = new GridLocation<int>(5, 5);
            var horizontalLine = new LineObject<int>(start, end);
            Assert.Null(horizontalLine.GetXIntercept());

            var start2 = new GridLocation<decimal>(3, 0);
            var end2 = new GridLocation<decimal>(6, 3);
            var line = new LineObject<decimal>(start2, end2);
            Assert.Equal(start2, line.GetXIntercept());
        }

        [Fact]
        public void LineObject_PointOnLine_InfiniteTest()
        {
            var start = new GridLocation<int>(0, 0);
            var point = new GridLocation<int>(5, 5);
            var badPoint = new GridLocation<int>(5, 0);
            var pointOnLineOutsideBounds = new GridLocation<int>(100, 100);
            var end = new GridLocation<int>(10, 10);
            var line = new LineObject<int>(start, end);
            Assert.True(line.PointOnLine(point));
            Assert.True(line.PointOnLine(pointOnLineOutsideBounds));
            Assert.False(line.PointOnLine(badPoint));
        }

        [Fact]
        public void LineObject_PointOnLine_InfiniteTest_float()
        {
            var start = new GridLocation<float>(0, 0);
            var point = new GridLocation<float>(5, 5);
            var badPoint = new GridLocation<float>(5, 0);
            var pointOnLineOutsideBounds = new GridLocation<float>(100, 100);
            var end = new GridLocation<float>(10, 10);
            var line = new LineObject<float>(start, end);
            Assert.True(line.PointOnLine(point));
            Assert.True(line.PointOnLine(pointOnLineOutsideBounds));
            Assert.False(line.PointOnLine(badPoint));
        }

        [Fact]
        public void LineObject_PointOnLine_FiniteTest()
        {
            var start = new GridLocation<int>(0, 0);
            var pointOnLine = new GridLocation<int>(5, 5);
            var pointNotOnLine = new GridLocation<int>(5, 0);
            var pointOnLineOutsideBounds = new GridLocation<int>(100, 100);
            var end = new GridLocation<int>(10, 10);
            var line = new LineObject<int>(start, end, false);
            Assert.True(line.PointOnLine(pointOnLine));
            Assert.False(line.PointOnLine(pointNotOnLine));
            Assert.False(line.PointOnLine(pointOnLineOutsideBounds));
        }

        [Fact]
        public void LineObject_PointOnLine_FiniteTest2()
        {
            var start = new GridLocation<int>(0, 0);
            var pointOnLine = new GridLocation<int>(1, 2);
            var pointNotOnLine = new GridLocation<int>(1, 3);
            var pointOnLineOutsideBounds = new GridLocation<int>(4, 8);
            var end = new GridLocation<int>(2, 4);
            var line = new LineObject<int>(start, end, false);
            Assert.True(line.PointOnLine(pointOnLine));
            Assert.False(line.PointOnLine(pointOnLineOutsideBounds));
            Assert.False(line.PointOnLine(pointNotOnLine));
        }

        [Fact]
        public void LineObject_PointOnLine_FiniteTest3()
        {
            var start = new GridLocation<int>(0, 10);
            var pointOnLine = new GridLocation<int>(1, 5);
            var pointNotOnLine = new GridLocation<int>(1, 3);
            var pointOnLineOutsideBounds = new GridLocation<int>(3, -5);
            var end = new GridLocation<int>(2, 0);
            var line = new LineObject<int>(start, end, false);
            Assert.True(line.PointOnLine(pointOnLine));
            Assert.False(line.PointOnLine(pointOnLineOutsideBounds));
            Assert.False(line.PointOnLine(pointNotOnLine));
        }

        [Fact]
        public void LineObject_PointOnLine_FiniteTest4()
        {
            var start = new GridLocation<int>(-5, -5);
            var pointOnLine = new GridLocation<int>(0, 0);
            var negativePointOnLine = new GridLocation<int>(-1, -1);
            var pointNotOnLine = new GridLocation<int>(-1, -2);
            var pointOnLineOutsideBounds = new GridLocation<int>(-10, -10);
            var end = new GridLocation<int>(10, 10);
            var line = new LineObject<int>(start, end, false);
            Assert.True(line.PointOnLine(pointOnLine));
            Assert.True(line.PointOnLine(negativePointOnLine));
            Assert.False(line.PointOnLine(pointNotOnLine));
            Assert.False(line.PointOnLine(pointOnLineOutsideBounds));
        }

        [Fact]
        public void LineObject_Test()
        {
        }
    }
}