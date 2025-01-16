using Microsoft.Z3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AdventLibrary.Extensions
{
    // Extending base list class with some helper methods
    public static class ListExtensions
    {
        public static List<T> GetAllExceptFirstItem<T>(this List<T> list)
        {
            return list.GetRange(1, list.Count - 1);
        }

        public static T GetLastItem<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        // get's middle item, left of middle if count is even.
        public static T GetMiddleItem<T>(this List<T> list, int left = 0, int right = -1)
        {
            if (list.Count == 0)
            {
                return default(T);
            }
            if (right == -1)
            {
                right = list.Count - 1;
            }
            var mid = left + ((right - left) / 2);
            return list[mid];
        }

        public static List<T> GetWithout<T>(this List<T> list, int index)
        {
            var except = list.Clone();
            except.RemoveAt(index);
            return except;
        }

        public static int GetWrappedIndex<T>(this List<T> list, int index)
        {
            var realIndex = -1;
            if (index >= 0)
            {
                realIndex = index % list.Count;
            }
            else if (index < 0)
            {
                realIndex = list.Count + (index % (list.Count * -1));
            }
            return realIndex;
        }

        public static T PopFirst<T>(this List<T> list)
        {
            return list.PopAtIndex(0);
        }

        public static T PopLast<T>(this List<T> list)
        {
            return list.PopAtIndex(list.Count - 1);
        }

        public static T PopAtIndex<T>(this List<T> list, int index)
        {
            var ret = list[index];
            list.RemoveAt(index);
            return ret;
        }

        public static void SwapItemsAtIndexes<T>(this List<T> list, int left, int right)
        {
            var tmp = list[left];
            list[left] = list[right];
            list[right] = tmp;
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

        // WARNING DO NOT USE THIS FOR 2D LISTS
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

        public static bool IsEmpty<T>(this List<T> list)
        {
            return list.Count == 0;
        }

        // If I want to exclude instances where there is the same number more than once
        // just couple this with a distinct() check.
        public static bool IsSorted<T>(this List<T> list) where T : IComparable
        {
            return list.IsSortedAscending() || list.IsSortedDescending();
        }

        public static bool IsSortedAscending<T>(this List<T> list) where T : IComparable
        {
            var ascList = list.SortAscending();
            return list.SequenceEqual(ascList);
        }

        public static bool IsSortedDescending<T>(this List<T> list) where T : IComparable
        {
            var descList = list.SortDescending();
            return list.SequenceEqual(descList);
        }

        public static bool IsValidIndex<T>(this List<T> list, int index)
        {
            return index >= 0 && index < list.Count;
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

        public static List<T> SubList<T>(this List<T> list, int start, int length)
        {
            return list.Slice(start, length);
        }

        public static List<T> SubList<T>(this List<T> list, int start)
        {
            return SubList(list, start, list.Count - start);
        }

        // lst = [ 1, 2, 3, 4, 5, 6 ] and n = 3, result is [ 1, 2, 3 ], [ 2, 3, 4 ], [ 3, 4, 5 ], and [ 4, 5, 6 ]
        // great for sliding window problems
        public static IEnumerable<IEnumerable<T>> GetOverlappingGroupsOfSize<T>(this List<T> list, int length)
        {
            if (list.Count < length)
                throw new ArgumentException("list must be at least size of length");
            var result = new List<List<T>>();
            for (var i = 0; i < list.Count - (length - 1); i++)
            {
                result.Add(list.Slice(i, length));
            }
            return result;
        }

        // hard to decipher, here are clearer ones https://rosettacode.org/wiki/Combinations#C.2B.2B
        // 1,2,3 and length 2 -> (1,2) (1,3) (2,3)
        public static List<List<T>> GetKCombinations<T>(this List<T> list, int length) where T : IComparable
        {
            if (length == 0) return new List<List<T>>();
            if (length == 1) return list.Select(t => new List<T> { t }).ToList();
            return list.GetKCombinations(length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new List<T> { t2 }).ToList()).ToList();
        }

        public static List<List<T>> Get0toKCombinations<T>(this List<T> list, int length) where T : IComparable
        {
            var result = new List<List<T>>() { new List<T>() };
            for (int i = 1; i <= length; i++)
            {
                result.AddRange(list.GetKCombinations(i).ToList());
            }
            return result;
        }

        public static List<List<T>> GetJtoKCombinations<T>(this List<T> list, int minLength, int maxLength) where T : IComparable
        {
            var result = new List<List<T>>() { new List<T>() };
            for (int i = minLength; i <= maxLength; i++)
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

        // hand made and probably reall inefficient
        // example [1,2,3] 3
        // output = [1,1,1] [1,1,2] [1,1,3] [1,2,1] [1,2,2] [1,2,3] [1,3,1] [1,3,2] [1,3,3]  ...etc
        // returns 27 lists aka listSize ^ n
        public static List<List<T>> GetPermutationsWithRepetitions<T>(this List<T> values, int n)
        {
            return GetPermutationsWithRepetitions(values, new List<T>(), n);
        }

        private static List<List<T>> GetPermutationsWithRepetitions<T>(this List<T> values, List<T> current, int sizeRemaining)
        {
            var output = new List<List<T>>();
            if (sizeRemaining == 0)
            {
                output.Add(current);
                return output;
            }
            foreach (var item in values)
            {
                var temp = current.Clone();
                temp.Add(item);
                output.AddRange(GetPermutationsWithRepetitions(values, temp, sizeRemaining - 1));
            }
            return output;
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

        public static string Stringify<T>(this List<T> list, string separator)
        {
            return string.Join(separator, list);
        }

        public static string Stringify<T>(this List<List<T>> list, string separator)
        {
            var str = string.Empty;
            foreach (var item in list)
            {
                str += item.Stringify(separator) + separator;
            }
            return str;
        }

        public static string Stringify<T>(this List<T> list, char separator = ',')
        {
            return string.Join(separator, list);
        }

        public static string Stringify<T>(this List<List<T>> list, char separator = ',')
        {
            var str = string.Empty;
            foreach (var item in list)
            {
                str += item.Stringify() + separator;
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

        public static Dictionary<T, int> ToIndexDictionary<T>(this List<T> list)
        {
            var dict = new Dictionary<T, int>();
            for (var i = 0; i < list.Count; i++)
            {
                dict.Add(list[i], i);
            }
            return dict;
        }

        public static string ConcatListToString<T>(this List<T> list, char? sep = null)
        {
            if (sep == null)
            {
                return string.Join(string.Empty, list.ToArray());
            }
            else
            {
                return string.Join(sep.Value, list.ToArray());
            }
        }

        public static string ConcatListToString<T>(this List<T> list, string sep)
        {
            return ConcatListToString(list, sep[0]);
        }

        public static List<List<T>> GetNSubLists<T>(this List<T> list, int n)
        {
            var subLists = new List<List<T>>();
            var subListSize = list.Count / n;
            for (var i = 0; i < n; i++)
            {
                var subList = list.GetRange(i * subListSize, subListSize);
                subLists.Add(subList);
            }
            return subLists;
        }

        public static List<T> GetAllCommonItems<T>(this List<List<T>> list)
        {
            var output = new List<T>();
            var megaList = list.CombineLists().Distinct();
            foreach (var item in megaList)
            {
                if (list.All(x => x.Contains(item)))
                {
                    output.Add(item);
                }
            }
            return output;
        }

        public static List<T> CombineLists<T>(this List<List<T>> list)
        {
            var megaList = new List<T>();
            foreach (var subList in list)
            {
                megaList.AddRange(subList);
            }
            return megaList;
        }

        public static T Product<T>(this List<T> list) where T : INumber<T>
        {
            dynamic product = 1;
            foreach (var item in list)
            {
                product *= item;
            }
            return product;
        }
    }
}