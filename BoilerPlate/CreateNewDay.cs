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
            FillFile2(filePath);
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
        }

        public static void FillFile2(string destFile)
        {
            string text = File.ReadAllText("..\\..\\..\\BoilerPlate.txt");
            text = text.Replace("{YEAR}", Year);
            text = text.Replace("{DAY}", Day);
            File.WriteAllText(destFile, text);
        }
    }
}