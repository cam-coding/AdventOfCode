﻿using AdventLibrary.Extensions;
using AdventLibrary.PathFinding;
using System.Collections.Generic;

namespace AdventLibrary.Helpers.Grids
{
    public class GridObject<T>
    {
        public GridObject(List<List<T>> grid)
        {
            Grid = grid.Clone2dList();
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

        public bool WithinGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool WithinGrid(GridLocation<int> location)
        {
            return location.X >= 0 && location.X < Width && location.Y >= 0 && location.Y < Height;
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

        public void Print()
        {
            GridHelper.PrintGrid(Grid);
        }

        public T Get(int x, int y)
        {
            return Grid[y][x];
        }

        public T Get(GridLocation<int> location)
        {
            return Get(location.X, location.Y);
        }

        public GridLocation<int> GetTopLeftLocation()
        {
            return new GridLocation<int>(0, 0);
        }

        public GridLocation<int> GetBottomRightLocation()
        {
            return new GridLocation<int>(Width - 1, Height - 1);
        }

        public GridLocation<int> GetLocationWhereCellEqualsValue(T value)
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (Get(i, j).Equals(value))
                    {
                        return new GridLocation<int>(i, j);
                    }
                }
            }

            return null;
        }

        public List<GridLocation<int>> GetAllLocationWhereCellEqualsValue(T value)
        {
            var listy = new List<GridLocation<int>>();
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (Get(i, j).Equals(value))
                    {
                        listy.Add(new GridLocation<int>(i, j));
                    }
                }
            }

            return listy;
        }

        public void Set(int x, int y, T value)
        {
            Grid[y][x] = value;
        }

        public void Set(GridLocation<int> location, T value)
        {
            Set(location.X, location.Y, value);
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