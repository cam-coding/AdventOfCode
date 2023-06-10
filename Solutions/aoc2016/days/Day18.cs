using AdventLibrary;
using System.Collections.Generic;
using System.Linq;

namespace aoc2016
{
    public class Day18: ISolver
  {
        private string _filePath;
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            return Solve(40);
        }
        
        private object Part2()
        {
            return Solve(400000);
        }

        private long Solve(int rows)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var startingLine = lines[0];
            var width = startingLine.Length;
            var history = new List<List<char>>
            {
                startingLine.ToList()
            };
            long counter = startingLine.Count(x => x == '.');

            for (var i = 1; i < rows; i++)
            {
                var newLine = new List<char>();
                for (var j = 0; j < width; j++)
                {
                    var leftSafe = true;
                    if (j > 0)
                    {
                        leftSafe = history[i - 1][j - 1] == '.';
                    }
                    var centreSafe = history[i - 1][j] == '.';
                    var rightSafe = true;
                    if (j < width - 1)
                    {
                        rightSafe = history[i - 1][j + 1] == '.';
                    }

                    if ((!leftSafe && !centreSafe && rightSafe) ||
                        (leftSafe && !centreSafe && !rightSafe) ||
                        (!leftSafe && centreSafe && rightSafe) ||
                        (leftSafe && centreSafe && !rightSafe))
                    {
                        newLine.Add('^');
                    }
                    else
                    {
                        newLine.Add('.');
                        counter++;
                    }
                }
                history.Add(newLine);
            }
            return counter;
        }
    }
}
