using AdventLibrary;
using AdventLibrary.PathFinding;

namespace aoc2017
{
    public class Day12 : ISolver
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
            var dict = input.GraphUndirected;
            long count = 0;

            Func<string, List<string>> NeighboursFunc = (current) =>
            {
                if (dict.ContainsKey(current))
                {
                    return dict[current];
                }
                return new List<string>();
            };

            Func<string, bool> GoalFunc = (current) =>
            {
                return current.Equals("0");
            };

            foreach (var key in dict.Keys)
            {
                var start = new List<string>() { key };
                var DFS = new DepthFirstSearch<string>();
                DFS.DFS_Weightless(start, NeighboursFunc, GoalFunc);
                if (DFS.GoalAchieved)
                {
                    count++;
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var dict = input.GraphUndirected;
            var groups = new List<string>();
            long count = 0;

            foreach (var key in dict.Keys)
            {
                Queue<string> q = new Queue<string>();
                var fullPathHistory = new HashSet<string>();
                var head = key;
                q.Enqueue(head);
                var endsHash = new HashSet<string>();
                while (q.Count > 0)
                {
                    var current = q.Dequeue();

                    if (
                        current == null ||
                        fullPathHistory.Contains(current))
                        continue;

                    fullPathHistory.Add(current);

                    foreach (var neighbour in dict[current].Where(x => !fullPathHistory.Contains(x)))
                    {
                        q.Enqueue(neighbour);
                    }
                }

                var goalAchieved = false;
                foreach (var hist in fullPathHistory)
                {
                    if (groups.Contains(hist))
                    {
                        goalAchieved = true;
                        break;
                    }
                }
                if (!goalAchieved)
                {
                    groups.Add(key);
                }
            }

            return groups.Count;
        }
    }
}