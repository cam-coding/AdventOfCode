using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using AdventLibrary;
using AdventLibrary.Helpers;
using System.Xml.Schema;
using System.Xml.Linq;

namespace aoc2015
{
    public class Day12: ISolver
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
			var counter = 0;
			
			foreach (var line in lines)
			{
				var nums = AdventLibrary.StringParsing.GetNumbersWithNegativesFromString(line);
                counter += nums.Sum();
			}
            return counter;
        }
        
        private object Part2()
        {
            // do something like this? https://www.reddit.com/r/adventofcode/comments/3wh73d/day_12_solutions/cxw7z9h/
            // was having a bad time installing the package
            string jsonString = ParseInput.GetTextFromFile(_filePath);
            var objects = JsonSerializer.Deserialize<object>(jsonString)!;
            /*
            var rrs = JsonConvert.DeserializeObject(jsonString);
            var total = 0;

            foreach (var obj in objects)
            {
                total++;
            }*/
            return 0;
        }
    }
}