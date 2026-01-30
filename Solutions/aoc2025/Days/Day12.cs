using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using DlxLib;
using Solution = AdventLibrary.Solution;

namespace aoc2025
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
            var lines = input.Lines;
            var numbers = input.Longs;
            long count = 0;
            var groups = input.LineGroupsSeperatedByWhiteSpace;

            // can return either of these
            var dumbestWayAnswer = DumbestWay(groups.Last());
            var dumbWayAnswer = DumbWay(groups);
            if (!isTest)
                return dumbestWayAnswer;

            var pieceIndexToTransformations = new Dictionary<int, List<GridObject<char>>>();
            var trees = new List<Tree>();

            foreach (var group in groups)
            {
                if (group[0].Contains("x"))
                {
                    break;
                }
                var index = group[0].GetLongsFromString()[0];
                var clone = group.Clone();
                clone.RemoveAt(0);
                var grid = clone.To2dList();

                // get every transformation
                var listOfGridTransformations = new List<GridObject<char>>();
                for (var i = 0; i <= 2; i++)
                {
                    var temp = grid.Clone();
                    if (i == 1)
                    {
                        GridHelper.FlipAboutHorizontal(temp);
                    }
                    else if (i == 2)
                    {
                        GridHelper.FlipAboutVertical(temp);
                    }
                    for (var j = 0; j <= 3; j++)
                    {
                        temp = RotateRight90(temp);
                        var gridObj = new GridObject<char>(temp);
                        listOfGridTransformations.Add(gridObj);

                        // gridObj.Print();
                        Console.WriteLine();
                    }
                }
                pieceIndexToTransformations.Add((int)index, listOfGridTransformations);
            }

            foreach (var line in groups.Last())
            {
                var nums = line.GetIntsFromString();
                var christmasTreeWidth = nums[0];
                var christmasTreeHeight = nums[1];
                nums.RemoveRange(0, 2);

                var currentTree = new Tree(christmasTreeWidth, christmasTreeHeight, nums);
                var currentTreeGrid = new GridObject<bool>(christmasTreeHeight, christmasTreeWidth);
                trees.Add(currentTree);
                var pieceTypeToConfigList = new Dictionary<int, List<List<bool>>>();

                // take every piece transformation (rotations and flips) for every piece
                foreach (var piece in pieceIndexToTransformations)
                {
                    pieceTypeToConfigList.Add(piece.Key, new List<List<bool>>());
                    var tempHashSet = new HashSet<List<bool>>();

                    // for each piece, look at every transformation possible
                    foreach (var transformation in piece.Value)
                    {
                        // for every position in the grid, see if this transformation fits
                        for (var treeGridY = 0; treeGridY < christmasTreeHeight; treeGridY++)
                        {
                            for (var treeGridX = 0; treeGridX < christmasTreeWidth; treeGridX++)
                            {
                                var currentConfig = new List<bool>();
                                currentConfig.FillEmptyListWithValue(false, currentTree.GridSize);
                                var currentLocation = new GridLocation<int>(treeGridX, treeGridY);

                                var valid = true;

                                // look at each position in the transformation, transposed into the current pos in the grid
                                // make sure each piece ('#') in the transformation is within the grid
                                for (var y = 0; y < transformation.Height && valid; y++)
                                {
                                    for (var x = 0; x < transformation.Width && valid; x++)
                                    {
                                        var treeGridLoc = new GridLocation<int>(treeGridX + x, treeGridY + y);
                                        var transformationGridLoc = new GridLocation<int>(x, y);
                                        if (transformation.Get(transformationGridLoc) == '#')
                                        {
                                            if (currentTreeGrid.WithinGrid(treeGridLoc))
                                            {
                                                // record this specific transformation + grid location as a configuration for later
                                                currentConfig[GetxyIndex(treeGridLoc, currentTree.Width)] = true;
                                            }
                                            else
                                            {
                                                valid = false;
                                            }
                                        }
                                    }
                                }

                                if (valid)
                                {
                                    tempHashSet.Add(currentConfig);
                                }
                            }
                        }
                    }
                    pieceTypeToConfigList[piece.Key] = tempHashSet.ToList();
                }

                var blah = pieceTypeToConfigList.Count();

                var blankPiecesMatrixColumns = new List<bool>();
                blankPiecesMatrixColumns.FillEmptyListWithValue(false, currentTree.PiecesCount);
                var pieceTypeToUniqueRows = new Dictionary<int, List<List<bool>>>();
                var columnsSetup = 0;

                // build the final matrix
                // unique pieces * unique configurations for that piece type
                var finalMatrix = new List<List<bool>>();

                // for each piece type
                for (var pieceType = 0; pieceType < currentTree.CountsPerPiece.Count; pieceType++)
                {
                    pieceTypeToUniqueRows[pieceType] = new List<List<bool>>();

                    // each piece has to showup a number of times in the solution.
                    // each unique version of a piece/shape is it's own column
                    for (var pieceUniqueNum = 0; pieceUniqueNum < currentTree.CountsPerPiece[pieceType]; pieceUniqueNum++)
                    {
                        var columnsClone = blankPiecesMatrixColumns.Clone();

                        // make the one column aligning to this unique piece true
                        columnsClone[columnsSetup + pieceUniqueNum] = true;
                        pieceTypeToUniqueRows[pieceType].Add(columnsClone);

                        // now this one unique piece could appear in any of the configurations this piece type has associated with it
                        for (var configNumber = 0; configNumber < pieceTypeToConfigList[pieceType].Count; configNumber++)
                        {
                            var config = pieceTypeToConfigList[pieceType][configNumber];
                            var temp = columnsClone.Concat(config).ToList();
                            finalMatrix.Add(temp);
                        }
                    }
                    columnsSetup += currentTree.CountsPerPiece[pieceType];
                }

                // fill in extra 1x1's here!!!!
                var blankGridLocationConfig = new List<bool>();
                blankGridLocationConfig.FillEmptyListWithValue(false, currentTree.GridSize);
                for (var treeGridY = 0; treeGridY < christmasTreeHeight; treeGridY++)
                {
                    for (var treeGridX = 0; treeGridX < christmasTreeWidth; treeGridX++)
                    {
                        var clone = blankGridLocationConfig.Clone();
                        var treeGridLoc = new GridLocation<int>(treeGridX, treeGridY);
                        clone[GetxyIndex(treeGridLoc, currentTree.Width)] = true;

                        var matrixRow1x1square = blankPiecesMatrixColumns.Concat(clone).ToList();
                        finalMatrix.Add(matrixRow1x1square);
                    }
                }

                // call external algorithm x nuget package
                var dlx = new Dlx();
                var firstTwoSolutions = dlx.Solve<bool>(finalMatrix.ConvertTo2DArray());

                try
                {
                    // the package is weird and just hangs when it can't find a solution. So force a timeout
                    RunWithTimeout(() => firstTwoSolutions.Any(), TimeSpan.FromSeconds(1));
                    if (firstTwoSolutions.Any())
                    {
                        count++;
                    }
                }
                catch (Exception e)
                {
                    // catch and do nothing, we know it failed
                }
            }
            return count;
        }

        private int DumbWay(List<List<string>> groups)
        {
            int count = 0;
            var sizePerPiece = new Dictionary<int, int>();

            foreach (var group in groups)
            {
                if (group[0].Contains("x"))
                {
                    break;
                }
                var index = group[0].GetIntsFromString()[0];
                var clone = group.Clone();
                clone.RemoveAt(0);
                var grid = clone.To2dList();
                var camGridObj = new GridObject<char>(grid);
                sizePerPiece.Add(index, camGridObj.GetAllLocationsWhereValue(x => x.Equals('#')).Count);
            }

            foreach (var line in groups.Last())
            {
                var nums = line.GetIntsFromString();
                var christmasTreeWidth = nums[0];
                var christmasTreeHeight = nums[1];
                nums.RemoveRange(0, 2);
                var area = christmasTreeHeight * christmasTreeWidth;

                var total2 = 0;
                for (var i = 0; i < nums.Count; i++)
                {
                    total2 += nums[i] * sizePerPiece[i];
                }

                if (total2 <= area)
                {
                    count++;
                }
                continue;
            }

            return count;
        }

        private int DumbestWay(List<string> trees)
        {
            int count = 0;

            foreach (var line in trees)
            {
                var nums = line.GetIntsFromString();
                var christmasTreeWidth = nums[0];
                var christmasTreeHeight = nums[1];
                nums.RemoveRange(0, 2);
                var area = christmasTreeHeight * christmasTreeWidth;

                if ((nums.Sum() * 9) <= area)
                {
                    count++;
                }
                continue;
            }

            return count;
        }

        public static void RunWithTimeout(Action action, TimeSpan timeout)
        {
            var task = Task.Run(action);
            try
            {
                var success = task.Wait(timeout);
                if (!success)
                {
                    throw new TimeoutException();
                }
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public static List<List<char>> RotateRight90(List<List<char>> grid)
        {
            int rows = grid.Count;
            int cols = grid[0].Count;

            // New grid has swapped dimensions
            var rotated = new List<List<char>>(cols);
            for (int c = 0; c < cols; c++)
            {
                rotated.Add(new List<char>(rows));
                for (int r = rows - 1; r >= 0; r--)
                {
                    rotated[c].Add(grid[r][c]);
                }
            }

            return rotated;
        }

        private int GetxyIndex(GridLocation<int> loc, int width)
        {
            return width * loc.Y + loc.X;
        }

        private object Part2(bool isTest = false)
        {
            return 0;
        }

        public class Tree
        {
            public Tree(int width, int height, List<int> counts)
            {
                Width = width;
                Height = height;
                CountsPerPiece = counts;

                GridSize = Width * Height;
                PiecesCount = CountsPerPiece.Sum();
                TotalColumns = GridSize + PiecesCount;
            }

            public int Width { get; set; }

            public int Height { get; set; }

            public int GridSize { get; set; }

            public int PiecesCount { get; set; }

            public int TotalColumns { get; set; }

            public List<int> CountsPerPiece { get; set; }
        }
    }
}