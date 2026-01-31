using AdventLibrary;
using AdventLibrary.Helpers;
using SetupLibrary;
using System.Reflection;

namespace Runner
{
    public class RunnerHelper
    {
        private readonly Dictionary<string, Assembly> _assemblies;
        private readonly string _solutionRoot;
        private readonly string _inputRoot;

        public RunnerHelper()
        {
            _assemblies = new Dictionary<string, Assembly>
            {
                { "2015", typeof(aoc2015.Day01).Assembly },
                { "2016", typeof(aoc2016.Day01).Assembly },
                { "2017", typeof(aoc2017.Day01).Assembly },
                { "2018", typeof(aoc2018.Day01).Assembly },
                { "2019", typeof(aoc2019.Day01).Assembly },
                { "2020", typeof(aoc2020.Day01).Assembly },
                { "2021", typeof(aoc2021.Day01).Assembly },
                { "2022", typeof(aoc2022.Day01).Assembly },
                { "2023", typeof(aoc2023.Day01).Assembly },
                { "2024", typeof(aoc2024.Day01).Assembly },
                { "2025", typeof(aoc2025.Day01).Assembly },
            };

            var directory = DirectoryHelper.TryGetSolutionDirectoryInfo();
            if (directory != null)
            {
                _solutionRoot = directory.FullName;
                _inputRoot = _solutionRoot + "\\AdventOfCodeInput";
            }
            else
            {
                throw new Exception("Couldn't find the solution root directory.");
            }
        }

        public void GetDateAndYear(string[] args, out string day, out string year)
        {
            if (args.Length == 0)
            {
                year = RunnerStaticArgs.YEAR;
                day = RunnerStaticArgs.DAY;
            }
            else if (args[0] == "true" || args[0] == "-t")
            {
                year = System.DateTime.Now.ToString("yyyy");
                day = System.DateTime.Now.ToString("dd");
            }
            else
            {
                year = args[0];
                day = args[1];
            }
            day = day.PadLeft(2, '0');
        }

        public ISolver GetSolver(string day, string year)
        {
            var type = _assemblies[year].GetType($"aoc{year}.Day{day}");

            // If type is null and file doesn't exist, create a file and exit
            if (type == null && !File.Exists(_solutionRoot + $"\\Solutions\\aoc{year}\\Days\\Day{day}.cs"))
            {
                var creator = new FileCreator(day, year, _solutionRoot);
                creator.SetupFiles();
                Console.WriteLine("Files have now been created, please run again");
                Environment.Exit(0);
            }
            return (ISolver)Activator.CreateInstance(type);
        }

        public async Task<string> GetInputPath(string day, string year)
        {
            var inputFile = _inputRoot + $"\\Input\\{year}\\Day{day}.txt";
            if (!File.Exists(inputFile) || IsTextFileEmpty(inputFile))
            {
                await GetFromServerAsync(day, year);
            }

            return inputFile;
        }

        public string GetHistoryPath(string day, string year)
        {
            var historyPath = _solutionRoot + $"\\OutputProject\\Output\\{year}\\Day{day}History.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(historyPath));
            if (!File.Exists(historyPath))
            {
                File.Create(historyPath).Dispose();
                Console.WriteLine("Created empty file:\n" + historyPath);
            }

            return historyPath;
        }

        public void OutputHistory(string historyPath, string newAddition)
        {
            File.AppendAllText(historyPath, newAddition);
        }

        public string GetTestInputPath(string day, string year)
        {
            var fileName = _inputRoot + $"\\TestInput\\{year}\\Day{day}Test.txt";
            if (!File.Exists(fileName) || IsTextFileEmpty(fileName))
            {
                return string.Empty;
            }
            return fileName;
        }

        // Not used atm, the solution grabs the input
        public string GetTestInput(string day, string year)
        {
            if (File.Exists(_inputRoot + $"\\TestInput\\{year}\\Day{day}Test.txt"))
            {
                var input = AdventLibrary.ParseInput.GetTextFromFile(_inputRoot + $"\\TestInput\\{year}\\Day{day}Test.txt");
                return input;
            }
            return string.Empty;
        }

        private async Task GetFromServerAsync(string day, string year)
        {
            var sessionCookie = File.ReadAllText(_solutionRoot + "\\Runner\\SessionCookie.txt");

            var inputResult = string.Empty;
            var baseAddress = new Uri("https://adventofcode.com");
            using (var handler = new HttpClientHandler { UseCookies = false })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var message = new HttpRequestMessage(HttpMethod.Get, $"/{year}/day/{day.TrimStart('0')}/input");
                message.Headers.Add("Cookie", sessionCookie);
                var result = await client.SendAsync(message);
                result.EnsureSuccessStatusCode();
                inputResult = await result.Content.ReadAsStringAsync();
            }

            File.WriteAllText(_inputRoot + $"\\Input\\{year}\\Day{day}.txt", inputResult);
        }

        private bool IsTextFileEmpty(string fileName)
        {
            var info = new FileInfo(fileName);
            if (info.Length == 0)
                return true;

            // only if your use case can involve files with 1 or a few bytes of content.
            if (info.Length < 6)
            {
                var content = File.ReadAllText(fileName);
                return content.Length == 0;
            }
            return false;
        }
    }
}