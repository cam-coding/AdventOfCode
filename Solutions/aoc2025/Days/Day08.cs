using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2025
{
    public class Day08 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private HashSet<(Box, Box)> _usedConnections;

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
            long count = 0;

            var boxes = new List<Box>();
            var circuits = new Dictionary<string, List<Box>>();
            var circuitLookup = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var longs = line.GetLongsFromString();
                var boxy = new Box(longs[0], longs[1], longs[2]);
                boxes.Add(boxy);
                circuits.Add(boxy.CircuitId, new List<Box>() { boxy });
                circuitLookup.Add(boxy.Id, boxy.CircuitId);
            }

            var shortestDistances = new List<(double distance, (Box, Box) pair)>();
            var pairs = boxes.GetPairs_Unique();
            foreach (var pair in pairs)
            {
                var distance = GetDistance(pair.Item1, pair.Item2);
                shortestDistances.Add((distance, pair));
            }

            shortestDistances.Sort((a, b) => a.distance.CompareTo(b.distance));

            var special = isTest ? 10 : 1000;
            for (var i = 0; i < special; i++)
            {
                var myPair = shortestDistances[i].pair;

                // if not already in same circuit
                if (!circuitLookup[myPair.Item1.Id].Equals(circuitLookup[myPair.Item2.Id]))
                {
                    var largerCircuitId = circuitLookup[myPair.Item2.Id];

                    // find the circuit of one box, and connect it to the circuit of the other box
                    circuits.Remove(circuitLookup[myPair.Item1.Id], out var movingCircuit);
                    circuits[largerCircuitId] = circuits[largerCircuitId].Concat(movingCircuit).ToList();

                    // make sure every box in the new circuit says its in that circuit.
                    foreach (var item in circuits[largerCircuitId])
                    {
                        circuitLookup[item.Id] = largerCircuitId;
                    }
                }
            }

            var largestCircuits = circuits.Select(x => x.Value.Count).ToList().SortDescending();
            return largestCircuits[0] * largestCircuits[1] * largestCircuits[2];
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            long count = 0;

            var boxes = new List<Box>();
            var circuits = new Dictionary<string, List<Box>>();
            var circuitLookup = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var longs = line.GetLongsFromString();
                var boxy = new Box(longs[0], longs[1], longs[2]);
                boxes.Add(boxy);
                circuits.Add(boxy.CircuitId, new List<Box>() { boxy });
                circuitLookup.Add(boxy.Id, boxy.CircuitId);
            }

            var shortestDistances = new List<(double distance, (Box, Box) pair)>();
            var pairs = boxes.GetPairs_Unique();
            foreach (var pair in pairs)
            {
                var distance = GetDistance(pair.Item1, pair.Item2);
                shortestDistances.Add((distance, pair));
            }

            shortestDistances.Sort((a, b) => a.distance.CompareTo(b.distance));
            var index = 0;
            (Box, Box) last = default;
            while (circuits.Count > 1)
            {
                var myPair = shortestDistances[index].pair;

                // if not already in same circuit
                if (!circuitLookup[myPair.Item1.Id].Equals(circuitLookup[myPair.Item2.Id]))
                {
                    var largerCircuitId = circuitLookup[myPair.Item2.Id];

                    // find the circuit of one box, and connect it to the circuit of the other box
                    circuits.Remove(circuitLookup[myPair.Item1.Id], out var movingCircuit);
                    circuits[largerCircuitId] = circuits[largerCircuitId].Concat(movingCircuit).ToList();

                    // make sure every box in the new circuit says its in that circuit.
                    foreach (var item in circuits[largerCircuitId])
                    {
                        circuitLookup[item.Id] = largerCircuitId;
                    }
                    last = myPair;
                }

                index++;
            }

            return last.Item1.X * last.Item2.X;
        }

        private double GetDistance(Box box1, Box box2)
        {
            var x = Math.Pow(box1.X - box2.X, 2);
            var y = Math.Pow(box1.Y - box2.Y, 2);
            var z = Math.Pow(box1.Z - box2.Z, 2);
            return Math.Pow(x + y + z, .5);
        }

        private class Box
        {
            public Box(long x2, long y2, long z2)
            {
                X = x2;
                Y = y2;
                Z = z2;
                Id = "" + x2.ToString() + y2.ToString() + z2.ToString();
                CircuitId = "circuit" + x2.ToString() + y2.ToString() + z2.ToString();
            }

            public long X;
            public long Y;
            public long Z;
            public string Id;
            public string CircuitId;
        }
    }
}