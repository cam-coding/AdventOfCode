﻿using AdventLibrary.Helpers.Grids;
using System.Collections.Generic;
using System.Linq;

namespace AdventLibrary
{
    public class InputObjectCollection
    {
        public InputObjectCollection(string filePath)
        {
            InputParser = InputParserFactory.CreateFromFile(filePath);

            Text = System.IO.File.ReadAllText(filePath);
            Lines = System.IO.File.ReadAllLines(filePath).ToList();
            LineGroupsSeperatedByWhiteSpace = GetLineGroups(Lines);
            Tokens = InputParser.GetTextAsTokenList();
            TokenLines = InputParser.GetLinesAsTokenLists();
            Longs = InputParser.GetTextAsLongs();
            Long = Longs != null && Longs.Any() ? Longs[0] : 0;
            LongsWithNegatives = InputParser.GetTextAsLongsWithNegatives();
            LongLines = InputParser.GetLinesAsListLongs();
            LongLinesWithNegatives = InputParser.GetLinesAsListLongsWithNegatives();
            Doubles = InputParser.GetTextAsDoubles();
            DoubleLines = InputParser.GetLinesAsListDoubles();
            Digits = InputParser.GetTextAsDigits();
            DigitLines = InputParser.GetLinesAsListDigits();
            GridInt = InputParser.GetLinesAsGrid<int>();
            GridChar = InputParser.GetLinesAsGrid<char>();
            Coords = InputParser.GetLinesAsCoords();
            GraphDirected = InputParser.GetLinesAsGraphDirected();
            GraphUndirected = InputParser.GetLinesAsGraphUndirected();
        }

        public InputParser InputParser { get; }

        public string Text { get; }

        public List<string> Lines { get; }

        public List<List<string>> LineGroupsSeperatedByWhiteSpace { get; }

        public List<string> Tokens { get; }

        public List<List<string>> TokenLines { get; }

        // If there's just a single number as input, this will be it
        public long Long { get; }

        public List<long> Longs { get; }

        public List<long> LongsWithNegatives { get; }

        public List<List<long>> LongLines { get; }
        public List<List<long>> LongLinesWithNegatives { get; }

        public List<double> Doubles { get; }

        public List<List<double>> DoubleLines { get; }

        public List<long> Digits { get; }

        public List<List<long>> DigitLines { get; }

        public GridObject<int> GridInt { get; }

        public GridObject<char> GridChar { get; }

        public List<GridLocation<int>> Coords { get; }

        public Dictionary<string, List<string>> GraphDirected { get; }

        public Dictionary<string, List<string>> GraphUndirected { get; }

        private List<List<string>> GetLineGroups(List<string> lines)
        {
            var groups = new List<List<string>>();
            for (var i = 0; i < lines.Count; i++)
            {
                var group = new List<string>();
                for (var j = i; j < lines.Count; j++)
                {
                    if (string.IsNullOrWhiteSpace(lines[j]))
                    {
                        i = j;
                        break;
                    }
                    group.Add(lines[j]);
                    i = j;
                }
                if (group.Count > 0)
                {
                    groups.Add(group);
                }
            }
            return groups;
        }
    }
}