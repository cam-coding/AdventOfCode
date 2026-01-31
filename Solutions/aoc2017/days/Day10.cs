using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2017
{
    public class Day10 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var numbers = input.Longs.Select(x => (int)x);

            var length = isTest ? 5 : 256;
            var listy = new List<int>();
            for (var i = 0; i < length; i++)
            {
                listy.Add(i);
            }

            var skipSize = 0;
            var currentPos = 0;

            foreach (var num in numbers)
            {
                if (num > listy.Count)
                {
                    continue;
                }

                // reverse list
                var half = num / 2;
                for (var i = 0; i < half; i++)
                {
                    var frontIndex = listy.GetWrappedIndex(currentPos + i);
                    var endIndex = listy.GetWrappedIndex(currentPos + ((num - 1) - i));
                    var temp = listy[frontIndex];
                    listy[frontIndex] = listy[endIndex];
                    listy[endIndex] = temp;
                }

                currentPos = currentPos + num + skipSize;
                skipSize += 1;
            }

            return listy[0] * listy[1];
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var line = input.Lines[0];
            if (isTest)
            {
                line = "1,2,3";
            }
            var hasher = new KnotHasher(line);
            return hasher.GenerateHash();
        }
    }

    public class KnotHasher
    {
        private List<int> _lengths;
        private List<int> _numberList;

        public KnotHasher(string input)
        {
            var lengths = input.Select(x => 0 + x).ToList();
            lengths.AddRange(new List<int>() { 17, 31, 73, 47, 23 });
            _lengths = lengths;
            _numberList = new List<int>();
            for (var i = 0; i < 256; i++)
            {
                _numberList.Add(i);
            }
        }

        public string GenerateHash()
        {
            var skipSize = 0;
            var currentPos = 0;
            for (var round = 0; round < 64; round++)
            {
                foreach (var num in _lengths)
                {
                    if (num > _numberList.Count)
                    {
                        continue;
                    }

                    // reverse list
                    var half = num / 2;
                    for (var i = 0; i < half; i++)
                    {
                        var frontIndex = _numberList.GetWrappedIndex(currentPos + i);
                        var endIndex = _numberList.GetWrappedIndex(currentPos + ((num - 1) - i));
                        var temp = _numberList[frontIndex];
                        _numberList[frontIndex] = _numberList[endIndex];
                        _numberList[endIndex] = temp;
                    }

                    currentPos = currentPos + num + skipSize;
                    skipSize += 1;
                }
            }

            return ReduceSparseHash(_numberList);
        }

        private static string ReduceSparseHash(List<int> nums)
        {
            var denseHash = new List<int>();
            for (var i = 0; i < 16; i++)
            {
                var offset = i * 16;
                var value = nums[offset];
                for (var j = 1; j < 16; j++)
                {
                    value = BitwiseHelper.XOR(value, nums[offset + j]);
                }
                denseHash.Add(value);
            }

            var returnString = string.Empty;
            foreach (var num in denseHash)
            {
                returnString += ConversionHelper.ConvertToHexString(num, 2);
            }

            return returnString;
        }
    }
}