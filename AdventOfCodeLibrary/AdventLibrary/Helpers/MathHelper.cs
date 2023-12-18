using AdventLibrary;
using AdventLibrary.CustomObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace AdventLibrary.Helpers
{
    public static class MathHelper
    {
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

        public static double ShoelaceArea(List<LocationTuple<int>> points)
        {
            int n = points.Count;
            double a = 0.0;
            for (int i = 0; i < n - 1; i++)
            {
                a += points[i].Item1 * points[i + 1].Item2 - points[i + 1].Item1 * points[i].Item2;
            }
            return Math.Abs(a + points[n - 1].Item1 * points[0].Item2 - points[0].Item1 * points[n - 1].Item2) / 2.0;
        }

        /* Calculate the area of a simple polygon using a set of coords
         * https://en.wikipedia.org/wiki/Shoelace_formula
         * */
        public static long ShoelaceArea(List<LocationTuple<long>> points)
        {
            int n = points.Count;
            long a = 0;
            for (int i = 0; i < n - 1; i++)
            {
                a += points[i].Item1 * points[i + 1].Item2 - points[i + 1].Item1 * points[i].Item2;
            }
            return Math.Abs(a + points[n - 1].Item1 * points[0].Item2 - points[0].Item1 * points[n - 1].Item2) / 2;
        }

        public static long PicksAndShoelaceArea(List<LocationTuple<long>> points)
        {
            long edgeLength = 1;
            var previous = points[^1];
            foreach (var item in points)
            {
                edgeLength += Math.Abs(previous.Item1 - item.Item1) + Math.Abs(previous.Item2 - item.Item2);
                previous = item;
            }
            var area = ShoelaceArea(points);

            // https://en.wikipedia.org/wiki/Pick%27s_theorem
            // area + "circumference" /2 + 1 = total area for this case
            return area + edgeLength / 2 + 1;
        }
    }
}
