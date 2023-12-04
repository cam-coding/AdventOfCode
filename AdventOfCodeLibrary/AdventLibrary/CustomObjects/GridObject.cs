using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary.CustomObjects
{
    public class GridObject<T>
    {
        public GridObject(List<List<T>> grid)
        {
            Grid = grid;
            Height = Grid.Count;
            Width = Grid[0].Count;
        }

        public List<List<T>> Grid { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
