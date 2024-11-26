using AdventLibrary;
using AdventLibrary.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static aoc2016.Day11;

namespace aoc2016
{
    public class Day11 : ISolver
    {
        private string _filePath;
        /*
        private readonly List<List<Item>> _testFloors = new List<List<Item>>()
        {
            new List<Item>() { new Item('H', true), new Item('L', true) },
            new List<Item>() { new Item('H', false) },
            new List<Item>() { new Item('L', false) },
            new List<Item>(),
        };

        private readonly List<List<Item>> _testFloors2 = new List<List<Item>>()
        {
            new List<Item>() { new Item('H', true), new Item('L', true), new Item('A', true)},
            new List<Item>() { new Item('H', false), new Item('A', false) },
            new List<Item>() { new Item('L', false) },
            new List<Item>(),
        };

        private readonly List<List<Item>> _testFloors3 = new List<List<Item>>()
        {
            new List<Item>() { new Item('H', true), new Item('L', true), new Item('A', true), new Item('B', true)},
            new List<Item>() { new Item('H', false), new Item('A', false), new Item('B', false) },
            new List<Item>() { new Item('L', false) },
            new List<Item>(),
        };*/

        private readonly List<List<Item>> _part1Floors = new List<List<Item>>()
        {
            new List<Item>() { new Item('S', false), new Item('S', true), new Item('P', false), new Item('P', true) },
            new List<Item>() { new Item('T', false), new Item('R', false), new Item('R', true), new Item('C', false), new Item('C', true) },
            new List<Item>() { new Item('T', true) },
            new List<Item>(),
        };

        private readonly List<List<Item>> _part2Floors = new List<List<Item>>()
        {
            new List<Item>() { new Item('S', false), new Item('S', true), new Item('P', false), new Item('P', true), new Item('E', false), new Item('E', true), new Item('D', false), new Item('D', true) },
            new List<Item>() { new Item('T', false), new Item('R', false), new Item('R', true), new Item('C', false), new Item('C', true) },
            new List<Item>() { new Item('T', true) },
            new List<Item>(),
        };

        private int _totalObjects;

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1()
        {
            return GoTime(_part1Floors);
        }

        private object Part2()
        {
            return GoTime(_part2Floors);
        }

        private object GoTime(List<List<Item>> startingFloors)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var q = new Queue<State>();
            _totalObjects = startingFloors.Sum(x => x.Count);
            var previousStates = new Dictionary<string, int>();
            var startingState = new State(
                startingFloors,
                0,
                0);
            q.Enqueue(startingState);
            var best = int.MaxValue;
            var statesTried = 0;

