using AdventLibrary;
using System.Collections.Generic;

namespace aoc2015
{
    public class Day19: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
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

        Dictionary<string, string> _doublers2 = new Dictionary<string, string>()
        {
            { "Ca", "CaCa" },
            { "Ti", "TiTi" },
        };

        Dictionary<string, string> _doublers = new Dictionary<string, string>()
        {
            { "CaCa", "Ca" },
            { "TiTi", "Ti" },
        };

        Dictionary<string, string> _CaAdders = new Dictionary<string, string>()
        {
            { "CaF", "F" },
            { "TiMg", "Mg" },
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
            var baseString = string.Empty;

            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            Dictionary<string, string> reverseDict = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                if (line.Contains("=>"))
                {
                    var tokens = line.Split(delimiterChars);

                    dict.TryAdd(tokens[0], new List<string>());
                    dict[tokens[0]].Add(tokens[3]);
                    reverseDict.Add(tokens[3], tokens[0]);
                }
                else
                {
                    baseString = line;
                }
            }

            var hashy = new HashSet<string>();
            while (!baseString.Equals("e"))
            {

            }
            return hashy.Count;
        }
    }
}