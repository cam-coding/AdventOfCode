using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day04: ISolver
    {
        public Solution Solve(string filePath, bool isTest = false)
        {
            return new Solution(Part1(filePath), Part2(filePath));
        }

        private object Part1(string filePath)
        {
            var strings = AdventLibrary.ParseInput.GetLinesFromFile(filePath);
            // first line is the numbers being called for bingo
            var calling = new Queue<int>(AdventLibrary.ParseInput.ParseCommaSeperatedAsType<int>(strings.First()));
            var cards = new List<List<List<int>>>();
            var current = new List<List<int>>();
            var called = new List<int>();

            foreach (var line in strings.Skip(2))
            {
                if (line.Equals(string.Empty))
                {
                    cards.Add(current);
                    current = new List<List<int>>();
                }
                else
                {
                    current.Add(AdventLibrary.ParseInput.TokenizeAndParseIntoList<int>(line, " "));
                }
            }
            cards.Add(current);

            while (true)
            {
                called.Add(calling.Dequeue());
                foreach (var card in cards)
                {
                    if (WinningCard(card, called))
                    {
                        var sum = card.SelectMany(x => x).ToList().Where(x => !called.Contains(x)).Sum();
                        return sum*called.Last();
                    }
                }
            }
        }
        
        private object Part2(string filePath)
        {
            var strings = AdventLibrary.ParseInput.GetLinesFromFile(filePath);
            var calling = new Queue<int>(AdventLibrary.ParseInput.ParseCommaSeperatedAsType<int>(strings.First()));
            var cardsOG = new List<List<List<int>>>();
            var current = new List<List<int>>();
            var called = new List<int>();

            foreach (var line in strings.Skip(2))
            {
                if (line.Equals(string.Empty))
                {
                    cardsOG.Add(current);
                    current = new List<List<int>>();
                }
                else
                {
                    current.Add(AdventLibrary.ParseInput.TokenizeAndParseIntoList<int>(line, " "));
                }
            }
            cardsOG.Add(current);

            while (true)
            {
                var cards = cardsOG.ToList();
                called.Add(calling.Dequeue());
                foreach (var card in cards)
                {
                    if (WinningCard(card, called))
                    {
                        if (cardsOG.Count == 1)
                        {
                            var sum = card.SelectMany(x => x).ToList().Where(x => !called.Contains(x)).Sum();
                            return sum*called.Last();
                        }
                        else
                        {
                            var index = cardsOG.IndexOf(card);
                            cardsOG.RemoveAt(index);
                        }
                    }
                }
            }
        }

        private bool WinningCard(List<List<int>> grid, List<int> numbers)
        {
            var flippedGrid = TransposeMatrix(grid);
            return grid.Any(x => x.All(y => numbers.Contains(y))) || flippedGrid.Any(x => x.All(y => numbers.Contains(y)));
        }

        private List<List<int>> TransposeMatrix(List<List<int>> grid)
        {
            var newGrid = new List<List<int>>();
            for (var i = 0; i < grid.First().Count; i++) {
                newGrid.Add(new List<int>());
                for (var j = 0; j < grid.Count; j++) {
                    newGrid[i].Add(grid[j][i]);
                }
            }
            return newGrid;
        }
    }
}
