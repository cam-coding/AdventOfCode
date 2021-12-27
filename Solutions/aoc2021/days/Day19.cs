using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day19: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var i = 0;
            List<List<(int x, int y, int z)>> scanners = new List<List<(int x, int y, int z)>>();
            while (i < lines.Count)
            {
                if (lines[i].StartsWith("---"))
                {
                    var beacons = new List<(int x, int y, int z)>();

                    i++;
                    var line = lines[i];
                    while (!string.IsNullOrWhiteSpace(line))
                    {
                        var nums = StringParsing.GetNumbersWithNegativesFromString(line);
                        (int x, int y, int z) toople = (nums[0], nums[1], nums[2]);
                        beacons.Add(toople);

                        i++;
                        if (i == lines.Count)
                            break;
                        line = lines[i];
                    }
                    scanners.Add(beacons);
                }
                i++;
            }

/*
            var myTotalScans = new Dictionary<((int from, int to, int permu), (int x, int y, int z))>();

            // var myBeacon = scanners[0][0];
            for (var i = 0; i < scanners.Count; i++)
            {
                var possibleScannerPos = new Dictionary<(int x, int y, int z), int>();
                var scannerPermutation = new Dictionary<(int x, int y, int z), int>();
                for(var j = 0; j < scanners.Count; j++)
                {
                    if (i != j)
                    {
                        var allScanners = AllPermutations2(new List<int>() {beacon.x, beacon.y, beacon.z});
                        var k = 0;
                        foreach (var scannerPos in allScanners)
                        {
                            (int x, int y, int z) toople = (scannerPos[0] + myBeacon.x, scannerPos[1] + myBeacon.y, scannerPos[2] + myBeacon.z);
                            if (toople.x == 68 && toople.y == -1246 && toople.z == -43)
                            {
                                Console.WriteLine(k);
                            }
                            if (possibleScannerPos.ContainsKey(toople))
                            {
                                possibleScannerPos[toople] = possibleScannerPos[toople] + 1;
                            }
                            else
                            {
                                possibleScannerPos.Add(toople, 1);
                                scannerPermutation.Add(toople, k);
                            }
                            k++;
                        }
                    }
                }

                var sortedDict = from entry in possibleScannerPos orderby entry.Value descending select entry;
                var bestScan = sortedDict.First();
                var bestScanPermutation = scannerPermutation[bestScan.Key];

                if (bestScan.Value >= 12)
                {
                    (int from, int to, int permu) toople = (i, j, bestScanPermutation);
                    myTotalScans.Add(toople, bestScan.Key);
                }
            }*/
            /*
            var possibleScannerPos = new Dictionary<(int x, int y, int z), int>();
            var scannerPermutation = new Dictionary<(int x, int y, int z), int>();
            foreach (var myBeacon in scanners[1])
            {
                foreach(var beacon in scanners[4])
                {
                    var allScanners = AllPermutations2(new List<int>() {beacon.x, beacon.y, beacon.z});
                    var k = 0;
                    foreach (var scannerPos in allScanners)
                    {
                        (int x, int y, int z) toople = (scannerPos[0] + myBeacon.x, scannerPos[1] + myBeacon.y, scannerPos[2] + myBeacon.z);
                        if (toople.x == 68 && toople.y == -1246 && toople.z == -43)
                        {
                            Console.WriteLine(k);
                        }
                        if (possibleScannerPos.ContainsKey(toople))
                        {
                            possibleScannerPos[toople] = possibleScannerPos[toople] + 1;
                        }
                        else
                        {
                            possibleScannerPos.Add(toople, 1);
                            scannerPermutation.Add(toople, k);
                        }
                        k++;
                    }
                }
            }

            var sortedDict = from entry in possibleScannerPos orderby entry.Value descending select entry;
            var bestScan = sortedDict.First();
            var bestScanPermutation = scannerPermutation[bestScan.Key];

            if (bestScan.Value >= 12)
            {
                myTotalScans.Add(new (i, j, bestScanPermutation, bestScan.Key))
            }
            /*
            var permuteOfZero = AllPermutations2(new List<int>() {68, -1246, -43})[1];
            var permuteOfOne = AllPermutations2(new List<int>() {88, 113, -1104})[1];
            var permuteOfZero2 = AllPermutations2(new List<int>(permuteOfZero))[28];
            var weird1 = AllPermutations2(new List<int>() {bestScan.Key.x, bestScan.Key.y, bestScan.Key.z})[1];
            var weird2 = AllPermutations2(new List<int>() {bestScan.Key.x, bestScan.Key.y, bestScan.Key.z})[28];*/

            /*
            var blah = possibleScannerPos.Where( x => x.Key.x == 68 && x.Key.y == -1246).ToList();

            List<int> seq = new List<int>() { 686,422,578 };
            var myList = AllPermutations(seq);

            var beaconPos = new List<List<int>>();

            foreach(var item in myList)
            {
                beaconPos.Add(new List<int>() {item[0] + 68, item[1] - 1246, item[2] - 43});
            }*/
			var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var nodes = ParseInput.ParseFileAsGraph(_filePath);
            var grid = ParseInput.ParseFileAsGrid(_filePath);
            var total = 1000000;
			var counter = 0;
            return total;
        }
        
        private object Part2()
        {
            return 0;
        }

        public List<List<int>> AllPermutations2(List<int> listy)
        {
            var val = new List<List<int>>();
            foreach (var perm in PermuteSpots(listy))
            {
                val.AddRange(PermutePosNeg(perm));
            }
            return val;
        }

        public List<List<int>> PermuteSpots(List<int> listy)
        {
            var val = new List<List<int>>();
            val.Add(new List<int>() { listy[0], listy[1], listy[2] } );
            val.Add(new List<int>() { listy[0], listy[2], listy[1] } );
            val.Add(new List<int>() { listy[1], listy[0], listy[2] } );
            val.Add(new List<int>() { listy[1], listy[2], listy[0] } );
            val.Add(new List<int>() { listy[2], listy[1], listy[0] } );
            val.Add(new List<int>() { listy[2], listy[0], listy[1] } );
            return val;
        }

        public List<List<int>> PermutePosNeg(List<int> listy)
        {
            var val = new List<List<int>>();
            val.Add(new List<int>() { listy[0], listy[1], listy[2] } );
            val.Add(new List<int>() { listy[0], listy[1]*-1, listy[2] } );
            val.Add(new List<int>() { listy[0], listy[1], listy[2]*-1 } );
            val.Add(new List<int>() { listy[0], listy[1]*-1, listy[2]*-1 } );
            val.Add(new List<int>() { listy[0]*-1, listy[1], listy[2] } );
            val.Add(new List<int>() { listy[0]*-1, listy[1]*-1, listy[2] } );
            val.Add(new List<int>() { listy[0]*-1, listy[1], listy[2]*-1 } );
            val.Add(new List<int>() { listy[0]*-1, listy[1]*-1, listy[2]*-1 } );
            return val;
        }

        public List<List<int>> AllPermutations(List<int> listy)
        {
            var val = new List<List<int>>();
            foreach (var rotate in PermutateRotate(listy))
            {
                val.AddRange(PermutateFacing(rotate));
            }
            return val;
        }

        public List<List<int>> PermutateFacing(List<int> listy)
        {
            var val = new List<List<int>>();
            val.Add(new List<int>() { listy[0], listy[1], listy[2] } );
            val.Add(new List<int>() { listy[0], listy[2], listy[1]*-1 } );
            val.Add(new List<int>() { listy[0], listy[1]*-1, listy[2]*-1 } );
            val.Add(new List<int>() { listy[0], listy[2]*-1, listy[1] } );
            val.Add(new List<int>() { listy[2]*-1, listy[1], listy[0] } );
            val.Add(new List<int>() { listy[2], listy[1], listy[0]*-1 } );

            return val;
        }

        public List<List<int>> PermutateRotate(List<int> listy)
        {
            var val = new List<List<int>>();
            val.Add(new List<int>() { listy[0], listy[1], listy[2] } );
            val.Add(new List<int>() { listy[1]*-1, listy[0], listy[2] } );
            val.Add(new List<int>() { listy[0]*-1, listy[1]*-1, listy[2] } );
            val.Add(new List<int>() { listy[1], listy[0]*-1, listy[2] } );
            
            return val;
        }
    }
}