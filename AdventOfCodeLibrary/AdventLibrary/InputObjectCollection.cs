using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventLibrary
{
    public class InputObjectCollection
    {
        public InputObjectCollection(string filePath)
        {
            InputParser = new InputParser(filePath);
            Text = System.IO.File.ReadAllText(filePath);
            Lines = System.IO.File.ReadAllLines(filePath).ToList();
            Tokens = InputParser.GetTextAsTokenList();
            TokenLines = InputParser.GetLinesAsTokenLists();
            Longs = InputParser.GetTextAsLongs();
            LongLines = InputParser.GetLinesAsListLongs();
            Doubles = InputParser.GetTextAsDoubles();
            DoubleLines = InputParser.GetLinesAsListDoubles();
            Digits = InputParser.GetTextAsDigits();
            DigitLines = InputParser.GetLinesAsListDigits();
            IntGrid = InputParser.GetLinesAsGrid<int>();
            CharGrid = InputParser.GetLinesAsGrid<char>();
            Graph = InputParser.GetLinesAsGraph();
        }

        public InputParser InputParser { get; }

        public string Text { get; }

        public List<string> Lines { get; }

        public List<string> Tokens { get; }

        public List<List<string>> TokenLines { get; }

        public List<long> Longs { get; }

        public List<List<long>> LongLines { get; }

        public List<double> Doubles { get; }

        public List<List<double>> DoubleLines { get; }

        public List<long> Digits { get; }

        public List<List<long>> DigitLines { get; }

        public List<List<int>> IntGrid { get; }

        public List<List<char>> CharGrid { get; }

        public Dictionary<string, List<string>> Graph { get; }
    }
}