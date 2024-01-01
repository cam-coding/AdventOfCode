using System.Collections.Generic;

namespace AdventLibrary.Helpers.Grids
{
    public class GridRoller<T>
    {
        public GridRoller(
            List<List<T>> grid,
            HashSet<T> emptySpaceValues,
            HashSet<T> wallValues,
            HashSet<T> movableValues)
        {
            Grid = grid;
            EmptySpaceValues = emptySpaceValues;
            WallValues = wallValues;
            MovableValues = movableValues;
        }

        public List<List<T>> Grid { get; set; }

        public HashSet<T> EmptySpaceValues { get; set; }

        public HashSet<T> WallValues { get; set; }

        public HashSet<T> MovableValues { get; set; }

        // roll each item until it hits the edge or a wall.
        // Could make a version with wrap as well?
        public List<List<T>> RollUp()
        {
            for (var j = 0; j < Grid[0].Count; j++)
            {
                var freeSpaces = new List<int>();
                for (var i = 0; i < Grid.Count; i++)
                {
                    freeSpaces = RollHelperUpDown(
                        freeSpaces,
                        i,
                        j);
                }
            }
            return Grid;
        }

        public List<List<T>> RollDown()
        {
            for (var j = 0; j < Grid[0].Count; j++)
            {
                var freeSpaces = new List<int>();
                for (var i = Grid.Count - 1; i >= 0 ; i--)
                {
                    freeSpaces = RollHelperUpDown(
                        freeSpaces,
                        i,
                        j);
                }
            }
            return Grid;
        }

        public List<List<T>> RollLeft()
        {
            for (var i = 0; i < Grid.Count; i++)
            {
                var freeSpaces = new List<int>();
                for (var j = 0; j < Grid[i].Count; j++)
                {
                    freeSpaces = RollHelperLeftRight(
                        freeSpaces,
                        i,
                        j);
                }
            }
            return Grid;
        }
        public List<List<T>> RollRight()
        {
            for (var i = 0; i < Grid.Count; i++)
            {
                var freeSpaces = new List<int>();
                for (var j = Grid[i].Count - 1; j >= 0; j--)
                {
                    freeSpaces = RollHelperLeftRight(
                        freeSpaces,
                        i,
                        j);
                }
            }
            return Grid;
        }

        private List<int> RollHelperUpDown(
            List<int> freeSpaces,
            int y,
            int x)
        {
            if (MovableValues.Contains(Grid[y][x]))
            {
                if (freeSpaces.Count > 0)
                {
                    var lastFree = freeSpaces[0];
                    freeSpaces.RemoveAt(0);
                    T tempMovable = Grid[y][x];
                    T tempEmpty = Grid[lastFree][x];
                    Grid[lastFree][x] = tempMovable;
                    Grid[y][x] = tempEmpty;
                    freeSpaces.Add(y);
                }
            }
            else if (EmptySpaceValues.Contains(Grid[y][x]))
            {
                freeSpaces.Add(y);
            }
            else if (WallValues.Contains(Grid[y][x]))
            {
                freeSpaces.Clear();
            }
            return freeSpaces;
        }

        private List<int> RollHelperLeftRight(
            List<int> freeSpaces,
            int y,
            int x)
        {
            if (MovableValues.Contains(Grid[y][x]))
            {
                if (freeSpaces.Count > 0)
                {
                    var lastFree = freeSpaces[0];
                    freeSpaces.RemoveAt(0);
                    T tempMovable = Grid[y][x];
                    T tempEmpty = Grid[y][lastFree];
                    Grid[y][lastFree] = tempMovable;
                    Grid[y][x] = tempEmpty;
                    freeSpaces.Add(x);
                }
            }
            else if (EmptySpaceValues.Contains(Grid[y][x]))
            {
                freeSpaces.Add(x);
            }
            else if (WallValues.Contains(Grid[y][x]))
            {
                freeSpaces.Clear();
            }
            return freeSpaces;
        }
    }
}
