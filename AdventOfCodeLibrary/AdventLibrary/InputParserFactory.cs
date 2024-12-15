using System.Collections.Generic;

namespace AdventLibrary
{
    public static class InputParserFactory
    {
        public static InputParser CreateFromFile(string filePath)
        {
            var parser = new InputParser();
            parser.SetupFromFile(filePath);
            return parser;
        }

        public static InputParser CreateFromText(string text)
        {
            var parser = new InputParser();
            parser.SetupFromText(text);
            return parser;
        }

        public static InputParser CreateFromText(List<string> lines)
        {
            var parser = new InputParser();
            parser.SetupFromLines(lines);
            return parser;
        }
    }
}
