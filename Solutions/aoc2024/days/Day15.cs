using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day15: ISolver
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
            var str = string.Empty;
            var gridStrings = new List<List<char>>();
            foreach (var g in text)
            {
                gridStrings.Add(g.ToList());
            }
            var grid = new GridObject<char>(gridStrings);
            var gridStart = grid.GetFirstLocationWhereCellEqualsValue('@');


            var text2 = groups[1];
            var str2 = string.Empty;
            foreach (var g in text2)
            {
                str2 += g;
            }

            var inputs = str2.ToList();

            Dictionary<char, GridLocation<int>> StringLookup = new Dictionary<char, GridLocation<int>>()
        {
            { '^', Directions.Up },
            { '<', Directions.Left },
            { 'v', Directions.Down },
            { '>', Directions.Right },
        };
            var roller = new GridRoller2(
                grid,
                gridStart,
                '.',
                '#',
                'O'
                );
            foreach (var move in inputs)
            {
                var dir = StringLookup[move];
                roller.Roll(dir);
            }

            var posy = grid.GetAllLocationWhereCellEqualsValue('O');
            foreach (var t in posy)
            {
                count += t.Y * 100 + t.X;
            }
            // roller.Grid.Print();
            return count;
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
                var tempoList = new List<char>();
                foreach (var c in g)
                {
                    if (c == '#')
                    {
                        tempoList.Add('#');
                        tempoList.Add('#');
                    }
                    else if (c == 'O')
                    {
                        tempoList.Add('[');
                        tempoList.Add(']');
                    }
                    else
                    {
                        tempoList.Add(c);
                        tempoList.Add('.');
                    }
                }
                gridStrings.Add(tempoList);
            }
            var grid = new GridObject<char>(gridStrings);
            var gridStart = grid.GetFirstLocationWhereCellEqualsValue('@');
            grid.Print();

            var text2 = groups[1];
            var str2 = string.Empty;
            foreach (var g in text2)
            {
                str2 += g;
            }

            var inputs = str2.ToList();

            Dictionary<char, GridLocation<int>> StringLookup = new Dictionary<char, GridLocation<int>>()
        {
            { '^', Directions.Up },
            { '<', Directions.Left },
            { 'v', Directions.Down },
            { '>', Directions.Right },
        };
            var roller = new GridRoller3(
                grid,
                gridStart,
                '.',
                '#',
                new List<char>() { '[', ']' }
                );
            foreach (var move in inputs)
            {
                var dir = StringLookup[move];
                roller.Roll(dir);
                roller.Grid.Print();
            }

            var posy = grid.GetAllLocationWhereCellEqualsValue('O');
            foreach (var t in posy)
            {
                count += t.Y * 100 + t.X;
            }
            roller.Grid.Print();
            return count;
        }
    }
    public class GridRoller2
    {
        public GridRoller2(
            GridObject<char> grid,
            GridLocation<int> hero,
            char emptySpaceValues,
            char wallValues,
            char movableValues)
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

        public char MovableValues { get; set; }

        public GridLocation<int> Hero { get; set; }

        public void Roll(GridLocation<int> dir)
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
            else if (MovableValues.Equals(Grid.Get(Hero + dir)))
            {
                var listy = new List<(GridLocation<int>, char)>() { (Hero + dir, Grid.Get(Hero + dir)) };
                var count = 1;
                var old = Hero;
                var current = Hero + dir + dir;
                while (Grid.Get(current).Equals(MovableValues))
                {
                    listy.Add((current, Grid.Get(current)));
                    current = current + dir;
                    count++;
                }

                var val = Grid.Get(current);
                if (WallValues.Equals(val))
                {
                    return;
                }
                else
                {
                    for (var i = count; i > 0; i--)
                    {
                        Grid.Set(current, MovableValues);
                        current = current - dir;
                    }

                    Grid.Set(current, '@');
                    Hero = current;
                    current = current - dir;
                    Grid.Set(old, '.');
                }
            }
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

        public void Roll(GridLocation<int> dir)
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
                        return;
                    }
                }
                GridLocation<int> otherHalf;
                GridLocation<int> currentHalf = Hero + dir;

                if (Grid.Get(currentHalf) == ']')
                {
                    otherHalf = currentHalf - Directions.Left;
                }
                else
                {
                    otherHalf = currentHalf + Directions.Right;
                }

                var boxesIn = new List<GridLocation<int>>() { currentHalf, otherHalf };

                // var listy = new List<(GridLocation<int>, char)>() { (currentHalf, Grid.Get(Hero + dir)) };
                var count = 1;
                var old = Hero;
                var current = Hero + dir + dir;
                while (MovableValues.Contains(Grid.Get(current)))
                {
                    current = current + dir;
                    count++;
                }

                var val = Grid.Get(current);
                if (WallValues.Equals(val))
                {
                    return;
                }
                else
                {
                    var boxesOut = new List<GridLocation<int>>();
                    var res = Recursion(
                        boxesIn,
                        current,
                        dir,
                        out boxesOut);

                    if (!res)
                    {
                        return;
                    }

                    if (dir == Directions.Left || dir == Directions.Right)
                    {
                        var currentSpot = boxesOut.Last() + dir * 2;
                        for (var i = boxesOut.Count - 1; i > 0; i--)
                        {
                            var val2 = Grid.Get(currentSpot - dir);
                            Grid.Set(currentSpot, val2);
                            currentSpot = currentSpot - dir;
                        }
                        Hero = currentSpot + dir;
                        Grid.Set(Hero, '@');
                        Grid.Set(old, '.');
                    }
                    else if (dir == Directions.Down)
                    {
                        var startingY = boxesOut.Max(x => x.Y);
                        while (boxesOut.Count > 0)
                        {
                            var movingBoxes = boxesOut.Where(x => x.Y == startingY).ToList();
                            foreach (var item in movingBoxes)
                            {
                                var val2 = Grid.Get(item);
                                Grid.Set(item + dir, val2);
                                Grid.Set(item, '.');
                            }
                            movingBoxes.ForEach(x => boxesOut.Remove(x));
                        }
                        Hero = Hero + dir;
                        Grid.Set(Hero, '@');
                        Grid.Set(old, '.');
                    }
                }
            }
        }

        bool Recursion(
            List<GridLocation<int>> boxesIn,
            GridLocation<int> current,
            GridLocation<int> dir,
            out List<GridLocation<int>> boxesOut)
        {
            boxesOut = boxesIn.Clone();
            if (dir == Directions.Left || dir == Directions.Right)
            {
                var nextSpace = current + dir * 2;
                if (Grid.Get(nextSpace).Equals(WallValues))
                {
                    return false;
                }
                else if (Grid.Get(nextSpace).Equals(EmptySpaceValues))
                {
                    return true;
                }
                else
                {
                    var newBox = new List<GridLocation<int>>() { nextSpace, nextSpace + dir };
                    List<GridLocation<int>> newBoxes;
                    if (Recursion(newBox, nextSpace, dir, out newBoxes))
                    {
                        boxesOut.AddRange(newBoxes);
                        return true;
                    }
                }
            }
            else if (dir == Directions.Up || dir == Directions.Down)
            {
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
                                otherHalf = currentHalf - Directions.Left;
                            }
                            else
                            {
                                otherHalf = currentHalf + Directions.Right;
                            }
                            nextSpaces.Add(otherHalf);
                        }
                    }
                }
                List<GridLocation<int>> newBoxes;
                if (Recursion(nextSpaces.ToList(), current, dir, out newBoxes))
                {
                    boxesOut.AddRange(nextSpaces.ToList());
                    return true;
                }
            }
            return false;
        }
    }
}