using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2025
{
    public class Day11 : ISolver
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
            return 0;
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;

            var adjList = new Dictionary<string, List<string>>();
            if (isTest)
            {
                var str = @"aaa: you hhh
                you: bbb ccc
                bbb: ddd eee
                ccc: ddd eee fff
                ddd: ggg
                eee: out
                fff: out
                ggg: out
                hhh: ccc fff iii
                iii: out";

                lines = str.Split(new char[] { '\r', '\n' }).ToList();
            }

            foreach (var line in lines)
            {
                if (line.Equals(string.Empty))
                    continue;
                var tokens = line.GetRealTokens();
                adjList.Add(tokens[0], tokens.SubList(1));
            }

            // return BFS_Search(adjList, "you", "out");
        }

        private object Part2(bool isTest = false)
        {
            if (isTest)
            {
                return 0;
            }
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var adjList = new Dictionary<string, List<string>>();
            var reverseAdjList = new Dictionary<string, HashSet<string>>();

            for (var i = 0; i < lines.Count; i++)
            {
            }

            foreach (var line in lines)
            {
                var tokens = line.GetRealTokens();
                var subList = tokens.SubList(1);
                foreach (var token in subList)
                {
                    if (reverseAdjList.ContainsKey(token))
                    {
                        reverseAdjList[token].Add(tokens[0]);
                    }
                    else
                    {
                        reverseAdjList.Add(token, new HashSet<string>() { tokens[0] });
                    }
                }
                adjList.Add(tokens[0], subList);
            }

            /* this strat should work but the fft-dac && dac-fft
             * search space is huge */
            var possibles_fft = GetPossibles(reverseAdjList, "fft");
            var possible_dac = GetPossibles(reverseAdjList, "dac");

            var value1 = BFS_Search(adjList, possibles_fft, "svr", "fft") *
                BFS_Search(adjList, possible_dac, "fft", "dac") *
                BFS_Search(adjList, null, "dac", "out");

            var value2 = BFS_Search(adjList, possible_dac, "svr", "dac") *
                BFS_Search(adjList, possibles_fft, "dac", "fft") *
                BFS_Search(adjList, null, "fft", "out");

            return value1 * value2;

            Queue<List<string>> q = new Queue<List<string>>();
            var fullPathHistory = new HashSet<string>();

            var circle1 = GetAllPathsCount(
                adjList,
                new List<string>() { "svr" },
                new HashSet<string>() { "xvt", "caw", "yad", "jub", "aek" });

            var circle2 = GetAllPathsMiddleCount(
                adjList,
                circle1,
                new List<string>() { "xvt", "caw", "yad", "jub", "aek" },
                new HashSet<string>() { "nxq", "nhk", "bmm", "xxt" },
                "fft");

            var circle3 = GetAllPathsMiddleCount(
                adjList,
                circle2,
                new List<string>() { "nxq", "nhk", "bmm", "xxt" },
                new HashSet<string>() { "arh", "zgf", "sxr", "tku", "els" });

            var circle4 = GetAllPathsMiddleCount(
                adjList,
                circle3,
                new List<string>() { "arh", "zgf", "sxr", "tku", "els" },
                new HashSet<string>() { "xdu", "fbt", "tak", "xvf" });

            var circle5 = GetAllPathsMiddleCount(
                adjList,
                circle4,
                new List<string>() { "xdu", "fbt", "tak", "xvf" },
                new HashSet<string>() { "you", "sus", "tgd" },
                "dac");

            var circle6 = GetAllPathsMiddleCount(
                adjList,
                circle5,
                new List<string>() { "you", "sus", "tgd" },
                new HashSet<string>() { "out" });

            return circle6.Values.Sum();
            /*
            long circle1 = GetAllPathsTrueCount(
                adjList,
                new List<string>() { "svr" },
                new HashSet<string>() { "xvt", "caw", "yad", "jub", "aek" });

            long circle2 = GetAllPathsTrueCount(
                adjList,
                new List<string>() { "xvt", "caw", "yad", "jub", "aek" },
                new HashSet<string>() { "nxq", "nhk", "bmm", "xxt" },
                "fft");

            long circle3 = GetAllPathsTrueCount(
                adjList,
                new List<string>() { "nxq", "nhk", "bmm", "xxt" },
                new HashSet<string>() { "arh", "zgf", "sxr", "tku", "els" });

            long circle4 = GetAllPathsTrueCount(
                adjList,
                new List<string>() { "arh", "zgf", "sxr", "tku", "els" },
                new HashSet<string>() { "xdu", "fbt", "tak", "xvf" });

            long circle5 = GetAllPathsTrueCount(
                adjList,
                new List<string>() { "xdu", "fbt", "tak", "xvf" },
                new HashSet<string>() { "you", "sus", "tgd" },
                "dac");

            long circle6 = GetAllPathsTrueCount(
                adjList,
                new List<string>() { "you", "sus", "tgd" },
                new HashSet<string>() { "out" });

            return circle1 *
                circle2 *
                circle3 *
                circle4 *
                circle5 *
                circle6;
            */

            // (0,0) can be anything, just needs to be your starting point.
            var head = new GridLocation<int>(0, 0);
            q.Enqueue(new List<string>() { "svr" });
            var endsHash = new HashSet<string>();
            while (q.Count > 0)
            {
                var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var currentLocation = fullPath.Last(); // this is just the most recent point
                var stringy = ListExtensions.Stringify(fullPath);

                if (
                    fullPath == null ||
                    fullPathHistory.Contains(stringy)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                    continue;

                var isValid = (fullPath.Contains("fft") && fullPath.Contains("dac")) ||
                    (fullPath.Contains("fft") && possible_dac.Contains(currentLocation)) ||
                    (fullPath.Contains("dac") && possibles_fft.Contains(currentLocation)) ||
                    (possibles_fft.Contains(currentLocation) && possible_dac.Contains(currentLocation));
                if (!isValid)
                    continue;

                // make sure to add current state to history
                fullPathHistory.Add(stringy);

                // This is where you check your end goal
                if (currentLocation.Equals("out")) // or check you've reached a certain value
                {
                    endsHash.Add(stringy);
                    continue;
                }

                //Get the next nodes/grids/etc to visit next
                foreach (var neighbour in adjList[currentLocation])
                {
                    var val = neighbour;

                    /*
                    // this is very important. Always make sure the next value is valid in your search
                    // this might be checking total weight of path, or if the next value is accessible, etc.
                    if (
                        val != currentValue + 1 || // for example make sure the next value is valid
                        fullPath.Sum(x => exampleGrid.Get(x)) + val < 100 // or make sure the total weight hasn't gone overboard
                        )
                    {
                        continue;
                    }*/
                    var temp = fullPath.Clone(); // very important, do not miss this clone
                    temp.Add(neighbour);
                    q.Enqueue(temp);
                }
            }
            return endsHash.Count(x => x.Contains("fft") && x.Contains("dac"));
        }

        private int BFS_Search(
            Dictionary<string, List<string>> adjList,
            HashSet<string> reverseList,
            string start,
            string end)
        {
            Queue<List<string>> q = new Queue<List<string>>();
            var fullPathHistory = new HashSet<string>();
            q.Enqueue(new List<string>() { start });
            var endsHash = new HashSet<string>();
            while (q.Count > 0)
            {
                var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var currentLocation = fullPath.Last(); // this is just the most recent point

                if (reverseList != null && !reverseList.Contains(currentLocation))
                    continue;

                var stringy = ListExtensions.Stringify(fullPath);

                if (
                    fullPath == null ||
                    fullPathHistory.Contains(stringy)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                    continue;

                // make sure to add current state to history
                fullPathHistory.Add(stringy);

                // This is where you check your end goal
                if (currentLocation.Equals(end)) // or check you've reached a certain value
                {
                    endsHash.Add(stringy);
                    continue;
                }

                // if out isn't the end, we still want to exit when we run into it
                if (currentLocation.Equals("out"))
                {
                    continue;
                }

                //Get the next nodes/grids/etc to visit next
                foreach (var neighbour in adjList[currentLocation])
                {
                    var val = neighbour;

                    /*
                    // this is very important. Always make sure the next value is valid in your search
                    // this might be checking total weight of path, or if the next value is accessible, etc.
                    if (
                        val != currentValue + 1 || // for example make sure the next value is valid
                        fullPath.Sum(x => exampleGrid.Get(x)) + val < 100 // or make sure the total weight hasn't gone overboard
                        )
                    {
                        continue;
                    }*/
                    var temp = fullPath.Clone(); // very important, do not miss this clone
                    temp.Add(neighbour);
                    q.Enqueue(temp);
                }
            }
            return endsHash.Count;
        }

        private Dictionary<string, long> GetAllPathsMiddleCount(
            Dictionary<string, List<string>> adjList,
            Dictionary<string, long> startingCounts,
            List<string> starts,
            HashSet<string> goals,
            string requirement = "")
        {
            Queue<List<string>> q = new Queue<List<string>>();
            var fullPathHistory = new HashSet<string>();

            var myDict = new Dictionary<string, long>();
            foreach (var g in goals)
            {
                myDict.Add(g, 0);
            }
            foreach (var s in starts)
            {
                q.Enqueue(new List<string>() { s });
            }

            // (0,0) can be anything, just needs to be your starting point.
            var head = new GridLocation<int>(0, 0);
            var endsHash = new HashSet<string>();
            while (q.Count > 0)
            {
                var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var currentLocation = fullPath.Last(); // this is just the most recent point
                var stringy = ListExtensions.Stringify(fullPath);

                if (
                    fullPath == null ||
                    fullPathHistory.Contains(stringy)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                    continue;

                // make sure to add current state to history
                fullPathHistory.Add(stringy);

                // This is where you check your end goal
                if (goals.Contains(currentLocation)) // or check you've reached a certain value
                {
                    if (requirement.Equals(string.Empty) || stringy.Contains(requirement))
                    {
                        endsHash.Add(stringy);
                        foreach (var g in goals)
                        {
                            if (stringy.Contains(g))
                            {
                                var firsty = starts.First(x => stringy.Contains(x));
                                var myCount = startingCounts[firsty];
                                myDict[g] += startingCounts[firsty];
                            }
                        }
                    }
                    continue;
                }

                //Get the next nodes/grids/etc to visit next
                foreach (var neighbour in adjList[currentLocation])
                {
                    var val = neighbour;

                    /*
                    // this is very important. Always make sure the next value is valid in your search
                    // this might be checking total weight of path, or if the next value is accessible, etc.
                    if (
                        val != currentValue + 1 || // for example make sure the next value is valid
                        fullPath.Sum(x => exampleGrid.Get(x)) + val < 100 // or make sure the total weight hasn't gone overboard
                        )
                    {
                        continue;
                    }*/
                    var temp = fullPath.Clone(); // very important, do not miss this clone
                    temp.Add(neighbour);
                    q.Enqueue(temp);
                }
            }
            return myDict;
        }

        private Dictionary<string, long> GetAllPathsCount(
            Dictionary<string, List<string>> adjList,
            List<string> starts,
            HashSet<string> goals,
            string requirement = "")
        {
            Queue<List<string>> q = new Queue<List<string>>();
            var fullPathHistory = new HashSet<string>();

            var myDict = new Dictionary<string, long>();
            foreach (var g in goals)
            {
                myDict.Add(g, 0);
            }
            foreach (var s in starts)
            {
                q.Enqueue(new List<string>() { s });
            }

            // (0,0) can be anything, just needs to be your starting point.
            var head = new GridLocation<int>(0, 0);
            var endsHash = new HashSet<string>();
            while (q.Count > 0)
            {
                var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var currentLocation = fullPath.Last(); // this is just the most recent point
                var stringy = ListExtensions.Stringify(fullPath);

                if (
                    fullPath == null ||
                    fullPathHistory.Contains(stringy)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                    continue;

                // make sure to add current state to history
                fullPathHistory.Add(stringy);

                // This is where you check your end goal
                if (goals.Contains(currentLocation)) // or check you've reached a certain value
                {
                    if (requirement.Equals(string.Empty) || stringy.Contains(requirement))
                    {
                        endsHash.Add(stringy);
                        foreach (var g in goals)
                        {
                            if (stringy.Contains(g))
                            {
                                myDict[g]++;
                            }
                        }
                    }
                    continue;
                }

                //Get the next nodes/grids/etc to visit next
                foreach (var neighbour in adjList[currentLocation])
                {
                    var val = neighbour;

                    /*
                    // this is very important. Always make sure the next value is valid in your search
                    // this might be checking total weight of path, or if the next value is accessible, etc.
                    if (
                        val != currentValue + 1 || // for example make sure the next value is valid
                        fullPath.Sum(x => exampleGrid.Get(x)) + val < 100 // or make sure the total weight hasn't gone overboard
                        )
                    {
                        continue;
                    }*/
                    var temp = fullPath.Clone(); // very important, do not miss this clone
                    temp.Add(neighbour);
                    q.Enqueue(temp);
                }
            }
            return myDict;
        }

        private int GetAllPathsTrueCount(
            Dictionary<string, List<string>> adjList,
            List<string> starts,
            HashSet<string> goals,
            string requirement = "")
        {
            Queue<List<string>> q = new Queue<List<string>>();
            var fullPathHistory = new HashSet<string>();

            var myDict = new Dictionary<string, int>();
            foreach (var g in goals)
            {
                myDict.Add(g, 0);
            }

            // (0,0) can be anything, just needs to be your starting point.
            var head = new GridLocation<int>(0, 0);
            foreach (var s in starts)
            {
                q.Enqueue(new List<string>() { s });
            }
            var endsHash = new HashSet<string>();
            while (q.Count > 0)
            {
                var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var currentLocation = fullPath.Last(); // this is just the most recent point
                var stringy = ListExtensions.Stringify(fullPath);

                if (
                    fullPath == null ||
                    fullPathHistory.Contains(stringy)) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                    continue;

                // make sure to add current state to history
                fullPathHistory.Add(stringy);

                // This is where you check your end goal
                if (goals.Contains(currentLocation)) // or check you've reached a certain value
                {
                    if (requirement.Equals(string.Empty) || stringy.Contains(requirement))
                    {
                        endsHash.Add(stringy);
                        foreach (var g in goals)
                        {
                            if (stringy.Contains(g))
                            {
                                myDict[g]++;
                            }
                        }
                    }
                    continue;
                }

                //Get the next nodes/grids/etc to visit next
                foreach (var neighbour in adjList[currentLocation])
                {
                    var val = neighbour;

                    /*
                    // this is very important. Always make sure the next value is valid in your search
                    // this might be checking total weight of path, or if the next value is accessible, etc.
                    if (
                        val != currentValue + 1 || // for example make sure the next value is valid
                        fullPath.Sum(x => exampleGrid.Get(x)) + val < 100 // or make sure the total weight hasn't gone overboard
                        )
                    {
                        continue;
                    }*/
                    var temp = fullPath.Clone(); // very important, do not miss this clone
                    temp.Add(neighbour);
                    q.Enqueue(temp);
                }
            }
            return endsHash.Count;
        }

        private HashSet<string> GetPossibles(Dictionary<string, HashSet<string>> reverseAdjList, string starting)
        {
            var result = new HashSet<string>() { "svr" };
            Queue<string> q = new Queue<string>();
            q.Enqueue(starting);

            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if (result.Contains(current))
                {
                    continue;
                }

                result.Add(current);

                foreach (var linked in reverseAdjList[current])
                {
                    q.Enqueue(linked);
                }
            }

            return result;
        }
    }
}