            while (q.Count > 0)
            {
                var state = q.Dequeue();
                var currentFloor = state.Elevator;
                var stateHash = state.GetHash();
                var bestOfThisState = true;

                if (!previousStates.ContainsKey(stateHash))
                {
                    previousStates.Add(stateHash, state.Count);
                }
                else
                {
                    if (state.Count < previousStates[stateHash])
                    {
                        previousStates[stateHash] = state.Count;
                    }
                    else
                    {
                        bestOfThisState = false;
                    }
                }
                if (!bestOfThisState)
                {
                    continue;
                }
                if (state.Count < best)
                {
                    statesTried++;
                    if (state.Floors[3].Count == _totalObjects)
                    {
                        if (state.Count < best)
                        {
                            best = state.Count;
                            // PrintHistory(state.History);
                        }
                    }
                    else
                    {
                        var indexesOfCurrentFloor = new List<int>();
                        for (var x = 0; x < state.Floors[currentFloor].Count; x++)
                        {
                            indexesOfCurrentFloor.Add(x);
                        }

                        // Don't go back to the first floor once it's emptied
                        var lowest = state.Floors[0].Count == 0 ? 1 : 0;
                        var min = Math.Max(lowest, currentFloor - 1);
                        // Don't tackle the 4th floor until the bottom is cleared out
                        var highest = state.Floors[0].Count == 0 ? 3 : 2;
                        var max = Math.Min(highest, currentFloor + 1);

                        if (min < currentFloor)
                        {
                            foreach (var index in indexesOfCurrentFloor)
                            {
                                var newFloors = state.Floors.Clone2dList();
                                newFloors[currentFloor].RemoveAt(index);
                                newFloors[min].Add(state.Floors[currentFloor][index]);

                                var newItem2 = new State(
                                    newFloors,
                                    state.Count + 1,
                                    min);

                                if (IsStateValid(newItem2) && IdealFloors(newFloors))
                                {
                                    q.Enqueue(newItem2);
                                }
                            }
                        }

                        if (max > currentFloor)
                        {
                            var pairs = GetKCombs(indexesOfCurrentFloor, 2);
                            foreach (var pair in pairs)
                            {
                                var newFloors = state.Floors.Clone2dList();
                                foreach (var removal in pair)
                                {
                                    newFloors[currentFloor].Remove(state.Floors[currentFloor][removal]);
                                }
                                foreach (var removal in pair)
                                {
                                    newFloors[max].Add(state.Floors[currentFloor][removal]);
                                }
                                var newItem2 = new State(
                                    newFloors,
                                    state.Count + 1,
                                    max);

                                if (IsStateValid(newItem2) && IdealFloors(newFloors))
                                {
                                    q.Enqueue(newItem2);
                                }
                            }
                        }
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Time for day 11: " + stopwatch.ElapsedMilliseconds);
            return best;
        }

        private bool IdealFloors(List<List<Item>> floors)
        {
            if (floors[3].Count == 0 || floors[0].Count == 0)
            {
                return true;
            }
            return false;
        }

        public  static IEnumerable<IEnumerable<T>> GetKCombs<T>(IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetKCombs(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public class State
        {
            public State(
                List<List<Item>> floors,
                int count,
                int elevator)
            {
                Floors = floors;
                Count = count;
                Elevator = elevator;
            }

            public List<List<Item>> Floors { get; private set; }

            public int Count { get; private set; }

            // zero indexed
            public int Elevator { get; set; }

            public string GetHash()
            {
                var str = string.Empty + Elevator;
                var dict = new Dictionary<char, int>();
                var counter = 0;

                for (var i = 0; i < Floors.Count; i++)
                {
                    var floorHash = new List<string>();

                    foreach (var obj in Floors[i])
                    {
                        if (!dict.ContainsKey(obj.Element))
                        {
                            dict.Add(obj.Element, counter);
                            counter++;
                        }
                        var c = obj.IsChip ? 'C' : 'G';
                        floorHash.Add(string.Empty + c + dict[obj.Element]);
                    }
                    floorHash.Sort();
                    str = str + string.Join("", floorHash) + i;
                }
                return str;
            }
        }

        public class Item
        {
            public Item(char element, bool isChip)
            {
                Element = element;
                IsChip = isChip;
            }

            public char Element { get; private set; }

            public bool IsChip { get; private set; }

            public bool Match(Item other)
            {
                if (this == other)
                {
                    return false;
                }
                if (this.Element == other.Element)
                {
                    return true;
                }
                return false;
            }

            public override string ToString()
            {
                var str = string.Empty + Element;

                if (IsChip)
                {
                    str += 'M';
                }
                else
                {
                    str += 'G';
                }
                return str;
            }
        }

        public bool IsStateValid(State state)
        {
            foreach (var floor in state.Floors)
            {
                var chipWithoutGenerator = false;
                var chipWithGenerator = false;
                foreach (var obj in floor)
                {
                    // if there's a chip on the floor
                    if (obj.IsChip)
                    {
                        if (floor.Any(x => x.Match(obj)))
                        {
                            chipWithGenerator = true;
                        }
                        else
                        {
                            chipWithoutGenerator = true;
                        }
                    }
                }
                if (chipWithGenerator && chipWithoutGenerator)
                {
                    return false;
                }
            }

            return true;
        }
    }
}