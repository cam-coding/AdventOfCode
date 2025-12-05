using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Does a deep clone to remove references
        /// </summary>
        public GridObject<T> Clone()
        {
            return new GridObject<T>(Grid);
        }

        public bool Equals(GridObject<T> otherGrid)
        {
            if (Width == otherGrid.Width && Height == otherGrid.Height)
            {
                for (var i = 0; i < Height; i++)
                {
                    if (!Grid[i].SequenceEqual(otherGrid.Grid[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the value at that location or default value. True if within grid or infinite. False otherwise.
        /// </summary>
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

        /// <summary>
        /// X is column & y is Row
        /// </summary>
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

        public List<T> GetRow(int y)
        {
            return Grid[y];
        }

        public List<T> GetColumn(int x)
        {
            var column = new List<T>();
            for (var y = 0; y < Height; y++)
            {
                column.Add(Get(x, y));
            }
            return column;
        }

        public void Print()
        {
            GridHelper.PrintGrid(Grid);
        }

        /// <summary>
        /// Assumes 0,0 as minimum values in grid.
        /// </summary>
        public bool WithinGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        /// <summary>
        /// Assumes 0,0 as minimum values in grid.
        /// </summary>
        public bool WithinGrid(GridLocation<int> location)
        {
            return location.X >= 0 && location.X < Width && location.Y >= 0 && location.Y < Height;
        }

        public List<GridLocation<int>> GetAllLocations()
        {
            var listy = new List<GridLocation<int>>();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    listy.Add(new GridLocation<int>(x, y));
                }
            }

            return listy;
        }

        /// <summary>
        /// Pass in a function that uses a grid location to return a bool.
        /// </summary>
        public List<GridLocation<int>> GetAllLocationsWhere(Func<GridLocation<int>, bool> filter = null)
        {
            var listy = new List<GridLocation<int>>();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (filter(new GridLocation<int>(x, y)))
                    {
                        listy.Add(new GridLocation<int>(x, y));
                    }
                }
            }

            return listy;
        }

        /// <summary>
        /// Pass in a function that uses a cell value to return a bool
        /// </summary>
        public List<GridLocation<int>> GetAllLocationsWhereValue(Func<T, bool> filter = null)
        {
            var listy = new List<GridLocation<int>>();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (filter(Get(x, y)))
                    {
                        listy.Add(new GridLocation<int>(x, y));
                    }
                }
            }

            return listy;
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

        /// <summary>
        /// Gets 4 neighbours that are directly N/E/S/W aka Up/Right/Down/Left
        /// </summary>
        public List<GridLocation<int>> GetOrthogonalNeighbours(GridLocation<int> location)
        {
            return GetNeighbours(location, Directions.OrthogonalDirections);
        }

        /// <summary>
        /// Gets 4 neighbours that are diagonal NE/SE/SW/NW aka UpRight/DownRight/DownLeft/UpLeft
        /// </summary>
        public List<GridLocation<int>> GetDiagonalNeighbours(GridLocation<int> location)
        {
            return GetNeighbours(location, Directions.DiagonalDirections);
        }

        /// <summary>
        /// Gets all 8 neighbours. Diagonal and orthogonal
        /// </summary>
        public List<GridLocation<int>> GetAllNeighbours(GridLocation<int> location)
        {
            return GetNeighbours(location, Directions.AllDirections);
        }

        /// <summary>
        /// Returns a string representation of the grid. No seperators between columns and '\n' seperator between rows by default.
        /// </summary>
        public string Stringify(char seperator = '\n')
        {
            return Grid.Stringify(seperator);
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