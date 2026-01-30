using AdventLibrary;
using AdventLibrary.Helpers.Grids;
using Microsoft.Z3;

namespace aoc2024
{
    public class Day13 : ISolver
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
            var magic = 0;

            return Setup(input, magic);
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var magic = 10000000000000;

            return Setup(input, magic);
        }

        private object Setup(InputObjectCollection input, long magic)
        {
            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var grid = input.GridInt;
            long count = 0;

            foreach (var group in groups)
            {
                var i = 0;
                var currentLine = group[i];
                var toks = currentLine.GetRealTokens(delimiterChars);

                var aLine = group[0];
                var aNums = StringParsing.GetIntssWithNegativesFromString(aLine);
                var aButton = new GridLocation<long>(aNums[0], aNums[1]);

                var bLine = group[1];
                var bNums = StringParsing.GetIntssWithNegativesFromString(bLine);
                var bButton = new GridLocation<long>(bNums[0], bNums[1]);

                var cLine = group[2];
                var goalNums = StringParsing.GetIntssWithNegativesFromString(cLine);
                var goal = new GridLocation<long>(goalNums[0] + magic, goalNums[1] + magic);

                var result = SolveLinearSystem(aButton, bButton, goal);
                if (result != int.MaxValue)
                {
                    count += result;
                }
            }
            return count;
        }

        private long SolveLinearSystem(
            GridLocation<long> aButton,
            GridLocation<long> bButton,
            GridLocation<long> goal)
        {
            var context = new Context();
            var solver = context.MkSolver();

            // setup our constansts we are solving for
            var pressCountA = context.MkIntConst("a");
            var pressCountB = context.MkIntConst("b");

            // convert our needed values to Z3 values
            var xFromA = context.MkInt(aButton.X);
            var yFromA = context.MkInt(aButton.Y);
            var xFromB = context.MkInt(bButton.X);
            var yFromB = context.MkInt(bButton.Y);

            // Make the left side of the equations
            var xVal = context.MkAdd(context.MkMul(pressCountA, xFromA), context.MkMul(pressCountB, xFromB));
            var yVal = context.MkAdd(context.MkMul(pressCountA, yFromA), context.MkMul(pressCountB, yFromB));

            // make the right side
            var contextGoalX = context.MkInt(goal.X);
            var contextGoalY = context.MkInt(goal.Y);

            // Tell it that left and right are equal
            solver.Add(context.MkEq(contextGoalX, xVal));
            solver.Add(context.MkEq(contextGoalY, yVal));

            // solve
            var status = solver.Check();

            // we only care about when reaching the goal point is possible
            if (status.Equals(Status.SATISFIABLE))
            {
                var model = solver.Model;

                var aPresses = long.Parse(model.Eval(pressCountA).ToString());
                var bPresses = long.Parse(model.Eval(pressCountB).ToString());
                return aPresses * 3 + bPresses;
            }

            return int.MaxValue;
        }
    }
}