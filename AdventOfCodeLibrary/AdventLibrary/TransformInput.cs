using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    public static class TransformInput
    {
        public static List<List<bool>> ParseBoolGrid(List<string> input, char specialCharacter)
        {
            var list = new List<List<bool>>();
            foreach (var line in input)
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

        public static List<List<char>> ParseCharGrid(string input, char specialCharacter)
        {
            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var list = new List<List<char>>();
            foreach (var line in lines)
            {
                var lineList = new List<char>();
                foreach (var c in line)
                {
                    lineList.Add(c);
                }
                list.Add(lineList);
            }
            return list;
        }

        public static List<List<int>> ParseIntGrid(string input, char specialCharacter)
        {
            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var list = new List<List<int>>();
            foreach (var line in lines)
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
    }

}