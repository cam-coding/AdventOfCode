using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    public static class StringHelper
    {
        public static string ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}