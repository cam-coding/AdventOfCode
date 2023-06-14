using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;
using AdventLibrary.PathFinding;

namespace aoc2016
{
    public class Day11 : ISolver
    {
        private string _filePath;

        private readonly List<List<Item>> _testFloors = new List<List<Item>>()
        {
            new List<Item>() { new Item('H', true), new Item('L', true) },
            new List<Item>() { new Item('H', false) },
            new List<Item>() { new Item('L', false) },
            new List<Item>(),
        };

        private readonly List<List<Item>> _floors = new List<List<Item>>()
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

        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var q = new PriorityQueue<State, int>();
            var totalObjects = _testFloors.Sum(x => x.Count);
            var previousStates = new Dictionary<string, int>();
            q.Enqueue(new State(_testFloors, 0, 0), GetPriority(new State(_testFloors, 0, 0)));
            var best = int.MaxValue;
            var counter = 0;

            while (q.Count > 0)
            {
                counter++;
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
                if (bestOfThisState &&
                    state.Count < 12 &&
                    state.Count < best &&
                    IsStateValid(state))
                {
                    if (state.Floors[3].Count == totalObjects)
                    {
                        if (state.Count < best)
                        {
                            best = state.Count;
                        }
                    }
                    else
                    {
                        // within each object on the current floor, try moving it
                        for (var i = 0; i < state.Floors[currentFloor].Count; i++)
                        {
                            // take that item out and add it to listy which goes into a different floor
                            var temp = ListTransforming.Clone2dList(state.Floors);
                            var removal = temp[currentFloor][i];
                            temp[currentFloor].RemoveAt(i);
                            for (var j = -1; j < temp[currentFloor].Count; j++)
                            {
                                var listy = new List<Item>() { removal };
                                // try taking i on it's own (j=-1) & every combination of i + something else
                                var newFloors = ListTransforming.Clone2dList(temp);
                                if (j != -1)
                                {
                                    listy.Add(newFloors[state.Elevator][j]);
                                    newFloors[state.Elevator].RemoveAt(j);
                                }

                                // look at moving elevator up or down a floor
                                var min = Math.Max(0, state.Elevator - 1);
                                var max = Math.Min(3, state.Elevator + 1);
                                for (var k = min; k <= max; k++)
                                {
                                    if (k != state.Elevator)
                                    {
                                        var newItem2 = new State(ListTransforming.Clone2dList(newFloors), state.Count + 1, k);
                                        newItem2.Floors[k].InsertRange(0, listy);

                                        if (newItem2.Floors.Sum(x => x.Count) > totalObjects)
                                        {
                                            Console.WriteLine("FAIL");
                                        }
                                        q.Enqueue(newItem2, GetPriority(newItem2));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return best;
        }

        private object Part2()
        {
            return 0;
        }

        public class State
        {
            public State(List<List<Item>> floors, int count, int elevator)
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
                // past state/hash shouldn't care about order in floors
                var str = string.Empty + Elevator;

                for (var i = 0; i < Floors.Count; i++)
                {
                    var floorHash = new List<string>();
                    foreach (var obj in Floors[i])
                    {
                        var c = obj.IsChip ? 'C' : 'G';
                        floorHash.Add(string.Empty + c + obj.Element);
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
                    if (this.IsChip == other.IsChip)
                    {
                        Console.WriteLine("SOMETHING IS TERRIBLY WRONG");
                    }
                    return true;
                }
                return false;
            }
            public bool IsSameItem(Item other)
            {
                if (this == other)
                {
                    return true;
                }
                if (this.Element == other.Element)
                {
                    if (this.IsChip == other.IsChip)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsStateValid(State state)
        {
            foreach (var floor in state.Floors)
            {
                var chipHereAndHasGenerator = new List<bool>();
                foreach (var obj in floor)
                {
                    // if there's a chip on the floor
                    if (obj.IsChip)
                    {
                        chipHereAndHasGenerator.Add(floor.Any(x => x.Match(obj)));
                    }
                }
                if (chipHereAndHasGenerator.Any(x => !x) && !chipHereAndHasGenerator.All(x => !x))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetPriority(State state)
        {
            var priority = 0;

            for (var i = 3; i >= 0; i--)
            {
                priority = priority + (state.Floors[i].Count * i);
            }

            priority = priority + ((100 - state.Count) / 10);
            return priority;
        }
    }
}