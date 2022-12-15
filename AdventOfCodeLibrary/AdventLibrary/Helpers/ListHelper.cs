using System;
using System.Collections.Generic;

namespace AdventLibrary.Helpers
{
    public static class ListHelper
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
    }
}