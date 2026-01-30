using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
                // list.RemoveAt(list.Count - 1);
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

        /// <summary>
        /// Same as slice but I never remember the word slice or the syntax.
        /// </summary>
        public static List<T> SubList<T>(this List<T> list, int start, int length)
        {
            return list.Slice(start, length);
        }

        /// <summary>
        /// Same as slice but I never remember the word slice or the syntax.
        /// </summary>
        public static List<T> SubList<T>(this List<T> list, int start)
        {
            return SubList(list, start, list.Count - start);
        }

        /// <summary>
        /// Get groups from a list of a certain size. Can set overlap value
        /// </summary>
        // list = [ 1, 2, 3, 4, 5, 6 ] and n = 3 and overlap == true, result is [ 1, 2, 3 ], [ 2, 3, 4 ], [ 3, 4, 5 ], and [ 4, 5, 6 ]
        // list = [ 1, 2, 3, 4, 5, 6 ] and n = 2 and overlap == false, result is [ 1, 2 ], [ 3, 4 ], [ 5, 6 ]
        // great for sliding window problems
        public static IEnumerable<IEnumerable<T>> GetGroupsOfSizeN_Overlapping<T>(this List<T> list, int length, bool overlap)
        {
            if (list.Count < length)
                throw new ArgumentException("list must be at least size of length");
            var result = new List<List<T>>();
            var modifier = overlap ? 1 : length;
            for (var i = 0; i <= list.Count - length; i += modifier)
            {
                result.Add(list.Slice(i, length));
            }
            return result;
        }

        /// <summary>
        /// Gets combinations from a list of size 1 to N. NO REPETITION
        /// </summary>
        public static List<List<T>> GetCombinationsSize1toNWithRepetition<T>(this List<T> list, int length) where T : IComparable
        {
            var result = new List<List<T>>() { new List<T>() };
            for (int i = 1; i <= length; i++)
            {
                result.AddRange(list.GetCombinationsSizeNWithRepetition(i).ToList());
            }
            return result;
        }

        /// <summary>
        /// Gets combinations from a list of a specific size with repetition
        /// </summary>
        public static List<List<T>> GetCombinationsSizeNWithRepetition<T>(this List<T> list, int n)
        {
            var combinationList = list.Clone();
            var combinations = new List<List<T>>();

            if (n == 0)
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
            var copiedCombinationList = combinationList.Clone();

            List<List<T>> subcombinations = copiedCombinationList.GetCombinationsSizeNWithRepetition(n - 1);

            foreach (var subcombination in subcombinations)
            {
                subcombination.Insert(0, head);
                combinations.Add(subcombination);
            }

            combinationList.RemoveAt(0);
            combinations.AddRange(combinationList.GetCombinationsSizeNWithRepetition(n));

            return combinations;
        }

        /// <summary>
        /// Gets combinations from a list of a specific size. NO REPETITION
        /// </summary>
        // hard to decipher, here are clearer ones https://rosettacode.org/wiki/Combinations#C.2B.2B
        // 1,2,3 and length 2 -> (1,2) (1,3) (2,3)
        public static List<List<T>> GetCombinationsSizeN<T>(this List<T> list, int length) where T : IComparable
        {
            if (length == 0) return new List<List<T>>();
            if (length == 1) return list.Select(t => new List<T> { t }).ToList();
            return list.GetCombinationsSizeN(length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new List<T> { t2 }).ToList()).ToList();
        }

        /// <summary>
        /// Gets combinations from a list of size 1 to N. NO REPETITION
        /// </summary>
        public static List<List<T>> GetCombinationsSize0toN<T>(this List<T> list, int length) where T : IComparable
        {
            var result = new List<List<T>>() { new List<T>() };
            for (int i = 1; i <= length; i++)
            {
                result.AddRange(list.GetCombinationsSizeN(i).ToList());
            }
            return result;
        }

        /// <summary>
        /// Gets combinations from a list size M to N. NO REPETITION
        /// </summary>
        public static List<List<T>> GetCombinationsSizeMtoN<T>(this List<T> list, int minLength, int maxLength) where T : IComparable
        {
            var result = new List<List<T>>() { new List<T>() };
            for (int i = minLength; i <= maxLength; i++)
            {
                result.AddRange(list.GetCombinationsSizeN(i).ToList());
            }
            return result;
        }

        /// <summary>
        /// Get permutations using every item in the list.
        /// </summary>
        // 1,2,3 -> (1,2,3) (1,3, 2) (2,1,3) (2,3,1) (3,1,2) (3,2,1)
        // result is N! lists
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> values) where T : IComparable<T>
        {
            if (values.Count() == 1)
                return new[] { values };
            return values.SelectMany(v => GetPermutations(values.Where(x => x.CompareTo(v) != 0)), (v, p) => p.Prepend(v));
        }

        /// <summary>
        /// Generates all permutations of a specified size from the given list.
        /// </summary>
        /// Written by AI
        public static IEnumerable<List<T>> GetPermutationsOfSize<T>(this List<T> items, int size)
        {
            if (size == 0)
            {
                yield return new List<T>();
                yield break;
            }

            if (items.Count == 0)
            {
                yield break;
            }

            for (int i = 0; i < items.Count; i++)
            {
                T item = items[i];
                var remaining = items.Where((x, index) => index != i).ToList();

                foreach (var permutation in GetPermutationsOfSizeHelper(remaining, size - 1, new List<T> { item }))
                {
                    yield return permutation;
                }
            }
        }

        private static IEnumerable<List<T>> GetPermutationsOfSizeHelper<T>(List<T> items, int size, List<T> current)
        {
            if (size == 0)
            {
                yield return new List<T>(current);
                yield break;
            }

            if (items.Count == 0)
            {
                yield break;
            }

            for (int i = 0; i < items.Count; i++)
            {
                T item = items[i];
                var remaining = items.Where((x, index) => index != i).ToList();
                current.Add(item);

                foreach (var permutation in GetPermutationsOfSizeHelper(remaining, size - 1, current))
                {
                    yield return permutation;
                }

                current.RemoveAt(current.Count - 1);
            }
        }

        /// <summary>
        /// Generates all combinations of a specified size from the given list, maintaining order.
        /// Order within the source list is preserved; only the selection changes.
        /// </summary>
        /// <example>
        /// List { 1, 2, 3, 4 } with size 2 produces: [1,2], [1,3], [1,4], [2,3], [2,4], [3,4]
        /// </example>
        public static IEnumerable<List<T>> GetPermutationsOrderedOfSize<T>(this List<T> items, int size)
        {
            if (size == 0)
            {
                yield return new List<T>();
                yield break;
            }

            if (items.Count < size)
            {
                yield break;
            }

            for (int i = 0; i <= items.Count - size; i++)
            {
                T item = items[i];
                var remaining = items.GetRange(i + 1, items.Count - i - 1);

                foreach (var combination in GetPermutationsOrderedOfSizeHelper(remaining, size - 1, new List<T> { item }))
                {
                    yield return combination;
                }
            }
        }

        private static IEnumerable<List<T>> GetPermutationsOrderedOfSizeHelper<T>(List<T> items, int size, List<T> current)
        {
            if (size == 0)
            {
                yield return new List<T>(current);
                yield break;
            }

            if (items.Count < size)
            {
                yield break;
            }

            for (int i = 0; i <= items.Count - size; i++)
            {
                T item = items[i];
                var remaining = items.GetRange(i + 1, items.Count - i - 1);
                current.Add(item);

                foreach (var combination in GetPermutationsOrderedOfSizeHelper(remaining, size - 1, current))
                {
                    yield return combination;
                }

                current.RemoveAt(current.Count - 1);
            }
        }

        // hand made and probably really inefficient
        // example [1,2,3] & 3
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
            return Multiply(list);
        }

        public static T Multiply<T>(this List<T> list) where T : INumber<T>
        {
            dynamic product = 1;
            foreach (var item in list)
            {
                product *= item;
            }
            return product;
        }

        /// <summary>
        /// Takes a list and swaps rows and columns.
        /// </summary>
        public static List<List<T>> InvertList<T>(this List<List<T>> originalList)
        {
            var newList = new List<List<T>>();
            for (var col = 0; col < originalList[0].Count; col++)
            {
                var currentCol = new List<T>();
                for (var row = 0; row < originalList.Count; row++)
                {
                    currentCol.Add(originalList[row][col]);
                }
                newList.Add(currentCol);
            }
            return newList;
        }

        public static List<List<char>> To2dList(this List<String> list)
        {
            var newList = new List<List<char>>();
            foreach (var str in list)
            {
                newList.Add(str.ToList());
            }
            return newList;
        }

        /// <summary>
        /// Gets all pairs assuming (A,B) is the same as (B,A). AKA Unique pairwise comparison.
        /// (A,B) and nothing else.
        /// </summary>
        public static List<(T, T)> GetPairs_Unique<T>(this List<T> list)
        {
            var result = new List<(T, T)>();
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = i + 1; j < list.Count; j++)
                {
                    result.Add((list[i], list[j]));
                }
            }

            return result;
        }

        /// <summary>
        /// Gets all pairs assuming from a list excluding pairing with same item.
        /// (A,B) & (B,A)
        /// </summary>
        public static List<(T, T)> GetPairs<T>(this List<T> list)
        {
            var result = new List<(T, T)>();
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = 0; j < list.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    result.Add((list[i], list[j]));
                }
            }

            return result;
        }

        /// <summary>
        /// Gets all pairs including pairing with itself.
        /// (A,B) & (B,A) & (A,A)
        /// </summary>
        public static List<(T, T)> GetPairs_IncludingSelfPair<T>(this List<T> list)
        {
            var result = new List<(T, T)>();
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = 0; j < list.Count; j++)
                {
                    result.Add((list[i], list[j]));
                }
            }

            return result;
        }

        public static bool[,] ConvertTo2DArray(this List<List<bool>> list)
        {
            int rows = list.Count;
            int cols = list[0].Count;
            bool[,] result = new bool[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                if (list[r].Count != cols)
                    throw new ArgumentException("All inner lists must have the same length.");

                for (int c = 0; c < cols; c++)
                {
                    result[r, c] = list[r][c];
                }
            }

            return result;
        }
    }
}