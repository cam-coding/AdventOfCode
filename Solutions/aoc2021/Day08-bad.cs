using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day08Bad: ISolver
    {
        private List<string> liney;
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine("Part 1: " + Part1.ToString());
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
            var counter = 0;
            var highest = Int64.MaxValue;
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            // var lines = AdventLibrary.ParseInput.GetNumbersFromFile(_filePath);
			
			foreach (var line in lines)
			{
                var parts = line.Split('|');
                foreach (var item in parts[1].Split(' '))
                {
                    if (item.Length == 2 || item.Length == 3 || item.Length == 4 || item.Length == 7)
                    {
                        counter++;
                    }
                }
				var things = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
			}
            return counter;
        }
        
        private object Part2()
        {
            var counter = 0;
            var highest = Int64.MaxValue;
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            // var lines = AdventLibrary.ParseInput.GetNumbersFromFile(_filePath);
			
			foreach (var line in lines)
			{
                var codes = new string[10];
                var badCodes = new List<string>();
                var parts = line.Split('|');
                foreach (var item in parts[1].Split(' '))
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        badCodes.Add(item);
                    }

                    if (item.Length == 2)
                    {
                        codes[1] = item;
                    }
                    else if (item.Length == 3)
                    {
                        codes[7] = item;
                    }
                    else if (item.Length == 4)
                    {
                        codes[4] = item;
                    }
                    else if (item.Length == 7)
                    {
                        codes[8] = item;
                    }
                }

                var total = 0;
                string eightFourDiff = null;

                if (codes[8] != null && codes[4] != null)
                {
                    eightFourDiff = new string(codes[8].Except(codes[4]).ToArray());
                }

                var solvingThis = parts[0].Split(' ').ToList();
                liney = new List<string> { null, null, null, null, null, null, null};

                while (!SolveLine(solvingThis, codes.ToList(), out total))
                {
                    total = 0;
                    Get0(solvingThis, codes.ToList(), out codes[0]);
                    Get2(solvingThis, codes.ToList(), out codes[2]);
                    Get3(solvingThis, codes.ToList(), out codes[3]);
                    Get5(solvingThis, codes.ToList(), out codes[5]);
                    Get6(solvingThis, codes.ToList(), out codes[6]);
                    Get9(solvingThis, codes.ToList(), out codes[9]);
                    Get00(codes.ToList());
                    Get01(codes.ToList());
                    Get02(codes.ToList());
                    //Get03(solvingThis, codes.ToList());
                    Get04(codes.ToList());
                    //Get05(solvingThis, codes.ToList());
                    Get06(codes.ToList());
                    /*
                    codes[3] = badCodes.Where(x => x.Length == 5 && (MyContains(x, codes[1]) || MyContains(x, codes[7]))).FirstOrDefault();
                    codes[9] = badCodes.Where(x => x.Length == 6 && 
                    (MyContains(x, codes[1]) || MyContains(x, codes[4]))).FirstOrDefault();
                    codes[0] = badCodes.Where(x => x.Length == 6 && 
                    (MyContains(x, codes[7]) && MyDoesntContains(x, codes[4]) ||
                    MyDoesntContains(x, codes[4]))).FirstOrDefault();
                    codes[6] = badCodes.Where(x => x.Length == 6 && MyContains(x, eightFourDiff) && MyDoesntContains(x, codes[1])).FirstOrDefault(); */
                }

                codes[3] = badCodes.Where(x => x.Length == 5 && x.Contains(codes[1])).FirstOrDefault();
                codes[3] = badCodes.Where(x => x.Length == 5 && x.Contains(codes[1])).FirstOrDefault();
			}
            return counter;
        }

        private bool SolveLine(List<string> codes, List<string> answers, out int total)
        {
            total = 0;
            foreach(var code in codes)
            {
                if (!answers.Contains(code))
                {
                    return false;
                }

                total = total + answers.IndexOf(code);
            }
            return true;
        }

        private bool MyContains(string str1, string str2)
        {
            if (string.IsNullOrWhiteSpace(str2))
            {
                return false;
            }
            return str1.Contains(str2);
        }

        private bool MyDoesntContains(string str1, string str2)
        {
            if (string.IsNullOrWhiteSpace(str2))
            {
                return true;
            }
            return !str1.Contains(str2);
        }

        private void Get00(List<string> codes)
        {
            if (codes[7] != null && codes[1] != null)
            {
                liney[0] = new string(codes[7].Except(codes[1]).ToArray());
            }
            if (codes[9] != null && codes[4] != null && liney[3] != null)
            {
                liney[0] = new string(codes[9].Except(codes[4]).Except(liney[3]).ToArray());
            }
        }

        private void Get01(List<string> codes)
        {
            if (codes[8] != null && codes[6] != null)
            {
                liney[1] = new string(codes[8].Except(codes[6]).ToArray());
            }
            if (codes[9] != null && codes[5] != null)
            {
                liney[1] = new string(codes[9].Except(codes[5]).ToArray());
            }
            if (codes[1] != null && codes[6] != null)
            {
                liney[1] = new string(codes[1].Except(codes[6]).ToArray());
            }
        }

        private void Get02(List<string> codes)
        {
            if (codes[2] != null && codes[1] != null)
            {
                liney[2] = new string(codes[1].Except(codes[2]).ToArray());
            }
        }

        private void Get04(List<string> codes)
        {
            if (codes[8] != null && codes[9] != null)
            {
                liney[4] = new string(codes[8].Except(codes[9]).ToArray());
            }
            if (codes[6] != null && codes[5] != null)
            {
                liney[4] = new string(codes[6].Except(codes[5]).ToArray());
            }
            if (codes[8] != null && codes[5] != null && codes[4] != null)
            {
                liney[4] = new string((codes[8].Except(codes[5])).Except(codes[4]).ToArray());
            }
        }

        private void Get06(List<string> codes)
        {
            if (codes[8] != null && codes[0] != null)
            {
                liney[6] = new string(codes[8].Except(codes[0]).ToArray());
            }
        }

        private void Get0(List<string> solvingThis, List<string> codes, out string solved)
        {
            solved = solvingThis.Where(x => x != null && x.Length == 6 &&
            (codes[1] == null || x.Contains(codes[1])) &&
            (codes[2] == null || !x.Contains(codes[2])) &&
            (codes[3] == null || !x.Contains(codes[3])) &&
            (codes[4] == null || !x.Contains(codes[4])) &&
            (codes[5] == null || !x.Contains(codes[5])) &&
            (codes[6] == null || !x.Contains(codes[6])) &&
            (codes[7] == null || x.Contains(codes[7])) &&
            (codes[8] == null || !x.Contains(codes[8])) &&
            (codes[9] == null || !x.Contains(codes[9]))).FirstOrDefault();
        }
        /*

        private void Get1(List<string> codes, List<string> answers, out string solved)
        {
            solved = codes.Where(x => x.Length == 2 &&
            (codes[0] == null || !x.Contains(codes[0])) &&
            (codes[2] == null || !x.Contains(codes[2])) &&
            (codes[3] == null || !x.Contains(codes[3])) &&
            (codes[4] == null || !x.Contains(codes[4])) &&
            (codes[5] == null || !x.Contains(codes[5])) &&
            (codes[6] == null || !x.Contains(codes[6])) &&
            (codes[7] == null || !x.Contains(codes[7])) &&
            (codes[8] == null || !x.Contains(codes[8])) &&
            (codes[9] == null || !x.Contains(codes[9]))).FirstOrDefault();
        } */

        private void Get2(List<string> solvingThis, List<string> codes, out string solved)
        {
            solved = solvingThis.Where(x => x != null && x.Length == 5 &&
            (codes[0] == null || !x.Contains(codes[0])) &&
            (codes[1] == null || x.Contains(codes[1])) &&
            (codes[3] == null || !x.Contains(codes[3])) &&
            (codes[4] == null || !x.Contains(codes[4])) &&
            (codes[5] == null || !x.Contains(codes[5])) &&
            (codes[6] == null || !x.Contains(codes[6])) &&
            (codes[7] == null || !x.Contains(codes[7])) &&
            (codes[8] == null || !x.Contains(codes[8])) &&
            (codes[9] == null || !x.Contains(codes[9]))).FirstOrDefault();
        }

        private void Get3(List<string> solvingThis, List<string> codes, out string solved)
        {
            solved = solvingThis.Where(x => x != null && x.Length == 5 &&
            (codes[0] == null || !x.Contains(codes[0])) &&
            (codes[1] == null || x.Contains(codes[1])) &&
            (codes[2] == null || !x.Contains(codes[2])) &&
            (codes[4] == null || !x.Contains(codes[4])) &&
            (codes[5] == null || !x.Contains(codes[5])) &&
            (codes[6] == null || !x.Contains(codes[6])) &&
            (codes[7] == null || x.Contains(codes[7])) &&
            (codes[8] == null || !x.Contains(codes[8])) &&
            (codes[9] == null || !x.Contains(codes[9]))).FirstOrDefault();
        }

        private void Get5(List<string> solvingThis, List<string> codes, out string solved)
        {
            if (codes[6] != null && liney[4] != null)
            {
                solved = new string(codes[6].Except(liney[4]).ToArray());
                return;
            }
            solved = null;
            /*
            solved = solvingThis.Where(x => x != null && x.Length == 5 &&
            (codes[0] == null || !x.Contains(codes[0])) &&
            (codes[1] == null || !x.Contains(codes[1])) &&
            (codes[2] == null || !x.Contains(codes[2])) &&
            (codes[3] == null || !x.Contains(codes[3])) &&
            (codes[4] == null || !x.Contains(codes[4])) &&
            (codes[6] == null || !x.Contains(codes[6])) &&
            (codes[7] == null || !x.Contains(codes[7])) &&
            (codes[8] == null || !x.Contains(codes[8])) &&
            (codes[9] == null || !x.Contains(codes[9]))).FirstOrDefault(); */
        }

        private void Get6(List<string> solvingThis, List<string> codes, out string solved)
        {
            if (codes[5] != null && liney[4] != null)
            {
                solved = codes[5] + liney[4];
                return;
            }
            solved = solvingThis.Where(x => x != null && x.Length == 6 &&
            (codes[0] == null || !x.Contains(codes[0])) &&
            (codes[1] == null || !x.Contains(codes[1])) &&
            (codes[2] == null || !x.Contains(codes[2])) &&
            (codes[3] == null || !x.Contains(codes[3])) &&
            (codes[4] == null || !x.Contains(codes[4])) &&
            (codes[5] == null || x.Contains(codes[5])) &&
            (codes[7] == null || !x.Contains(codes[7])) &&
            (codes[8] == null || !x.Contains(codes[8])) &&
            (codes[9] == null || !x.Contains(codes[9]))).FirstOrDefault();
            if (solved != null)
            {
                return;
            }
        }

        private void Get9(List<string> solvingThis, List<string> codes, out string solved)
        {
            if (codes[8] != null && liney[4] != null)
            {
                solved = new string(codes[8].Except(liney[4]).ToArray());
                return;
            }
            if (codes[8] != null && codes[6] != null)
            {
                solved = solvingThis.Where(x => x != null && x.Length == 6 && !x.Equals(codes[6])).FirstOrDefault();
                return;
            }
            solved = solvingThis.Where(x => x != null && x.Length == 6 &&
            (codes[0] == null || !x.Contains(codes[0])) &&
            (codes[1] == null || x.Contains(codes[1])) &&
            (codes[2] == null || !x.Contains(codes[2])) &&
            (codes[3] == null || x.Contains(codes[3])) &&
            (codes[4] == null || x.Contains(codes[4])) &&
            (codes[5] == null || x.Contains(codes[5])) &&
            (codes[6] == null || !x.Contains(codes[6])) &&
            (codes[7] == null || x.Contains(codes[7])) &&
            (codes[8] == null || !x.Contains(codes[8]))).FirstOrDefault();
        }
    }
}