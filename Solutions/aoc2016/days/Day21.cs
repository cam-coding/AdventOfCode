using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day21: ISolver
  {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        // private char[] password = { 'a', 'b', 'c', 'd', 'e' };
        private char[] password = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            string[] filelns = ParseInput.GetLinesFromFile(_filePath).ToArray();

            char[] buffer = new char[input.Length];
            Array.Copy(input.ToCharArray(), buffer, input.Length);
            for (int i = 0; i < filelns.Length; i++)
            {
                string instruction = filelns[i];
                scramble(ref buffer, instruction);
                if (true)
                {
                    Console.Write(buffer);
                    Console.WriteLine("->{0}:\t{1}", new string(buffer), instruction);
                }
            }
            return new string(buffer);
            }
        
        private object Part2()
        {
            return 0;
        }

        const string input = "abcdefgh";
        const string input2 = "fbgdceah";
        static Regex reg = new Regex(@"^swap (position|letter) (\w|\d*) with \1 (\w|\d*)");
        static Regex regRotate = new Regex(@"^rotate (left|right|based on position of letter) (\w|\d*)");
        static Regex regRevOrMove = new Regex(@"^(reverse|move) positions? (\d*) (through|to position) (\d*)");

        private static void scramble(ref char[] buffer, string instruction)
        {
            Match match = reg.Match(instruction);
            if (match.Success)
            {
                if (match.Groups[1].Value == "position")
                {
                    int from = int.Parse(match.Groups[2].Value);
                    int to = int.Parse(match.Groups[3].Value);
                    char temp = buffer[from]; buffer[from] = buffer[to]; buffer[to] = temp;
                }
                else
                {
                    char cfrom = match.Groups[2].Value[0];
                    char cto = match.Groups[3].Value[0];
                    for (int t = 0; t < buffer.Length; t++)
                    {
                        if (buffer[t] == cfrom) buffer[t] = cto;
                        else if (buffer[t] == cto) buffer[t] = cfrom;
                    }
                }
                return;
            }
            match = regRotate.Match(instruction);
            if (match.Success)
            {
                int X = 0; string pattern = match.Groups[1].Value;
                if (pattern.StartsWith("based"))
                {
                    X = Array.IndexOf(buffer, match.Groups[2].Value[0]);
                    X += X > 3 ? 2 : 1;
                    pattern = "right";
                }
                else
                    X = int.Parse(match.Groups[2].Value);
                X = X % buffer.Length;

                if (X == 0)
                {
                    Console.WriteLine("Nothing! X is 0.");
                    return;
                }


                if (pattern == "left")
                {
                    char[] newbuffer = new char[buffer.Length];
                    Array.Copy(buffer, X, newbuffer, 0, buffer.Length - X);
                    Array.Copy(buffer, 0, newbuffer, buffer.Length - X, X);
                    buffer = newbuffer;
                }
                else
                {
                    char[] newbuffer = new char[buffer.Length];
                    Array.Copy(buffer, 0, newbuffer, X, buffer.Length - X);
                    Array.Copy(buffer, buffer.Length - X, newbuffer, 0, X);
                    buffer = newbuffer;
                }
                return;
            }

            match = regRevOrMove.Match(instruction);
            if (match.Success)
            {
                string pattern = match.Groups[1].Value;
                int from = int.Parse(match.Groups[2].Value);
                int to = int.Parse(match.Groups[4].Value);

                if (pattern == "reverse")
                {
                    int l = to - from;
                    for (int t = 0; t <= l / 2; t++)
                    {
                        //SWAP t <-> 2L-t:
                        char temp = buffer[from + t]; buffer[from + t] = buffer[to - t]; buffer[to - t] = temp;
                    }
                }
                else
                {
                    char fromx = buffer[from];
                    if (from < to)
                        Array.Copy(buffer, from + 1, buffer, from, to - from);
                    else
                        Array.Copy(buffer, to, buffer, to + 1, from - to);
                    buffer[to] = fromx;
                }

            }
        }

        private static void unscramble(ref char[] buffer, string instruction)
        {
            Match match = reg.Match(instruction);
            if (match.Success)
            {
                if (match.Groups[1].Value == "position")
                {
                    //CHANGED from part 1:
                    int from = int.Parse(match.Groups[3].Value);
                    int to = int.Parse(match.Groups[2].Value);
                    char temp = buffer[from]; buffer[from] = buffer[to]; buffer[to] = temp;
                }
                else
                {
                    char cfrom = match.Groups[2].Value[0];
                    char cto = match.Groups[3].Value[0];
                    for (int t = 0; t < buffer.Length; t++)
                    {
                        if (buffer[t] == cfrom) buffer[t] = cto;
                        else if (buffer[t] == cto) buffer[t] = cfrom;
                    }
                }

                return;
            }
            match = regRotate.Match(instruction);
            if (match.Success)
            {
                int X = 0; string pattern = match.Groups[1].Value;
                if (pattern.StartsWith("based"))
                {
                    X = Array.IndexOf(buffer, match.Groups[2].Value[0]);
                    //CHANGED from part 1:
                    if (X % 2 == 0)
                        X = ((X + buffer.Length) / 2 + 1);
                    else
                        X = (X / 2) + 1;

                    pattern = "right";
                }
                else
                    X = int.Parse(match.Groups[2].Value);
                X = X % buffer.Length;

                if (X == 0)
                    return;

                //CHANGED from part 1:
                if (pattern == "right")
                {
                    char[] newbuffer = new char[buffer.Length];
                    Array.Copy(buffer, X, newbuffer, 0, buffer.Length - X);
                    Array.Copy(buffer, 0, newbuffer, buffer.Length - X, X);
                    buffer = newbuffer;
                }
                else
                {
                    char[] newbuffer = new char[buffer.Length];
                    Array.Copy(buffer, 0, newbuffer, X, buffer.Length - X);
                    Array.Copy(buffer, buffer.Length - X, newbuffer, 0, X);
                    buffer = newbuffer;
                }
                return;
            }

            match = regRevOrMove.Match(instruction);
            if (match.Success)
            {
                string pattern = match.Groups[1].Value;
                int from = int.Parse(match.Groups[2].Value);
                int to = int.Parse(match.Groups[4].Value);

                if (pattern == "reverse")
                {
                    int l = to - from;
                    for (int t = 0; t <= l / 2; t++)
                    {
                        char temp = buffer[from + t]; buffer[from + t] = buffer[to - t]; buffer[to - t] = temp;
                    }
                }
                else
                {
                    //CHANGED from part 1:
                    int temp = from; from = to; to = temp;
                    char fromx = buffer[from];

                    if (from < to)
                        Array.Copy(buffer, from + 1, buffer, from, to - from);
                    else
                        Array.Copy(buffer, to, buffer, to + 1, from - to);
                    buffer[to] = fromx;
                }

            }
        }
    }
}
