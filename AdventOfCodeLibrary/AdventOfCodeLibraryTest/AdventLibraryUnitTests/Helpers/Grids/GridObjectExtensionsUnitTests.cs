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
    public class GridObjectExtensionsUnitTests
    {
        private List<List<int>> GenerateSquareGrid()
        {
            var listy = new List<List<int>>();
            listy.Add(new List<int> { 1, 1, 1, 1 });
            listy.Add(new List<int> { 1, 1, 1, 1 });
            listy.Add(new List<int> { 1, 1, 1, 1 });
            listy.Add(new List<int> { 1, 1, 1, 1 });

            return listy;
        }
        private List<List<int>> GenerateRectangleGrid()
        {
            var listy = new List<List<int>>();
            listy.Add(new List<int> { 1, 1, 1, 1, 1, 1 });
            listy.Add(new List<int> { 1, 1, 1, 1, 1, 1 });
            listy.Add(new List<int> { 1, 1, 1, 1, 1, 1 });
            listy.Add(new List<int> { 1, 1, 1, 1, 1, 1 });

            return listy;
        }

        [Fact]
        public void SubDivide_SquareGrid_ExpectedResult()
        {
            var grid = new GridObject<int>(GenerateSquareGrid());
            var newGrids = grid.SubDivideGrid(2, 2);

            // a 4x4 grid should divide into 4 2x2 grids
            Assert.Equal(4, newGrids.Count);
            Assert.All(newGrids, (x) => Assert.True(x.Width == 2 && x.Height == 2));
        }

        [Fact]
        public void SubDivide_RectangleGrid_IntoSquareGrids_ExpectedResult()
        {
            var grid = new GridObject<int>(GenerateRectangleGrid());
            var newGrids = grid.SubDivideGrid(2, 2);

            // a 6x4 grid should divide into 6 2x2 grids
            Assert.Equal(6, newGrids.Count);
            Assert.All(newGrids, (x) => Assert.True(x.Width == 2 && x.Height == 2));
        }

        [Fact]
        public void SubDivide_RectangleGrid_IntoRectangleGrids_ExpectedResult()
        {
            var grid = new GridObject<int>(GenerateRectangleGrid());
            var newGrids = grid.SubDivideGrid(3, 2);

            // a 6x4 grid should divide into 4 3x2 grids
            Assert.Equal(4, newGrids.Count);
            Assert.All(newGrids, (x) => Assert.True(x.Width == 3 && x.Height == 2));
        }

        [Fact]
        public void SubDivide_RectangleGrid_IntoSameGrid_ExpectedResult()
        {
            var grid = new GridObject<int>(GenerateRectangleGrid());
            var newGrids = grid.SubDivideGrid(6, 4);

            // a 6x4 grid should divide into 1 6x4 grids
            Assert.Equal(1, newGrids.Count);
            Assert.All(newGrids, (x) => Assert.True(x.Width == 6 && x.Height == 4));
        }
    }
}