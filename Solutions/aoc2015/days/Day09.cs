using AdventLibrary;
using AdventLibrary.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    public class Day09: ISolver
    {
        private string _filePath;
        private Dictionary<string, List<(string, int)>> _distances = new Dictionary<string, List<(string, int)>>();
        private List<string> _listOfLocations = new List<string>();
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            Parse();
            Func<List<List<string>>, List<string>> func = (results) =>
            {
                List<string> best = results[0];
                var bestCount = int.MaxValue;
                foreach (var result in results)
                {
                    var total = TotalDistance(result);
                    if (total < bestCount)
                    {
                        best = result;
                        bestCount = total;
                    }
                }
                return best;
            };
            var result = BreadthFirstSearch.BFS(_listOfLocations, new List<string> { "dummy" }, func);
            var total = TotalDistance(result);
            return total;
        }

        private object Part2()
        {
            Parse();
            Func<List<List<string>>, List<string>> func = (results) =>
            {
                List<string> best = results[0];
                var bestCount = 0;
                foreach (var result in results)
                {
                    var total = TotalDistance(result);
                    if (total > bestCount)
                    {
                        best = result;
                        bestCount = total;
                    }
                }
                return best;
            };
            var result = BreadthFirstSearch.BFS(_listOfLocations, new List<string> { "dummy" }, func);
            var total = TotalDistance(result);
            return total;
        }

        private int TotalDistance(List<string> listy)
        {
            var total = 0;
            for (var i = 0; i < listy.Count - 1; i++)
            {
                total += _distances[listy[i]].First(x => x.Item1.Equals(listy[i + 1])).Item2;
            }
            return total;
        }

        private void Parse()
        {
            if (_distances.Count > 0)
            {
                return;
            }

            var lines = ParseInput.GetLinesFromFile(_filePath);

            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars);
                var distance = AdventLibrary.StringParsing.GetNumbersFromString(line)[0];
                AddToRecords(tokens[0], tokens[2], distance);
                AddToRecords(tokens[2], tokens[0], distance);
            }

            _distances.Add("dummy", new List<(string, int)>());
            foreach (var location in _listOfLocations)
            {
                _distances["dummy"].Add((location, 0));
            }
        }

        private void AddToRecords(string location1, string location2, int distance)
        {
            if (!_listOfLocations.Contains(location1))
            {
                _listOfLocations.Add(location1);
            }
            if (_distances.ContainsKey(location1))
            {
                _distances[location1].Add((location2, distance));
            }
            else
            {
                _distances.Add(location1, new List<(string, int)> { (location2, distance) });
            }
        }
    }
}