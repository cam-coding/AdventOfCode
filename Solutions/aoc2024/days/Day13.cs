using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace aoc2024
{
    public class Day13: ISolver
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
            var lines = input.Lines;
            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var grid = input.GridInt;

            double count = 0;

			foreach (var group in groups)
			{
                var i = 0;
                var currentLine = group[i];
                var toks = currentLine.GetRealTokens(delimiterChars);

                var aLine = group[0];
                var aNums = StringParsing.GetNumbersWithNegativesFromString(aLine);
                var aButton = new GridLocation<double>(aNums[0], aNums[1]);

                var bLine = group[1];
                var bNums = StringParsing.GetNumbersWithNegativesFromString(bLine);
                var bButton = new GridLocation<double>(bNums[0], bNums[1]);

                var cLine = group[2];
                var goalNums = StringParsing.GetNumbersWithNegativesFromString(cLine);
                var goal = new GridLocation<double>(goalNums[0], goalNums[1]);

                var result = GoTime(aButton, bButton, goal);
                if (result != int.MaxValue)
                {
                    count += result;
                }
            }
            return (long)count;
        }

        private double GoTime(GridLocation<double> aButton, GridLocation<double> bButton, GridLocation<double> goal)
        {
            var maxA = Math.Min(goal.X / aButton.X, goal.Y / aButton.Y);
            var maxB = Math.Min(goal.X / bButton.X, goal.Y / bButton.Y);

            var min = Math.Min(maxA, maxB);
            var max = Math.Max(maxA, maxB);

            var cost = int.MaxValue;
            var totalPresses = 0;

            var A = Matrix<double>.Build.DenseOfArray(new double[,] {
                                                        { aButton.X, bButton.X },
                                                        { aButton.Y, bButton.Y }
                                                    });
            var B = Vector<double>.Build.Dense(new double[] { goal.X, goal.Y});
            var x = A.Solve(B);
            var vals = x.Norm(2);
            var blah = Math.Round(x.First());
            var blah2 = Math.Round(x.Last());

            long answer = 0;
            var answerLoc = new GridLocation<double>(blah, blah2);
            var actual = (aButton * blah) + (bButton * blah2);
            if (actual == goal)
            {
                return blah * 3 + blah2;
            }

            return int.MaxValue;
            /*
            for (var b = 0; b < 101; b++)
            {
                for (var a = 0; a < 101; a++)
                {
                    var actual = (aButton * a) + (bButton * b);
                    if (actual.CompareTo(goal) > 0)
                    {
                        break;
                    }
                    if (actual == goal)
                    {
                        var thisCost = a * 3 + b;
                        cost = Math.Min(cost, thisCost);
                    }
                }
            }
            return cost;*/

        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var grid = input.GridInt;
            var magic = 10000000000000;

            double count = 0;

            foreach (var group in groups)
            {
                var i = 0;
                var currentLine = group[i];
                var toks = currentLine.GetRealTokens(delimiterChars);

                var aLine = group[0];
                var aNums = StringParsing.GetNumbersWithNegativesFromString(aLine);
                var aButton = new GridLocation<double>(aNums[0], aNums[1]);

                var bLine = group[1];
                var bNums = StringParsing.GetNumbersWithNegativesFromString(bLine);
                var bButton = new GridLocation<double>(bNums[0], bNums[1]);

                var cLine = group[2];
                var goalNums = StringParsing.GetNumbersWithNegativesFromString(cLine);
                var goal = new GridLocation<double>(goalNums[0] + magic, goalNums[1] + magic);

                var result = GoTime(aButton, bButton, goal);
                if (result != int.MaxValue)
                {
                    count += result;
                }
            }
            return count;
        }
    }
}