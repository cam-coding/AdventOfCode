using System.Collections.Generic;
using AdventLibrary.Extensions;

namespace AdventLibrary.Helpers.Grids
{
    public class GridObject<T>
    {
        private List<List<T>> _grid;

        public GridObject(List<List<T>> grid)
        {
            Grid = grid.Clone2dList();
            Height = Grid.Count;
            Width = Grid[0].Count;
            DefaultValue = default(T);
            Infinite = false;
        }

        public List<List<T>> Grid
        {
            get => _grid;
            set
            {
                _grid = value;
                Height = _grid.Count;
                Width = _grid[0].Count;
            }
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool Infinite { get; set; }

        public T DefaultValue { get; set; }

        public int MaxX => Width - 1;

        public int MaxY => Height - 1;

        public GridObject<T> Clone()
        {
            return new GridObject<T>(Grid);
        }

        public bool TryGet(out T value, GridLocation<int> location)
        {
            value = DefaultValue;
            if (WithinGrid(location))
            {
                value = Get(location);
                return true;
            }
            else
            {
                return Infinite;
            }
        }

        public T Get(int x, int y)
        {
            return Grid[y][x];
        }

        public T Get(GridLocation<int> location)
        {
            return Get(location.X, location.Y);
        }

        public void Set(int x, int y, T value)
        {
            Grid[y][x] = value;
        }

        public void Set(GridLocation<int> location, T value)
        {
            Set(location.X, location.Y, value);
        }

        public void Print()
        {
            GridHelper.PrintGrid(Grid);
        }

        public bool WithinGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool WithinGrid(GridLocation<int> location)
        {
            return location.X >= 0 && location.X < Width && location.Y >= 0 && location.Y < Height;
        }

        public GridLocation<int> GetFirstLocationWhereCellEqualsValue(T value)
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (Get(x, y).Equals(value))
                    {
                        return new GridLocation<int>(x, y);
                    }
                }
            }

            return null;
        }

        public List<GridLocation<int>> GetAllLocationWhereCellEqualsValue(T value)
        {
            var listy = new List<GridLocation<int>>();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (Get(x, y).Equals(value))
                    {
                        listy.Add(new GridLocation<int>(x, y));
                    }
                }
            }

            return listy;
        }

        // Gets 4 neighbours that are directly N/E/S/W aka Up/Right/Down/Left
        public List<GridLocation<int>> GetOrthogonalNeighbours(GridLocation<int> location)
        {
            return GetNeighbours(location, Directions.OrthogonalDirections);
        }

        // Gets 4 neighbours that are diagonal NE/SE/SW/NW aka UpRight/DownRight/DownLeft/UpLeft
        public List<GridLocation<int>> GetDiagonalNeighbours(GridLocation<int> location)
        {
            return GetNeighbours(location, Directions.DiagonalDirections);
        }

        // Gets all 8 neighbours. Diagonal and orthogonal
        public List<GridLocation<int>> GetAllNeighbours(GridLocation<int> location)
        {
            return GetNeighbours(location, Directions.AllDirections);
        }

        private List<GridLocation<int>> GetNeighbours(GridLocation<int> currentLocation, List<GridLocation<int>> directions)
        {
            var result = new List<GridLocation<int>>();
            foreach (var direction in directions)
            {
                var tempLocation = currentLocation + direction;
                if (WithinGrid(tempLocation))
                {
                    result.Add(tempLocation);
                }
            }
            return result;
        }
    }
}