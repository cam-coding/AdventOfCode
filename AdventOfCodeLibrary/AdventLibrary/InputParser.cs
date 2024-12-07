using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventLibrary
{
    /* This Class is similar to ParseInput expect it already has the data from the file.
     * Usually this is wrapped in InputObjectCollection but I'm leaving this public in case
     * I need access suddenly.
     * */

    public class InputParser
    {
        private static char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t', '\n', '\r' };
        private static char[] delimiterCharsSansPeriod = { ' ', ',', ':', '-', '>', '<', '+', '\t', '\n', '\r' };
        private static char[] lineEndingChars = { '\n', '\r' };
        private string _text;
        private string _textNoLineBreaks;
        private List<string> _lines;

        public InputParser(string filePath)
        {
            _text = System.IO.File.ReadAllText(filePath);
            _lines = System.IO.File.ReadAllLines(filePath).ToList();
            _textNoLineBreaks = GetTextWithoutLineBreaks();
        }

        public List<long> GetTextAsLongs()
        {
            try
            {
                var tokens = _text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                var longs = tokens.Select(x => Int64.Parse(x)).ToList();
                return longs;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public List<long> GetTextAsLongsWithNegatives()
        {
            return StringParsing.GetLongsWithNegativesFromString(_text).Select(x => x).ToList();
        }

        public List<List<long>> GetLinesAsListLongsWithNegatives()
        {
            var numbers = new List<List<long>>();
            foreach (var line in _lines)
            {
                numbers.Add(StringParsing.GetLongsWithNegativesFromString(line).Select(x => x).ToList());
            }

            return numbers;
        }

        public List<List<long>> GetLinesAsListLongs()
        {
            try
            {
                var returnList = new List<List<long>>();
                foreach (var line in _lines)
                {
                    var tokens = line.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                    var longs = tokens.Select(x => Int64.Parse(x)).ToList();
                    returnList.Add(longs);
                }
                return returnList;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public List<long> GetTextAsDigits()
        {
            try
            {
                return StringParsing.GetDigitsFromString(_textNoLineBreaks).Select(x => (long)x).ToList();
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public List<List<long>> GetLinesAsListDigits()
        {
            try
            {
                var returnList = new List<List<long>>();
                foreach (var line in _lines)
                {
                    var digits = StringParsing.GetDigitsFromString(line).Select(x => (long)x).ToList();
                    returnList.Add(digits);
                }
                return returnList;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public List<double> GetTextAsDoubles()
        {
            try
            {
                var tokens = _text.Split(delimiterCharsSansPeriod, StringSplitOptions.RemoveEmptyEntries);
                var doubles = tokens.Select(x => Double.Parse(x)).ToList();
                return doubles;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public List<List<double>> GetLinesAsListDoubles()
        {
            try
            {
                var returnList = new List<List<double>>();
                foreach (var line in _lines)
                {
                    var tokens = line.Split(delimiterCharsSansPeriod, StringSplitOptions.RemoveEmptyEntries);
                    var longs = tokens.Select(x => Double.Parse(x)).ToList();
                    returnList.Add(longs);
                }
                return returnList;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public List<string> GetTextAsTokenList()
        {
            var lines = _text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<string>();
            foreach (var line in lines)
            {
                var cleanLine = line.Trim();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    list.Add(line);
                }
            }
            return list;
        }

        public List<List<string>> GetLinesAsTokenLists()
        {
            var list = new List<List<string>>();
            foreach (var line in _lines)
            {
                var currentList = new List<string>();
                var tokens = line.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                foreach (var token in tokens)
                {
                    var cleanLine = token.Trim();
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        currentList.Add(token);
                    }
                }
                list.Add(currentList);
            }
            return list;
        }

        public GridObject<T> GetLinesAsGrid<T>()
        {
            try
            {
                var grid = new List<List<T>>();
                for (var i = 0; i < _lines.Count; i++)
                {
                    grid.Add(new List<T>());
                    for (var j = 0; j < _lines[i].Length; j++)
                    {
                        var typedObject = ChangeType<T>(_lines[i][j].ToString());
                        grid[i].Add(typedObject);
                    }
                }
                return new GridObject<T>(grid);
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public T ChangeType<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public Dictionary<string, List<string>> GetLinesAsGraph()
        {
            try
            {
                var nodes = new Dictionary<string, List<string>>();
                if (_lines.Count < 2)
                {
                    return null;
                }
                foreach (var line in _lines)
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
            catch (Exception e)
            {
            }
            return null;
        }

        public List<List<bool>> ParseFileAsBoolGrid(string filePath, char specialCharacter)
        {
            try
            {
                var list = new List<List<bool>>();
                foreach (var line in _lines)
                {
                    var lineList = new List<bool>();
                    foreach (var c in line)
                    {
                        lineList.Add(c.Equals(specialCharacter));
                    }
                    list.Add(lineList);
                }
                return list;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input is something other than a grid");
            }
            return null;
        }

        public List<List<int>> ParseFileAsBoolIntGrid(string filePath, char specialCharacter)
        {
            try
            {
                var list = new List<List<int>>();
                foreach (var line in _lines)
                {
                    var lineList = new List<int>();
                    foreach (var c in line)
                    {
                        lineList.Add(c.Equals(specialCharacter) ? 0 : 1);
                    }
                    list.Add(lineList);
                }
                return list;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input is something other than a grid");
            }
            return null;
        }

        private string GetTextWithoutLineBreaks()
        {
            var tokens = _text.Split(lineEndingChars, StringSplitOptions.RemoveEmptyEntries);
            return StringExtensions.ConcatListOfStrings(tokens.ToList());
        }
    }
}