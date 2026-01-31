using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2021
{
    public class Day21 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        private List<int> _rollMemo;
        private Dictionary<(int, int, int, int, int), List<long>> _memo;

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var playerPositions = new List<long>() { input.Longs[1], input.Longs[3] };
            var playerScores = new List<long>() { 0, 0 };
            var currentDiceValue = 0;
            var rollCount = 0;

            while (true)
            {
                for (var player = 0; player < 2; player++)
                {
                    var roll = 0;
                    for (var i = 1; i < 4; i++)
                    {
                        currentDiceValue++;
                        if (currentDiceValue == 101)
                        {
                            currentDiceValue = 1;
                        }
                        roll += currentDiceValue;
                    }
                    rollCount += 3;

                    playerPositions[player] += roll;
                    playerScores[player] += ((playerPositions[player] - 1) % 10) + 1;

                    if (playerScores[player] >= 1000)
                    {
                        return playerScores[MathHelper.GetOppositeIntBool(player)] * rollCount;
                    }
                }
            }
        }

        private object Part2()
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var playerPositions = new List<int>() { (int)input.Longs[1], (int)input.Longs[3] };
            var playerScores = new List<int>() { 0, 0 };
            // SetupRollMemo();

            SetupRollMemo();
            _memo = new Dictionary<(int, int, int, int, int), List<long>>();
            var ans = Recursion(0, playerScores, playerPositions);

            return ans.Max();
        }

        private void SetupRollMemo()
        {
            // pos to all possible scores from there
            _rollMemo = new List<int>();
            var listy = new List<int>() { 1, 2, 3 };
            var blah = listy.GetPermutationsWithRepetitions(3);
            foreach (var item in blah)
            {
                _rollMemo.Add(item.Sum());
            }
        }

        private List<long> Recursion(int playerTurn, List<int> playerScores, List<int> playerPositions)
        {
            var adjustedPositions = playerPositions.Select(x => ((x - 1) % 10) + 1).ToList();
            var tuple = (playerTurn, playerScores[0], playerScores[1], adjustedPositions[0], adjustedPositions[1]);
            // check for memo
            if (_memo.ContainsKey(tuple))
            {
                return _memo[tuple];
            }

            var otherPlayer = MathHelper.GetOppositeIntBool(playerTurn);
            var wins = new List<long>() { 0, 0 };
            if (playerScores[otherPlayer] >= 21)
            {
                wins[otherPlayer]++;
                _memo.Add(tuple, wins);
                return wins;
            }

            foreach (var item in _rollMemo)
            {
                var newScores = playerScores.Clone();
                var newPlayerPositions = playerPositions.Clone();
                newPlayerPositions[playerTurn] += item;
                newScores[playerTurn] += ((newPlayerPositions[playerTurn] - 1) % 10) + 1;
                var results = Recursion(MathHelper.GetOppositeIntBool(playerTurn), newScores, newPlayerPositions);
                wins[0] += results[0];
                wins[1] += results[1];
            }
            _memo.Add(tuple, wins);
            return wins;
        }
    }
}