using System.Collections.Generic;
using System.Numerics;

namespace AdventLibrary.Helpers.Grids
{
    /* Designed for: https://adventofcode.com/2023/day/14
     * Will roll all moveable spot in the grid in the desired direction
     * Will move as far as possible until running into a wall or the edge of the grid
     * */
    public class GridRoller<T>
    {
        public GridRoller(
            GridObject<T> grid,
            HashSet<T> emptySpaceValues,
            HashSet<T> wallValues,
            HashSet<T> movableValues)
        {
            Grid = grid;
            EmptySpaceValues = emptySpaceValues;
            WallValues = wallValues;
            MovableValues = movableValues;
        }

        public GridObject<T> Grid { get; set; }

        public HashSet<T> EmptySpaceValues { get; set; }

        public HashSet<T> WallValues { get; set; }

        public HashSet<T> MovableValues { get; set; }

        // roll each item until it hits the edge or a wall.
        // Could make a version with wrap as well?
        public void RollUp()
        {
            for (var x = 0; x < Grid.Width; x++)
            {
                var freeSpaces = new List<int>();
                for (var y = 0; y < Grid.Height; y++)
                {
                    freeSpaces = RollHelperUpDown(
                        freeSpaces,
                        x,
                        y);
                }
            }
        }

        public void RollDown()
        {
            for (var x = 0; x < Grid.Width; x++)
            {
                var freeSpaces = new List<int>();
                for (var y = Grid.Height - 1; y >= 0 ; y--)
                {
                    freeSpaces = RollHelperUpDown(
                        freeSpaces,
                        x,
                        y);
                }
            }
        }

        public void RollLeft()
        {
            for (var y = 0; y < Grid.Height; y++)
            {
                var freeSpaces = new List<int>();
                for (var x = 0; x < Grid.Width; x++)
                {
                    freeSpaces = RollHelperLeftRight(
                        freeSpaces,
                        x,
                        y);
                }
            }
        }
        public void RollRight()
        {
            for (var y = 0; y < Grid.Height; y++)
            {
                var freeSpaces = new List<int>();
                for (var x = Grid.Width - 1; x >= 0; x--)
                {
                    freeSpaces = RollHelperLeftRight(
                        freeSpaces,
                        x,
                        y);
                }
            }
        }

        private List<int> RollHelperUpDown(
            List<int> freeSpaces,
            int x,
            int y)
        {
            var originalValue = Grid.Get(x, y);
            if (MovableValues.Contains(originalValue))
            {
                if (freeSpaces.Count > 0)
                {
                    var lastFree = freeSpaces[0];
                    freeSpaces.RemoveAt(0);
                    T tempMovable = originalValue;
                    T tempEmpty = Grid.Get(x, lastFree);
                    Grid.Set(x, lastFree, tempMovable);
                    Grid.Set(x, y, tempEmpty);
                    freeSpaces.Add(y);
                }
            }
            else if (EmptySpaceValues.Contains(originalValue))
            {
                freeSpaces.Add(y);
            }
            else if (WallValues.Contains(originalValue))
            {
                freeSpaces.Clear();
            }
            return freeSpaces;
        }

        private List<int> RollHelperLeftRight(
            List<int> freeSpaces,
            int x,
            int y)
        {
            var originalValue = Grid.Get(x, y);
            if (MovableValues.Contains(originalValue))
            {
                if (freeSpaces.Count > 0)
                {
                    var lastFree = freeSpaces[0];
                    freeSpaces.RemoveAt(0);
                    T tempMovable = originalValue;
                    T tempEmpty = Grid.Get(lastFree, y);
                    Grid.Set(lastFree, y, tempMovable);
                    Grid.Set(x, y, tempEmpty);
                    freeSpaces.Add(x);
                }
            }
            else if (EmptySpaceValues.Contains(originalValue))
            {
                freeSpaces.Add(x);
            }
            else if (WallValues.Contains(originalValue))
            {
                freeSpaces.Clear();
            }
            return freeSpaces;
        }
    }
}
