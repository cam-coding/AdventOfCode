﻿using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;

namespace AdventLibrary.PathFinding
{
    public class DepthFirstSearch<T>
    {
        HashSet<T> _visited;
        Dictionary<T, (int Distance, List<T> Path)> _distanceDictionary;
        bool _goalAchieved;

        public DepthFirstSearch()
        {
            _visited = new HashSet<T>();
            _distanceDictionary = new Dictionary<T, (int Distance, List<T> Path)>();
            _goalAchieved = false;
        }

        public Dictionary<T, (int Distance, List<T> Path)> DistanceDictionary { get { return _distanceDictionary; } }

        public bool GoalAchieved { get { return _goalAchieved; } }

        // STILL NEEDS TESTING
        // search from starting point to every point if you don't put a goal.
        // start to end if you do put a goal
        public void DFSgeneric(
            (int Distance, List<T> Path) current,
            Func<T, List<T>> GetNeighboursFunc,
            Func<T, bool> GoalEvaluation = null,
            Func<T, int> GetWeight = null
            )
        {
            var currentNode = current.Path.Last();
            var path = current.Path;

            if (_goalAchieved || (GoalEvaluation != null && GoalEvaluation(currentNode)))
            {
                _goalAchieved = true;
                return ;
            }

            // only process each node once
            if (!_visited.Add(currentNode))
            {
                return;
            }

            // each node we see starts with an infinite distance
            if (!_distanceDictionary.ContainsKey(currentNode))
            {
                _distanceDictionary.Add(currentNode, (Int32.MaxValue, path));
            }

            // Neighbours are figured out before objects are passed in, so no logic around that here.
            foreach (var neighbour in GetNeighboursFunc(currentNode))
            {
                var newPath = path.Clone();
                newPath.Add(neighbour);
                if (!_distanceDictionary.ContainsKey(neighbour))
                {
                    _distanceDictionary.Add(neighbour, (Int32.MaxValue, newPath));
                }
                var weight = GetWeight == null? 1 : GetWeight(neighbour);
                var distance = _distanceDictionary[currentNode].Distance + weight;
                if (distance < _distanceDictionary[neighbour].Distance)
                {
                    var tup = (distance, newPath);
                    _distanceDictionary[neighbour] = tup;
                    DFSgeneric(tup, GetNeighboursFunc, GoalEvaluation, GetWeight);
                }
            }
        }

        private void ExampleUsage()
        {
            var exampleGrid = new List<List<int>>();

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in GridHelper.GetOrthogonalNeighbours(exampleGrid, node))
                {
                    neighbours.Add(edge);
                }
                return neighbours;
            };

            Func<GridLocation<int>, bool> GoalFunc = (node) =>
            {
                return node.Y == 3 && node.X == 3;
            };

            Func<GridLocation<int>, int> WeightFunc = (node) =>
            {
                return exampleGrid[node.Y][node.X];
            };
            var start = (0, new List<GridLocation<int>>() { new GridLocation<int>(0, 0) });
            var DFS = new DepthFirstSearch<GridLocation<int>>();
            DFS.DFSgeneric(start, NeighboursFunc, GoalFunc, WeightFunc);
        }
    }
}
