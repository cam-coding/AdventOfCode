using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary.Helpers.Grids
{
    public class GridObject<T>
    {
        public GridObject(List<List<T>> grid)
        {
            Grid = grid;
            Height = Grid.Count;
            Width = Grid[0].Count;
            DefaultValue = default(T);
            Infinite = false;
        }

        public List<List<T>> Grid { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool Infinite { get; set; }

        public T DefaultValue { get; set; }

        public bool TryGet(
            out T value,
            int x = int.MinValue,
            int y = int.MinValue,
            (int x, int y)? point = null)
        {
            var coords = GetCoords(x, y, point);
            value = DefaultValue;
            if (WithinGrid(coords.x, coords.y))
            {
                value = Get(coords.x, coords.y);
                return true;
            }
            else
            {
                return Infinite;
            }
        }

        public bool WithinGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private T Get(int x, int y)
        {
            return Grid[y][x];
        }

        private (int x, int y) GetCoords(
            int x = int.MinValue,
            int y = int.MinValue,
            (int x, int y)? point = null)
        {
            if (point == null)
            {
                return (x, y);
            }
            return point.Value;
        }
    }
}
