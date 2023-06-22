using AdventLibrary;
using AdventLibrary.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    public class Day13: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }
        private int[,] _arr;

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            _arr = new int[8, 8];
            var guests = new List<int>();
			var lookup2 = new Dictionary<string, int>();
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                var value = nums[0];

                if (tokens[2].Equals("lose"))
                {
                    value *= -1;
                }

                lookup2.TryAdd(tokens[0], lookup2.Count);
                lookup2.TryAdd(tokens[10], lookup2.Count);

                var current = lookup2[tokens[0]];

                if (!guests.Contains(current))
                {
                    guests.Add(current);
                }
                var target = lookup2[tokens[10]];
                _arr[current, target] = value;
            }


            Func<List<List<int>>, List<int>> func = (results) =>
            {
                List<int> best = results[0];
                var bestCount = 0;
                foreach (var result in results)
                {
                    var total = TotalHappiness(result);
                    if (total > bestCount)
                    {
                        best = result;
                        bestCount = total;
                    }
                }
                return best;
            };

            var starting = guests.ToList();
            starting.Remove(guests[0]);
            var bestList = BreadthFirstSearch.BFS(starting, new List<int>() { guests[0] }, func);
            return TotalHappiness(bestList);
        }

        private int TotalHappiness(List<int> people)
        {
            var total = 0;
            for (var i = 0; i < people.Count; i++)
            {
                var neighbours = people.GetNeighbours(i);
                var guest1 = people[i];
                var guest2 = people[neighbours.Item1];
                var guest3 = people[neighbours.Item2];
                total += _arr[guest1, guest2] + _arr[guest1, guest3];
            }
            return total;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            _arr = new int[9, 9];
            var guests = new List<int>();
            var lookup2 = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars);
                var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                var value = nums[0];

                if (tokens[2].Equals("lose"))
                {
                    value *= -1;
                }
                lookup2.TryAdd(tokens[0], lookup2.Count);
                lookup2.TryAdd(tokens[10], lookup2.Count);
                var current = lookup2[tokens[0]];

                if (!guests.Contains(current))
                {
                    guests.Add(current);
                }
                var target = lookup2[tokens[10]];
                _arr[current, target] = value;
            }


            Func<List<List<int>>, List<int>> func = (results) =>
            {
                List<int> best = results[0];
                var bestCount = 0;
                foreach (var result in results)
                {
                    var total = TotalHappiness(result);
                    if (total > bestCount)
                    {
                        best = result;
                        bestCount = total;
                    }
                }
                return best;
            };

            var bestList = BreadthFirstSearch.BFS(guests, new List<int>() { 8 }, func);
            return TotalHappiness(bestList);
        }
    }
}