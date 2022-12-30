using System;
using System.Reflection;
using System.Security.Permissions;
using System.Linq;

namespace CreateNewDay
{
    class CreateNewDay
    {
        static void Main(string[] args)
        {
            if (args.Count() != 2)
            {
                Console.WriteLine("./BoilerPlate.exe {year} {day}");
                Console.WriteLine(args[0]);
                Console.WriteLine(args[1]);
                return;
            }
            
            Year = args[0];
            Day = args[1].PadLeft(2, '0');

            var filePath = CreateDirectoriesAndFile(args);
            FillFile(filePath);
        }

        public static string Year { get; set; }

        public static string Day { get; set; }

        public static string CreateDirectoriesAndFile(string[] args)
        {
            var currentDir = Path.GetFileName(Environment.CurrentDirectory);
            var baseDir = string.Empty;

            if (string.Equals(currentDir, "net6.0"))
            {
                baseDir = "..\\..\\..\\..\\";
            }

            CreateTestInputFile(baseDir);
            var solutionsDir = baseDir + $"Solutions\\";
            if (!Directory.Exists(solutionsDir))
            {
                Directory.CreateDirectory(solutionsDir);
            }
            var yearDir = solutionsDir + $"aoc{Year}\\";
            if (!Directory.Exists(yearDir))
            {
                Directory.CreateDirectory(yearDir);
            }
            var daysDir = yearDir + $"days\\";
            if (!Directory.Exists(daysDir))
            {
                Directory.CreateDirectory(daysDir);
            }
            var targetFile = daysDir + $"Day{Day}.cs";
            return targetFile;
        }

        private static void CreateTestInputFile(string baseDir)
        {
            var yearDir = baseDir + $"TestInput\\{Year}\\";
            Directory.CreateDirectory(yearDir);
            var targetFile = yearDir + $"Day{Day}Test.txt";
            if (!File.Exists(targetFile))
            {
                using (File.Create(targetFile));
            }
            /*
            var inputDir = baseDir + $"TestInput\\";
            if (!Directory.Exists(inputDir))
            {
                Directory.CreateDirectory(inputDir);
            }
            var yearDir = inputDir + $"{Year}\\";
            if (!Directory.Exists(yearDir))
            {
                Directory.CreateDirectory(yearDir);
            }
            var targetFile = daysDir + $"Day{Day}Test.txt";
            if (!File.Exists(targetFile))
            {
                using (File.Create(targetFile));
            }*/
        }

        public static void FillFile(string filePath)
        {
            var part1 = $@"using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;
";
            var nameSpace = $"namespace aoc{Year}";
            var className = $"    public class Day{Day}: ISolver";
            var part2 = @"  {
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
			var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var nodes = ParseInput.ParseFileAsGraph(_filePath);
            var grid = ParseInput.ParseFileAsGrid(_filePath);
            var total = 1000000;
			var counter = 0;
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                
				foreach (var num in nums)
				{
				}

                for (var i = 0; i < 0; i++)
                {
                    for (var j = 0; j < 0; j++)
                    {
                        
                    }
                }
			}
            return 0;
        }
        
        private object Part2()
        {
            return 0;
        }
    }
}";
            using (StreamWriter writer = new StreamWriter(filePath))  
            {  
                writer.WriteLine(part1);
                writer.WriteLine(nameSpace);
                writer.WriteLine("{");
                writer.WriteLine(className);
                writer.WriteLine(part2);
            } 
        }
    }
}