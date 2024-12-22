using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using AdventLibrary.PathFinding;

namespace aoc2024
{
    public class Day21 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private GridLocation<int> _keypadLocation;
        private Dictionary<GridLocation<int>, List<GridLocation<int>>> _dirGoingTo;
        private Dictionary<GridLocation<int>, List<GridLocation<int>>> _dirComingFrom;

        private Dictionary<char, GridLocation<int>> _keyPadCharToLocation = new Dictionary<char, GridLocation<int>>();

        private Dictionary<char, GridLocation<int>> _dirPadCharToLocation = new Dictionary<char, GridLocation<int>>();

        private GridLocation<int> _aLocation = new GridLocation<int>(2, 3);
        private GridLocation<int> _dirALocation = new GridLocation<int>(2, 0);

        private Dictionary<GridLocation<int>, Dictionary<GridLocation<int>, (int Distance, List<GridLocation<int>> Path)>> _dirKeyPadPathLookup;

        private Dictionary<(GridLocation<int> start, GridLocation<int> end), List<List<GridLocation<int>>>> _allDirectionPadPaths;

        private Dictionary<GridLocation<int>, char> _difToChar = new Dictionary<GridLocation<int>, char>()
        {
            {Directions.Up, '^'},
            {Directions.Down, 'V'},
            {Directions.Left, '<'},
            {Directions.Right, '>'},
        };

        private Dictionary<(List<List<GridLocation<int>>> possibleRoutes, int level), long> _memo = new Dictionary<(List<List<GridLocation<int>>> possibleRoutes, int level), long>();

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
            _keypadLocation = new GridLocation<int>(2, 3);

