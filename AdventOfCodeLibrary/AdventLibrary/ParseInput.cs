using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventLibrary
{
    public static class ParseInput
    {
        private static char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public static string GetTextFromFile(string fileName)
        {
            string text = System.IO.File.ReadAllText(fileName);
            return text;
        }

        public static List<string> GetLinesFromFile(string fileName)
        {
            return System.IO.File.ReadAllLines(fileName).ToList();
        }

        public static List<int> GetNumbersFromFile(string fileName)
        {
            try
            {
                return System.IO.File.ReadAllLines(fileName).ToList().Select(x => Int32.Parse(x)).ToList();
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input is something other than all numbers");
            }
            return null;
        }

        public static T ChangeType<T>(this string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static List<T> TokenizeAndParseIntoList<T>(string input, string splitChar)
        {
            var lines = input.Split(new string[] { splitChar }, StringSplitOptions.None);
            var list = new List<T>();
            foreach (var line in lines)
            {
                var cleanLine = line.Trim();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    list.Add(ChangeType<T>(line));
                }
            }
            return list;
        }

        public static List<T> ParseLinesAsType<T>(string text)
        {
            return TokenizeAndParseIntoList<T>(text, "\n");
        }

        public static List<T> ParseCommaSeperatedAsType<T>(string text)
        {
            return TokenizeAndParseIntoList<T>(text, ",");
        }

        // This could probably be generic
        public static List<List<int>> ParseFileAsGrid(string filePath)
        {
			var lines = GetLinesFromFile(filePath);
            var grid = new List<List<int>>();
            for (var i = 0; i < lines.Count; i++)
            {
                grid.Add(new List<int>());
                var nums = StringParsing.GetDigitsFromString(lines[i]);
                for (var j = 0; j < nums.Count; j++)
                {
                    grid[i].Add(nums[j]);
                }
            }
            return grid;
        }
        
        public static List<List<char>> ParseFileAsCharGrid(string filePath)
        {
			var lines = GetLinesFromFile(filePath);
            var grid = new List<List<char>>();
            for (var i = 0; i < lines.Count; i++)
            {
                grid.Add(new List<char>());
                for (var j = 0; j < lines[i].Length; j++)
                {
                    grid[i].Add(lines[i][j]);
                }
            }
            return grid;
        }

        public static List<List<bool>> ParseFileAsBoolGrid(string filePath, char special)
        {
            var lines = GetLinesFromFile(filePath);
            var grid = new List<List<bool>>();
            for (var i = 0; i < lines.Count; i++)
            {
                grid.Add(new List<bool>());
                for (var j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == special)
                    {
                        grid[i].Add(true);
                    }
                    else
                    {
                        grid[i].Add(false);
                    }
                }
            }
            return grid;
        }

        public static Dictionary<string, List<string>> ParseFileAsGraph(string filePath)
        {
            var nodes = new Dictionary<string, List<string>>();
			var lines = GetLinesFromFile(filePath);
            if (lines.Count < 2)
            {
                return null;
            }
            foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
                if (nodes.ContainsKey(tokens[0]))
                {
                    nodes[tokens[0]].Add(tokens[1]);
                }
                else
                {
                    nodes.Add(tokens[0], new List<string>() { tokens[1] });
                }
                if (nodes.ContainsKey(tokens[1]))
                {
                    nodes[tokens[1]].Add(tokens[0]);
                }
                else
                {
                    nodes.Add(tokens[1], new List<string>() { tokens[0] });
                }
			}
            return nodes;
        }
    }
}
