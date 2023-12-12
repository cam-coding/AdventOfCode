using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using AdventLibrary;
using AdventLibrary.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace aoc2023
{
    public class Day12: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ':', '-', '>', '<', '+', '=', '\t' };
        private int _possible;
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var count = 0;
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars).ToList().OnlyRealStrings(delimiterChars);

				var nums = StringParsing.GetNumbersFromString(line);

                var temp = BackTrack(tokens[0], nums);
                count += temp;
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
            // return nums.SequenceEqual(groups);
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var count = 0;

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

                var temp = BackTrack3(tokens2[0], nums2);
                // var temp = BackTrack2(tokens2[0].ToArray(), nums2, tokens2[0].Count(x => x == '?' || x == '#'), 0, new List<int>());
                count += temp;
            }
            return count;
            /*Okay so I need to trim the string as I work my way down
             * look for ### followed by a . and then trim that out. Trim that out of the nums list as well and move on
             * This should remove the need to validate as we go, just validate on current move or something?
             * could try making groups work right as well.
             * Use day 11 2016 logic.*/
        }

        private int BackTrack3(string str, List<int> nums)
        {
            if (!str.Contains('?'))
            {
                if (Valid(str, nums))
                {
                    return 1;
                }
                return 0;
            }
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
                    var total2 = BackTrack3(copy3, newNums);
                    var copy4 = string.Empty + newStr;
                    copy4 = copy4.ReplaceFirstInstanceOf("?", "#");
                    total2 += BackTrack3(copy4, newNums);
                    return total2;
                }
            }
            var copy1 = string.Empty + str;
            copy1 = copy1.ReplaceFirstInstanceOf("?", ".");
            var total = BackTrack3(copy1, nums);
            var copy2 = string.Empty + str;
            copy2 = copy2.ReplaceFirstInstanceOf("?", "#");
            total += BackTrack3(copy2, nums);
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