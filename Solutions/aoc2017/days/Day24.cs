using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day24: ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<List<List<long>>, long> _bridges;
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
            var ports = new List<List<long>>();
            _bridges = new Dictionary<List<List<long>>, long>();

            foreach (var line in input.Lines)
            {
                ports.Add(StringParsing.GetLongsFromString(line));
            }

            Recursion(ports, new List<List<long>>(), 0);

            return _bridges.Values.Max();
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var ports = new List<List<long>>();
            _bridges = new Dictionary<List<List<long>>, long>();

            foreach (var line in input.Lines)
            {
                ports.Add(StringParsing.GetLongsFromString(line));
            }

            Recursion(ports, new List<List<long>>(), 0);

            var length =  _bridges.Keys.Max(x => x.Count);
            return _bridges.Max(x => x.Key.Count == length ? x.Value : 0);
        }

        private void Recursion(List<List<long>> availiblePorts, List<List<long>> history, long currentExposed)
        {
            for (var i = 0; i <  availiblePorts.Count; i++)
            {
                var port = availiblePorts[i];
                if (port[0] == currentExposed || port[1] == currentExposed)
                {
                    var newHistory = history.Clone();
                    newHistory.Add(port);
                    var newAvailiblePorts = availiblePorts.GetWithout(i);
                    if (port[0] == currentExposed)
                    {
                        Recursion(newAvailiblePorts, newHistory, port[1]);
                    }
                    else if (port[1] == currentExposed)
                    {
                        Recursion(newAvailiblePorts, newHistory, port[0]);
                    }
                }
            }
            long strength = 0;
            foreach (var piece in history)
            {
                foreach (var port in piece)
                {
                    strength += port;
                }
            }
            _bridges.Add(history, strength);
        }
    }
}