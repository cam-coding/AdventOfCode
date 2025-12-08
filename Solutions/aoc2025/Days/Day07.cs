using AdventLibrary;
using AdventLibrary.Helpers.Grids;

namespace aoc2025
{
    public class Day07 : ISolver
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
            var gridStart = grid.GetAllLocationsWhereValue(x => x == 'S')[0];

            var initialWalker = new GridWalker(gridStart, Directions.Down, 1);

            var listOfWalkers = new List<GridWalker>() { initialWalker };

            while (listOfWalkers.Count > 0)
            {
                var newListOfWalkers = new List<GridWalker>();

                for (var i = 0; i < listOfWalkers.Count; i++)
                {
                    for (var j = i + 1; j < listOfWalkers.Count; j++)
                    {
                        if (listOfWalkers[i].Current == listOfWalkers[j].Current)
                        {
                            listOfWalkers.RemoveAt(j);
                        }
                    }
                }
                foreach (var walker in listOfWalkers)
                {
                    walker.Walk();
                    if (grid.Get(walker.Current) == '^')
                    {
                        var rightWalker = new GridWalker(walker);
                        rightWalker.Current += Directions.Right;
                        newListOfWalkers.Add(rightWalker);
                        walker.Current += Directions.Left;
                        newListOfWalkers.Add(walker);
                        count++;
                    }
                    else if (walker.Current.Y == grid.Height - 1)
                    {
                    }
                    else
                    {
                        newListOfWalkers.Add(walker);
                    }
                }
                listOfWalkers = newListOfWalkers;
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.GridChar;
            long count = 0;
            var gridStart = grid.GetAllLocationsWhereValue(x => x == 'S')[0];

            var initialWalker = new GridWalker(gridStart, Directions.Down, 1);

            var listOfWalkers = new List<GridWalker>() { initialWalker };

            // store a walker and how many possible paths could create this walker
            var pathLookup = new Dictionary<GridWalker, long>
            {
                { initialWalker, 1 }
            };

            while (listOfWalkers.Count > 0)
            {
                var listy2 = new List<GridWalker>();

                for (var i = 0; i < listOfWalkers.Count; i++)
                {
                    for (var j = i + 1; j < listOfWalkers.Count; j++)
                    {
                        if (listOfWalkers[i].Current == listOfWalkers[j].Current)
                        {
                            var value = pathLookup[listOfWalkers[j]];
                            pathLookup[listOfWalkers[i]] += value;
                            pathLookup.Remove(listOfWalkers[j]);
                            listOfWalkers.RemoveAt(j);
                        }
                    }
                }
                foreach (var walker in listOfWalkers)
                {
                    walker.Walk();
                    if (grid.Get(walker.Current) == '^')
                    {
                        var walker3 = new GridWalker(walker);
                        walker3.Current += Directions.Right;
                        listy2.Add(walker3);
                        pathLookup.Add(walker3, pathLookup[walker]);
                        walker.Current += Directions.Left;
                        listy2.Add(walker);
                        count++;
                    }
                    else if (walker.Current.Y == grid.Height - 1)
                    {
                    }
                    else
                    {
                        listy2.Add(walker);
                    }
                }
                listOfWalkers = listy2;
            }

            return pathLookup.Values.ToList().Sum();
        }
    }
}