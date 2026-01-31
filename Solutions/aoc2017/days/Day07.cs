using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2017
{
    public class Day07 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t', '(', ')' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var grid = input.GridChar;
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var graphUp = new Dictionary<string, HashSet<string>>();
            var graphDown = new Dictionary<string, HashSet<string>>();
            var weightLookup = new Dictionary<string, int>();

            if (lines.Count < 2)
            {
                return null;
            }
            foreach (var line in lines)
            {
                var tokens = line.GetRealTokens(delimiterChars);
                var currentNode = tokens[0];
                var weight = int.Parse(tokens[1]);
                var graphUpConnected = graphUp.GetOrCreate(currentNode, new HashSet<string>());
                graphDown.GetOrCreate(currentNode, new HashSet<string>());

                for (var i = 2; i < tokens.Count; i++)
                {
                    graphUpConnected.Add(tokens[i]);
                    var downConnected = graphDown.GetOrCreate(tokens[i], new HashSet<string>());
                    downConnected.Add(currentNode);
                }
            }
            return graphDown.Where(x => x.Value.Count == 0).First().Key;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var grid = input.GridChar;
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var graphUp = new Dictionary<string, HashSet<string>>();
            var graphDown = new Dictionary<string, HashSet<string>>();
            var weightLookup = new Dictionary<string, int>();

            if (lines.Count < 2)
            {
                return null;
            }
            foreach (var line in lines)
            {
                var tokens = line.GetRealTokens(delimiterChars);
                var currentNode = tokens[0];
                var weight = int.Parse(tokens[1]);
                weightLookup.Add(currentNode, weight);
                var graphUpConnected = graphUp.GetOrCreate(currentNode, new HashSet<string>());
                graphDown.GetOrCreate(currentNode, new HashSet<string>());

                for (var i = 2; i < tokens.Count; i++)
                {
                    graphUpConnected.Add(tokens[i]);
                    var downConnected = graphDown.GetOrCreate(tokens[i], new HashSet<string>());
                    downConnected.Add(currentNode);
                }
            }
            var weightAboveLookup = new Dictionary<string, int>();
            var rootStr = graphDown.Where(x => x.Value.Count == 0).First().Key;

            int ans;
            GetImbalance(rootStr, graphUp, weightLookup, out ans);
            return ans;
        }

        private bool GetImbalance(string key, Dictionary<string, HashSet<string>> graphUp, Dictionary<string, int> weightLookup, out int answer)
        {
            answer = 0;
            var currentNode = graphUp[key];
            if (currentNode.Count == 0)
            {
                answer = weightLookup[key];
                return false;
            }

            var answers = new List<(string key, int upWeight)>();
            foreach (var linked in currentNode)
            {
                int temp;
                var result = GetImbalance(linked, graphUp, weightLookup, out temp);
                if (result)
                {
                    answer = temp;
                    return true;
                }
                else
                {
                    answers.Add((linked, temp));
                }
            }
            if (answers.Select(x => x.upWeight).ToList().AllItemsSame())
            {
                answer = answers.Sum(x => x.upWeight) + weightLookup[key];
                return false;
            }
            else
            {
                var countDict = answers.Select(x => x.upWeight).ToList().GetCountsOfItems();
                var rightWeight = countDict.First(x => x.Value != 1).Key;
                var wrongWeight = countDict.First(x => x.Value == 1).Key;
                var wrongNode = answers.First(x => x.upWeight == wrongWeight);
                answer = weightLookup[wrongNode.key] - (wrongWeight - rightWeight);
                return true;
            }
        }
    }
}