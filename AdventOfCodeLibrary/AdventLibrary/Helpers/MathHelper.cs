using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using AdventLibrary.Helpers.Grids;
using Microsoft.Z3;

namespace AdventLibrary.Helpers
{
    public static class MathHelper
    {
        public static int GetMiddle(int left, int right)
        {
            return left + ((right - left) / 2);
        }

        public static int LCM(IEnumerable<int> numbers)
        {
            return numbers.Aggregate((S, val) => S * val / LCM(S, val));
        }

        public static long LCM(IEnumerable<long> numbers)
        {
            return numbers.Aggregate((S, val) => S * val / LCM(S, val));
        }

        // source: https://www.w3resource.com/csharp-exercises/math/csharp-math-exercise-20.php
        public static int LCM(int n1, int n2)
        {
            if (n2 == 0)
            {
                return n1;
            }
            else
            {
                return LCM(n2, n1 % n2);
            }
        }

        public static long LCM(long n1, long n2)
        {
            if (n2 == 0)
            {
                return n1;
            }
            else
            {
                return LCM(n2, n1 % n2);
            }
        }

        public static int GCD(List<int> numbers)
        {
            int gcd = numbers[0];
            for (var i = 1; i < numbers.Count; i++)
            {
                gcd = GCD(numbers[i], gcd);
            }
            return gcd;
        }

        public static long GCD(List<long> numbers)
        {
            long gcd = numbers[0];
            for (var i = 1; i < numbers.Count; i++)
            {
                gcd = GCD(numbers[i], gcd);
            }
            return gcd;
        }

        public static int GCD(int a, int b)
        {
            return (int)GCD((uint)a, (uint)b);
        }

        public static long GCD(long a, long b)
        {
            return (long)GCD((ulong)a, (ulong)b);
        }

        public static uint GCD(uint a, uint b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public static ulong GCD(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public static (int, int) Fibonacci(int n)
        {
            if (n == 0)
                return (0, 1);

            var p = Fibonacci(n >> 1);
            int c = p.Item1 * (2 * p.Item2 - p.Item1);
            int d = p.Item1 * p.Item1 + p.Item2 * p.Item2;
            if ((n & 1) == 1)
            {
                return (d, c + d);
            }
            else
            {
                return (c, d);
            }
        }

        public static double ShoelaceArea(List<GridLocation<int>> points)
        {
            int n = points.Count;
            double a = 0.0;
            for (int i = 0; i < n - 1; i++)
            {
                a += points[i].Y * points[i + 1].X - points[i + 1].Y * points[i].X;
            }
            return Math.Abs(a + points[n - 1].Y * points[0].X - points[0].Y * points[n - 1].X) / 2.0;
        }

        /* Calculate the area of a simple polygon using a set of coords
         * https://en.wikipedia.org/wiki/Shoelace_formula
         * */

        public static long ShoelaceArea(List<GridLocation<long>> points)
        {
            int n = points.Count;
            long a = 0;
            for (int i = 0; i < n - 1; i++)
            {
                a += points[i].Y * points[i + 1].X - points[i + 1].Y * points[i].X;
            }
            return Math.Abs(a + points[n - 1].Y * points[0].X - points[0].Y * points[n - 1].X) / 2;
        }

        public static double PicksAndShoelaceArea(List<GridLocation<int>> points)
        {
            long edgeLength = 1;
            var previous = points[^1];
            foreach (var item in points)
            {
                edgeLength += Math.Abs(previous.Y - item.Y) + Math.Abs(previous.X - item.X);
                previous = item;
            }
            var area = ShoelaceArea(points);

            // https://en.wikipedia.org/wiki/Pick%27s_theorem
            // area + "circumference" /2 + 1 = total area for this case
            return area + edgeLength / 2 + 1;
        }

        public static double PythagoreanTheorem<T>(T a, T b) where T : INumber<T>
        {
            var c2 = a * a + b * b;
            return Math.Sqrt(Convert.ToDouble(c2));
        }

        public static double GetFirstHalfDigits(double number)
        {
            var digitsLength = GetNumberOfDigits(number);
            if (digitsLength % 2 == 0)
            {
                return Math.Floor(number / Math.Pow(10, digitsLength / 2));
            }
            else
            {
                return Math.Floor(number / Math.Pow(10, digitsLength / 2));
            }
        }

        // Only works for whole numbers, is a double for ease of using math tools
        public static double GetNumberOfDigits(double number)
        {
            if (number == 0) return 1;
            return Math.Floor(Math.Log10(number)) + 1;
        }

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
    }
}