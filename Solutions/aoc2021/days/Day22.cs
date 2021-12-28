using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventLibrary;

namespace aoc2021
{
    public class Day22: ISolver
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
            return 0;
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var onCubes = new Dictionary<Tuple<int, int, int>, int>();
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetNumbersWithNegativesFromString(line);
                var x = new List<int>() { nums[0], nums[1] };
                var y = new List<int>() { nums[2], nums[3] };
                var z = new List<int>() { nums[4], nums[5] };

                var myBool = tokens[0].Equals("on");

                if (x[0] >= -50 && x[0] <= 50)
                {
                    for (var i = x[0]; i <= x[1]; i++)
                    {
                        for (var j = y[0]; j <= y[1]; j++)
                        {
                            for (var k = z[0]; k <= z[1]; k++)
                            {
                                if (myBool)
                                {
                                    if (!onCubes.ContainsKey(new Tuple<int, int, int>(i, j, k)))
                                    {
                                        onCubes.Add(new Tuple<int, int, int>(i, j, k), 1);
                                    }
                                }
                                else
                                {
                                    if (onCubes.ContainsKey(new Tuple<int, int, int>(i, j, k)))
                                    {
                                        onCubes.Remove(new Tuple<int, int, int>(i, j, k));
                                    }
                                }
                            }
                        }
                    }
                }
			}
            return onCubes.Count;
        }
        
        private object Part2()
        {

            var test1 = new List<int>() { 0, 10, 0, 10, 0, 10 };
            var test2 = new List<int>() { 3, 4, 3, 4, 3, 4 };
            Breakup(test1, test2);

            var lines = ParseInput.GetLinesFromFile(_filePath);
            var myCount = lines.Count-2;
            var onCubes = new Dictionary<Tuple<int, int, int>, int>();
            var tokens = lines[myCount].Split(delimiterChars);
            var nums = AdventLibrary.StringParsing.GetNumbersWithNegativesFromString(lines[myCount]);
            var specialX = new List<int>() { nums[0], nums[1] };
            var specialY = new List<int>() { nums[2], nums[3] };
            var specialZ = new List<int>() { nums[4], nums[5] };
            var deltaX = (BigInteger)Math.Abs(specialX[0] - specialX[1]);
            var deltaY = (BigInteger)Math.Abs(specialY[0] - specialY[1]);
            var deltaZ = (BigInteger)Math.Abs(specialZ[0] - specialZ[1]);
            BigInteger count = deltaX*deltaY*deltaZ;
            var listOfOff = new List<List<int>>();
            var listOfOn = new List<List<int>>();

            for (var i = myCount - 1; i >= 0; i--)
            {
                tokens = lines[myCount].Split(delimiterChars);
                nums = AdventLibrary.StringParsing.GetNumbersWithNegativesFromString(lines[i]);
                var x = new List<int>() { nums[0], nums[1] };
                var y = new List<int>() { nums[2], nums[3] };
                var z = new List<int>() { nums[4], nums[5] };
                if (tokens[0].Equals("on"))
                {
                    var onBox = new List<int>() { x[0], x[1], y[0], y[1], z[0], z[1] };
                    var onBoxes = new List<List<int>>();
                    if (listOfOff.Any())
                    {
                        foreach(var off in listOfOff)
                        {
                            var brokenUp = Breakup(onBox, off);

                            if (brokenUp != null)
                            {
                                onBoxes.AddRange(brokenUp);
                            }
                        }
                    }
                    else
                    {
                        listOfOn.Add(onBox);
                    }
                }
                else if (tokens[0].Equals("off"))
                {
                    var offBox = new List<int>() { x[0], x[1], y[0], y[1], z[0], z[1] };
                    listOfOff.Add(offBox);
                }
            }
            return count;
        }

        private List<List<int>> Breakup(List<int> onVertices, List<int> offVertices)
        {
            var listOfBoxes = new List<List<int>>();
            if (Intersects(onVertices, offVertices))
                {
                    if (onVertices[0] < offVertices[0])
                    {
                        //below x?
                        var mini = Math.Min(onVertices[1], offVertices[0]);
                        var blah = new List<int>() { onVertices[0], mini, onVertices[2], onVertices[3], onVertices[4], onVertices[5] };
                        listOfBoxes.Add(blah);
                    }
                    else
                    {
                        listOfBoxes.Add(null);
                    }
                    if (onVertices[1] > offVertices[1])
                    {
                        // above x?
                        var mini = Math.Max(onVertices[0], offVertices[1]);
                        var blah = new List<int>() { mini, onVertices[1], onVertices[2], onVertices[3], onVertices[4], onVertices[5] };
                        listOfBoxes.Add(blah);
                    }
                    else
                    {
                        listOfBoxes.Add(null);
                    }
                    if (onVertices[2] < offVertices[2])
                    {
                        // below y
                        var mini = Math.Min(onVertices[3], offVertices[2]);
                        var blah = new List<int>() { onVertices[0], onVertices[1], onVertices[2], mini, onVertices[4], onVertices[5] };
                        listOfBoxes.Add(blah);
                    }
                    else
                    {
                        listOfBoxes.Add(null);
                    }
                    if (onVertices[3] > offVertices[3])
                    {
                        // above y
                        var mini = Math.Max(onVertices[2], offVertices[3]);
                        var blah = new List<int>() { onVertices[0], onVertices[1], mini, onVertices[3], onVertices[4], onVertices[5] };
                        listOfBoxes.Add(blah);
                    }
                    else
                    {
                        listOfBoxes.Add(null);
                    }
                    if (onVertices[4] < offVertices[4])
                    {
                        // below z
                        var mini = Math.Min(onVertices[5], offVertices[4]);
                        var blah = new List<int>() { onVertices[0], onVertices[1], onVertices[2], onVertices[3], onVertices[4], mini };
                        listOfBoxes.Add(blah);
                    }
                    else
                    {
                        listOfBoxes.Add(null);
                    }
                    if (onVertices[5] > offVertices[5])
                    {
                        // above z
                        var mini = Math.Max(onVertices[4], offVertices[5]);
                        var blah = new List<int>() { onVertices[0], onVertices[1], onVertices[2], onVertices[3], mini, onVertices[5] };
                        listOfBoxes.Add(blah);
                    }
                    else
                    {
                        listOfBoxes.Add(null);
                    }
                    Console.WriteLine("Hello");
                }
                else
                {
                    return null;
                }

            return listOfBoxes;
        }

        private bool Intersects(List<int> onVertices, List<int> offVertices)
        {
            var x = ((offVertices[0] <= onVertices[0] || onVertices[0] <= offVertices[0]) ||
                    (offVertices[1] <= onVertices[1] || onVertices[1] <= offVertices[1]));
            var y = ((offVertices[2] <= onVertices[2] || onVertices[2] <= offVertices[2]) ||
                    (offVertices[3] <= onVertices[3] || onVertices[3] <= offVertices[3]));
            var z = ((offVertices[4] <= onVertices[4] || onVertices[4] <= offVertices[4]) ||
                    (offVertices[5] <= onVertices[5] || onVertices[5] <= offVertices[5]));
            return x && y && z;
        }

        private List<int> RemoveOverlap(List<int> alpha, List<int> beta)
        {
            return null;
        }
    }
}