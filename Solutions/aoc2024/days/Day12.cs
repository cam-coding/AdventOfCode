using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day12 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
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
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var locToGroup = new Dictionary<GridLocation<int>, HashSet<GridLocation<int>>>();
            var locations = new List<HashSet<GridLocation<int>>>();

            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var loc = new GridLocation<int>(x, y);
                    var val = grid.Get(loc);
                    if (!locToGroup.ContainsKey(loc))
                    {
                        Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
                        var history = new HashSet<GridLocation<int>>();
                        var valid = new HashSet<GridLocation<int>>();

                        q.Enqueue(new List<GridLocation<int>>() { loc });
                        while (q.Count > 0)
                        {
                            var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                            var currentLocation = fullPath.Last(); // this is just the most recent point
                            var currentValue = grid.Get(currentLocation);

                            if (
                                fullPath == null ||
                                history.Contains(currentLocation)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                                continue;

                            // make sure to add current state to history
                            history.Add(currentLocation);
                            if (currentValue == val)
                            {
                                valid.Add(currentLocation);
                            }


                            //Get the next nodes/grids/etc to visit next
                            foreach (var neighbour in grid.GetOrthogonalNeighbours(currentLocation))
                            {
                                var neighVal = grid.Get(neighbour);
                                if (neighVal != val)
                                {
                                    continue;
                                }
                                var temp = fullPath.Clone(); // very important, do not miss this clone
                                temp.Add(neighbour);
                                q.Enqueue(temp);
                            }
                        }
                        foreach (var item in valid)
                        {
                            locToGroup.Add(item, valid);
                        }
                        locations.Add(valid);

                        var areaShoe = MathHelper.ShoelaceArea(valid.ToList());
                        var areaPick = MathHelper.PicksAndShoelaceArea(valid.ToList());
                        var blah = valid;
                    }
                }
            }

            foreach (var list in locations)
            {
                var area = list.Count;
                var perim = 0;
                var val = grid.Get(list.First());


                foreach (var item in list)
                {
                    var neighs = new List<GridLocation<int>>();
                    foreach (var direction in Directions.OrthogonalDirections)
                    {
                        var tempLocation = item + direction;
                        neighs.Add(tempLocation);
                    }
                    foreach (var neigh in neighs)
                    {
                        if (!grid.WithinGrid(neigh) || grid.Get(neigh) != grid.Get(item))
                        {
                            perim++;
                        }
                    }
                }
                var cost = area * perim;
                count += cost;
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var locToGroup = new Dictionary<GridLocation<int>, HashSet<GridLocation<int>>>();
            var locations = new List<HashSet<GridLocation<int>>>();

            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var loc = new GridLocation<int>(x, y);
                    var val = grid.Get(loc);
                    if (!locToGroup.ContainsKey(loc))
                    {
                        Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
                        var history = new HashSet<GridLocation<int>>();
                        var valid = new HashSet<GridLocation<int>>();

                        q.Enqueue(new List<GridLocation<int>>() { loc });
                        while (q.Count > 0)
                        {
                            var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                            var currentLocation = fullPath.Last(); // this is just the most recent point
                            var currentValue = grid.Get(currentLocation);

                            if (
                                fullPath == null ||
                                history.Contains(currentLocation)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                                continue;

                            // make sure to add current state to history
                            history.Add(currentLocation);
                            if (currentValue == val)
                            {
                                valid.Add(currentLocation);
                            }


                            //Get the next nodes/grids/etc to visit next
                            foreach (var neighbour in grid.GetOrthogonalNeighbours(currentLocation))
                            {
                                var neighVal = grid.Get(neighbour);
                                if (neighVal != val)
                                {
                                    continue;
                                }
                                var temp = fullPath.Clone(); // very important, do not miss this clone
                                temp.Add(neighbour);
                                q.Enqueue(temp);
                            }
                        }
                        foreach (var item in valid)
                        {
                            locToGroup.Add(item, valid);
                        }
                        locations.Add(valid);

                        var areaShoe = MathHelper.ShoelaceArea(valid.ToList());
                        var areaPick = MathHelper.PicksAndShoelaceArea(valid.ToList());
                        var blah = valid;
                    }
                }
            }

            foreach (var list in locations)
            {
                var area = list.Count;
                var sidesFromThisLocation = 0;
                var val = grid.Get(list.First());

                var dictLocationToSides = new Dictionary<GridLocation<int>, List<GridLocation<int>>>();

                foreach (var item in list)
                {
                    var neighs = new List<GridLocation<int>>();
                    foreach (var direction in Directions.OrthogonalDirections)
                    {
                        var neigh = item + direction;
                        if (!grid.WithinGrid(neigh) || grid.Get(neigh) != grid.Get(item))
                        {
                            if (!dictLocationToSides.TryAdd(item, new List<GridLocation<int>>() { direction }))
                            {
                                dictLocationToSides[item].Add(direction);
                            }
                        }
                    }
                }
                var sideCount = 0;
                foreach (var item in list)
                {
                    if (!dictLocationToSides.ContainsKey(item))
                    {
                        continue;
                    }
                    var tempLocation = item + Directions.Up;
                    var currentSides = dictLocationToSides[item];
                    var invalidSides = new List<GridLocation<int>>();
                    if (dictLocationToSides.ContainsKey(tempLocation))
                    {
                        invalidSides.AddRange(dictLocationToSides[tempLocation]);
                    }
                    tempLocation = item + Directions.Right;
                    if (dictLocationToSides.ContainsKey(tempLocation))
                    {
                        invalidSides.AddRange(dictLocationToSides[tempLocation]);
                    }
                    sideCount += currentSides.Where(x => !invalidSides.Contains(x)).Count();
                }
                var cost = area * sideCount;
                count += cost;
            }
            return count;
        }

        private object Part2_a(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var locToGroup = new Dictionary<GridLocation<int>, HashSet<GridLocation<int>>>();
            var locations = new List<HashSet<GridLocation<int>>>();

            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var loc = new GridLocation<int>(x, y);
                    var val = grid.Get(loc);
                    if (!locToGroup.ContainsKey(loc))
                    {
                        Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
                        var history = new HashSet<GridLocation<int>>();
                        var valid = new HashSet<GridLocation<int>>();

                        q.Enqueue(new List<GridLocation<int>>() { loc });
                        while (q.Count > 0)
                        {
                            var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                            var currentLocation = fullPath.Last(); // this is just the most recent point
                            var currentValue = grid.Get(currentLocation);

                            if (
                                fullPath == null ||
                                history.Contains(currentLocation)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                                continue;

                            // make sure to add current state to history
                            history.Add(currentLocation);
                            if (currentValue == val)
                            {
                                valid.Add(currentLocation);
                            }


                            //Get the next nodes/grids/etc to visit next
                            foreach (var neighbour in grid.GetOrthogonalNeighbours(currentLocation))
                            {
                                var neighVal = grid.Get(neighbour);
                                if (neighVal != val)
                                {
                                    continue;
                                }
                                var temp = fullPath.Clone(); // very important, do not miss this clone
                                temp.Add(neighbour);
                                q.Enqueue(temp);
                            }
                        }
                        foreach (var item in valid)
                        {
                            locToGroup.Add(item, valid);
                        }
                        locations.Add(valid);

                        var areaShoe = MathHelper.ShoelaceArea(valid.ToList());
                        var areaPick = MathHelper.PicksAndShoelaceArea(valid.ToList());
                        var blah = valid;
                    }
                }
            }

            var horGroup = new Dictionary<HashSet<GridLocation<int>>, int>();
            var locToHorGroup = new Dictionary<GridLocation<int>, HashSet<GridLocation<int>>>();
            var vertGroup = new Dictionary<HashSet<GridLocation<int>>, int>();
            var locToVertGroup = new Dictionary<GridLocation<int>, HashSet<GridLocation<int>>>();
            foreach (var list in locations)
            {
                foreach (var loc in list)
                {
                    var val = grid.Get(loc);
                    if (!locToHorGroup.ContainsKey(loc))
                    {
                        Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
                        var history = new HashSet<GridLocation<int>>();
                        var valid = new HashSet<GridLocation<int>>();

                        q.Enqueue(new List<GridLocation<int>>() { loc });
                        while (q.Count > 0)
                        {
                            var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                            var currentLocation = fullPath.Last(); // this is just the most recent point
                            var currentValue = grid.Get(currentLocation);

                            if (
                                fullPath == null ||
                                history.Contains(currentLocation)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                                continue;

                            // make sure to add current state to history
                            history.Add(currentLocation);
                            if (currentValue == val)
                            {
                                valid.Add(currentLocation);
                            }


                            //Get the next nodes/grids/etc to visit next
                            foreach (var neighbour in new List<GridLocation<int>> { currentLocation + Directions.Left, currentLocation + Directions.Right})
                            {
                                if (grid.WithinGrid(neighbour))
                                {
                                    var neighVal = grid.Get(neighbour);
                                    if (neighVal != val)
                                    {
                                        continue;
                                    }
                                    var temp = fullPath.Clone(); // very important, do not miss this clone
                                    temp.Add(neighbour);
                                    q.Enqueue(temp);
                                }
                            }
                        }
                        foreach (var item in valid)
                        {
                            locToHorGroup.Add(item, valid);
                        }
                        horGroup.Add(valid, 0);
                    }
                    if (!locToVertGroup.ContainsKey(loc))
                    {
                        Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
                        var history = new HashSet<GridLocation<int>>();
                        var valid = new HashSet<GridLocation<int>>();

                        q.Enqueue(new List<GridLocation<int>>() { loc });
                        while (q.Count > 0)
                        {
                            var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                            var currentLocation = fullPath.Last(); // this is just the most recent point
                            var currentValue = grid.Get(currentLocation);

                            if (
                                fullPath == null ||
                                history.Contains(currentLocation)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                                continue;

                            // make sure to add current state to history
                            history.Add(currentLocation);
                            if (currentValue == val)
                            {
                                valid.Add(currentLocation);
                            }


                            //Get the next nodes/grids/etc to visit next
                            foreach (var neighbour in new List<GridLocation<int>> { currentLocation + Directions.Up, currentLocation + Directions.Down })
                            {
                                if (grid.WithinGrid(neighbour))
                                {
                                    var neighVal = grid.Get(neighbour);
                                    if (neighVal != val)
                                    {
                                        continue;
                                    }
                                    var temp = fullPath.Clone(); // very important, do not miss this clone
                                    temp.Add(neighbour);
                                    q.Enqueue(temp);
                                }
                            }
                        }
                        foreach (var item in valid)
                        {
                            locToVertGroup.Add(item, valid);
                        }
                        vertGroup.Add(valid, 0);
                    }
                    /*
                    var area = list.Count;
                    var perim = 0;
                    var val = grid.Get(list.First());*/
                }
                var sideCount = 0;
                foreach (var item in horGroup)
                {
                    var history = new HashSet<GridLocation<int>>();
                    foreach (var location in item.Key)
                    {
                        var horDirections = new List<GridLocation<int>>() { Directions.Up, Directions.Down};
                        foreach (var direction in horDirections)
                        {
                            var tempLocation = location + direction;
                            if (!grid.WithinGrid(tempLocation) || grid.Get(tempLocation) != grid.Get(location))
                            {
                                history.Add(direction);
                            }
                        }
                    }
                    sideCount += history.Count;
                }
                foreach (var item in vertGroup)
                {
                    var history = new HashSet<GridLocation<int>>();
                    foreach (var location in item.Key)
                    {
                        var vertDirections = new List<GridLocation<int>>() { Directions.Right, Directions.Left };
                        foreach (var direction in vertDirections)
                        {
                            var tempLocation = location + direction;
                            if (!grid.WithinGrid(tempLocation) || grid.Get(tempLocation) != grid.Get(location))
                            {
                                history.Add(direction);
                            }
                        }
                    }
                    sideCount += history.Count;
                }
                var blah = sideCount;
                /*
                var cost = area * perim;
                count += cost;*/
            }
            return count;
        }
    }
}