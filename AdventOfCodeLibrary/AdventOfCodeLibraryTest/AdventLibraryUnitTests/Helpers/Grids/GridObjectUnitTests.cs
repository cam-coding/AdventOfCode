using AdventLibrary.Helpers.Grids;
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
            bool actualReturn = _grid.TryGet(out actualValue, x, y);
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
