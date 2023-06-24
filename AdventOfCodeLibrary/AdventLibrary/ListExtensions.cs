using System;
using System.Collections.Generic;
using System.Linq;

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

        // hard to decipher, here are clearer ones https://rosettacode.org/wiki/Combinations#C.2B.2B
        // 1,2,3 -> (1,2) (1,3) (2,3)
        public static IEnumerable<IEnumerable<T>> GetKCombinations<T>(this List<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetKCombinations(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        // 1,2 -> (1,2) (2,1)
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> values) where T : IComparable<T>
        {
            if (values.Count() == 1)
                return new[] { values };
            return values.SelectMany(v => GetPermutations(values.Where(x => x.CompareTo(v) != 0)), (v, p) => p.Prepend(v));
        }

        public static List<List<T>> GenerateCombinationsWithRepetition<T>(this List<T> combinationList, int k)
        {
            var combinations = new List<List<T>>();

            if (k == 0)
            {
                var emptyCombination = new List<T>();
                combinations.Add(emptyCombination);

                return combinations;
            }

            if (combinationList.Count == 0)
            {
                return combinations;
            }

            T head = combinationList[0];
            var copiedCombinationList = new List<T>(combinationList);

            List<List<T>> subcombinations = GenerateCombinationsWithRepetition(copiedCombinationList, k - 1);

            foreach (var subcombination in subcombinations)
            {
                subcombination.Insert(0, head);
                combinations.Add(subcombination);
            }

            combinationList.RemoveAt(0);
            combinations.AddRange(GenerateCombinationsWithRepetition(combinationList, k));

            return combinations;
        }
    }

}