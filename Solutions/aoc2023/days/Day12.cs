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
        public Solution Solve(string filePath, bool isTest = false)
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
                count += temp;
                // Console.WriteLine($"Finished line with nums {tokens[1]}. Added {temp} to the total");
            }
            return count;
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

        // str = "???????#????.#???????????#????.#???????????#????.#???????????#????.#???????"
        private long BackTrack4(string str, List<int> nums)
        {
            var dictKey = new DictKey(str, nums);
            if (_dicty.ContainsKey(dictKey))
            {
                var val = _dicty[dictKey];
                return val;
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
            if (str[0] == '#')
            {
                for (var repeats = nums.Count / 2; repeats > 1; repeats--)
                {
                    if (RepeatingPattern(nums, repeats))
                    {
                        if (RepeatingPattern(str.ToList(), repeats))
                        {
                            var newStr2 = str.Substring(0, str.Length / repeats);
                            if (newStr2.Last() == '.')
                            {
                                var newNums2 = nums[0..(nums.Count / repeats)];
                                var strLen = newStr2.Length;
                                var result = BackTrack4(newStr2, newNums2);
                                var total = (long)Math.Pow(result, repeats);
                                _dicty.TryAdd(dictKey, total);
                                return total;
                            }
                        }
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
    }
}