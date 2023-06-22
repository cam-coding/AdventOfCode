using AdventLibrary;
using AdventLibrary.PathFinding;
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
            return BFS2(_listOfLocations, new List<string> { "dummy" });
            //return BFS(_listOfLocations, "dummy", 0);
        }

        private object Part2()
        {
            Parse();
            return BFS2(_listOfLocations, new List<string> {  "dummy" }, false);
            //return BFS(_listOfLocations, "dummy", 0, false);
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

        private int BFS(List<string> remaining, string current, int distance, bool min = true)
        {
            if (remaining.Count == 0)
            {
                return distance;
            }
            var results = new List<int>();
            foreach (var location in remaining)
            {
                var newRemaining = remaining.ToList();
                newRemaining.Remove(location);
                var returnVal = BFS(newRemaining, location, distance + _distances[current].First(x => x.Item1.Equals(location)).Item2, min);
                results.Add(returnVal);
            }
            return min ? results.Min() : results.Max();
        }

        private int BFS2(List<string> remaining, List<string> current, bool min = true)
        {
            if (remaining.Count == 0)
            {
                var total = 0;

                for (var i = 0; i < current.Count - 1; i++)
                {
                    total += _distances[current[i]].First(x => x.Item1.Equals(current[i + 1])).Item2;
                }
                return total;
            }
            var results = new List<int>();
            foreach (var location in remaining)
            {
                var newRemaining = remaining.ToList();
                newRemaining.Remove(location);
                var newCurrent = current.ToList();
                newCurrent.Add(location);
                var returnVal = BFS2(newRemaining, newCurrent, min);
                results.Add(returnVal);
            }
            return min ? results.Min() : results.Max();
        }
    }
}