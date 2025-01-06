using System;

namespace AdventLibrary.Helpers
{
    public static class ConversionHelper
    {
        public static int ConvertToHex(this string str)
        {
            var index = str.IndexOf('#');
            if (index != -1)
            {
                var length = str.Length - index - 1;
                return int.Parse(str.Substring(index + 1, length), System.Globalization.NumberStyles.HexNumber);
            }
            return int.Parse(str, System.Globalization.NumberStyles.HexNumber);
        }

        public static string ConvertToHex(this int value)
        {
            return value.ToString("X");
        }

        public static int ConvertFromHex(this string str)
        {
            return ConvertBaseToBase(str, 16);
        }

        public static string ConvertFromHex(this int num)
        {
            return ConvertBaseToBase(num, 16);
        }

        // converts to a specific length with leading zeroes
        public static string ConvertToHexString(this int value, int length)
        {
            return value.ToString($"X{length}");
        }

        public static int ConvertToInt(this string str)
        {
            return int.Parse(str);
        }

        public static int ConvertToBinary(this string str)
        {
            return ConvertBaseToBase(str, 2);
        }

        public static string ConvertToBinary(this int num)
        {
            return ConvertBaseToBase(num, 2);
        }

        public static int ConvertFromBinary(this string str)
        {
            return ConvertBaseToBase(str, 10);
        }

        public static string ConvertFromBinary(this int num)
        {
            return ConvertBaseToBase(num, 10);
        }

        public static int ConvertToBaseEight(this string str)
        {
            return ConvertBaseToBase(str, 8);
        }

        public static string ConvertToBaseEight(this int num)
        {
            return ConvertBaseToBase(num, 8);
        }

        public static int ConvertFromBaseEight(this string str)
        {
            return ConvertBaseToBase(str, 10);
        }

        public static string ConvertFromBaseEight(this int num)
        {
            return ConvertBaseToBase(num, 10);
        }

        public static int ConvertToDecimal(this string str)
        {
            return ConvertBaseToBase(str, 10);
        }

        public static string ConvertToDecimal(this int num)
        {
            return ConvertBaseToBase(num, 10);
        }

        //ex (16, 10, "100") = "256"
        // support 2,8,10,16
        public static string ConvertBaseToBase(int fromBase, int toBase, string number)
        {
            return Convert.ToString(Convert.ToInt32(number, fromBase), toBase);
        }

        public static string ConvertBaseToBase(int number, int toBase)
        {
            return Convert.ToString(number, toBase);
        }

        public static int ConvertBaseToBase(string number, int toBase)
        {
            return Convert.ToInt32(number, toBase);
        }
    }
}
