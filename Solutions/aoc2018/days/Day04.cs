using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;

namespace aoc2018
{
    public class Day04 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

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
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            lines.Sort();

            var currentGuard = lines[0].GetIntsFromString().Last();
            var lastAsleep = 0;
            var isAsleep = false;

            var myDict = new Dictionary<int, List<int>>();

            for (var i = 0; i < lines.Count; i++)
            {
                var currentMinute = lines[i].GetIntsFromString()[4];
                if (lines[i].Contains("guard", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAsleep)
                    {
                        for (var j = lastAsleep; j < 60; j++)
                        {
                            myDict[currentGuard][j]++;
                        }
                    }
                    currentGuard = lines[i].GetIntsFromString().Last();
                    if (!myDict.ContainsKey(currentGuard))
                    {
                        var myList = new List<int>();
                        myList.FillEmptyListWithValue(0, 60);
                        myDict.Add(currentGuard, myList);
                    }
                    isAsleep = false;
                    lastAsleep = currentMinute;
                    if (lines[i].GetIntsFromString()[3] == 23)
                    {
                        lastAsleep = 0;
                    }
                }
                else
                {
                    if (lines[i].Contains("wake", StringComparison.OrdinalIgnoreCase) && isAsleep)
                    {
                        for (var j = lastAsleep; j < currentMinute; j++)
                        {
                            myDict[currentGuard][j]++;
                        }
                        isAsleep = false;
                    }
                    else if (lines[i].Contains("falls", StringComparison.OrdinalIgnoreCase) && !isAsleep)
                    {
                        isAsleep = true;
                        lastAsleep = currentMinute;
                    }
                }
            }

            var top = myDict.MaxBy(x => x.Value.Sum());
            var guard = top.Key;
            var minute = top.Value.IndexOf(top.Value.Max());

            return guard * minute;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            lines.Sort();

            var currentGuard = lines[0].GetIntsFromString().Last();
            var lastAsleep = 0;
            var isAsleep = false;

            var myDict = new Dictionary<int, List<int>>();

            for (var i = 0; i < lines.Count; i++)
            {
                var currentMinute = lines[i].GetIntsFromString()[4];
                if (lines[i].Contains("guard", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAsleep)
                    {
                        for (var j = lastAsleep; j < 60; j++)
                        {
                            myDict[currentGuard][j]++;
                        }
                    }
                    currentGuard = lines[i].GetIntsFromString().Last();
                    if (!myDict.ContainsKey(currentGuard))
                    {
                        var myList = new List<int>();
                        myList.FillEmptyListWithValue(0, 60);
                        myDict.Add(currentGuard, myList);
                    }
                    isAsleep = false;
                    lastAsleep = currentMinute;
                    if (lines[i].GetIntsFromString()[3] == 23)
                    {
                        lastAsleep = 0;
                    }
                }
                else
                {
                    if (lines[i].Contains("wake", StringComparison.OrdinalIgnoreCase) && isAsleep)
                    {
                        for (var j = lastAsleep; j < currentMinute; j++)
                        {
                            myDict[currentGuard][j]++;
                        }
                        isAsleep = false;
                    }
                    else if (lines[i].Contains("falls", StringComparison.OrdinalIgnoreCase) && !isAsleep)
                    {
                        isAsleep = true;
                        lastAsleep = currentMinute;
                    }
                }
            }

            var max2 = 0;
            var id2 = 0;
            foreach (var pair in myDict)
            {
                foreach (var num in pair.Value)
                {
                    if (num > max2)
                    {
                        max2 = num;
                        id2 = pair.Key;
                    }
                }
            }

            return id2 * myDict[id2].IndexOf(max2);
        }
    }
}