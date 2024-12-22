using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day21 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        private Dictionary<
            char,
            GridLocation<int>>
            _dirPadCharToLocation = new Dictionary<char, GridLocation<int>>();

        private Dictionary<
            (GridLocation<int> start, GridLocation<int> end),
            List<List<GridLocation<int>>>>
            _allDirectionPadPaths;

        private Dictionary<
            GridLocation<int>,
            char> _difToChar = new Dictionary<GridLocation<int>, char>()
        {
            {Directions.Up, '^'},
            {Directions.Down, 'V'},
            {Directions.Left, '<'},
            {Directions.Right, '>'},
        };

        private Dictionary<
            (List<List<GridLocation<int>>> possibleRoutes, int level),
            long>
            _memo = new Dictionary<(List<List<GridLocation<int>>> possibleRoutes, int level), long>();

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var dirKeyGridBase = GridHelper.GenerateGrid<char>(3, 2, '.');
            dirKeyGridBase[0][0] = 'X';
            dirKeyGridBase[0][1] = '^';
            dirKeyGridBase[0][2] = 'A';
            dirKeyGridBase[1][0] = '<';
            dirKeyGridBase[1][1] = 'V';
            dirKeyGridBase[1][2] = '>';

            var keypadGridBase = GridHelper.GenerateGrid<char>(3, 4, '.');
            keypadGridBase[0][0] = '7';
            keypadGridBase[0][1] = '8';
            keypadGridBase[0][2] = '9';
            keypadGridBase[1][0] = '4';
            keypadGridBase[1][1] = '5';
            keypadGridBase[1][2] = '6';
            keypadGridBase[2][0] = '1';
            keypadGridBase[2][1] = '2';
            keypadGridBase[2][2] = '3';
            keypadGridBase[3][0] = 'X';
            keypadGridBase[3][1] = '0';
            keypadGridBase[3][2] = 'A';

            var directionArrowGrid = new GridObject<char>(dirKeyGridBase);
            var dirKeyStartLocation = directionArrowGrid.GetFirstLocationWhereCellEqualsValue('A');
            var dirLocations = directionArrowGrid.GetAllLocationsWhere(x => true);
            foreach (var item in dirLocations)
            {
                _dirPadCharToLocation.Add(directionArrowGrid.Get(item), item);
            }

            var keypadGrid = new GridObject<char>(keypadGridBase);

            _allDirectionPadPaths = new Dictionary<(GridLocation<int> start, GridLocation<int> end), List<List<GridLocation<int>>>>();

            for (var y = 0; y < directionArrowGrid.Height; y++)
            {
                for (var x = 0; x < directionArrowGrid.Width; x++)
                {
                    if (directionArrowGrid.Get(x, y) != 'X')
                    {
                        for (var i = 0; i < directionArrowGrid.Height; i++)
                        {
                            for (var j = 0; j < directionArrowGrid.Width; j++)
                            {
                                if ((directionArrowGrid.Get(j, i) == 'X'))
                                {
                                    continue;
                                }
                                var myStart = new GridLocation<int>(x, y);
                                var myEnd = new GridLocation<int>(j, i);
                                if (myStart == myEnd)
                                {
                                    _allDirectionPadPaths.Add((myStart, myEnd), new List<List<GridLocation<int>>>());
                                    continue;
                                }
                                var temp = EveryKeyPadPath(myStart, myEnd, directionArrowGrid);
                                _allDirectionPadPaths.Add((myStart, myEnd), temp);
                            }
                        }
                    }
                }
            }
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.LongsWithNegatives;
            long count = 0;

            // start at 'A' in the bottom right of the keypad
            var keypadLocation = new GridLocation<int>(2, 3);
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var listOfDirs = new List<List<GridLocation<int>>>();

                long myTotal = 0;

                foreach (var c in line)
                {
                    var goalLocationKeypad = keypadGrid.GetFirstLocationWhereCellEqualsValue(c);
                    var keyPadPaths = EveryKeyPadPath(keypadLocation, goalLocationKeypad, keypadGrid);
                    keypadLocation = goalLocationKeypad;
                    myTotal += ScorePossible(keyPadPaths, 1);
                }
                var num1 = numbers[i];
                count += myTotal * num1;
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            return 0;
        }

        private long ScorePossible(
            List<List<GridLocation<int>>> possibleRoutes,
            int level)
        {
            if (_memo.ContainsKey((possibleRoutes, level)))
            {
                return _memo[(possibleRoutes, level)];
            }
            if (level == 26)
            {
                if (possibleRoutes.Count == 0)
                {
                    _memo.TryAdd((possibleRoutes, level), 1);
                    return 1;
                }
                var val = possibleRoutes.Min(x => x.Count + 1);
                _memo.TryAdd((possibleRoutes, level), val);
                return val;
            }

            long best = long.MaxValue;
            if (possibleRoutes.Count == 0)
            {
                best = ScorePossible(possibleRoutes, level + 1);
            }
            // At the base version and assuming: 029A
            // all possible routes from 0 to 2 let's say
            foreach (var route in possibleRoutes)
            {
                long total = 0;
                var chars = route.Select(x => _difToChar[x]).ToList();

                var start = _dirPadCharToLocation['A'];
                for (var i = 0; i < chars.Count; i++)
                {
                    var end = _dirPadCharToLocation[chars[i]];
                    var nextRoutes = _allDirectionPadPaths[(start, end)];
                    long score = ScorePossible(nextRoutes, level + 1);
                    total += score;

                    start = _dirPadCharToLocation[chars[i]];
                }
                var nextRoutes2 = _allDirectionPadPaths[(start, _dirPadCharToLocation['A'])];
                total += ScorePossible(nextRoutes2, level + 1);

                if (total < best)
                {
                    best = total;
                }
            }

            _memo.TryAdd((possibleRoutes, level), best);
            return best;
        }

        private List<GridLocation<int>> DirPathToDiff2(List<GridLocation<int>> fullPath)
        {
            var offsetPath = new List<GridLocation<int>>();
            var previous = fullPath[0];
            for (var i = 1; i < fullPath.Count; i++)
            {
                offsetPath.Add(fullPath[i] - previous);
                previous = fullPath[i];
            }
            return offsetPath;
        }

        private List<List<GridLocation<int>>> EveryKeyPadPath(
            GridLocation<int> start,
            GridLocation<int> end,
            GridObject<char> grid)
        {
            Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
            var fullPathHistory = new HashSet<string>();

            var bestVal = int.MaxValue;
            var best = new List<List<GridLocation<int>>>();
            var bestPathLength = int.MaxValue;

            // (0,0) can be anything, just needs to be your starting point.
            q.Enqueue(new List<GridLocation<int>>() { start });
            var endsHash = new HashSet<GridLocation<int>>();
            while (q.Count > 0)
            {
                var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var currentLocation = fullPath.Last(); // this is just the most recent point
                var currentValue = grid.Get(currentLocation);
                var stringy = ListExtensions.Stringify(fullPath);

                if (
                    fullPath == null ||
                    fullPathHistory.Contains(stringy) ||
                    fullPath.Count > 6) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                    continue;

                // make sure to add current state to history
                fullPathHistory.Add(stringy);

                if (currentLocation == end)
                {
                    if (fullPath.Count <= bestPathLength)
                    {
                        var offsetPath = DirPathToDiff2(fullPath);

                        if (fullPath.Count < bestPathLength)
                        {
                            best = new List<List<GridLocation<int>>>() { offsetPath };
                            bestPathLength = fullPath.Count;
                        }
                        else
                        {
                            best.Add(offsetPath);
                        }
                    }
                    continue;
                }

                //Get the next nodes/grids/etc to visit next
                foreach (var neighbour in grid.GetOrthogonalNeighbours(currentLocation))
                {
                    var val = grid.Get(neighbour);

                    if (val == 'X' || fullPath.Contains(neighbour))
                    {
                        continue;
                    }
                    var temp = fullPath.Clone(); // very important, do not miss this clone
                    temp.Add(neighbour);
                    q.Enqueue(temp);
                }
            }

            return best;
        }
    }
}