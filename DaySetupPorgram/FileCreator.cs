using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace SetupLibrary
{
    public class FileCreator
    {
        public FileCreator(string day, string year, string solutionRoot)
        {
            if (day == null || day.IsEmpty() || year == null || year.IsEmpty())
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
            var solutionPath = RepositoryRoot + $"\\Solutions\\aoc{Year}\\days\\Day{Day}.cs";
            var outputPath = RepositoryRoot + $"\\Output\\{Year}\\Day{Day}History.txt";
            var inputPath = RepositoryRoot + $"\\Input\\{Year}\\Day{Day}.txt";
            var testInputPath = RepositoryRoot + $"\\TestInput\\{Year}\\Day{Day}Test.txt";
            CreateDirectoriesAndFileRecursive(solutionPath);
            if (DirectoryHelper.IsTextFileEmpty(solutionPath))
            {
                FillFileWithBoilerPlate(solutionPath);
            }
            CreateDirectoriesAndFileRecursive(outputPath);
            CreateDirectoriesAndFileRecursive(inputPath);
            CreateDirectoriesAndFileRecursive(testInputPath);
        }

        private void CreateDirectoriesAndFileRecursive(
            string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                Console.WriteLine("Created empty file:\n" + path);
            }
        }

        public void FillFileWithBoilerPlate(string destFile)
        {
            string text = File.ReadAllText("BoilerPlate.txt");
            text = text.Replace("{YEAR}", Year);
            text = text.Replace("{DAY}", Day);
            File.WriteAllText(destFile, text);
            Console.WriteLine("Filled file:\n" + destFile);
        }
    }
}