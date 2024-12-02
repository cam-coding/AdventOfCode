using AdventLibrary.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventLibrary.Extensions
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

        public static List<T> GetWithout<T>(this List<T> list, int index)
        {
            var except = list.Clone();
            except.RemoveAt(index);
            return except;
        }

        /* These 4/6 methods don't work yet and need unit tests*/
        public static bool IsSorted<T>(this List<T> list) where T : IComparable
        {
            var ascList = list.SortAscending();
            var descList = list.SortDescending();
            return list.SequenceEqual(descList) || list.SequenceEqual(ascList);
        }

        public static bool IsSortedOrEqual<T>(this List<T> list) where T : IComparable
        {
            return IsSortedAscendingOrEqual(list) || IsSortedDescendingOrEqual(list);
        }

        public static bool IsSortedDescending<T>(this List<T> list) where T : IComparable
        {
            var descending = true;
            for (var i = 0; i < list.Count - 1; i++)
            {
                if (list[i].CompareTo(list[i + 1]) < 0)
                {
                    descending = false;
                }
            }
            return descending;
        }

        public static bool IsSortedDescendingOrEqual<T>(this List<T> list) where T : IComparable
        {
            var descending = true;
            for (var i = 0; i < list.Count - 1; i++)
            {
                if (list[i].CompareTo(list[i + 1]) <= 0)
                {
                    descending = false;
                }
            }
            return descending;
        }

        public static bool IsSortedAscending<T>(this List<T> list) where T : IComparable
        {
            var ascending = true;
            for (var i = 0; i < list.Count-1; i++)
            {
                if (list[i].CompareTo(list[i + 1]) >= 0)
                {
                    ascending = false;
                }
            }
            return ascending;
        }

        public static bool IsSortedAscendingOrEqual<T>(this List<T> list) where T : IComparable
        {
            var ascending = true;
            for (var i = 0; i < list.Count - 1; i++)
            {
                if (list[i].CompareTo(list[i + 1]) > 0)
                {
                    ascending = false;
                }
            }
            return ascending;
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

        public static bool AllItemsSame<T>(this IList<T> input)
        {
            return input.Distinct().Count() == 1;
        }

        public static List<T> Clone<T>(this List<T> original)
        {
            var newList = new List<T>();
            for (var x = 0; x < original.Count; x++)
            {
                newList.Add(original[x]);
            }
            return newList;
        }

        public static List<T> SortAscending<T>(this List<T> list)
        {
            return list.OrderBy(x => x).ToList();
        }

        public static List<T> SortDescending<T>(this List<T> list)
        {
            return list.OrderByDescending(x => x).ToList();
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
        public static Dictionary<T, int> GetCountsOfItems<T>(this IList<T> input)
        {
            var dict = new Dictionary<T, int>();
            foreach (var item in input)
            {
                dict.TryAdd(item, 0);
                dict[item]++;
            }
            return dict;
        }

        public static List<T> RotateListLeft<T>(this List<T> list, int n = 1)
        {
            var rotatedList = new List<T>();
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    rotatedList.Add(list[i + 1]);
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

        public static (int, int) GetNeighbours<T>(this List<T> list, int index)
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
        // 1,2,3 and length 2 -> (1,2) (1,3) (2,3)
        public static IEnumerable<IEnumerable<T>> GetKCombinations<T>(this List<T> list, int length) where T : IComparable
        {
            if (length == 0) return new List<IEnumerable<T>>();
            if (length == 1) return list.Select(t => new T[] { t });
            return list.GetKCombinations(length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> Get0toKCombinations<T>(this List<T> list, int length) where T : IComparable
        {
            var result = new List<IEnumerable<T>>() { new List<T>() };
            for (int i = 1; i <= length; i++)
            {
                result.AddRange(list.GetKCombinations(i).ToList());
            }
            return result;
        }

        // 1,2 -> (1,2) (2,1)
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> values) where T : IComparable<T>
        {
            if (values.Count() == 1)
                return new[] { values };
            return values.SelectMany(v => GetPermutations(values.Where(x => x.CompareTo(v) != 0)), (v, p) => p.Prepend(v));
        }

        public static List<T> FillWithValue<T>(this List<T> list, T value)
        {
            for (var i = 0; i < list.Count(); i++)
            {
                list[i] = value;
            }
            return list;
        }

        public static List<T> FillEmptyListWithValue<T>(this List<T> list, T value, int count)
        {
            for (var i = 0; i < count; i++)
            {
                list.Add(value);
            }
            return list;
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

            List<List<T>> subcombinations = copiedCombinationList.GenerateCombinationsWithRepetition(k - 1);

            foreach (var subcombination in subcombinations)
            {
                subcombination.Insert(0, head);
                combinations.Add(subcombination);
            }

            combinationList.RemoveAt(0);
            combinations.AddRange(combinationList.GenerateCombinationsWithRepetition(k));

            return combinations;
        }

        public static List<string> GetRealStrings(this List<string> list)
        {
            return list.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }

        public static List<string> GetRealStrings(this List<string> list, char[] delimiterChars)
        {
            var delimiterStrings = delimiterChars.Select(x => x.ToString());
            return list.Where(x => !string.IsNullOrWhiteSpace(x) && !delimiterStrings.Any(y => y.Equals(x))).ToList();
        }

        public static int CountDifferences<T>(this List<T> list, List<T> list2)
        {
            if (list.Equals(list2))
            {
                return 0;
            }

            if (list.Count != list2.Count)
            {
                return -1;
            }

            var count = 0;
            for (var i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals(list2[i]))
                {
                    count++;
                }
            }
            return count;
        }

        public static string Stringify<T>(this List<T> list)
        {
            var str = string.Empty;
            foreach (var item in list)
            {
                str += item.ToString() + ":";
            }
            return str;
        }

        public static string Stringify<T>(this List<List<T>> list)
        {
            var str = string.Empty;
            foreach (var item in list)
            {
                str += item.Stringify();
            }
            return str;
        }

        public static int CycleDetection<T>(this List<T> list, int startIndex = 0)
        {
            var slow = startIndex;
            var fast = slow + 1;
            var cycleLength = 1;
            while (fast < list.Count && !list[slow].Equals(list[fast]))
            {
                cycleLength++;
                slow++;
                fast += 2;
            }
            if (fast == list.Count)
            {
                return -1;
            }
            return cycleLength;
        }

        public static void RemoveEverythingAfter<T>(this List<T> list, int index)
        {
            while (index + 1 < list.Count)
            {
                list.RemoveAt(index + 1);
            }
        }

        public static int GetIndexWithOffset<T>(this List<T> list, int index, int offset, bool wrapping = false)
        {
            var newIndex = index + offset;
            if (newIndex < 0)
            {
                if (!wrapping)
                {
                    return -1;
                }
                newIndex += list.Count;
            }
            else if (newIndex >= list.Count)
            {
                if (!wrapping)
                {
                    return -1;
                }
                newIndex -= list.Count;
            }

            return newIndex;
        }
    }
}