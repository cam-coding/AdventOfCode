using AdventLibrary;
using AdventLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    public class Day19: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var baseString = string.Empty;

            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            foreach (var line in lines)
			{
                if (line.Contains("=>"))
                {
                    var tokens = line.Split(delimiterChars);

                    dict.TryAdd(tokens[0], new List<string>());
                    dict[tokens[0]].Add(tokens[3]);
                }
                else
                {
                    baseString = line;
                }
			}

            var hashy = new HashSet<string>();
            foreach (var item in dict)
            {
                foreach (var index in baseString.GetIndexesOfSubstring(item.Key))
                {
                    foreach (var replacement in item.Value)
                    {
                        var newstr = baseString.Remove(index, item.Key.Length);
                        var newnewstr = newstr.Insert(index, replacement);
                        hashy.Add(newnewstr);
                    }
                }
            }
            return hashy.Count;
        }

        Dictionary<string, string> _doublers = new Dictionary<string, string>()
        {
            { "CaCa", "Ca" },
            { "TiTi", "Ti" },
        };

        Dictionary<string, string> _others = new Dictionary<string, string>()
        {
            { "PB", "Ca" },
            { "SiTh", "Ca" },
            { "PMg", "F" },
            { "SiAl", "F" },
            { "NTh", "H" },
            { "OB", "H" },
            { "BF", "Mg" },
            { "HP", "O" },
            { "BP", "Ti" },
        };

        Dictionary<string, string> _enders = new Dictionary<string, string>()
        {
            { "HF", "e" },
            { "NAl", "e" },
            { "OMg", "e" },
        };

        Dictionary<string, string> _adders = new Dictionary<string, string>()
        {
            { "BCa", "B" },
            { "TiB", "B" },
            { "CaF", "F" },
            { "HCa", "H" },
            { "TiMg", "Mg" },
            { "OTi", "O" },
            { "CaP", "P" },
            { "PTi", "P" },
            { "CaSi", "Si" },
            { "ThCa", "Th" },
        };

        Dictionary<string, string> _hunters = new Dictionary<string, string>()
        {
            { "ThRnFAr", "Al" },
            { "TiRnFAr", "B" },
            { "PRnFAr", "Ca" },
            { "SiRnFYFAr", "Ca" },
            { "SiRnMgAr", "Ca" },
            { "CRnAlAr", "H" },
            { "CRnFYFYFAr", "H" },
            { "CRnFYMgAr", "H" },
            { "CRnMgYFAr", "H" },
            { "NRnFYFAr", "H" },
            { "NRnMgAr", "H" },
            { "ORnFAr", "H" },
            { "CRnFAr", "N" },
            { "CRnFYFAr", "O" },
            { "CRnMgAr", "O" },
            { "NRnFAr", "O" },
            { "SiRnFAr", "P" },
        };

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var baseString = lines.GetLastItem();

            var hashy = new HashSet<string>();
            var count = 0;
            bool workDone;
            string lastSeen = string.Empty;
            while (!baseString.Equals("e"))
            {
                if (lastSeen.Equals(baseString))
                {
                    return baseString.Length;
                }
                lastSeen = baseString.ToString();
                workDone = false;
                count++;

                var listy = new List<Dictionary<string, string>>() { _hunters, _doublers, _adders, _others, _enders };

                foreach (var lookup in listy)
                {
                    if (!workDone)
                    {
                        foreach (var item in lookup)
                        {
                            if (baseString.Contains(item.Key))
                            {
                                baseString = baseString.ReplaceFirstInstanceOf(item.Key, item.Value);
                                workDone = true;
                                break;
                            }
                        }
                    }
                }
            }
            return count;
        }
    }
}