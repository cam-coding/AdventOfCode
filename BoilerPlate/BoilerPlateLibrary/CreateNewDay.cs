namespace BoilerPlateLibrary
{
    public class CreateNewDay
    {
        public CreateNewDay(string day, string year)
        {
            if (day == null || year == null)
            {
                throw new ArgumentNullException();
            }
            Day = day;
            Year = year;
        }

        public static string Day { get; set; }

        public static string Year { get; set; }

        public void SetupFiles()
        {
            var filePath = CreateDirectoriesAndFile();
            FillFileWithBoilerPlate(filePath);
        }

        public string CreateDirectoriesAndFile()
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

        private void CreateTestInputFile(string baseDir)
        {
            var yearDir = baseDir + $"TestInput\\{Year}\\";
            Directory.CreateDirectory(yearDir);
            var targetFile = yearDir + $"Day{Day}Test.txt";
            if (!File.Exists(targetFile))
            {
                using (File.Create(targetFile));
            }
        }

        public void FillFileWithBoilerPlate(string destFile)
        {
            string text = File.ReadAllText("BoilerPlate.txt");
            text = text.Replace("{YEAR}", Year);
            text = text.Replace("{DAY}", Day);
            File.WriteAllText(destFile, text);
        }
    }
}