            var directionArrowGrid = new GridObject<char>(dirKeyGridBase);
            var dirKeyStartLocation = directionArrowGrid.GetFirstLocationWhereCellEqualsValue('A');
            var dirLocations = directionArrowGrid.GetAllLocationsWhere(x => true);
            foreach (var item in dirLocations)
            {
                _dirPadCharToLocation.Add(directionArrowGrid.Get(item), item);
            }

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in directionArrowGrid.GetOrthogonalNeighbours(node))
                {
                    // remove any edges where the height difference is too great
                    if (directionArrowGrid.Get(edge) != 'X')
                    {
                        neighbours.Add(edge);
                    }
                }
                return neighbours;
            };
            Func<GridLocation<int>, GridLocation<int>, int> WeightFunc = (current, neigh) =>
            {
                return 1;
            };

            Func<GridLocation<int>, bool> GoalFunc = (current) =>
            {
                return current == null;
            };
            var res = Dijkstra<GridLocation<int>>.SearchEverywhere(dirKeyStartLocation, NeighboursFunc, WeightFunc, GoalFunc);

            _dirKeyPadPathLookup =
                new Dictionary<GridLocation<int>, Dictionary<GridLocation<int>, (int Distance, List<GridLocation<int>> Path)>>();
            foreach (var loc in dirLocations)
            {
                var res2 = Dijkstra<GridLocation<int>>.SearchEverywhere(loc, NeighboursFunc, WeightFunc, GoalFunc);
                _dirKeyPadPathLookup.Add(loc, res2);
            }
            // where you are going and then path of offsets to get there
            _dirGoingTo = new Dictionary<GridLocation<int>, List<GridLocation<int>>>();
            foreach (var key in res.Keys)
            {
                var offsetPath = new List<GridLocation<int>>();
                var previous = dirKeyStartLocation;
                for (var i = 0; i < res[key].Path.Count; i++)
                {
                    offsetPath.Add(res[key].Path[i] - previous);
                    previous = res[key].Path[i];
                }
                _dirGoingTo.Add(key, offsetPath);
            }

            _dirComingFrom = new Dictionary<GridLocation<int>, List<GridLocation<int>>>();
            foreach (var item in _dirGoingTo)
            {
                var newValue = item.Value.Select(x => Directions.Opposites[x]).ToList();
                newValue.Reverse();
                _dirComingFrom.Add(item.Key, newValue);
            }

            var keypadGrid = new GridObject<char>(keypadGridBase);
            var keypadGridStartLocation = keypadGrid.GetFirstLocationWhereCellEqualsValue('A');
            var gridLocatnios = keypadGrid.GetAllLocationsWhere(x => true);
            foreach (var item in gridLocatnios)
            {
                _keyPadCharToLocation.Add(keypadGrid.Get(item), item);
            }

            GridLocation<int> goalLocationKeypad = null;

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFuncKeypad = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in keypadGrid.GetOrthogonalNeighbours(node))
                {
                    // remove any edges where the height difference is too great
                    if (keypadGrid.Get(edge) != 'X')
                    {
                        neighbours.Add(edge);
                    }
                }
                return neighbours;
            };
            Func<GridLocation<int>, GridLocation<int>, int> WeightFuncKeypad = (current, neigh) =>
            {
                return 1;
            };

            Func<GridLocation<int>, bool> GoalFuncKeypad = (current) =>
            {
                return current == goalLocationKeypad;
            };
            var keyPadLookup = Dijkstra<GridLocation<int>>.SearchEverywhere(keypadGridStartLocation, NeighboursFuncKeypad, WeightFuncKeypad, GoalFuncKeypad);

            goalLocationKeypad = new GridLocation<int>(0, 0);
            var keypadDirections = GetKeypadPath(goalLocationKeypad, keypadGridStartLocation, NeighboursFuncKeypad, WeightFuncKeypad, GoalFuncKeypad);

            var testy = BestKeyPadPath(_keyPadCharToLocation['A'], _keyPadCharToLocation['7'], keypadGrid);

            var dicty = new Dictionary<(GridLocation<int> start, GridLocation<int> end), List<List<GridLocation<int>>>>();

            for (var y = 0; y < keypadGrid.Height; y++)
            {
                for (var x = 0; x < keypadGrid.Width; x++)
                {
                    if (keypadGrid.Get(x, y) != 'X')
                    {
                        for (var i = 0; i < keypadGrid.Height; i++)
                        {
                            for (var j = 0; j < keypadGrid.Width; j++)
                            {
                                if ((i == y && j == x) || (keypadGrid.Get(j, i) == 'X'))
                                {
                                    continue;
                                }
                                var myStart = new GridLocation<int>(x, y);
                                var myEnd = new GridLocation<int>(j, i);
                                var temp = EveryKeyPadPath(myStart, myEnd, keypadGrid);
                                dicty.Add((myStart, myEnd), temp);
                            }
                        }
                    }
                }
            }

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
            /*
             *
             *
             *
             *
             *
             *
             * */
            /*
            var test = "029A";

            var listOfDirs = new List<List<GridLocation<int>>>();

            foreach (var c in test)
            {
                goalLocationKeypad = keypadGrid.GetFirstLocationWhereCellEqualsValue(c);
                var path = GetKeypadPath(goalLocationKeypad, _keypadLocation, NeighboursFuncKeypad, WeightFuncKeypad, GoalFuncKeypad);
                listOfDirs.Add(path);
                _keypadLocation = goalLocationKeypad;
            }
            var secondLayerString = TranslateToString(listOfDirs);
            var nextLayerIn = TranslateUpALayer(secondLayerString);
            var thirdLayerString = TranslateToString(nextLayerIn);
            // thirdLayerString = "V<<A>>^A<A>AVA<^AA>A<VAAA>^A";
            var blah = thirdLayerString.Count();
            var nextNextLayerIn = TranslateUpALayer(thirdLayerString);
            var fourthLayerString = TranslateToString(nextNextLayerIn);
            */
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.LongsWithNegatives;
            long count = 0;

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var listOfDirs = new List<List<GridLocation<int>>>();

                long myTotal = 0;

                foreach (var c in line)
                {
                    goalLocationKeypad = keypadGrid.GetFirstLocationWhereCellEqualsValue(c);
                    var keyPadPaths = EveryKeyPadPath(_keypadLocation, goalLocationKeypad, keypadGrid);
                    _keypadLocation = goalLocationKeypad;
                    myTotal += ScorePossible(keyPadPaths, 1);
                }
                /*
                foreach (var c in line)
                {
                    goalLocationKeypad = keypadGrid.GetFirstLocationWhereCellEqualsValue(c);
                    var path2 = BestKeyPadPath(_keypadLocation, goalLocationKeypad, keypadGrid);
                    // var path = GetKeypadPath(goalLocationKeypad, _keypadLocation, NeighboursFuncKeypad, WeightFuncKeypad, GoalFuncKeypad);
                    listOfDirs.Add(path2);
                    _keypadLocation = goalLocationKeypad;
                }
                var secondLayerString = TranslateToString(listOfDirs);
                var nextLayerIn = TranslateUpALayer(secondLayerString, directionArrowGrid);
                var thirdLayerString = TranslateToString(nextLayerIn);
                // thirdLayerString = "V<<A>>^A<A>AVA<^AA>A<VAAA>^A";
                var blah = thirdLayerString.Count();
                var nextNextLayerIn = TranslateUpALayer(thirdLayerString, directionArrowGrid);
                var fourthLayerString = TranslateToString(nextNextLayerIn);

                var num1 = fourthLayerString.Count();
                var num2 = numbers[i];
                count += num1 * num2;*/
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

        private List<GridLocation<int>> GetKeypadPath(
            GridLocation<int> goalLocationKeypad,
            GridLocation<int> start,
            Func<GridLocation<int>, List<GridLocation<int>>> getNeighboursFunc,
            Func<GridLocation<int>, GridLocation<int>, int> getWeightFunc,
            Func<GridLocation<int>, bool> goalFunc)
        {
            var keyPadLookup = Dijkstra<GridLocation<int>>.SearchEverywhere(start, getNeighboursFunc, getWeightFunc, goalFunc);
            var path = keyPadLookup[goalLocationKeypad].Path;

            var offsetPath = new List<GridLocation<int>>();
            var previous = start;
            for (var i = 0; i < path.Count; i++)
            {
                offsetPath.Add(path[i] - previous);
                previous = path[i];
            }

            return offsetPath;
        }

        private List<GridLocation<int>> GetDirpadPath(
            GridLocation<int> goalLocationKeypad,
            GridLocation<int> start,
            Func<GridLocation<int>, List<GridLocation<int>>> getNeighboursFunc,
            Func<GridLocation<int>, GridLocation<int>, int> getWeightFunc,
            Func<GridLocation<int>, bool> goalFunc)
        {
            var keyPadLookup = Dijkstra<GridLocation<int>>.SearchEverywhere(start, getNeighboursFunc, getWeightFunc, goalFunc);
            var path = keyPadLookup[goalLocationKeypad].Path;

            var offsetPath = new List<GridLocation<int>>();
            var previous = start;
            for (var i = 0; i < path.Count; i++)
            {
                offsetPath.Add(path[i] - previous);
                previous = path[i];
            }

            return offsetPath;
        }

        private List<List<GridLocation<int>>> TranslateUpALayer(
            string currentInstructions,
            GridObject<char> dirGrid)
        {
            var nextInstructions = new List<List<GridLocation<int>>>();
            var currentLocation = _dirPadCharToLocation['A'];

            foreach (var chr in currentInstructions)
            {
                var newInstruction = new List<GridLocation<int>>();
                var targetLocation = _dirPadCharToLocation[chr];
                var pathToGetThere = _dirKeyPadPathLookup[currentLocation][targetLocation].Path;
                var diffPath = DirPathToDiff(currentLocation, pathToGetThere);
                var diffPath2 = BestDirPadPath(currentLocation, targetLocation, dirGrid);
                if (currentLocation == _dirPadCharToLocation['<'] && targetLocation == _dirPadCharToLocation['A'])
                {
                    diffPath = new List<GridLocation<int>>() { Directions.Right, Directions.Right, Directions.Up };
                }
                else if (currentLocation == _dirPadCharToLocation['A'] && targetLocation == _dirPadCharToLocation['<'])
                {
                    diffPath = new List<GridLocation<int>>() { Directions.Down, Directions.Left, Directions.Left };
                }
                if (!diffPath.SequenceEqual(diffPath2))
                {
                    Console.WriteLine("Error");
                }
                nextInstructions.Add(diffPath2);
                currentLocation = targetLocation;

                /*
                var pathToGetBack = _dirComingFrom[targetLocation];
                nextInstructions.Add(pathToGetBack);*/
            }
            return nextInstructions;
        }

        private List<GridLocation<int>> DirPathToDiff(GridLocation<int> start, List<GridLocation<int>> pathToGetThere)
        {
            var offsetPath = new List<GridLocation<int>>();
            var previous = start;
            for (var i = 0; i < pathToGetThere.Count; i++)
            {
                offsetPath.Add(pathToGetThere[i] - previous);
                previous = pathToGetThere[i];
            }
            return offsetPath;
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

        private string TranslateToString(List<List<GridLocation<int>>> diffInstructions)
        {
            var str = string.Empty;

            foreach (var line in diffInstructions)
            {
                foreach (var item in line)
                {
                    str += _difToChar[item];
                }
                str += 'A';
            }
            return str;
        }

        private List<GridLocation<int>> BestDirPadPath(
            GridLocation<int> start,
            GridLocation<int> end,
            GridObject<char> grid)
        {
            Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
            var fullPathHistory = new HashSet<string>();

            var bestVal = int.MaxValue;
            var best = new List<GridLocation<int>>();
            var bestPathLength = int.MaxValue;

            var dumbdict = new Dictionary<GridLocation<int>, int>()
            {
                { Directions.Left, 3},
                { Directions.Up, 1},
                { Directions.Down, 2},
                { Directions.Right, 1},
            };

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
                    fullPath.Count > 4) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                    continue;

                // make sure to add current state to history
                fullPathHistory.Add(stringy);

                if (currentLocation == end)
                {
                    if (fullPath.Count <= bestPathLength)
                    {
                        var offsetPath = DirPathToDiff2(fullPath);
                        var weighty = 0;
                        for (var i = 0; i < offsetPath.Count - 1; i++)
                        {
                            weighty += dumbdict[offsetPath[i]];
                            if (offsetPath[i] == offsetPath[i + 1])
                            {
                                weighty -= dumbdict[offsetPath[i]];
                            }
                        }
                        if (weighty < bestVal)
                        {
                            bestVal = weighty;
                            best = offsetPath;
                            bestPathLength = fullPath.Count;
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

        private List<GridLocation<int>> BestKeyPadPath(
            GridLocation<int> start,
            GridLocation<int> end,
            GridObject<char> grid)
        {
            Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
            var fullPathHistory = new HashSet<string>();

            var bestVal = int.MaxValue;
            var best = new List<GridLocation<int>>();
            var bestPathLength = int.MaxValue;

            var dumbdict = new Dictionary<GridLocation<int>, int>()
            {
                { Directions.Left, 3},
                { Directions.Up, 1},
                { Directions.Down, 2},
                { Directions.Right, 1},
            };

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
                        var weighty = 0;
                        for (var i = 0; i < offsetPath.Count - 1; i++)
                        {
                            weighty += dumbdict[offsetPath[i]];
                            if (offsetPath[i] == offsetPath[i + 1])
                            {
                                weighty -= dumbdict[offsetPath[i]];
                            }
                        }
                        if (weighty < bestVal)
                        {
                            bestVal = weighty;
                            best = offsetPath;
                            bestPathLength = fullPath.Count;
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