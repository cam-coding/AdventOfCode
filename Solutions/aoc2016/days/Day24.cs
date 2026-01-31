using AdventLibrary;
using System.Numerics;

namespace aoc2016
{
    public class Day24 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        private char[] specialChars = { '0', '1', '2', '3', '4', '5', '6', '7' };
        private int[,] _arr = new int[8, 8];
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var betterGrid = MakeMyGrid(grid);
            var pather = new AStarSharp.Astar(betterGrid);

            var locations = new Dictionary<int, (int, int)>();

            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[i].Count; j++)
                {
                    if (specialChars.Contains(grid[i][j]))
                    {
                        var intValue = grid[i][j] - '0';
                        locations.Add(intValue, (i, j));
                    }
                }
            }

            var dict = new Dictionary<int, Dictionary<int, int>>();
            var path = pather.FindPath(new Vector2(0, 0), new Vector2(grid.Count - 1, grid[0].Count - 1));

            for (var i = 0; i < 8; i++)
            {
                dict.Add(i, new Dictionary<int, int>());
                for (var j = i + 1; j < 8; j++)
                {
                    var a = locations[i];
                    var b = locations[j];
                    var distance = pather.FindPath(new Vector2(a.Item1, a.Item2), new Vector2(b.Item1, b.Item2)).Count;
                    _arr[i, j] = distance;
                    _arr[j, i] = distance;
                }
            }
            var listy = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            return TryAllPaths(listy, 0, 0);
        }

        private object Part2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var betterGrid = MakeMyGrid(grid);
            var pather = new AStarSharp.Astar(betterGrid);

            var locations = new Dictionary<int, (int, int)>();

            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[i].Count; j++)
                {
                    if (specialChars.Contains(grid[i][j]))
                    {
                        var intValue = grid[i][j] - '0';
                        locations.Add(intValue, (i, j));
                    }
                }
            }

            var dict = new Dictionary<int, Dictionary<int, int>>();
            var path = pather.FindPath(new Vector2(0, 0), new Vector2(grid.Count - 1, grid[0].Count - 1));

            for (var i = 0; i < 8; i++)
            {
                dict.Add(i, new Dictionary<int, int>());
                for (var j = i + 1; j < 8; j++)
                {
                    var a = locations[i];
                    var b = locations[j];
                    var distance = pather.FindPath(new Vector2(a.Item1, a.Item2), new Vector2(b.Item1, b.Item2)).Count;
                    _arr[i, j] = distance;
                    _arr[j, i] = distance;
                }
            }
            var listy = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            return TryAllPaths2(listy, 0, 0);
        }

        private int TryAllPaths(List<int> canChooseFrom, int current, int total)
        {
            if (canChooseFrom.Count == 0)
            {
                return total;
            }
            var best = int.MaxValue;
            foreach (var next in canChooseFrom)
            {
                var blah = canChooseFrom.ToList();
                blah.Remove(next);

                // probably need to do A* calc here, in case you run over a path maybe?
                var temp = TryAllPaths(blah, next, total + _arr[current, next]);

                if (temp < best)
                    best = temp;
            }
            return best;
        }

        private int TryAllPaths2(List<int> canChooseFrom, int current, int total)
        {
            if (canChooseFrom.Count == 0)
            {
                return total + _arr[current, 0];
            }
            var best = int.MaxValue;
            foreach (var next in canChooseFrom)
            {
                var blah = canChooseFrom.ToList();
                blah.Remove(next);

                // probably need to do A* calc here, in case you run over a path maybe?
                var temp = TryAllPaths2(blah, next, total + _arr[current, next]);

                if (temp < best)
                    best = temp;
            }
            return best;
        }

        private List<List<AStarSharp.Node>> MakeMyGrid(List<List<char>> grid)
        {
            List<List<AStarSharp.Node>> nodeGrid = new List<List<AStarSharp.Node>>();
            for (var i = 0; i < grid.Count; i++)
            {
                nodeGrid.Add(new List<AStarSharp.Node>());
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j] == '#')
                    {
                        nodeGrid[i].Add(new AStarSharp.Node(new Vector2(i, j), grid[i][j], false));
                    }
                    else
                    {
                        nodeGrid[i].Add(new AStarSharp.Node(new Vector2(i, j), grid[i][j]));
                    }
                }
            }
            return nodeGrid;
        }
    }
}