using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day10: ISolver
  {
        private string _filePath;
        private int specialItem1 = 17;
        private int specialItem2 = 61;
        private Dictionary<int, List<int>> botLookup;
        private Dictionary<int, string> botInstructions;
        private Dictionary<int, List<int>> outputBins;


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

            botLookup = new Dictionary<int, List<int>>();
            botInstructions = new Dictionary<int, string>();
            outputBins = new Dictionary<int, List<int>>();
			
			foreach (var line in lines)
			{
				var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                if (line[0] == 'v')
                {
                    if (botLookup.ContainsKey(nums[1]))
                    {
                        botLookup[nums[1]].Add(nums[0]);
                    }
                    else
                    {
                        botLookup.Add(nums[1], new List<int>() { nums[0] });
                    }
                }
                else
                {
                    botInstructions.Add(nums[0], line);
                }
			}

            while (botLookup.Any(x => x.Value.Count() == 2))
            {
                foreach (var key in botLookup.Keys.ToList())
                {
                    GoTime(key);
                }
            }
            
            return 0;
        }

        private void GoTime(int currentBot)
        {
            if (botLookup[currentBot].Count() != 2)
            {
                return;
            }
            var str = botInstructions[currentBot];
            var nums = AdventLibrary.StringParsing.GetNumbersFromString(str);
            var boxes = botLookup[currentBot];
            var low = Math.Min(boxes[0], boxes[1]);
            var high = Math.Max(boxes[0], boxes[1]);

            if (low == specialItem1 && high == specialItem2)
            {
                Console.WriteLine($"robot number {currentBot}");
            }

            var giveBot1 = false;
            var giveBot2 = false;
            botLookup[currentBot].Clear();

            var tokens = str.Split(delimiterChars);
            if (tokens[5].Equals("output"))
            {
                if (outputBins.ContainsKey(nums[1]))
                {
                    outputBins[nums[1]].Add(low);
                }
                else
                {
                    outputBins.Add(nums[1], new List<int>() { low });
                }
            }
            else
            {
                giveBot1 = true;
                if (botLookup.ContainsKey(nums[1]))
                {
                    botLookup[nums[1]].Add(low);
                }
                else
                {
                    botLookup.Add(nums[1], new List<int>() { low });
                }
            }
            if (tokens[10].Equals("output"))
            {
                if (outputBins.ContainsKey(nums[2]))
                {
                    outputBins[nums[2]].Add(high);
                }
                else
                {
                    outputBins.Add(nums[2], new List<int>() { high });
                }
            }
            else
            {
                giveBot2 = true;
                if (botLookup.ContainsKey(nums[2]))
                {
                    botLookup[nums[2]].Add(high);
                }
                else
                {
                    botLookup.Add(nums[2], new List<int>() { high });
                }
            }

            if (giveBot1)
            {
                GoTime(nums[1]);
            }
            if (giveBot2)
            {
                GoTime(nums[2]);
            }
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var counter = 0;

            botLookup = new Dictionary<int, List<int>>();
            botInstructions = new Dictionary<int, string>();
            outputBins = new Dictionary<int, List<int>>();
			
			foreach (var line in lines)
			{
				var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                if (line[0] == 'v')
                {
                    if (botLookup.ContainsKey(nums[1]))
                    {
                        botLookup[nums[1]].Add(nums[0]);
                    }
                    else
                    {
                        botLookup.Add(nums[1], new List<int>() { nums[0] });
                    }
                }
                else
                {
                    botInstructions.Add(nums[0], line);
                }
			}

            while (botLookup.Any(x => x.Value.Count() == 2))
            {
                foreach (var key in botLookup.Keys.ToList())
                {
                    GoTime(key);
                }
            }
            
            return outputBins[0][0] * outputBins[1][0] * outputBins[2][0];
        }
    }
}
