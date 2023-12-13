using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2023
{
    public class Day12: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<DictKey, long> _dicty;
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			long count = 0;

			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars).ToList().OnlyRealStrings(delimiterChars);

				var nums = StringParsing.GetNumbersFromString(line);

                _dicty = new Dictionary<DictKey, long>();
                var temp = BackTrack(tokens[0], nums);
                var temp2 = BackTrack4(tokens[0], nums);
                if (temp != temp2)
                {
                    Console.WriteLine("uhoh");
                    _dicty = new Dictionary<DictKey, long>();
                    var temp3 = BackTrack4(tokens[0], nums);
                }
                count += temp2;
                // Console.WriteLine($"Finished line with nums {tokens[1]}. Added {temp} to the total");
            }
            return count;
        }
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            long count = 0;

            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars).ToList().OnlyRealStrings(delimiterChars);

                var nums = StringParsing.GetNumbersFromString(line);

                var newLine = string.Empty;
                for (var j = 0; j < 5; j++)
                {
                    newLine += tokens[0] + '?';
                }
                newLine = newLine[..^1];
                newLine += ' ';
                for (var j = 0; j < 5; j++)
                {
                    newLine += tokens[1] + ',';
                }
                newLine = newLine[..^1];
                var nums2 = StringParsing.GetNumbersFromString(newLine);
                var tokens2 = newLine.Split(delimiterChars).ToList().OnlyRealStrings(delimiterChars);

                _dicty = new Dictionary<DictKey, long>();
                long temp = BackTrack4(tokens2[0], nums2);
                // long temp2 = BackTrack3(tokens2[0], nums2, tokens2[0].Count(x => x == '?' || x == '#'), nums2.Sum());
                // var temp = BackTrack2(tokens2[0].ToArray(), nums2, tokens2[0].Count(x => x == '?' || x == '#'), 0, new List<int>());
                count += temp;
                Console.WriteLine($"Finished line with nums {tokens[1]}. Added {temp} to the total");
            }
            return count;
            /*Okay so I need to trim the string as I work my way down
             * look for ### followed by a . and then trim that out. Trim that out of the nums list as well and move on
             * This should remove the need to validate as we go, just validate on current move or something?
             * could try making groups work right as well.
             * Use day 11 2016 logic.*/
        }

        private int BackTrack(string str, List<int> nums)
        {
            if (!str.Contains('?'))
            {
                if (Valid(str, nums))
                {
                    return 1;
                }
                return 0;
            }
            var copy1 = string.Empty + str;
            copy1 = copy1.ReplaceFirstInstanceOf("?", ".");
            var total = BackTrack(copy1, nums);
            var copy2 = string.Empty + str;
            copy2 = copy2.ReplaceFirstInstanceOf("?", "#");
            total += BackTrack(copy2, nums);
            return total;
        }

        private bool Valid(string str, List<int> nums)
        {
            var counter = 0;
            var i = 0;
            var groups = new List<int>();
            while (i < str.Length)
            {
                if (str[i] == '#')
                {
                    counter = 1;
                    i++;
                    while (i < str.Length && str[i] == '#')
                    {
                        counter++;
                        i++;
                    }
                    groups.Add(counter);
                    counter = 0;
                }
                else
                {
                    i++;
                }
            }
            var boo = true;
            if (groups.Count != nums.Count)
            {
                return false;
            }
            for (var j = 0; j < nums.Count; j++)
            {
                if (nums[j] != groups[j])
                {
                    return false;
                }
            }
            return true;
        }


        private class DictKey
        {
            private int _sums;
            public DictKey(string str, List<int> nums)
            {
                Str = str;
                Nums = nums;
                _sums = Nums.Sum();
            }

            public string Str { get; set; }
            public List<int> Nums { get; set; }

            public override bool Equals(object other)
            {
                if (!(other is DictKey))
                {
                    return false;
                }
                var otherDictKey = (DictKey)other;
                return Str.Equals(otherDictKey.Str) && Nums.SequenceEqual(otherDictKey.Nums);
            }

            public override int GetHashCode()
            {
                var blah = this.Str.GetHashCode();
                var blah2 = Nums.GetHashCode();
                return blah + _sums;
            }
        }

        private class ListComparer<T> : IEqualityComparer<List<T>>
        {
            public bool Equals(List<T> x, List<T> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(List<T> obj)
            {
                int hashcode = 0;
                foreach (T t in obj)
                {
                    hashcode ^= t.GetHashCode();
                }
                return hashcode;
            }
        }

        // str = "???????#????.#???????????#????.#???????????#????.#???????????#????.#???????"
        private long BackTrack4(string str, List<int> nums)
        {
            var dictKey = new DictKey(str, nums);
            if (_dicty.ContainsKey(dictKey))
            {
                return _dicty[dictKey];
            }
            if (!str.Contains('?'))
            {
                if (Valid(str, nums))
                {
                    return 1;
                }
                return 0;
            }


            if (str[0] == '.')
            {
                return BackTrack4(str[1..], nums);
            }
            // if you still have ?'s but all the groups are filled, there's 1 solution. All ? are .
            if (nums.Count == 0)
            {
                if (str.Contains('#'))
                {
                    return 0;
                }
                return 1;
            }
            var length = FindLengthOfFirstGroup(str);
            if (length != -1 && length > nums[0])
            {
                return 0;
            }
            // check if memo
            for (var repeats = nums.Count / 2; repeats > 1; repeats--)
            {
                if (RepeatingPattern(nums,repeats))
                {
                    if (RepeatingPattern(str.ToList(), repeats))
                    {
                        var newStr2 = str.Substring(0, str.Length/repeats);
                        var newNums2 = nums[0..(nums.Count/repeats)];
                        var strLen = newStr2.Length;
                        var result = BackTrack4(newStr2, newNums2);
                        var total = (long)Math.Pow(result, repeats);
                        _dicty.TryAdd(dictKey, total);
                        return total;
                    }
                }
            }

            var currentString = str;
            var indx1 = str.IndexOf("?");
            var indx2 = str.IndexOf("#.");
            var counter = 0;
            long total2 = 0;

            // trimming section
            while (indx2 != -1 && indx2 < indx1)
            {
                var toks = currentString.Split("#.", 2, StringSplitOptions.None);
                var first = toks[0] + "#";
                var second = "." + toks[1];
                var count = first.Count(x => x == '#');
                if (counter >= nums.Count || count != nums[counter])
                {
                    return 0;
                }
                else
                {
                    currentString = "." + toks[1];
                    counter++;
                    indx1 = currentString.IndexOf("?");
                    indx2 = currentString.IndexOf("#.");
                }
            }
            if (counter != 0)
            {
                var blah = BackTrack4(currentString, nums[counter..].ToList());
                // var blah2 = BackTrack4(str.Replace(currentString, string.Empty), nums[..counter].ToList());
                _dicty.Add(dictKey, blah);
                return blah;
            }

            var copy3 = string.Empty + str;
            copy3 = copy3.ReplaceFirstInstanceOf("?", ".");
            total2 += BackTrack4(copy3, nums);
            var copy4 = string.Empty + str;
            copy4 = copy4.ReplaceFirstInstanceOf("?", "#");
            total2 += BackTrack4(copy4, nums);
            _dicty.Add(dictKey, total2);
            return total2;
        }

        private int FindLengthOfFirstGroup(string str)
        {
            var indx1 = str.IndexOf("?");
            var indx2 = str.IndexOf("#");
            if ((indx1 != -1 && indx1 < indx2) || indx2 == -1)
            {
                return -1;
            }

            var count = 0;
            while (str[indx2] == '#')
            {
                indx2++;
                count++;
            }
            return count;
        }

        private (string, List<int>) Trimmer(string str, List<int> nums)
        {
            var currentString = str;
            var indx1 = str.IndexOf("?");
            var indx2 = str.IndexOf("#.");
            var counter = 0;

            while (indx2 != -1 && indx2 < indx1)
            {
                var toks = currentString.Split("#.", 2, StringSplitOptions.None);
                var first = toks[0] + "#";
                var second = "." + toks[1];
                var count = first.Count(x => x == '#');
                if (count != nums[counter])
                {
                    break;
                }
                else
                {
                    currentString = "." + toks[1];
                    counter++;
                    indx1 = currentString.IndexOf("?");
                    indx2 = currentString.IndexOf("#.");
                }
            }
            return (currentString, nums[counter..].ToList());
        }

        // listy = 12 count, repeats = 4
        private bool RepeatingPattern<T>(List<T> listy, int repeats)
        {
            if (listy.Count % repeats != 0)
            {
                return false;
            }
            var patternLength = listy.Count / repeats;
            for (var j = 0; j < patternLength; j++)
            {
                for (var k = 1; k < repeats; k++)
                {
                    if (!listy[j].Equals(listy[j + patternLength * k]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private string RemovePeriods(string str)
        {
            var start = 0;
            var end = str.Length-1;

            var i = 0;
            while (str[i] == '.')
            {
                i++;
            }
            start = i;

            i = end;
            while (str[i] == '.')
            {
                i--;
            }
            end = i;

            if (start != 0 || end != (str.Length-1))
            {
                return str.Substring(start, end - start + 1);
            }
            return str;
        }

        private string RemovePeriods2(string str)
        {
            var start = 0;

            var i = 0;
            while (str[i] == '.')
            {
                i++;
            }
            start = i;

            if (start != 0)
            {
                return str.Substring(start);
            }
            return str;
        }

        private long BackTrack3(string str, List<int> nums, int possible, int needed)
        {
            if (needed > possible)
            {
                return 0;
            }
            if (needed == 0)
            {
                if (str.Contains('#'))
                {
                    return 0;
                }
                return 1;
            }
            if (!str.Contains('?'))
            {
                if (Valid(str, nums))
                {
                    return 1;
                }
                return 0;
            }
            str = RemovePeriods(str);
            var indx1 = str.IndexOf("?");
            var indx2 = str.IndexOf("#.");
            if (indx2 != -1 && indx2 < indx1)
            {
                var toks = str.Split("#.", 2, StringSplitOptions.None);
                var first = toks[0] + "#.";
                var count = first.Count(x => x == '#');
                if (count != nums[0])
                {
                    return 0;
                }
                else
                {
                    var newStr = toks[1];
                    var newNums = nums[1..].ToList();
                    var copy3 = string.Empty + newStr;
                    copy3 = copy3.ReplaceFirstInstanceOf("?", ".");
                    var total2 = BackTrack3(copy3, newNums, copy3.Count(x => x == '?' || x == '#'), newNums.Sum());
                    var copy4 = string.Empty + newStr;
                    copy4 = copy4.ReplaceFirstInstanceOf("?", "#");
                    total2 += BackTrack3(copy4, newNums, copy4.Count(x => x == '?' || x == '#'), newNums.Sum());
                    return total2;
                }
            }
            var copy1 = string.Empty + str;
            copy1 = copy1.ReplaceFirstInstanceOf("?", ".");
            var total = BackTrack3(copy1, nums, copy1.Count(x => x == '?' || x == '#'), nums.Sum());
            var copy2 = string.Empty + str;
            copy2 = copy2.ReplaceFirstInstanceOf("?", "#");
            total += BackTrack3(copy2, nums, copy2.Count(x => x == '?' || x == '#'), nums.Sum());
            return total;
        }
        private List<int> ValidSoFar(char[] str, List<int> nums, int current, int pos, out int newPos, List<int> groups)
        {
            var counter = 0;
            var i = pos;
            while (i < str.Length && str[i] != '?')
            {
                if (str[i] == '#')
                {
                    counter = 1;
                    i++;
                    while (i < str.Length && str[i] == '#' && str[i] != '?')
                    {
                        counter++;
                        i++;
                    }
                    if (i == str.Length || str[i] != '?')
                    {
                        groups.Add(counter);
                        counter = 0;
                    }
                }
                else
                {
                    i++;
                }
            }
            newPos = i;
            /*
            if (groups.Count > nums.Count)
            {
                return null;
            }*/
            var j = 0;
            for (j = 0; j < groups.Count; j++)
            {
                if (nums[j] != groups[j])
                {
                    return null;
                }
            }
            j++;
            var county = 0;
            var county2 = 0;
            while (j < nums.Count)
            {
                county += nums[j] +  1;
                county2 += nums[j];
                j++;
            }

            /*
            if (county2 > current)
            {
                return null;
            }*/
            // carry extra 1 at the end

            /*
            var remaining = (str.Length) - i;
            if (remaining < county)
            {
                return null;
            }*/
            return groups;
        }
        private int BackTrack2(char[] str, List<int> nums, int possibleBrokenCount, int pos, List<int> groups)
        {
            if (!str.Contains('?'))
            {
                if (Valid2(str, nums))
                {
                    return 1;
                }
                return 0;
            }
            var newPos = 0;
            groups = ValidSoFar(str, nums, possibleBrokenCount, pos, out newPos, groups);
            pos = newPos;
            if (groups != null)
            {
                var index = -1;
                for (var j = 0; j < str.Length; j++)
                {
                    if (str[j] == '?')
                    {
                        index = j;
                        break;
                    }
                }

                str[index] = '.';
                var total = BackTrack2(str.ToArray(), nums, possibleBrokenCount-1, pos, groups.ToList());
                str[index] = '#';
                total += BackTrack2(str.ToArray(), nums, possibleBrokenCount-1, pos, groups.ToList());
                return total;
            }
            else
            {
                return 0;
            }
        }
        private bool Valid2(char[] str, List<int> nums)
        {
            var counter = 0;
            var i = 0;
            var groups = new List<int>();
            while (i < str.Length)
            {
                if (str[i] == '#')
                {
                    counter = 1;
                    i++;
                    while (i < str.Length && str[i] == '#')
                    {
                        counter++;
                        i++;
                    }
                    groups.Add(counter);
                    counter = 0;
                }
                else
                {
                    i++;
                }
            }
            var boo = true;
            if (groups.Count != nums.Count)
            {
                return false;
            }
            for (var j = 0; j < nums.Count; j++)
            {
                if (nums[j] != groups[j])
                {
                    return false;
                }
            }
            return true;
        }
    }
}