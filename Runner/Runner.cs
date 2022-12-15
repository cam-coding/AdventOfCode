using System;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Reflection;
using aoc2022;
using AdventLibrary;

namespace Runner
{
    class Runner
    {
        static string aoc = new string("aoc");

        static void Main(string[] args)
        {
            var helper = new RunnerHelper();
            helper.GetDateAndYear(args, out string day, out string year);
            var solver = helper.GetSolver(day, year);
            var filePath = helper.GetInputPath(day, year);
            var testFilePath = helper.GetTestInputPath(day, year);
            if (!testFilePath.Equals(String.Empty) && new FileInfo(testFilePath).Length != 0)
            {
                Console.WriteLine("<<<<<TEST INPUT START>>>>>");
                solver.Solve(testFilePath).Output();
                Console.WriteLine("<<<<<TEST INPUT END>>>>>");
            }
            solver.Solve(filePath).Output();
        }
    }
}
