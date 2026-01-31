using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day15 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            long count = 0;

            var groups = input.LineGroupsSeperatedByWhiteSpace;

            var text = groups[0];
            var parser = InputParserFactory.CreateFromText(text);
            var grid = parser.GetLinesAsGrid<char>();
            var gridStart = grid.GetFirstLocationWhereCellEqualsValue('@');

            var movements = groups[1].ConcatListToString();

            var roller = new GridPusher<char>(
                grid,
                gridStart,
                '@',
                '.',
                '#',
                'O'
                );
            foreach (var move in movements)
            {
                var dir = Directions.GetDirectionByString(move);
                roller.MoveAndPush(dir);
            }

            var boxPosistions = grid.GetAllLocationWhereCellEqualsValue('O');
            return boxPosistions.Sum(box => box.Y * 100 + box.X);
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            long count = 0;

            var groups = input.LineGroupsSeperatedByWhiteSpace;

            var text = groups[0];
            var str = string.Empty;
            var gridStrings = new List<List<char>>();
            foreach (var g in text)
            {
                var currentRow = new List<char>();
                foreach (var c in g)
                {
                    if (c == '#')
                    {
                        currentRow.Add('#');
                        currentRow.Add('#');
                    }
                    else if (c == 'O')
                    {
                        currentRow.Add('[');
                        currentRow.Add(']');
                    }
                    else
                    {
                        currentRow.Add(c);
                        currentRow.Add('.');
                    }
                }
                gridStrings.Add(currentRow);
            }
            var grid = new GridObject<char>(gridStrings);
            var gridStart = grid.GetFirstLocationWhereCellEqualsValue('@');

            var movements = groups[1].ConcatListToString();
            var roller = new GridRoller3(
                grid,
                gridStart,
                '.',
                '#',
                new List<char>() { '[', ']' }
                );
            foreach (var move in movements)
            {
                var dir = Directions.GetDirectionByString(move);
                roller.MoveAndPush(dir);
            }

            var boxPosistions = grid.GetAllLocationWhereCellEqualsValue('[');
            return boxPosistions.Sum(box => box.Y * 100 + box.X);
        }
    }

    public class GridRoller3
    {
        public GridRoller3(
            GridObject<char> grid,
            GridLocation<int> hero,
            char emptySpaceValues,
            char wallValues,
            List<char> movableValues)
        {
            Grid = grid;
            Hero = hero;
            EmptySpaceValues = emptySpaceValues;
            WallValues = wallValues;
            MovableValues = movableValues;
        }

        public GridObject<char> Grid { get; set; }

        public char EmptySpaceValues { get; set; }

        public char WallValues { get; set; }

        public List<char> MovableValues { get; set; }

        public GridLocation<int> Hero { get; set; }

        public void MoveAndPush(GridLocation<int> dir)
        {
            if (EmptySpaceValues.Equals(Grid.Get(Hero + dir)))
            {
                var old = Hero;
                Hero = Hero + dir;
                Grid.Set(old, EmptySpaceValues);
                Grid.Set(Hero, '@');
            }
            else if (WallValues.Equals(Grid.Get(Hero + dir)))
            {
                return;
            }
            else if (MovableValues.Contains(Grid.Get(Hero + dir)))
            {
                // move boxes left and right
                if (dir == Directions.Left || dir == Directions.Right)
                {
                    var old3 = Hero;
                    var current3 = Hero + dir + dir;
                    while (MovableValues.Contains(Grid.Get(current3)))
                    {
                        current3 = current3 + dir;
                    }
                    var val3 = Grid.Get(current3);
                    if (WallValues.Equals(val3))
                    {
                        return;
                    }
                    else
                    {
                        // must be an empty space
                        while (current3 != old3)
                        {
                            var tempVal = Grid.Get(current3 - dir);
                            Grid.Set(current3, tempVal);
                            current3 = current3 - dir;
                        }
                        Grid.Set(current3, '.');
                        Hero = current3 + dir;
                        return;
                    }
                }
                GridLocation<int> otherHalf;
                GridLocation<int> currentHalf = Hero + dir;

                if (Grid.Get(currentHalf) == ']')
                {
                    otherHalf = currentHalf + Directions.Left;
                }
                else
                {
                    otherHalf = currentHalf + Directions.Right;
                }

                var boxesIn = new List<GridLocation<int>>() { currentHalf, otherHalf };

                // var listy = new List<(GridLocation<int>, char)>() { (currentHalf, Grid.Get(Hero + dir)) };
                var count = 1;
                var old = Hero;
                var boxesOut = new List<GridLocation<int>>();
                var res = Recursion(
                    boxesIn,
                    dir,
                    out boxesOut);

                if (!res)
                {
                    return;
                }
                var blah = boxesIn.Clone();
                blah.AddRange(boxesOut);
                if (dir == Directions.Up)
                {
                    var startingY = blah.Min(x => x.Y);
                    while (blah.Count > 0)
                    {
                        var movingBoxes = blah.Where(x => x.Y == startingY).ToList();
                        foreach (var item in movingBoxes)
                        {
                            var val2 = Grid.Get(item);
                            Grid.Set(item + dir, val2);
                            Grid.Set(item, '.');
                        }
                        movingBoxes.ForEach(x => blah.Remove(x));
                        startingY++;
                    }
                    Hero = Hero + dir;
                    Grid.Set(Hero, '@');
                    Grid.Set(old, '.');
                }
                else if (dir == Directions.Down)
                {
                    var startingY = blah.Max(x => x.Y);
                    while (blah.Count > 0)
                    {
                        var movingBoxes = blah.Where(x => x.Y == startingY).ToList();
                        foreach (var item in movingBoxes)
                        {
                            var val2 = Grid.Get(item);
                            Grid.Set(item + dir, val2);
                            Grid.Set(item, '.');
                        }
                        movingBoxes.ForEach(x => blah.Remove(x));
                        startingY--;
                    }
                    Hero = Hero + dir;
                    Grid.Set(Hero, '@');
                    Grid.Set(old, '.');
                }
            }
        }

        private bool Recursion(
            List<GridLocation<int>> boxesIn,
            GridLocation<int> dir,
            out List<GridLocation<int>> boxesOut)
        {
            boxesOut = new List<GridLocation<int>>();
            // should be passing in a list of all the box points being considered tbh
            // loop all of them looking above/below them
            // for any that are free, don't keep looking
            // any are walls? return false
            // any others are boxes for the next layer to consider.
            var nextSpaces = new HashSet<GridLocation<int>>();
            foreach (var box in boxesIn)
            {
                var nextSpace = box + dir;
                if (Grid.Get(nextSpace).Equals(WallValues))
                {
                    return false;
                }
                else if (Grid.Get(nextSpace).Equals(EmptySpaceValues))
                {
                }
                else
                {
                    if (nextSpaces.Add(nextSpace))
                    {
                        GridLocation<int> otherHalf;
                        GridLocation<int> currentHalf = nextSpace;

                        if (Grid.Get(currentHalf) == ']')
                        {
                            otherHalf = currentHalf + Directions.Left;
                        }
                        else
                        {
                            otherHalf = currentHalf + Directions.Right;
                        }
                        nextSpaces.Add(otherHalf);
                    }
                }
            }
            if (nextSpaces.Count == 0)
            { return true; }
            List<GridLocation<int>> newBoxes;
            if (Recursion(nextSpaces.ToList(), dir, out newBoxes))
            {
                boxesOut.AddRange(nextSpaces.ToList());
                boxesOut.AddRange(newBoxes.ToList());
                return true;
            }
            return false;
        }
    }
}