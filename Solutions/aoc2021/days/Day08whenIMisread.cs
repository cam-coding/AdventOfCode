using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day08bad: ISolver
    {
        private List<char> liney;
        private List<string> permutations;
        private List<HashSet<char>> analogNums;
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var counter = 0;
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
			
			foreach (var line in lines)
			{
                var parts = line.Split('|');
                counter = counter + parts[1].Split(' ').Count(item => item.Length == 2 || item.Length == 3 || item.Length == 4 || item.Length == 7);
			}
            return counter;
        }
        
        private object Part2()
        {
            var bigOleTotal = 0;
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
			
			foreach (var line in lines)
			{
                var parts = line.Split('|');
                permutations = new List<string>();
                permute("abcdefg", 0, 6);
                var encrypted = StringListToHashes(parts[1].Split(' ').ToList());
                var inputs = StringListToHashes(parts[0].Split(' ').ToList());

                foreach (var permute in permutations)
                {
                    liney = permute.ToList();
                    PrepAnalogNums();
                    var total = 0;

                    if (CheckAnalogNums(inputs) && CheckEncryptedNums(encrypted, out total))
                    {
                        bigOleTotal = bigOleTotal + total;
                    }
                }
			}
            return bigOleTotal;
        }

        private void permute(String str, int l, int r)
        {
            if (l == r)
                permutations.Add(str);
            else {
                for (int i = l; i <= r; i++) {
                    str = swap(str, l, i);
                    permute(str, l + 1, r);
                    str = swap(str, l, i);
                }
            }
        }

        private void PrepAnalogNums()
        {
            analogNums = new List<HashSet<char>>() { new HashSet<char>(), new HashSet<char>(), new HashSet<char>(), new HashSet<char>(), new HashSet<char>(), new HashSet<char>(), new HashSet<char>(), new HashSet<char>(), new HashSet<char>(), new HashSet<char>()};
            CreateAnalogHashSets(0, new int[] { 0, 1, 2, 4, 5, 6});
            CreateAnalogHashSets(1, new int[] { 2, 5});
            CreateAnalogHashSets(2, new int[] { 0, 2, 3, 4, 6});
            CreateAnalogHashSets(3, new int[] { 0, 2, 3, 5, 6});
            CreateAnalogHashSets(4, new int[] { 1, 2, 3, 5});
            CreateAnalogHashSets(5, new int[] { 0, 1, 3, 5, 6});
            CreateAnalogHashSets(6, new int[] { 0, 1, 3, 4, 5, 6});
            CreateAnalogHashSets(7, new int[] { 0, 2, 5});
            CreateAnalogHashSets(8, new int[] { 0, 1, 2, 3, 4, 5, 6});
            CreateAnalogHashSets(9, new int[] { 0, 1, 2, 3, 5, 6});
        }

        private void CreateAnalogHashSets(int n, int[] nums)
        {
            foreach (var num in nums)
            {
                analogNums[n].Add(liney[num]);
            }
        }

        private List<HashSet<char>> StringListToHashes(List<string> words)
        {
            var listy = new List<HashSet<char>>();
            foreach (var word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    var hash = new HashSet<char>();
                    foreach (var item in word)
                    {
                        hash.Add(item);
                    }
                    listy.Add(hash);
                }
            }
            return listy;
        }

        private bool CheckAnalogNums(List<HashSet<char>> inputs)
        {
            foreach (HashSet<char> input in inputs)
            {
                if (!analogNums.Any(x => x.SetEquals(input)))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckEncryptedNums(List<HashSet<char>> encrypted, out int total)
        {
            var myString = string.Empty;
            total = 0;
            foreach (HashSet<char> enc in encrypted)
            {
                var matchingNum = analogNums.Where(x => x.SetEquals(enc)).FirstOrDefault();
                if (matchingNum == null)
                {
                    return false;
                }
                myString = myString + analogNums.IndexOf(matchingNum);
            }
            total = Convert.ToInt32(myString);
            return true;
        }
  
        private String swap(String a, int i, int j)
        {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }
    }
}
