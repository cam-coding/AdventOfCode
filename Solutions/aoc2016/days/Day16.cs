using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2016
{
    public class Day16 : ISolver
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
            string str = "01111010110010011";
            var MaxLength = 272;

            while (str.Length < MaxLength)
            {
                var b = StringExtensions.ReverseString(str);
                char[] charArray = b.ToCharArray();
                for (var i = 0; i < charArray.Length; i++)
                {
                    charArray[i] = charArray[i] == '0' ? '1' : '0';
                }
                str = str + "0" + new string(charArray);
            }

            var checksum = str.Substring(0, MaxLength).ToCharArray();
            while (checksum.Length % 2 == 0)
            {
                var temp = new string('0', checksum.Length / 2).ToCharArray();
                for (var i = 0; i < checksum.Length; i = i + 2)
                {
                    if (checksum[i] == checksum[i + 1])
                    {
                        temp[i / 2] = '1';
                    }
                }
                checksum = temp;
            }

            return new string(checksum);
        }

        private object Part2()
        {
            string str = "01111010110010011";
            var MaxLength = 35651584;

            while (str.Length < MaxLength)
            {
                var b = StringExtensions.ReverseString(str);
                char[] charArray = b.ToCharArray();
                for (var i = 0; i < charArray.Length; i++)
                {
                    charArray[i] = charArray[i] == '0' ? '1' : '0';
                }
                str = str + "0" + new string(charArray);
            }

            var checksum = str.Substring(0, MaxLength).ToCharArray();
            while (checksum.Length % 2 == 0)
            {
                var temp = new string('0', checksum.Length / 2).ToCharArray();
                for (var i = 0; i < checksum.Length; i = i + 2)
                {
                    if (checksum[i] == checksum[i + 1])
                    {
                        temp[i / 2] = '1';
                    }
                }
                checksum = temp;
            }

            return new string(checksum);
        }
    }
}
