using Microsoft.Z3;

namespace AdventLibrary.Examples
{
    internal class Z3examples
    {
        // This example is from 2024 Day 13
        // solve two unknowns between two equations
        public static void SolveLinearSystemExample(
            long x1mult,
            long y1mult,
            long x2mult,
            long y2mult,
            long xGoal,
            long yGoal)
        {
            /* This setup is solving the following system
             * x1mult * A + x2mult * B = xGoal
             * y1mult * A + y2mult * B = yGoal
             *
             * or the more concrete example
             * 94*A + 22*B = 8400
             * 34*A + 67*B = 5400
             *
             * The system may or may not have a solution.
             * */
            var context = new Context();
            var solver = context.MkSolver();

            // setup our constansts we are solving for
            // aka A and B from the above equation comment.
            var pressCountA = context.MkIntConst("a");
            var pressCountB = context.MkIntConst("b");

            // convert our needed values to Z3 values
            // aka 94, 34, 22, 67
            var xFromA = context.MkInt(x1mult);
            var yFromA = context.MkInt(y1mult);
            var xFromB = context.MkInt(x2mult);
            var yFromB = context.MkInt(y2mult);

            // Make the left side of the equations
            // x1mult * A + x2mult * B OR 94*A + 22*B
            // y1mult * A + y2mult * B OR 34*A + 67*B
            var xVal = context.MkAdd(context.MkMul(pressCountA, xFromA), context.MkMul(pressCountB, xFromB));
            var yVal = context.MkAdd(context.MkMul(pressCountA, yFromA), context.MkMul(pressCountB, yFromB));

            // make the right side
            // = xGoal OR = 8400
            // = yGoal OR = 5400
            var contextGoalX = context.MkInt(xGoal);
            var contextGoalY = context.MkInt(yGoal);

            // Tell it that left and right are equal
            // aka fully assemble the original equation
            solver.Add(context.MkEq(contextGoalX, xVal));
            solver.Add(context.MkEq(contextGoalY, yVal));

            // solve
            var status = solver.Check();

            // we only care about when reaching the goal point is possible
            if (status.Equals(Status.SATISFIABLE))
            {
                var model = solver.Model;

                // these are our answers and the values we were trying to find.
                var aPresses = long.Parse(model.Eval(pressCountA).ToString());
                var bPresses = long.Parse(model.Eval(pressCountB).ToString());
            }
        }

        /* a goal is a series of values for numeric counters
         * each button would raise the value of 1-? numeric counters
         * Each button is different and you need to find minimal # total button presses.
         * So each numeric counter is an equation where the expected value is one side and the button presses on the other.
         * ex (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
         * translate into the equations
         * 4const + 5const = 3
         * 1const + 5const = 5
         * 2const + 3const + 4const = 4
         * 0const + 1const + 3const = 7
         * then the system solves for all the consts. We want minimal sum of const values
         * */

        public static int Z3optimizeMinimum(List<int> goal, List<List<int>> buttons)
        {
            var context = new Context();
            // note I want an optimizer not a solver for this question
            var optimizer = context.MkOptimize();

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
                    // buttons are really a list of numeric counter indexes it adjusts
                    // button i affects equation j
                    var numericCounterIndex = buttons[i][j];
                    equationDict[numericCounterIndex].Add(i);
                }

                var buttonPressCountExpression = context.MkIntConst(i.ToString());
                buttonPressCountExpressions.Add(buttonPressCountExpression);

                // note this makes sure every button is pressed 0 or more times. Can't press a button negative times
                optimizer.Add(context.MkGe(buttonPressCountExpression, context.MkInt(0)));
            }

            for (var i = 0; i < goal.Count; i++)
            {
                var eq = context.MkAdd(equationDict[i].Select(x => buttonPressCountExpressions[x]));
                var eqValue = context.MkInt(goal[i]);
                optimizer.Add(context.MkEq(eqValue, eq));
            }

            // tell the optimizer we want to minimize the total sum of the const values
            var sumExpression = context.MkAdd(buttonPressCountExpressions);
            optimizer.MkMinimize(sumExpression);

            // solve
            var status = optimizer.Check();

            // we only care about when reaching the goal point is possible
            if (status.Equals(Status.SATISFIABLE))
            {
                var model = optimizer.Model;

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