using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    public static class ParseInput
    {
        public static string GetText(string fileName)
        {
            string text = System.IO.File.ReadAllText(fileName);
            return text;
        }

        public static List<string> GetLines(string fileName)
        {
            string text = System.IO.File.ReadAllText(fileName);
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return new List<string>(lines);
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
                list.Add(ChangeType<T>(line));
            }
            return list;
        }

        public static List<T> ParseLinesAsType<T>(string text)
        {
            return TokenizeAndParseIntoList<T>(text, Environment.NewLine);
        }

        public static List<T> ParseCommaSeperatedAsType<T>(string text)
        {
            return TokenizeAndParseIntoList<T>(text, ",");
        }
    }
}
