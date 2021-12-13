using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    // Can I just extend list and add these?
    public static class ListTransforming
    {
        public static List<T> AllExceptFirstItem<T>(this List<T> list)
        {
            return list.GetRange(1, list.Count - 1);
        }

        public static T LastItem<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static List<T> RotateListLeft<T>(this List<T> list, int n)
        {
            var rotatedList = new List<T>();
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    rotatedList.Add(list[i+1]);
                }
                rotatedList.Add(list[0]);
            }
            return rotatedList;
        }

        public static List<T> RotateListRight<T>(this List<T> list, int n)
        {
            var rotatedList = new List<T>();
            for (int i = 0; i < n; i++)
            {
                rotatedList.Insert(0, list[list.Count - 1]);
                list.RemoveAt(list.Count - 1);
            }
            return rotatedList;
        }
    }

}