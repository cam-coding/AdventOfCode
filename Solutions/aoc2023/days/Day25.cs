using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2023
{
    public class Day25 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private string _seperator = "-";
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var nodes = new Dictionary<string,Day25Node>();
            foreach (var line in lines)
            {
                var tokens = line.Split(':').ToList().OnlyRealStrings(delimiterChars);
                var tokens2 = tokens[1].Split(delimiterChars).ToList().OnlyRealStrings(delimiterChars);
                Day25Node first = null;
                if (nodes.ContainsKey(tokens[0]))
                {
                    first = nodes[tokens[0]];
                }
                else
                {
                    first = new Day25Node(tokens[0]);
                }

                nodes.TryAdd(first.Key,first);


                foreach (var item in tokens2)
                {
                    Day25Node temp = null;
                    if (nodes.ContainsKey(item))
                    {
                        temp = nodes[item];
                    }
                    else
                    {
                        temp = new Day25Node(item);
                    }
                    temp.ConnectedTo.Add(first);
                    first.ConnectedTo.Add(temp);
                    nodes.TryAdd(temp.Key,temp);
                }
            }

            var prunedGraph = DoTheThing(nodes);
            var count = TraverseAndGetCount(prunedGraph);
            return (nodes.Count - count) * count;
        }

        public List<Day25Node> DoTheThing(Dictionary<string, Day25Node> nodes)
        {
            for (var i = 0; i < 3; i++)
            {
                var nodies = nodes.Values.ToList();
                var counts = new Dictionary<string, int>();
                foreach (var node in nodies)
                {
                    var distances = DijkstraSearch(node);
                    foreach (var item in distances)
                    {
                        foreach (var edge in item.Value)
                        {
                            if (counts.ContainsKey(edge))
                            {
                                counts[edge]++;
                            }
                            else
                            {
                                counts.Add(edge, 1);
                            }
                        }
                    }
                }
                var first1 = MostUsedConnection(counts);
                var tokens1 = first1.Split(_seperator).ToList().OnlyRealStrings(delimiterChars);
                var lookup = nodes[tokens1[0]];
                lookup.ConnectedTo.RemoveWhere(x => x.Key.Equals(tokens1[1]));
                lookup = nodes[tokens1[1]];
                lookup.ConnectedTo.RemoveWhere(x => x.Key.Equals(tokens1[0]));
            }
            return nodes.Values.ToList();
        }

        public int TraverseAndGetCount(List<Day25Node> nodes)
        {
            var count = 0;
            var visited = new HashSet<Day25Node>();

            var queue = new Queue<Day25Node>();
            queue.Enqueue(nodes[0]);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current))
                {
                    continue;
                }
                count++;
                visited.Add(current);

                foreach (var connectedNode in current.ConnectedTo)
                {
                    if (!visited.Contains(connectedNode))
                    {
                        queue.Enqueue(connectedNode);
                    }
                }
            }

            return count;
        }

        public string MostUsedConnection(Dictionary<string,int> counts)
        {
            var bestStr = "";
            var highestValue = 0;

            foreach (var item in counts)
            {
                if (item.Value >  highestValue)
                {
                    bestStr = item.Key;
                    highestValue = item.Value;
                }
            }
            return bestStr;
        }

        public Dictionary<string, List<string>> DijkstraSearch(Day25Node start)
        {
            var DistanceDictionary = new Dictionary<string, List<string>>();
            var processed = new HashSet<Day25Node>();
            var queue = new PriorityQueue<(Day25Node, List<string>), int>();
            DistanceDictionary.Add(start.Key, new List<string>());
            queue.Enqueue((start, new List<string>()), 0);

            while (queue.Count != 0)
            {
                var item = queue.Dequeue();
                var current = item.Item1;
                var history = item.Item2;
                if (processed.Contains(current))
                {
                    continue;
                }
                processed.Add(current);
                if (!DistanceDictionary.ContainsKey(current.Key))
                {
                    DistanceDictionary.Add(current.Key, new List<string>(history));
                }

                foreach (var neighbour in current.ConnectedTo)
                {
                    var newHistory = new List<string>(history);
                    newHistory.Add("" + current.Key + _seperator + neighbour.Key);
                    if (!DistanceDictionary.ContainsKey(neighbour.Key) ||
                        DistanceDictionary[neighbour.Key].Count == 0 ||
                        DistanceDictionary[current.Key].Count + 1 < DistanceDictionary[neighbour.Key].Count)
                    {
                        if (DistanceDictionary.ContainsKey(neighbour.Key))
                        {
                            DistanceDictionary[neighbour.Key] = newHistory;
                        }
                        else
                        {
                            DistanceDictionary.Add(neighbour.Key, new List<string>(newHistory));
                        }
                        queue.Enqueue((neighbour, DistanceDictionary[neighbour.Key]), newHistory.Count);
                    }
                }
            }
            DistanceDictionary[start.Key] = new List<string>();
            return DistanceDictionary;
        }

        public class Day25Node
        {
            public Day25Node(string key)
            {
                Key = key;
                ConnectedTo = new HashSet<Day25Node>();
            }
            public string Key { get; set; }

            public HashSet<Day25Node> ConnectedTo { get; set; }
        }


        private object Part2()
        {
            // Merry Christmas!
            return 0;
        }
    }
}