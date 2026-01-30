using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day14: ISolver
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
            var dict = new Dictionary<string, (int speed, int time, int restTime)>();
            var finish = 2503;
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetIntsFromString(line);

                dict.Add(tokens[0], (nums[0], nums[1], nums[2]));
            }

            var best = 0;
            foreach (var num in dict)
            {
                var div = finish / (num.Value.time + num.Value.restTime);
                var rem = finish % (num.Value.time + num.Value.restTime);
                rem = Math.Min(rem, num.Value.time);

                var distance = div * num.Value.time * num.Value.speed;
                distance += rem * num.Value.speed;

                if (distance > best)
                {
                    best = distance;
                }

            }
            return best;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var dict = new Dictionary<string, (int speed, int time, int restTime)>();
            var scores = new Dictionary<string, int>();
            var finish = 2503;

            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars);
                var nums = AdventLibrary.StringParsing.GetIntsFromString(line);

                dict.Add(tokens[0], (nums[0], nums[1], nums[2]));
                scores.Add(tokens[0], 0);
            }

            for (var i = 1; i <= 2503; i++)
            {
                var leader = new List<KeyValuePair<string, (int speed, int time, int restTime)>>() { dict.First() };
                var best = 0;
                foreach (var num in dict)
                {
                    var div = i / (num.Value.time + num.Value.restTime);
                    var rem = i % (num.Value.time + num.Value.restTime);
                    rem = Math.Min(rem, num.Value.time);

                    var distance = div * num.Value.time * num.Value.speed;
                    distance += rem * num.Value.speed;

                    if (distance > best)
                    {
                        best = distance;
                        leader.Clear();
                        leader.Add(num);
                    }
                    else if (distance == best)
                    {
                        leader.Add(num);
                    }
                }
                foreach (var item in leader)
                {
                    scores[item.Key] += 1;
                }
            }
            return scores.Max(x => x.Value);
        }
    }
}