using AdventLibrary;

namespace aoc2015
{
    public class Day03 : ISolver
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
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var nodes = ParseInput.ParseFileAsGraph(_filePath);
            var grid = ParseInput.ParseFileAsGrid(_filePath);
            var total = 1000000;
            var counter = 0;
            (int x, int y) currentPos = (0, 0);

            var history = new HashSet<(int x, int y)>();

            foreach (var line in lines)
            {
                foreach (var c in line)
                {
                    if (c == '^')
                    {
                        currentPos.y++;
                    }
                    else if (c == 'v')
                    {
                        currentPos.y--;
                    }
                    else if (c == '>')
                    {
                        currentPos.x++;
                    }
                    else if (c == '<')
                    {
                        currentPos.x--;
                    }
                    history.Add((currentPos.x, currentPos.y));
                }
            }
            return history.Count;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var nodes = ParseInput.ParseFileAsGraph(_filePath);
            var grid = ParseInput.ParseFileAsGrid(_filePath);
            var total = 1000000;
            var counter = 0;
            var history = new HashSet<(int x, int y)>();
            var posDict = new (int x, int y)[2];
            posDict[0] = (0, 0);
            posDict[1] = (0, 0);
            var robo = 0;

            foreach (var line in lines)
            {
                foreach (var c in line)
                {
                    if (c == '^')
                    {
                        posDict[robo].y++;
                    }
                    else if (c == 'v')
                    {
                        posDict[robo].y--;
                    }
                    else if (c == '>')
                    {
                        posDict[robo].x++;
                    }
                    else if (c == '<')
                    {
                        posDict[robo].x--;
                    }
                    history.Add((posDict[robo].x, posDict[robo].y));
                    robo = 1 - robo;
                }
            }
            return history.Count;
        }
    }
}