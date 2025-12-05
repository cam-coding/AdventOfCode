using AdventLibrary;

namespace aoc2025
{
    public class Day04 : ISolver
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
            var grid = input.GridChar;
            long count = 0;

            // get all the pallets (@) and have less than 4 pallets around it
            var accessibleSquares = grid.GetAllLocationsWhere(
                x => grid.GetAllNeighbours(x).Count(
                    y => grid.Get(y) == '@') < 4);
            return accessibleSquares.Count(x => grid.Get(x) == '@');
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.GridChar;
            long count = 0;
            var repeat = true;

            while (repeat)
            {
                repeat = false;
                var accessibleSquares = grid.GetAllLocationsWhere(
                    x => grid.GetAllNeighbours(x).Count(
                        y => grid.Get(y) == '@') < 4);
                foreach (var loc in accessibleSquares.Where(x => grid.Get(x) == '@'))
                {
                    repeat = true;
                    count++;
                    grid.Set(loc, '.');
                }
            }

            return count;
        }
    }
}