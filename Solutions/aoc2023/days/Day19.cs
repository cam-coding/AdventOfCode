using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2023
{
    public class Day19: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private List<ulong> _special;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var dict = new Dictionary<string, string> ();
            var accepted = new List<Part>();
            var myBool = false;
			foreach (var line in lines)
			{
                if (string.IsNullOrWhiteSpace(line))
                {
                    myBool = true;
                }
                else if (!myBool)
                {
                    var tokens1 = line.Split('{', '}').ToList().OnlyRealStrings(delimiterChars);

                    dict.Add(tokens1[0], tokens1[1]);
                }
                else
                {

                    var part = new Part();
                    var nums = StringParsing.GetNumbersFromString(line);
                    part.x = nums[0];
                    part.m = nums[1];
                    part.a = nums[2];
                    part.s = nums[3];

                    var current = "in";

                    while (!current.Equals("A") && !current.Equals("R"))
                    {
                        var tokens = dict[current].Split(",").ToList().OnlyRealStrings(delimiterChars);
                        foreach (var tok in tokens)
                        {
                            var toks = tok.Split(':', '<', '>').ToList().OnlyRealStrings(delimiterChars);
                            var myNums = StringParsing.GetNumbersFromString(tok);

                            if (myNums.Count == 0)
                            {
                                current = tok;
                                break;
                            }
                            var num = myNums[0];
                            var greater = tok.Contains('>');
                            var valid = false;
                            if (toks[0][0] == 'x')
                            {
                                if (greater)
                                {
                                    valid = part.x > num;
                                }
                                else
                                {
                                    valid = part.x < num;
                                }
                            }
                            else if (toks[0][0] == 'm')
                            {
                                if (greater)
                                {
                                    valid = part.m > num;
                                }
                                else
                                {
                                    valid = part.m < num;
                                }
                            }
                            else if (toks[0][0] == 'a')
                            {
                                if (greater)
                                {
                                    valid = part.a > num;
                                }
                                else
                                {
                                    valid = part.a < num;
                                }
                            }
                            else if (toks[0][0] == 's')
                            {
                                if (greater)
                                {
                                    valid = part.s > num;
                                }
                                else
                                {
                                    valid = part.s < num;
                                }
                            }
                            if (valid)
                            {
                                // check
                                current = toks[^1];
                                break;
                            }
                        }
                    }
                    if (current.Equals("A"))
                    {
                        accepted.Add(part);
                    }
                }
			}
            var county = 0;

            foreach (var item in accepted)
            {
                county += item.a + item.x + item.m + item.s;
            }
            return county;
        }

        private class Part
        {
            public int x { get; set; }
            public int m { get; set; }
            public int a { get; set; }
            public int s { get; set; }
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var dict = new Dictionary<string, string>();
            var myBool = false;
            var nodes = new Dictionary<string, TreeNode>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    myBool = true;
                }
                else if (!myBool)
                {
                    var tokens1 = line.Split('{', '}').ToList().OnlyRealStrings(delimiterChars);
                    dict.Add(tokens1[0], tokens1[1]);
                }
                else
                {
                    foreach (var item in dict)
                    {
                        nodes.Add(item.Key, new TreeNode(item.Key));
                    }
                    break;
                }
            }

            nodes.Add("A", new TreeNode("A"));
            nodes.Add("R", new TreeNode("R"));
            var myCount = 0;

            foreach (var item in dict)
            {
                var node = nodes[item.Key];

                var tokens = item.Value.Split(",").ToList().OnlyRealStrings(delimiterChars);

                foreach (var tok in tokens)
                {
                    var toks = tok.Split(':', '<', '>').ToList().OnlyRealStrings(delimiterChars);
                    var myNums = StringParsing.GetNumbersFromString(tok);
                    var lookup = toks[^1];
                    if (lookup.Equals("R"))
                    {
                        myCount++;
                    }

                    if (myNums.Count == 0)
                    {
                        node.Connected.Add((nodes[lookup], 'i', -1, -1));
                        break;
                    }
                    var num = myNums[0];
                    var greater = tok.Contains('>');
                    var valid = false;
                    var cr = toks[0][0];
                    if (greater)
                    {
                        node.Connected.Add((nodes[lookup], cr, num + 1, 4000));
                    }
                    else
                    {
                        node.Connected.Add((nodes[lookup], cr, 1, num-1));
                    }
                }
            }
            var trav = new TraversalUnit();
            var myVal = (ulong)Calc(trav);
            _special = new List<ulong>();
            var value = Traverse(trav, nodes["in"], Calc(trav));

            ulong total = 0;
            foreach (var item in _special)
            {
                total += item;
            }
            ulong answer2 = myVal - total;
            return answer2;
        }

        private long Traverse(TraversalUnit current, TreeNode node, long debug)
        {
            if (node.Connected.Count == 0)
            {
                if (node.Key.Equals("A"))
                {
                    return Calc(current);
                }
                if (node.Key.Equals("R"))
                {
                    _special.Add((ulong)Calc(current));
                    return 0;
                }
            }
            long total = 0;
            var currentRemain = new TraversalUnit(current);
            foreach (var item in node.Connected)
            {
                var nextNode = item.Item1;
                var key = nextNode.Key;
                var cr = item.Item2;
                var min = item.Item3;
                var max = item.Item4;

                if (cr == 'i')
                {
                    // var remain = HandlePast(current, past);
                    var val = Traverse(currentRemain, nextNode, Calc(currentRemain));
                    total += val;
                }
                else
                {
                    var newy = new TraversalUnit(currentRemain);
                    if (cr == 'a')
                    {
                        newy.MinA = Math.Max(min, currentRemain.MinA);
                        newy.MaxA = Math.Min(max, currentRemain.MaxA);
                        if (max < currentRemain.MaxA)
                        {
                            currentRemain.MinA = max + 1;
                        }
                        else
                        {
                            currentRemain.MaxA = min - 1;
                        }
                    }
                    else if (cr == 'x')
                    {
                        newy.MinX = Math.Max(min, currentRemain.MinX);
                        newy.MaxX = Math.Min(max, currentRemain.MaxX);
                        if (max < currentRemain.MaxX)
                        {
                            currentRemain.MinX = max + 1;
                        }
                        else
                        {
                            currentRemain.MaxX = min - 1;
                        }
                    }
                    else if (cr == 's')
                    {
                        newy.MinS = Math.Max(min, currentRemain.MinS);
                        newy.MaxS = Math.Min(max, currentRemain.MaxS);
                        if (max < currentRemain.MaxS)
                        {
                            currentRemain.MinS = max + 1;
                        }
                        else
                        {
                            currentRemain.MaxS = min - 1;
                        }
                    }
                    else if (cr == 'm')
                    {
                        newy.MinM = Math.Max(min, currentRemain.MinM);
                        newy.MaxM = Math.Min(max, currentRemain.MaxM);
                        if (max < currentRemain.MaxM)
                        {
                            currentRemain.MinM = max + 1;
                        }
                        else
                        {
                            currentRemain.MaxM = min - 1;
                        }
                    }

                    var val = Traverse(newy, nextNode, Calc(newy));
                    total += val;
                }
            }
            if (total > debug)
            {
                Console.WriteLine("error");
            }
            return total;
        }

        private long Calc(TraversalUnit current)
        {
            if (current.MaxA == -1 ||
                current.MaxS == -1 ||
                current.MaxM == -1 ||
                current.MaxX == -1)
            {
                return 0;
            }
            long val = 1;
            val = (long)Math.Max((current.MaxX - current.MinX + 1),1) *
                (long)Math.Max((current.MaxA - current.MinA + 1), 1) *
                (long)Math.Max((current.MaxS - current.MinS + 1), 1) *
                (long)Math.Max((current.MaxM - current.MinM + 1), 1);
            if (val == 1)
            {
                return 0;
            }
            return val;
        }

        public class TraversalUnit
        {
            public TraversalUnit(bool zero = false)
            {
                if (zero)
                {
                    MaxX = -1;
                    MinX = 0;
                    MaxA = -1;
                    MinA = 0;
                    MaxS = -1;
                    MinS = 0;
                    MaxM = -1;
                    MinM = 0;
                }
                else
                {
                    MaxX = 4000;
                    MinX = 1;
                    MaxA = 4000;
                    MinA = 1;
                    MaxS = 4000;
                    MinS = 1;
                    MaxM = 4000;
                    MinM = 1;
                }
            }

            public TraversalUnit(
                int maxX, int minX, int maxS, int minS, int maxA, int minA, int maxM, int minM)
            {
                MaxX = maxX;
                MinX = minX;
                MaxS = maxS;
                MinS = minS;
                MaxA = maxA;
                MinA = minA;
                MaxM = maxM;
                MinM = minM;
            }
            public TraversalUnit(TraversalUnit other)
            {
                MaxX = other.MaxX;
                MinX = other.MinX;
                MaxS = other.MaxS;
                MinS = other.MinS;
                MaxA = other.MaxA;
                MinA = other.MinA;
                MaxM = other.MaxM;
                MinM = other.MinM;
            }

            public int MaxX { get; set; }
            public int MinX { get; set; }
            public int MaxS { get; set; }
            public int MinS { get; set; }
            public int MaxA { get; set; }
            public int MinA { get; set; }
            public int MaxM { get; set; }
            public int MinM { get; set; }
        }

        private class TreeNode
        {
            public TreeNode(string key)
            {
                Connected = new List<(TreeNode, char, int, int)>();
                Key = key;
            }

            public List<(TreeNode, char, int, int)> Connected;

            public string Key;
        }
    }
}