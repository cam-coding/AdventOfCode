using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using Microsoft.Win32.SafeHandles;

namespace aoc2024
{
    public class Day24: ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
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
			var numbers = input.Longs;
            var longLines = input.LongLines;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            var myDict = new Dictionary<string, bool>();

            var group1 = input.LineGroupsSeperatedByWhiteSpace[0];
            var group2 = input.LineGroupsSeperatedByWhiteSpace[1];

            foreach (var line in group1)
            {
                var toks = line.Split(_delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                var num = long.Parse(toks[1]);
                var myBool = num == 1;

                myDict.Add(toks[0], myBool);
            }

            while (group2.Count > 0)
            {
                var iter = 0;
                while (iter < group2.Count)
                {
                    var toks = group2[iter].Split(_delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                    var gate1 = toks[0];
                    var gate2 = toks[2];
                    var op = toks[1].ToUpper();
                    var resGate = toks[3];

                    if (myDict.ContainsKey(gate1) && myDict.ContainsKey(gate2))
                    {
                        var val1 = myDict[gate1];
                        var val2 = myDict[gate2];
                        var res = false;
                        if (op.Equals("AND"))
                        {
                            res = val1 && val2;
                        }
                        else if (op.Equals("OR"))
                        {
                            res = val1 || val2;
                        }
                        else if (op.Equals("XOR"))
                        {
                            if (val1 && !val2)
                            {
                                res = true;
                            }
                            else if (!val1 && val2)
                            {
                                res = true;
                            }
                        }
                        myDict.Add(resGate, res);
                        group2.RemoveAt(iter);
                    }
                    else
                    {
                        iter++;
                    }
                }
            }

            var zKeys = myDict.Where(x=> x.Key.StartsWith("z")).Select(x => x.Key).ToList();
            zKeys = zKeys.SortDescending();

            var str = string.Empty;

            foreach (var key in zKeys)
            {
                var val = myDict[key];
                if (val)
                                    {
                    str += "1";
                }
                else
                {
                    str += "0";
                }
            }

            var hexNumber = Convert.ToInt64(str, 2);
            return hexNumber;
        }

        private object Part2(bool isTest = false)
        {
            if (isTest)
                return 0;
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var myDict = new Dictionary<string, bool>();

            var group1 = input.LineGroupsSeperatedByWhiteSpace[0];
            var originalInstructions = input.LineGroupsSeperatedByWhiteSpace[1];

            /*
             * x12 AND y12 -> z12
                ggr XOR hnd -> kwb
             */
            var ind = originalInstructions.IndexOf("x12 AND y12 -> z12");
            originalInstructions[ind] = "x12 AND y12 -> kwb";
            ind = originalInstructions.IndexOf("ggr XOR hnd -> kwb");
            originalInstructions[ind] = "ggr XOR hnd -> z12";

            /*gkw AND cmc -> z16
                gkw XOR cmc -> qkf*/
            ind = originalInstructions.IndexOf("gkw AND cmc -> z16");
            originalInstructions[ind] = "gkw AND cmc -> qkf";
            ind = originalInstructions.IndexOf("gkw XOR cmc -> qkf");
            originalInstructions[ind] = "gkw XOR cmc -> z16";

            /*vhm OR wwd -> z24
                ttg XOR stj -> tgr*/
            ind = originalInstructions.IndexOf("vhm OR wwd -> z24");
            originalInstructions[ind] = "vhm OR wwd -> tgr";
            ind = originalInstructions.IndexOf("ttg XOR stj -> tgr");
            originalInstructions[ind] = "ttg XOR stj -> z24";

            /*x29 AND y29 -> jqn
                x29 XOR y29 -> cph*/
            ind = originalInstructions.IndexOf("x29 AND y29 -> jqn");
            originalInstructions[ind] = "x29 AND y29 -> cph";
            ind = originalInstructions.IndexOf("x29 XOR y29 -> cph");
            originalInstructions[ind] = "x29 XOR y29 -> jqn";

            var GraphDict = new Dictionary<string, List<string>>();

            foreach (var line in originalInstructions)
            {
                var tokens = StringParsing.GetRealTokens(line, _delimiterChars);
                var end = tokens[3];
                tokens.RemoveAt(3);
                tokens.RemoveAt(1);

                foreach (var item in tokens)
                {
                    if (GraphDict.ContainsKey(item))
                    {
                        var myList = GraphDict[item];
                        if (!myList.Contains(end))
                        {
                            myList.Add(end);
                        }
                    }
                    else
                    {
                        GraphDict.Add(item, new List<string>() { end });
                    }
                }
            }

            GraphVisualizerWrapper.StartVisualizerProgram(GraphDict);

            foreach (var line in group1)
            {
                var toks = line.Split(_delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                var num = long.Parse(toks[1]);
                var myBool = num == 1;

                myDict.Add(toks[0], myBool);
            }
            var xKeys = myDict.Where(x => x.Key.StartsWith("x")).Select(x => x.Key).ToList();
            var yKeys = myDict.Where(x => x.Key.StartsWith("y")).Select(x => x.Key).ToList();

            var xNumber = GetXNum(myDict);
            var yNumber = GetYNum(myDict);

            var answer = xNumber + yNumber;


            var testDict = myDict.ToDictionary(entry => entry.Key,
                                           entry => entry.Value);
            foreach (var key in xKeys)
            {
                testDict[key] = false;
            }
            foreach (var key in yKeys)
            {
                testDict[key] = false;
            }

            var myInst = originalInstructions.Clone();

            for (var i = 0; i < xKeys.Count; i++)
            {
                var xStr = "x" + GetNumberAsStringZeroPadded(i);
                var yStr = "y" + GetNumberAsStringZeroPadded(i);

                var andStr = xStr + " AND " + yStr;
                var orStr = xStr + " XOR " + yStr;
                var andStr2 = yStr + " AND " + xStr;
                var orStr2 = yStr + " XOR " + xStr;

                myInst.RemoveAll(x => x.StartsWith(andStr) ||
                x.StartsWith(orStr) ||
                x.StartsWith(andStr2) ||
                x.StartsWith(orStr2));
            }


            Console.WriteLine("Hello");
            for (var i = 0; i < xKeys.Count; i++)
            {
                var tempDict = testDict.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
                var numString = GetNumberAsStringZeroPadded(i);
                var tempStr = "y" + GetNumberAsStringZeroPadded(i);

                tempDict[tempStr] = true;


                var tempAnswer = GetXNum(tempDict) + GetYNum(tempDict);

                var results = GoTime(originalInstructions.Clone(), tempDict);

                var zNum = GetZNum(results);

                var blah = results.Where(x => x.Value).ToList();

                if (blah.Count() > 0)
                {
                    Console.WriteLine("Hello");
                }
                if (tempAnswer != zNum)
                {
                    Console.WriteLine("Bad");

                    var myToks = new List<string>() { "x" + numString, "y" + numString, "z" + numString };
                    var inst = originalInstructions.Where(j => j.Contains(myToks[0]) || j.Contains(myToks[1]) || j.Contains(myToks[2])).ToList();
                    var newToks = inst.Sum(x => x.Split(_delimiterChars, StringSplitOptions.RemoveEmptyEntries).Length);
                }

                var resStr = tempStr.Replace("y", "z");
            }

            while (originalInstructions.Count > 0)
            {
                var iter = 0;
                while (iter < originalInstructions.Count)
                {
                    var toks = originalInstructions[iter].Split(_delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                    var gate1 = toks[0];
                    var gate2 = toks[2];
                    var op = toks[1].ToUpper();
                    var resGate = toks[3];

                    if (myDict.ContainsKey(gate1) && myDict.ContainsKey(gate2))
                    {
                        var val1 = myDict[gate1];
                        var val2 = myDict[gate2];
                        var res = false;
                        if (op.Equals("AND"))
                        {
                            res = val1 && val2;
                        }
                        else if (op.Equals("OR"))
                        {
                            res = val1 || val2;
                        }
                        else if (op.Equals("XOR"))
                        {
                            if (val1 && !val2)
                            {
                                res = true;
                            }
                            else if (!val1 && val2)
                            {
                                res = true;
                            }
                        }
                        myDict.Add(resGate, res);
                        originalInstructions.RemoveAt(iter);
                    }
                    else
                    {
                        iter++;
                    }
                }
            }

            var zKeys = myDict.Where(x => x.Key.StartsWith("z")).Select(x => x.Key).ToList();
            zKeys = zKeys.SortDescending();

            var str = string.Empty;

            foreach (var key in zKeys)
            {
                var val = myDict[key];
                if (val)
                {
                    str += "1";
                }
                else
                {
                    str += "0";
                }
            }

            var listy = new List<string>() { "cph", "jqn", "kwb", "qkf", "tgr", "z12", "z16", "z24" };

            listy.Sort();

            var myStr = string.Empty;

            foreach (var key in listy)
            {
                myStr += key + ",";
            }

            myStr.Remove(myStr.Length - 1);
            return myStr;
        }

        private string GetNumberAsStringZeroPadded(long number)
        {
            var str = number.ToString();
            while (str.Length < 2)
            {
                str = "0" + str;
            }
            return str;
        }

        private long GetXNum(Dictionary<string, bool> myDict)
        {
            var xKeys = myDict.Where(x => x.Key.StartsWith("x")).Select(x => x.Key).ToList();
            xKeys = xKeys.SortDescending();

            var strX = string.Empty;

            foreach (var key in xKeys)
            {
                var val = myDict[key];
                if (val)
                {
                    strX += "1";
                }
                else
                {
                    strX += "0";
                }
            }
            var xNumber = Convert.ToInt64(strX, 2);

            return xNumber;
        }

        private long GetYNum(Dictionary<string, bool> myDict)
        {
            var xKeys = myDict.Where(x => x.Key.StartsWith("y")).Select(x => x.Key).ToList();
            xKeys = xKeys.SortDescending();

            var strX = string.Empty;

            foreach (var key in xKeys)
            {
                var val = myDict[key];
                if (val)
                {
                    strX += "1";
                }
                else
                {
                    strX += "0";
                }
            }
            var xNumber = Convert.ToInt64(strX, 2);

            return xNumber;
        }

        private long GetZNum(Dictionary<string, bool> myDict)
        {
            var xKeys = myDict.Where(x => x.Key.StartsWith("z")).Select(x => x.Key).ToList();
            xKeys = xKeys.SortDescending();

            var strX = string.Empty;

            foreach (var key in xKeys)
            {
                var val = myDict[key];
                if (val)
                {
                    strX += "1";
                }
                else
                {
                    strX += "0";
                }
            }
            var xNumber = Convert.ToInt64(strX, 2);

            return xNumber;
        }

        private Dictionary<string, bool> GoTime(List<string> instructions, Dictionary<string, bool> myDict)
        {
            while (instructions.Count > 0)
            {
                var iter = 0;
                while (iter < instructions.Count)
                {
                    var toks = instructions[iter].Split(_delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                    var gate1 = toks[0];
                    var gate2 = toks[2];
                    var op = toks[1].ToUpper();
                    var resGate = toks[3];

                    if (myDict.ContainsKey(gate1) && myDict.ContainsKey(gate2))
                    {
                        var val1 = myDict[gate1];
                        var val2 = myDict[gate2];
                        var res = false;
                        if (op.Equals("AND"))
                        {
                            res = val1 && val2;
                        }
                        else if (op.Equals("OR"))
                        {
                            res = val1 || val2;
                        }
                        else if (op.Equals("XOR"))
                        {
                            if (val1 && !val2)
                            {
                                res = true;
                            }
                            else if (!val1 && val2)
                            {
                                res = true;
                            }
                        }
                        myDict.Add(resGate, res);
                        instructions.RemoveAt(iter);
                    }
                    else
                    {
                        iter++;
                    }
                }
            }

            var zKeys = myDict.Where(x => x.Key.StartsWith("z")).ToDictionary();

            return zKeys;
        }
    }
}