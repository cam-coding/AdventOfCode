using AdventLibrary;
using System.Collections.Generic;

namespace aoc2015
{
    public class Day16: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        Dictionary<string, int> _dict = new Dictionary<string, int>()
        {
            { "children", 3},
            { "cats", 7},
            { "samoyeds", 2},
            { "pomeranians", 3},
            { "akitas", 0},
            { "vizslas", 0},
            { "goldfish", 5},
            { "trees", 3},
            { "cars", 2},
            { "perfumes", 1},
        };

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var possible = new List<string>();
            for (var i = 0; i < lines.Count; i++)
            {
                var tokens = lines[i].Split(delimiterChars);
                var nums = AdventLibrary.StringParsing.GetNumbersFromString(lines[i]);

                var valid = true;

                if (_dict[tokens[3]] != nums[1])
                {
                    valid = false;
                }
                if (_dict[tokens[7]] != nums[2])
                {
                    valid = false;
                }
                if (_dict[tokens[11]] != nums[3])
                {
                    valid = false;
                }

                if (valid)
                {
                    return nums[0];
                }
            }
            return 0;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var possible = new List<string>();
            for (var i = 0; i < lines.Count; i++)
            {
                var tokens = lines[i].Split(delimiterChars);
                var nums = AdventLibrary.StringParsing.GetNumbersFromString(lines[i]);

                var valid = true;

                if (_dict2[tokens[3]].Item1 > nums[1] || _dict2[tokens[3]].Item2 < nums[1])
                {
                    valid = false;
                }
                if (_dict2[tokens[7]].Item1 > nums[2] || _dict2[tokens[7]].Item2 < nums[2])
                {
                    valid = false;
                }
                if (_dict2[tokens[11]].Item1 > nums[3] || _dict2[tokens[11]].Item2 < nums[3])
                {
                    valid = false;
                }

                if (valid)
                {
                    return nums[0];
                }
            }
            return 0;
        }

        Dictionary<string, (int, int)> _dict2 = new Dictionary<string, (int,int)>()
        {
            { "children", (3,3)},
            { "cats", (8,int.MaxValue)},
            { "samoyeds", (2,2)},
            { "pomeranians", (0,2)},
            { "akitas", (0,0)},
            { "vizslas", (0,0)},
            { "goldfish", (0,4)},
            { "trees", (4,int.MaxValue) },
            { "cars", (2,2)},
            { "perfumes", (1,1)},
        };
    }
}