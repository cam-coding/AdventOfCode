using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day22: ISolver
  {
        private string _filePath;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath).Skip(2).ToList();
            var nodes = new List<Node>();
			var counter = 0;
			
			foreach (var line in lines)
			{
				var nums = StringParsing.GetNumbersFromString(line);
                nodes.Add(new Node(nums));
            }

            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Used > 0)
                {
                    for (var j = 0; j < nodes.Count; j++)
                    {
                        if (i != j && nodes[i].Used <= nodes[j].Avail)
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }
        
        private object Part2()
        {
            // still need to make a logic for this. Doesn't seem hard once you are beside the data.
            var lines = ParseInput.GetLinesFromFile(_filePath).Skip(2).ToList();
            var nodes = new List<Node>();
            var counter = 0;
            var maxX = 0;
            var maxY = 0;

            var printArray = new Char[33, 30];

            foreach (var line in lines)
            {
                var nums = StringParsing.GetNumbersFromString(line);
                var newNode = new Node(nums);
                nodes.Add(newNode);
                if (newNode.X > maxX)
                    maxX = newNode.X;
                if (newNode.Y > maxY)
                    maxY = newNode.Y;

                char insert = '.';
                if (newNode.Used > 90)
                {
                    insert = '#';
                }
                else if (newNode.Used > 75)
                {
                    insert = '$';
                }
                else if (newNode.Size < 75)
                {
                    insert = 'c';
                }
                else if (newNode.Used == 0)
                {
                    insert = '_';
                }
                printArray[newNode.X,newNode.Y] = insert;
            }

            GridHelper.PrintGrid(printArray);

            /* 2 to touch the wall
             * 16 to the top of the wall
             * 10 to the edge
             * 21 to beside the data
             * 1 to grab the data
             * 31 moves and each move takes 5
             * */
            return 205;
        }

        private class Node
        {
            public Node(List<int> nums)
            {
                X = nums[0];
                Y = nums[1];
                Size = nums[2];
                Used = nums[3];
                Avail = nums[4];
            }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int Size { get; private set; }

            public int Used { get; private set; }

            public int Avail { get; private set; }
        }
    }
}
