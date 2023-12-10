using AdventLibrary;
using System.Collections.Generic;
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

        public struct Congruence
        {
            long a, m;
        };
    }
}
