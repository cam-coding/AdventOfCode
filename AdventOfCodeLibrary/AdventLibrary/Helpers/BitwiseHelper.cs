using System;

namespace AdventLibrary.Helpers
{
    public class BitwiseHelper
    {
        public static int GetBit(int number, int position)
        {
            return (number & (1 << position)) >> position;
        }

        public static int SetBit(int number, int position)
        {
            return number | (1 << position);
        }

        public static int ClearBit(int number, int position)
        {
            return number & ~(1 << position);
        }

        public static int ToggleBit(int number, int position)
        {
            return number ^ (1 << position);
        }

        public static string ConvertToBinary(int number)
        {
            return Convert.ToString(number, 2);
        }

        public static string ConvertToBinary(uint number)
        {
            return Convert.ToString(number, 2);
        }

        public static string ConvertToBinary(long number)
        {
            return Convert.ToString(number, 2);
        }

        public static int AND(int num1, int num2)
        {
            return num1 & num2;
        }

        public static uint AND(uint num1, uint num2)
        {
            return num1 & num2;
        }

        public static long AND(long num1, long num2)
        {
            return num1 & num2;
        }

        public static ulong AND(ulong num1, ulong num2)
        {
            return num1 & num2;
        }

        public static int OR(int num1, int num2)
        {
            return num1 | num2;
        }

        public static uint OR(uint num1, uint num2)
        {
            return num1 | num2;
        }

        public static long OR(long num1, long num2)
        {
            return num1 | num2;
        }

        public static ulong OR(ulong num1, ulong num2)
        {
            return num1 | num2;
        }

        public static int NOT(int num)
        {
            return ~num;
        }

        public static uint NOT(uint num)
        {
            return ~num;
        }

        public static long NOT(long num)
        {
            return ~num;
        }

        public static ulong NOT(ulong num)
        {
            return ~num;
        }

        public static int XOR(int num1, int num2)
        {
            return num1 ^ num2;
        }

        public static uint XOR(uint num1, uint num2)
        {
            return num1 ^ num2;
        }

        public static long XOR(long num1, long num2)
        {
            return num1 ^ num2;
        }

        public static ulong XOR(ulong num1, ulong num2)
        {
            return num1 ^ num2;
        }

        public static int NOR(int num1, int num2)
        {
            return ~(num1 | num2);
        }

        public static uint NOR(uint num1, uint num2)
        {
            return ~(num1 | num2);
        }

        public static long NOR(long num1, long num2)
        {
            return ~(num1 | num2);
        }

        public static ulong NOR(ulong num1, ulong num2)
        {
            return ~(num1 | num2);
        }

        public static int ShiftLeft(int num, int shift)
        {
            return num << shift;
        }

        public static uint ShiftLeft(uint num, int shift)
        {
            return num << shift;
        }

        public static long ShiftLeft(long num, int shift)
        {
            return num << shift;
        }

        public static ulong ShiftLeft(ulong num, int shift)
        {
            return num << shift;
        }

        public static int ShiftRight(int num, int shift)
        {
            return num >> shift;
        }

        public static uint ShiftRight(uint num, int shift)
        {
            return num >> shift;
        }

        public static long ShiftRight(long num, int shift)
        {
            return num >> shift;
        }

        public static ulong ShiftRight(ulong num, int shift)
        {
            return num >> shift;
        }
    }
}