using AdventLibrary;
using System.Text.Json;

namespace aoc2015
{
    public class Day12 : ISolver
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
            var counter = 0;

            foreach (var line in lines)
            {
                var nums = AdventLibrary.StringParsing.GetIntssWithNegativesFromString(line);
                counter += nums.Sum();
            }
            return counter;
        }

        private object Part2()
        {
            string jsonString = ParseInput.GetTextFromFile(_filePath);

            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                return Walk(root);
            }
            return 0;
        }

        private int Walk(JsonElement current)
        {
            var count = 0;
            if (current.ValueKind.Equals(JsonValueKind.Object))
            {
                var properties = current.EnumerateObject().ToList();
                try
                {
                    foreach (var prop in properties)
                    {
                        var name = prop.Name;
                        var value = prop.Value;
                        count += Walk(value);
                    }
                }
                catch (System.Exception e)
                {
                    count = 0;
                }
            }
            else if (current.ValueKind.Equals(JsonValueKind.Array))
            {
                var elements = current.EnumerateArray().ToList();
                foreach (var elem in elements)
                {
                    try
                    {
                        count += Walk(elem);
                    }
                    catch
                    {
                    }
                }
            }
            else if (current.ValueKind.Equals(JsonValueKind.Number))
            {
                var num = current.GetInt32();
                return num;
            }
            else if (current.ValueKind.Equals(JsonValueKind.String))
            {
                var str = current.ToString();
                if (str.Equals("red"))
                {
                    throw new System.Exception("red");
                }
            }
            return count;
        }
    }
}