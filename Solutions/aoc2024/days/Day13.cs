using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.Z3;

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
                var result2 = GoTime2(
                    new GridLocation<long>(aNums[0], aNums[1]),
                    new GridLocation<long>(bNums[0], bNums[1]),
                    new GridLocation<long>(goalNums[0], goalNums[1]));
                if (result != int.MaxValue)
                {
                    count += result;
                }
            }
            return (long)count;
        }

        private long GoTime2(
            GridLocation<long> aButton,
            GridLocation<long> bButton,
            GridLocation<long> goal)
        {
            var context = new Context();
            var solver = context.MkSolver();

            var pressCountA = context.MkIntConst("a");
            var pressCountB = context.MkIntConst("b");

            var xFromA = context.MkInt(aButton.X);
            var yFromA = context.MkInt(aButton.Y);
            var xFromB = context.MkInt(bButton.X);
            var yFromB = context.MkInt(bButton.Y);

            var xVal = context.MkAdd(context.MkMul(pressCountA, xFromA), context.MkMul(pressCountB, xFromB));
            var yVal = context.MkAdd(context.MkMul(pressCountA, yFromA), context.MkMul(pressCountB, yFromB));

            var contextGoalX = context.MkInt(goal.X);
            var contextGoalY = context.MkInt(goal.Y);
            solver.Add(context.MkEq(contextGoalX, xVal));
            solver.Add(context.MkEq(contextGoalY, yVal));


            var status = solver.Check();
            if (status.Equals(Status.SATISFIABLE))
            {
                var model = solver.Model;

                var aPresses = long.Parse(model.Eval(pressCountA).ToString());
                var bPresses = long.Parse(model.Eval(pressCountB).ToString());
                return aPresses * 3 + bPresses;
            }

            return int.MaxValue;
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