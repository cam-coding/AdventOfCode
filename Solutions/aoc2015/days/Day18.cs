using AdventLibrary;

namespace aoc2015
{
    public class Day18: ISolver
    {
        private string _filePath;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsBoolGrid(_filePath, '#');
            for (var i = 0; i < 100; i++)
            {
                var newArr = grid.Clone2dList();
                for (var j = 0; j < 100; j++)
                {
                    for (var k = 0; k < 100; k++)
                    {
                        var neigh = GridHelperWeirdTypes.GetOrthoginalNeighbours(grid, j, k);
                        var count = 0;
                        foreach (var n in neigh)
                        {
                            if (grid[n.Item1][n.Item2])
                            {
                                count++;
                            }
                        }
                        if (grid[j][k])
                        {
                            if (count == 2 || count == 3)
                            {
                                newArr[j][k] = true;
                            }
                            else
                            {
                                newArr[j][k] = false;
                            }
                        }
                        else
                        {
                            if (count == 3)
                            {
                                newArr[j][k] = true;
                            }
                            else
                            {
                                newArr[j][k] = false;
                            }
                        }

                    }
                }
                grid = newArr;
            }

            var counter = 0;

            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j])
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
        
        private object Part2()
        {
            var grid = ParseInput.ParseFileAsBoolGrid(_filePath, '#');
            grid[0][0] = true;
            grid[0][99] = true;
            grid[99][0] = true;
            grid[99][99] = true;
            for (var i = 0; i < 100; i++)
            {
                var newArr = grid.Clone2dList();
                for (var j = 0; j < 100; j++)
                {
                    for (var k = 0; k < 100; k++)
                    {
                        var neigh = GridHelperWeirdTypes.GetOrthoginalNeighbours(grid, j, k);
                        var count = 0;
                        foreach (var n in neigh)
                        {
                            if (grid[n.Item1][n.Item2])
                            {
                                count++;
                            }
                        }
                        if (grid[j][k])
                        {
                            if (count == 2 || count == 3)
                            {
                                newArr[j][k] = true;
                            }
                            else
                            {
                                newArr[j][k] = false;
                            }
                        }
                        else
                        {
                            if (count == 3)
                            {
                                newArr[j][k] = true;
                            }
                            else
                            {
                                newArr[j][k] = false;
                            }
                        }

                    }
                }
                newArr[0][0] = true;
                newArr[0][99] = true;
                newArr[99][0] = true;
                newArr[99][99] = true;
                grid = newArr;
            }

            var counter = 0;

            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j])
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
    }
}