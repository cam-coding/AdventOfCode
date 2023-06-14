using AdventLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace aoc2016
{
    public class Day11 : ISolver
    {
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
        };

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

        public Solution Solve(string filePath)
        {
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var q = new Queue<State>();
            var qMaxSize = 0;
            var totalObjects = _part2Floors.Sum(x => x.Count);
            var previousStates = new Dictionary<string, int>();
            q.Enqueue(new State(_part2Floors, 0, 0));
            var best = 100;
            var counter = 0;
            var realCounter = 0;

            while (q.Count > 0)
            {
                if (q.Count > qMaxSize)
                {
                    qMaxSize = q.Count;
                }
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
                if (!bestOfThisState)
                {
                    continue;
                }
                if (state.Count >= best)
                {
                    previousStates[stateHash] = 0;
                    continue;
                }
                else if (StillPossible(state, best))
                {
                    realCounter++;
                    if (state.Floors[3].Count == totalObjects)
                    {
                        if (state.Count < best)
                        {
                            best = state.Count;
                        }
                    }
                    else
                    {
                        var camList = new List<int>();
                        for (var x = 0; x < state.Floors[currentFloor].Count; x++)
                        {
                            camList.Add(x);
                        }
                        var blah = GetKCombs(camList, 2);
                        var blah3 = camList.Select(x => new List<int>() { x }).ToList();
                        foreach (var item in blah)
                        {
                            blah3.Add(item.ToList());
                        }

                        foreach (var item in blah3)
                        {
                            var newFloors = ListTransforming.Clone2dList(state.Floors);
                            foreach (var removal in item)
                            {
                                newFloors[currentFloor].Remove(state.Floors[currentFloor][removal]);
                            }

                            if (IsFloorValid(newFloors[currentFloor]))
                            {
                                // look at moving elevator up or down a floor
                                var min = Math.Max(0, state.Elevator - 1);
                                var max = Math.Min(3, state.Elevator + 1);
                                for (var k = min; k <= max; k++)
                                {
                                    if (k != state.Elevator)
                                    {
                                        var newItem2 = new State(ListTransforming.Clone2dList(newFloors), state.Count + 1, k);
                                        foreach (var removal in item)
                                        {
                                            newItem2.Floors[k].Add(state.Floors[currentFloor][removal]);
                                        }

                                        if (IsFloorValid(newItem2.Floors[k]))
                                        {
                                            q.Enqueue(newItem2);
                                        }
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

        public  static IEnumerable<IEnumerable<T>> GetKCombs<T>(IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetKCombs(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
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

        public bool IsFloorValid(List<Item> floor)
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

            return true;
        }

        public bool StillPossible(State state, int best)
        {
            if (best == int.MaxValue)
            {
                return true;
            }

            var items = 0;
            var minMoves = 0;

            for (var i = 0; i < 3; i++)
            {
                items = items + state.Floors[i].Count;
                minMoves = minMoves + ((items + 2 - 1) / 2);
            }

            return state.Count + minMoves < best;
        }
    }
}