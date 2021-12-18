using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day14: ISolver
    {
        private string _filePath;
        private string _inputString;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var i = 0;
            var start = string.Empty;
            var pairs = new Dictionary<string, string>();
			
			foreach (var line in lines)
			{
                if (i == 0)
                {
                    start = line;
                    _inputString = line;
                }
                else if (i == 1)
                {
                }
                else
                {
                    var tokens = line.Split(delimiterChars);
                    pairs.Add(tokens[0], tokens[4]);
                }
                i++;
			}

            for (var j = 0; j < 10; j++)
            {
                var blah = start;
                var add = new Dictionary<int, string>();
                foreach(var item in pairs)
                {
                    var indexes = GetInstancesOf(start, item.Key);
                    foreach (var index in indexes)
                    {
                        if (add.ContainsKey(index))
                        {
                            add[index+1] = add[index] + item.Value;
                        }
                        else
                        {
                            add.Add(index, item.Value);
                        }
                    }
                }
                start = string.Empty;
                for (int k = 0; k < blah.Length; k++)
                {
                    start = start + blah[k].ToString();
                    if (add.ContainsKey(k))
                    {
                        start = start + add[k];
                    }
                }
            }
            return Count(start, pairs);
        }
        
        private object Part2()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var start = string.Empty;
            var i = 0;
            var pairs = new Dictionary<string, string>();
            var myStrings = new Dictionary<string, long>();
			
			foreach (var line in lines)
			{
                if (i == 0)
                {
                    start = line;
                }
                else if (i == 1)
                {
                }
                else
                {
                    var tokens = line.Split(delimiterChars);
                    pairs.Add(tokens[0], tokens[4]);
                }
                i++;
			}

            for (var j = 1; j < start.Length; j++)
            {
                var sub = start.Substring(j-1, 2);
                if (myStrings.ContainsKey(sub))
                {
                    myStrings[sub] = myStrings[sub] + 1;
                }
                else
                {
                    myStrings.Add(sub, 1);
                }
            }

            for (var j = 0; j < 40; j++)
            {
                var blah = new Dictionary<string,long>(myStrings);

                foreach (var item in pairs)
                {
                    if (myStrings.ContainsKey(item.Key))
                    {
                        var newKey = item.Key[0].ToString() + item.Value;
                        var newKey2 = item.Value + item.Key[1].ToString();
                        blah = AddIfNew(blah, newKey, myStrings[item.Key]);
                        blah = AddIfNew(blah, newKey2, myStrings[item.Key]);
                    }
                }


                foreach (var dead in myStrings)
                {
                    blah[dead.Key] = blah[dead.Key] - dead.Value;
                    if (blah[dead.Key] == 0)
                    {
                        blah.Remove(dead.Key);
                    }
                }
                myStrings = blah;
            }
            return Count(myStrings);
        }

        private Dictionary<string, long> AddIfNew(Dictionary<string, long> dict, string key, long value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = dict[key] + value;
            }
            else
            {
                dict.Add(key, value);
            }
            return dict;
        }

        private List<int> GetInstancesOf(string main, string search)
        {
            var foundIndexes = new List<int>();
            for (int i = main.IndexOf(search); i > -1; i = main.IndexOf(search, i + 1))
            {
                    foundIndexes.Add(i);
            }
            return foundIndexes;
        }

        private int Count(string input, Dictionary<string,string> pairs)
        {
            var keys = pairs.Values.ToList();
            var most = 0;
            var least = int.MaxValue;
            foreach (var key in keys)
            {
                var temp = input.Split(key).Length -1;
                if (temp > most)
                {
                    most = temp;
                }
                if (temp < least)
                {
                    least = temp;
                }
            }
            return most-least;
        }

        private double Count(Dictionary<string,long> pairs)
        {
            var letterDict = new Dictionary<string, long>();
            foreach (var item in pairs)
            {
                AddIfNew(letterDict, item.Key[0].ToString(), item.Value);
            }
            letterDict[_inputString[0].ToString()]++;
            letterDict[_inputString[_inputString.Length-1].ToString()]++;
            return letterDict.Max(x => x.Value)-letterDict.Min(x => x.Value);
        }
    }
}