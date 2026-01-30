using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using Microsoft.Z3;

namespace aoc2025
{
    public class Day10 : ISolver
    {
        private string _filePath;

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
            long count = 0;
            char[] _delimiterChars2 = { '[', ']', '(', ')', '{', '}', ' ', '\t' };

            foreach (var line in lines)
            {
                var tokens = line.GetRealTokens(_delimiterChars2).ToList();
                var lightsGoal = tokens[0];
                var buttons = new List<List<int>>();
                for (var i = 1; i < tokens.Count - 1; i++)
                {
                    var nums = tokens[i].GetIntsFromString();
                    buttons.Add(nums);
                }
                var lights = CreateLights(lightsGoal);

                var min = int.MaxValue;
                for (var i = 0; i < buttons.Count; i++)
                {
                    if (min != int.MaxValue)
                    {
                        break;
                    }
                    var combos = buttons.Clone().GetCombinationsSizeNWithRepetition(i).ToList();

                    foreach (var combo in combos)
                    {
                        var currentLights = lights.Clone();

                        foreach (var buttonInputs in combo)
                        {
                            foreach (var num in buttonInputs)
                            {
                                currentLights[num] = !currentLights[num];
                            }
                        }
                        if (IsGoal(lightsGoal, currentLights))
                        {
                            min = Math.Min(min, combo.Count);
                        }
                    }
                }

                if (min == int.MaxValue)
                {
                    throw new Exception("need more");
                }

                count += min;
            }
            return count;
        }

        private bool IsGoal(string lightsGoal, List<bool> lights)
        {
            for (var i = 0; i < lights.Count; i++)
            {
                var lightOn = lightsGoal[i] == '#';
                if (lightOn != lights[i])
                {
                    return false;
                }
            }
            return true;
        }

        private List<bool> CreateLights(string lightsGoal)
        {
            var ret = new List<bool>();
            for (var i = 0; i < lightsGoal.Length; i++)
            {
                ret.Add(false);
            }
            return ret;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            long count = 0;
            char[] _delimiterChars2 = { '[', ']', '(', ')', '{', '}', ' ', '\t' };

            foreach (var line in lines)
            {
                var tokens = line.GetRealTokens(_delimiterChars2).ToList();
                var joltageGoal = new List<int>();
                var buttons = new List<List<int>>();
                for (var i = 1; i < tokens.Count - 1; i++)
                {
                    var nums = tokens[i].GetIntsFromString();
                    buttons.Add(nums);
                }

                foreach (var num in tokens.Last().GetIntsFromString())
                {
                    joltageGoal.Add(num);
                }

                count += Z3actual(joltageGoal, buttons);
            }
            return count;
        }

        private int Z3actual(List<int> goal, List<List<int>> buttons)
        {
            var context = new Context();

            // note I want an optimizer not a solver for this question
            var solver = context.MkOptimize();

            // mapping equations to what buttons interact with them
            var equationDict = new Dictionary<int, List<int>>();
            for (var i = 0; i < goal.Count; i++)
            {
                equationDict.Add(i, new List<int>());
            }

            var buttonPressCountExpressions = new List<IntExpr>();
            for (var i = 0; i < buttons.Count; i++)
            {
                for (var j = 0; j < buttons[i].Count; j++)
                {
                    // button i affects equation j
                    equationDict[buttons[i][j]].Add(i);
                }

                var buttonPressCountExpression = context.MkIntConst(i.ToString());
                buttonPressCountExpressions.Add(buttonPressCountExpression);
                solver.Add(context.MkGe(buttonPressCountExpression, context.MkInt(0)));
            }

            var equations = new List<ArithExpr>();
            for (var i = 0; i < goal.Count; i++)
            {
                var eq = context.MkAdd(equationDict[i].Select(x => buttonPressCountExpressions[x]));
                var eqValue = context.MkInt(goal[i]);
                solver.Add(context.MkEq(eqValue, eq));
            }

            var sumExpression = context.MkAdd(buttonPressCountExpressions);
            solver.MkMinimize(sumExpression);

            // solve
            var status = solver.Check();

            // we only care about when reaching the goal point is possible
            if (status.Equals(Status.SATISFIABLE))
            {
                var model = solver.Model;

                var sum = 0;
                for (var i = 0; i < buttons.Count; i++)
                {
                    var myInt = int.Parse(model.Eval(buttonPressCountExpressions[i]).ToString());
                    sum += myInt;
                }

                return sum;
            }
            throw new Exception("something went wrong");
            return -1;
        }
    }
}