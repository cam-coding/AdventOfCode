using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2020
{
    public class Day19: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<int, List<Rule>> _lookup;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1(bool isTest = false)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            _lookup = new Dictionary<int, List<Rule>>();
            var iter = 0;
            for (; iter < lines.Count; iter++)
            {
                if (string.IsNullOrWhiteSpace(lines[iter]))
                {
                    iter++;
                    break;
                }
                var tokens = lines[iter].Split(':').ToList().OnlyRealStrings();
				var key = StringParsing.GetNumbersFromString(tokens[0])[0];
                var listy = new List<Rule>();

                if (tokens[1].Contains("\""))
                {
                    var subRules2 = tokens[1].Split('\"').ToList().OnlyRealStrings(delimiterChars);
                    listy.Add(new Rule(subRules2[0]));
                }
                else
                {
                    var subRules2 = tokens[1].Split('|').ToList().OnlyRealStrings(delimiterChars);

                    foreach (var rule in subRules2)
                    {
                        var rulesInts = StringParsing.GetNumbersFromString(rule);
                        listy.Add(new Rule(rulesInts));
                    }
                }

                _lookup.Add(key, listy);
			}
            var subRules = _lookup[0];
            var count = 0;
            for (; iter < lines.Count; iter++)
            {
                var removal = string.Empty;
                var result = Recursion(lines[iter], out removal, 0);
                if (result)
                {
                    if (string.Empty.Equals(RemoveFromFront(lines[iter], removal)))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private bool Recursion(string currentString, out string removalString, int currentRule)
        {
            string current = string.Empty;
            removalString = string.Empty;
            if (currentString.Equals(string.Empty))
            {
                return false;
            }
            var first = _lookup[currentRule].First();
            if (first.IsChar)
            {
                if (currentString[0] == first.SubRule)
                {
                    removalString += first.SubRule;
                    return true;
                }
                return false;
            }
            else
            {
                // look at each rule
                // either a single list, or a multiple lists
                foreach (var rule in _lookup[currentRule])
                {
                    removalString = string.Empty;
                    var valid = true;
                    var curString = currentString;
                    // each rule contains sub rules, all of which need to be true
                    foreach (var subRule in rule.SubRules)
                    {
                        var newRemovalString = string.Empty;
                        var result = Recursion(curString, out newRemovalString, subRule);
                        if (!result)
                        {
                            valid = false;
                            break;
                        }
                        curString = RemoveFromFront(curString, newRemovalString);
                        removalString += newRemovalString;
                    }
                    if (valid)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private string RemoveFromFront(string input, string remove)
        {
            foreach (var c in remove)
            {
                if (input[0] == c)
                {
                    input = input.Remove(0, 1);
                }
                else
                {
                    throw new Exception();
                }
            }
            return input;
        }

        private object Part2(bool isTest = false)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            _lookup = new Dictionary<int, List<Rule>>();
            var iter = 0;
            for (; iter < lines.Count; iter++)
            {
                if (lines[iter].StartsWith("8:"))
                {
                    lines[iter] = "8: 42 | 42 8";
                }
                else if (lines[iter].StartsWith("11:"))
                {
                    lines[iter] = "11: 42 31 | 42 11 31";
                }
                if (string.IsNullOrWhiteSpace(lines[iter]))
                {
                    iter++;
                    break;
                }
                var tokens = lines[iter].Split(':').ToList().OnlyRealStrings();
                var key = StringParsing.GetNumbersFromString(tokens[0])[0];
                var listy = new List<Rule>();

                if (tokens[1].Contains("\""))
                {
                    var subRules2 = tokens[1].Split('\"').ToList().OnlyRealStrings(delimiterChars);
                    listy.Add(new Rule(subRules2[0]));
                }
                else
                {
                    var subRules2 = tokens[1].Split('|').ToList().OnlyRealStrings(delimiterChars);

                    foreach (var rule in subRules2)
                    {
                        var rulesInts = StringParsing.GetNumbersFromString(rule);
                        listy.Add(new Rule(rulesInts));
                    }
                }

                _lookup.Add(key, listy);
            }
            var count = 0;
            for (; iter < lines.Count; iter++)
            {
                // copying someone's cheeky strat where every valid string is
                // 42 followed by some amount of 42's and then some lesser amount of 31's
                var removal = string.Empty;
                var currentString = lines[iter];
                var result = Recursion(currentString, out removal, 42);
                var count42 = 0;
                var count31 = 0;

                while (result)
                {
                    count42++;
                    currentString = RemoveFromFront(currentString, removal);
                    removal = string.Empty;
                    result = Recursion(currentString, out removal, 42);
                }

                if (count42 > 1)
                {
                    removal = string.Empty;
                    result = Recursion(currentString, out removal, 31);
                    while (result)
                    {
                        count31++;
                        currentString = RemoveFromFront(currentString, removal);
                        removal = string.Empty;
                        result = Recursion(currentString, out removal, 31);
                    }
                    if (count42 > count31 && count31 >= 1 && currentString.Equals(string.Empty))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private class Rule
        {
            public Rule(List<int> subRules)
            {
                IsChar = false;
                SubRules = subRules.Select(x => (int)x).ToList();
            }

            public Rule(string subRules)
            {
                IsChar = true;
                SubRule = subRules[0];
            }

            public List<int> SubRules { get; set; }

            public char SubRule { get; set; }

            public bool IsChar { get; set; }
        }
    }
}