using AdventLibrary;
using AdventLibrary.Helpers.Grids;

namespace aoc2022
{
    public class Day09 : ISolver
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
            var lines = input.Lines;

            var head = new GridLocation<int>(0, 4);
            var tail = new GridLocation<int>(0, 4);

            var hash = new HashSet<GridLocation<int>>();
            hash.Add(tail);

            foreach (var line in lines)
            {
                var toks = line.Split(' ');
                var str = toks[0];
                var val = int.Parse(toks[1]);

                var modifier = Directions.GetDirectionByString(str);

                for (var i = 0; i < val; i++)
                {
                    head = head + modifier;

                    if (GridHelper.Distances.ChebyshevDistance(head, tail) < 2)
                    {
                        continue;
                    }

                    var best = 4;
                    var bestLoc = new GridLocation<int>(0, 0);

                    List<GridLocation<int>> dirs;
                    var taxiDistance = GridHelper.Distances.TaxicabDistance(head, tail);
                    if (taxiDistance == 3)
                    {
                        dirs = Directions.DiagonalDirections;
                    }
                    else
                    {
                        dirs = Directions.OrthogonalDirections.Concat(Directions.DiagonalDirections).ToList();
                    }
                    foreach (var dir in dirs)
                    {
                        var tempTail = tail + dir;
                        var tempVal = GridHelper.Distances.TaxicabDistance(head, tempTail);
                        if (tempVal < best)
                        {
                            best = tempVal;
                            bestLoc = tempTail;
                        }
                    }
                    tail = bestLoc;
                    hash.Add(tail);
                }
            }
            return hash.Count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;

            var knots = new List<GridLocation<int>>();
            for (var i = 0; i < 10; i++)
            {
                knots.Add(new GridLocation<int>(0, 4));
            }

            var hash = new HashSet<GridLocation<int>>();
            hash.Add(knots[9]);

            foreach (var line in lines)
            {
                var toks = line.Split(' ');
                var str = toks[0];
                var val = int.Parse(toks[1]);

                var modifier = Directions.GetDirectionByString(str);

                for (var i = 0; i < val; i++)
                {
                    knots[0] = knots[0] + modifier;

                    for (var k = 1; k < 10; k++)
                    {
                        var lead = knots[k - 1];
                        var follow = knots[k];

                        if (GridHelper.Distances.ChebyshevDistance(lead, follow) < 2)
                        {
                            continue;
                        }

                        var best = 4;
                        var bestLoc = new GridLocation<int>(0, 0);

                        List<GridLocation<int>> dirs;
                        var taxiDistance = GridHelper.Distances.TaxicabDistance(lead, follow);
                        if (taxiDistance == 3)
                        {
                            dirs = Directions.DiagonalDirections;
                        }
                        else
                        {
                            dirs = Directions.OrthogonalDirections.Concat(Directions.DiagonalDirections).ToList();
                        }
                        foreach (var dir in dirs)
                        {
                            var tempTail = follow + dir;
                            var tempVal = GridHelper.Distances.TaxicabDistance(lead, tempTail);
                            if (tempVal < best)
                            {
                                best = tempVal;
                                bestLoc = tempTail;
                            }
                        }
                        knots[k] = bestLoc;
                        if (k == 9)
                        {
                            hash.Add(knots[k]);
                        }
                    }
                }
            }
            return hash.Count;
        }
    }
}