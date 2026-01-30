using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2020
{
    public class Day04: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1(bool isTest = false)
        {
            var listy = new List<Dictionary<string, string>>();
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;
            listy.Add(new Dictionary<string, string>());

            foreach (var line in lines)
			{
                if (string.IsNullOrWhiteSpace(line))
                {
                    counter++;
                    listy.Add(new Dictionary<string, string>());
                }
                else
                {
                    var tokens = line.Split(' ').ToList().GetRealStrings(delimiterChars);
                    foreach (var tok in tokens)
                    {
                        var innerTokens = tok.Split(':').ToList().GetRealStrings(delimiterChars);
                        if (!innerTokens[0].Equals("cid"))
                        {
                            listy[counter].Add(innerTokens[0], innerTokens[1]);
                        }
                    }
                }
			}
            return listy.Count(x => x.Count >= 7);
        }

        private object Part2(bool isTest = false)
        {
            var listy = new List<Dictionary<string, string>>();
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;
            listy.Add(new Dictionary<string, string>());

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    counter++;
                    listy.Add(new Dictionary<string, string>());
                }
                else
                {
                    var tokens = line.Split(' ').ToList().GetRealStrings(delimiterChars);
                    foreach (var tok in tokens)
                    {
                        var innerTokens = tok.Split(':').ToList().GetRealStrings(delimiterChars);
                        if (!innerTokens[0].Equals("cid"))
                        {
                            listy[counter].Add(innerTokens[0], innerTokens[1]);
                        }
                    }
                }
            }
            var count = 0;
            foreach (var item in listy)
            {
                if (IsValid(item))
                {
                    count++;
                }
            }
            return count;
        }

        private bool IsValid(Dictionary<string,string> dict)
        {
            var valid = true;
            if (dict.Count != 7)
            {
                return false;
            }
            foreach (var item in dict)
            {
                var num = 0;
                switch (item.Key)
                {
                    case "byr":
                        num = int.Parse(item.Value);
                        valid = valid && (1920 <= num && num <= 2002);
                        break;
                    case "iyr":
                        num = int.Parse(item.Value);
                        valid = valid && (2010 <= num && num <= 2020);
                        break;
                    case "eyr":
                        num = int.Parse(item.Value);
                        valid = valid && (2020 <= num && num <= 2030);
                        break;
                    case "hgt":
                        num = item.Value.GetIntsFromString()[0];
                        if (item.Value.Contains("cm"))
                        {
                            valid = valid && (150 <= num && num <= 193);
                        }
                        else if (item.Value.Contains("in"))
                        {
                            valid = valid && (59 <= num && num <= 76);
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                        break;
                    case "hcl":
                        valid = valid && item.Value[0] == '#';
                        var i = 1;
                        for (; i < item.Value.Length; i++)
                        {
                            if (i == 7)
                            {
                                valid = false;
                                break;
                            }
                            valid = valid && (item.Value[i].InAsciiRange('0', '9') || item.Value[i].InAsciiRange('a', 'f'));
                        }
                        break;
                    case "ecl":
                        var colours = new HashSet<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                        valid = valid && (colours.Contains(item.Value));
                        break;
                    case "pid":
                        num = item.Value.GetDigitsFromString().Count;
                        valid = valid && num == 9;
                        break;
                    default: break;
                }
            }
            return valid;
        }
    }
}