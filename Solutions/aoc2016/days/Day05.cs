using AdventLibrary;

namespace aoc2016
{
    public class Day05 : ISolver
    {
        private System.Security.Cryptography.MD5 _md5;
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var line = ParseInput.GetLinesFromFile(_filePath)[0];
            var answer = string.Empty;
            long count = 0;


            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 _md5 = System.Security.Cryptography.MD5.Create())
            {
                while (true)
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(line + count);
                    byte[] hashBytes = _md5.ComputeHash(inputBytes);

                    var hash = Convert.ToHexString(hashBytes);
                    if (hash[0] == '0' &&
                        hash[1] == '0' &&
                        hash[2] == '0' &&
                        hash[3] == '0' &&
                        hash[4] == '0')
                    {
                        answer = answer + hash[5];
                        if (answer.Length == 8)
                        {
                            return answer;
                        }
                    }
                    count++;
                }
            }
            return 0;
        }

        private object Part2()
        {
            var line = ParseInput.GetLinesFromFile(_filePath)[0];
            var answer = "$$$$$$$$".ToArray();
            long count = 0;


            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 _md5 = System.Security.Cryptography.MD5.Create())
            {
                while (true)
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(line + count);
                    byte[] hashBytes = _md5.ComputeHash(inputBytes);

                    var hash = Convert.ToHexString(hashBytes);
                    if (hash[0] == '0' &&
                        hash[1] == '0' &&
                        hash[2] == '0' &&
                        hash[3] == '0' &&
                        hash[4] == '0')
                    {
                        if (Char.IsDigit(hash[5]))
                        {
                            var num = Int32.Parse(hash[5].ToString());
                            if (num < 8)
                            {
                                if (answer[num] == '$')
                                {
                                    answer[num] = hash[6];
                                }
                                if (answer.All(x => x != '$'))
                                {
                                    return answer;
                                }
                            }
                        }
                    }
                    count++;
                }
            }
            return 0;
        }
    }
}
