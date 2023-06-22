using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    // Extending base list class with some helper methods
    public static class ListExtensions
    {
        public static List<T> AllExceptFirstItem<T>(this List<T> list)
        {
            return list.GetRange(1, list.Count - 1);
        }

        public static T LastItem<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static bool AllItemsUnique<T>(this IList<T> input)
        {
            var tempDict = new HashSet<T>();
            foreach (var item in input)
            {
                if (tempDict.Contains(item))
                {
                    return false;
                }

                tempDict.Add(item);
            }

            return true;
        }

        public static List<List<T>> Clone2dList<T>(this List<List<T>> original)
        {
            var listy2d = new List<List<T>>();
            for (var y = 0; y < original.Count; y++)
            {
                var listy = new List<T>();
                for (var x = 0; x < original[y].Count; x++)
                {
                    listy.Add(original[y][x]);
                }
                listy2d.Add(listy);
            }
            
            return listy2d;
        }

        public static List<T> RotateListLeft<T>(this List<T> list, int n = 1)
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

        public static List<T> RotateListRight<T>(this List<T> list, int n = 1)
        {
            var rotatedList = new List<T>();
            for (int i = 0; i < n; i++)
            {
                rotatedList.Insert(0, list[list.Count - 1]);
                list.RemoveAt(list.Count - 1);
            }
            return rotatedList;
        }

        public static (int,int) GetNeighbours<T>(this List<T> list, int index)
        {
            var lastIndex = list.Count - 1;
            if (index == 0)
            {
                return (lastIndex, 1);
            }
            else if (index == lastIndex)
            {
                return (lastIndex - 1, 0);
            }
            else
            {
                return (index - 1, index + 1);
            }
        }
    }

}