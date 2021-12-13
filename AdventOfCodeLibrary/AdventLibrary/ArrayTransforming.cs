using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    public static class ArrayTransforming
    {
        public static List<T> ArrayToList<T>(T[,] array)
        {
            var list = new List<T>();
            for(var i = 0; i < array.GetLength(0); i++)
            {
                for(var j = 0; j < array.GetLength(1); j++)
                {
                    list.Add(array[i,j]);
                }
            }
            return list;
        }
    }

}