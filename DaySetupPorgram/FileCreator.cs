using AdventLibrary.Helpers;

namespace SetupLibrary
{
    public class FileCreator
    {
        public FileCreator(string day, string year, string solutionRoot)
        {
            if (day == null || year == null)
            {
                throw new ArgumentNullException();
            }
            Day = day;
            Year = year;
            RepositoryRoot = solutionRoot;
            InputRoot = solutionRoot + "\\AdventOfCodeInput";
        }

        public static string Day { get; set; }

        public static string Year { get; set; }

        public static string RepositoryRoot { get; set; }

        public static string InputRoot { get; set; }

        public void SetupFiles()
        {
            var filePath = CreateDirectoriesAndFile();
            FillFileWithBoilerPlate(filePath);
            CreateEmptyInputFile();
            CreateEmptyTestInputFile();
        }

        public string CreateDirectoriesAndFile()
        {
            var directoryPath = RepositoryRoot + $"\\Solutions\\aoc{Year}\\days\\";
            Directory.CreateDirectory(directoryPath);
            var fullPath = directoryPath + $"Day{Day}.cs";
            return fullPath;
        }

        private void CreateEmptyInputFile()
        {
            CreateFileIncludingDirectories(
                $"Day{Day}.txt",
                InputRoot + $"\\Input\\{Year}\\");
        }

        private void CreateEmptyTestInputFile()
        {
            CreateFileIncludingDirectories(
                $"Day{Day}Test.txt",
                InputRoot + $"\\TestInput\\{Year}\\");
        }

        private void CreateFileIncludingDirectories(string fileName, string directoryPath)
        {
            var fullPath = Path.Combine(directoryPath, fileName);
            if (!File.Exists(Path.Combine(fullPath)))
            {
                Directory.CreateDirectory(directoryPath);
                DirectoryHelper.CreateEmptyFile(fullPath);
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