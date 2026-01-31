using AdventLibrary;

namespace aoc2016
{
    public class Day02 : ISolver
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

            var dict = new Dictionary<(int X, int Y), char>()
            {
                { (0,2), '1'},
                { (-1,1), '2'},
                { (0,1), '3'},
                { (1,1), '4'},
                { (-2,0), '5'},
                { (-1,0), '6'},
                { (0,0), '7'},
                { (1,0), '8'},
                { (2,0), '9'},
                { (-1,-1), 'A'},
                { (0,-1), 'B'},
                { (1,-1), 'C'},
                { (0,-2), 'D'},
            };
            var answer = string.Empty;

            var x = -2;
            var y = 0;
            foreach (var line in lines)
            {

                foreach (var c in line)
                {
                    var specialX = Math.Abs(Math.Abs(y) - 2);
                    var specialY = Math.Abs(Math.Abs(x) - 2);
                    if (c == 'U')
                        y = y + 1;
                    if (c == 'D')
                        y = y - 1;
                    if (c == 'L')
                        x = x - 1;
                    if (c == 'R')
                        x = x + 1;

                    x = Math.Min(x, specialX * 1);
                    x = Math.Max(x, specialX * -1);
                    y = Math.Min(y, specialY * 1);
                    y = Math.Max(y, specialY * -1);
                }
                answer = answer + dict[(x, y)];
            }
            return answer;
        }

        private object Part2()
        {
            return 0;
        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 &&
            x < 3 &&
            y >= 0 &&
            y < 3;
        }
    }
}
