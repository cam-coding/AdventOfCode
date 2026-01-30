using System.Collections.Generic;
using System.Diagnostics;
using AdventLibrary;

namespace aoc2023
{
    public class Day02 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;

            foreach (var line in lines)
            {
                var valid = true;
                var game = line.Split(":");
                var gameNum = AdventLibrary.StringParsing.GetIntsFromString(game[0])[0];
                var games = game[1].Split(";");

                foreach (var gm in games)
                {
                    var maxDict = new Dictionary<string, int>()
                    {
                        { "green", 0 },
                        { "red", 0 },
                        { "blue", 0 },
                    };
                    var tokens = gm.Split(delimiterChars);
                    for (var j = 2; j < tokens.Length; j+=3)
                    {
                        var num = int.Parse(tokens[j-1]);
                        var colour = tokens[j];
                        maxDict[colour] = num;
                    }

                    if (maxDict["red"] > 12 || maxDict["green"] > 13 || maxDict["blue"] > 14)
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    counter += gameNum;
                }
            }
            return counter;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;

            foreach (var line in lines)
            {
                var maxDict = new Dictionary<string, int>()
                {
                    { "green", 0 },
                    { "red", 0 },
                    { "blue", 0 },
                };

                var game = line.Split(":");
                var gameNum = AdventLibrary.StringParsing.GetIntsFromString(game[0])[0];
                var games = game[1].Split(";");

                foreach (var gm in games)
                {
                    var tokens = gm.Split(delimiterChars);
                    for (var j = 2; j < tokens.Length; j += 3)
                    {
                        var num = int.Parse(tokens[j - 1]);
                        var colour = tokens[j];

                        if (num > maxDict[colour])
                        {
                            maxDict[colour] = num;
                        }
                    }
                }
                var product = maxDict["red"] * maxDict["green"] * maxDict["blue"];
                counter += product;
            }
            return counter;
        }
    }
}