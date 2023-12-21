using AdventLibrary;
using System.Collections.Generic;
using System.Linq;

namespace aoc2016
{
    public class Day17: ISolver
  {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        private char[] openDoors = { 'b', 'c', 'd', 'e', 'f' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var key = lines[0];
            var best = int.MaxValue;
            var seen = new HashSet<string>();
            var solution = string.Empty;

            Queue<(int x, int y, string path)> q = new Queue<(int x, int y, string path)>();
            q.Enqueue((0, 3, string.Empty));

            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if (current.x == 3 && current.y == 0)
                {
                    if (current.path.Length < best)
                    {
                        best = current.path.Length;
                        solution = current.path;
                    }
                }
                else if (current.x < 4 &&
                        current.x >= 0 &&
                        current.y < 4 &&
                        current.y >= 0 &&
                        current.path.Length < best &&
                        !seen.Contains(current.path))
                {
                    seen.Add(current.path);
                    var hash = HashHelper.GetMd5HashAsHexString(key + current.path).Substring(0, 4).ToLower();
                    if (openDoors.Contains(hash[0]))
                    {
                        q.Enqueue((current.x, current.y + 1, current.path + "U"));
                    }
                    if (openDoors.Contains(hash[1]))
                    {
                        q.Enqueue((current.x, current.y - 1, current.path + "D"));
                    }
                    if (openDoors.Contains(hash[2]))
                    {
                        q.Enqueue((current.x - 1, current.y, current.path + "L"));
                    }
                    if (openDoors.Contains(hash[3]))
                    {
                        q.Enqueue((current.x + 1, current.y, current.path + "R"));
                    }
                }
            }
            return solution;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var key = lines[0];
            var best = 0;
            var seen = new HashSet<string>();
            var solution = string.Empty;

            Queue<(int x, int y, string path)> q = new Queue<(int x, int y, string path)>();
            q.Enqueue((0, 3, string.Empty));

            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if (current.x == 3 && current.y == 0)
                {
                    if (current.path.Length > best)
                    {
                        best = current.path.Length;
                        solution = current.path;
                    }
                }
                else if (current.x < 4 &&
                        current.x >= 0 &&
                        current.y < 4 &&
                        current.y >= 0 &&
                        !seen.Contains(current.path))
                {
                    seen.Add(current.path);
                    var hash = HashHelper.GetMd5HashAsHexString(key + current.path).Substring(0, 4).ToLower();
                    if (openDoors.Contains(hash[0]))
                    {
                        q.Enqueue((current.x, current.y + 1, current.path + "U"));
                    }
                    if (openDoors.Contains(hash[1]))
                    {
                        q.Enqueue((current.x, current.y - 1, current.path + "D"));
                    }
                    if (openDoors.Contains(hash[2]))
                    {
                        q.Enqueue((current.x - 1, current.y, current.path + "L"));
                    }
                    if (openDoors.Contains(hash[3]))
                    {
                        q.Enqueue((current.x + 1, current.y, current.path + "R"));
                    }
                }
            }
            return solution.Length;
        }
    }
}
