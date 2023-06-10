using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    public static class StringHelper
    {
        public static bool AllItemsUnique<T>(List<T> input)
        {
            var tempDict = new Dictionary<T, int>();
            for (var i =0; i < input.Count; i++)
            {
                if (tempDict.ContainsKey(input[i]))
                {
                    return false;
                }
                
                tempDict.Add(input[i], 1);
            }

            return true;
        }

        public static string ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}