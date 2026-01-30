using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventLibrary.Helpers.Grids;

namespace AdventLibrary.Helpers
{
    public static class MathHelper
    {
        public static int GetOppositeIntBool(int input)
        {
            if (input == 0) return 1;
            return 0;
        }

        /// <summary>
        /// Gets middle of 0 & num, rounded DOWN.
        /// </summary>
        public static T GetMiddle<T>(T num)
            where T : INumber<T>
        {
            return GetMiddle(T.CreateChecked(0), num);
        }

        /// <summary>
        /// Gets middle of 0 & num, rounded UP.
        /// </summary>
        public static T GetMiddleRoundedUp<T>(T num)
            where T : INumber<T>
        {
            return GetMiddleRoundedUp(T.CreateChecked(0), num);
        }

        /// <summary>
        /// Gets middle of two numbers, rounded DOWN.
        /// </summary>
        public static T GetMiddle<T>(T num1, T num2)
            where T : INumber<T>
        {
            T low = T.MinNumber(num1, num2);
            T high = T.MaxNumber(num1, num2);
            return low + ((high - low) / T.CreateChecked(2));
        }

        /// <summary>
        /// Gets middle of two numbers, rounded UP.
        /// </summary>
        public static T GetMiddleRoundedUp<T>(T num1, T num2)
            where T : INumber<T>
        {
            if (num1 is float or double)
            {
                throw new NotSupportedException();
            }
            T low = T.MinNumber(num1, num2);
            T high = T.MaxNumber(num1, num2);
            return low + (((high - low) + T.CreateChecked(1)) / T.CreateChecked(2));
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

        public static int ChineseRemainderTheorem(List<int> n, List<int> a)
        {
            return (int)ChineseRemainderTheorem(n.Select(x => (long)x).ToList(), a.Select(x => (long)x).ToList());
        }

        /* Format usually something like
         *  x ≡ rem ( mod val)
         *  x ≡ 3 (mod 5)
            x ≡ 2 (mod 7)
            x ≡ 5 (mod 9)
            so in this case I would call ChineseRemainderTheorem({5,7,9}, {3,2,5})
        */

        public static long ChineseRemainderTheorem(List<long> values, List<long> remainders)
        {
            long prod = values.Aggregate(1, (long i, long j) => i * j);
            long divResult;
            long sum = 0;
            for (var i = 0; i < values.Count; i++)
            {
                divResult = prod / values[i];
                sum += remainders[i] * ModularMultiplicativeInverse(divResult, values[i]) * divResult;
            }
            return sum % prod;
        }

        private static long ModularMultiplicativeInverse(long num, long modValue)
        {
            long remainder = num % modValue;
            for (long i = 1; i < modValue; i++)
            {
                if ((remainder * i) % modValue == 1)
                {
                    return i;
                }
            }
            return 1;
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

        public static int BinarySearchExample()
        {
            var fakeList = new List<int>();
            var min = 0;
            var max = 1024;
            var goal = 200;
            var index = -1;
            while (index == -1)
            {
                var mid = MathHelper.GetMiddle(min, max);
                var midValue = fakeList[mid];
                if (goal < midValue)
                {
                    max = mid;
                }
                else if (midValue < goal)
                {
                    min = mid;
                }
                else if (mid == goal)
                {
                    index = mid;
                }
            }
            return index;
        }

        public static int BinarySearchFindHighestIndexWhereTrue()
        {
            var fakeList = new List<int>();
            var min = 0;
            var max = 1024;
            while (min < max)
            {
                if (max - min == 1)
                {
                    // if you wanted lowest index where false, return max.
                    return min;
                }
                var mid = MathHelper.GetMiddle(min, max);
                var midValue = fakeList[mid] == 1;
                if (midValue)
                {
                    min = mid;
                }
                else
                {
                    max = mid;
                }
            }
            return min;
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        /// <summary>
        /// Finds if value is in the range, sorts range values automaticaly
        /// </summary>
        public static bool InRange_Inclusive<T>(T range1, T range2, T value)
            where T : INumber<T>
        {
            var min = T.MinNumber(range1, range2);
            var max = T.MaxNumber(range1, range2);

            if (value >= min && value <= max)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finds if value is in the range, sorts range values automaticaly
        /// </summary>
        public static bool InRange_Inclusive<T>((T range1, T range2) range, T value)
            where T : INumber<T>
        {
            return InRange_Inclusive(range.range1, range.range2, value);
        }

        /// <summary>
        /// Finds if both values are within the range. All values sorted automatically.
        /// </summary>
        public static bool InRange_Inclusive<T>(T range1, T range2, T value1, T value2)
            where T : INumber<T>
        {
            var minRange = T.MinNumber(range1, range2);
            var maxRange = T.MaxNumber(range1, range2);

            var minValue = T.MinNumber(value1, value2);
            var maxValue = T.MaxNumber(value1, value2);

            if (minValue >= minRange && maxValue <= maxRange)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finds if both values are within the range. All values sorted automatically.
        /// </summary>
        public static bool InRange_Inclusive<T>((T range1, T range2) range, (T value1, T value2) value)
            where T : INumber<T>
        {
            return InRange_Inclusive(range.range1, range.range2, value.value1, value.value2);
        }

        /// <summary>
        /// Finds if value is in the range, sorts range values automaticaly
        /// </summary>
        public static bool InRange_Exclusive<T>(T range1, T range2, T value)
            where T : INumber<T>
        {
            var min = T.MinNumber(range1, range2);
            var max = T.MaxNumber(range1, range2);

            if (value > min && value < max)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finds if value is in the range, sorts range values automaticaly
        /// </summary>
        public static bool InRange_Exclusive<T>((T range1, T range2) range, T value)
            where T : INumber<T>
        {
            return InRange_Exclusive(range.range1, range.range2, value);
        }
    }
}