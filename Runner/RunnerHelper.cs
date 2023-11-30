using AdventLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using BoilerPlateLibrary;

namespace Runner
{
    public class RunnerHelper
    {
        private readonly Dictionary<string, Assembly> _assemblies;
        private readonly string _pathAdjustment;
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
            };

            _pathAdjustment = Directory.GetCurrentDirectory() + "\\";
            if (_pathAdjustment.EndsWith("bin\\Debug\\net6.0\\"))
            {
                _pathAdjustment = _pathAdjustment + "..\\..\\..\\";
            }
        }

        public void GetDateAndYear(string[] args, out string day, out string year)
        {
            if (args.Length == 0)
            {
                year = "2023";
                day = "1";
            }
            else if (args[0] == "true" || args[0] == "-t" )
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

            // If type is null and file doesn't exist, create a file and crash
            // also ignore hot reload and close manually
            var temp = _pathAdjustment + $"..\\Solutions\\aoc{year}\\days\\Day{day}.cs";
            if (type == null && !File.Exists(_pathAdjustment + $"..\\Solutions\\aoc{year}\\Days\\Day{day}.cs"))
            {
                var creator = new CreateNewDay(day, year);
                creator.SetupFiles();
            }
            return (ISolver)Activator.CreateInstance(type);
        }

        public async Task<string> GetInputPath(string day, string year)
        {
            if (!File.Exists(_pathAdjustment + $"..\\Input\\{year}\\Day{day}.txt"))
            {
                await GetFromServerAsync(day, year);
            }

            return _pathAdjustment + $"..\\Input\\{year}\\Day{day}.txt";
        }

        public string GetTestInputPath(string day, string year)
        {
            var fileName = _pathAdjustment + $"..\\TestInput\\{year}\\Day{day}Test.txt";
            if (File.Exists(fileName) & !File.ReadAllText(fileName).Equals(string.Empty))
            {
                return fileName;
            }
            return string.Empty;
        }

        public string GetTestInput(string day, string year)
        {
            if (File.Exists(_pathAdjustment + $"..\\TestInput\\{year}\\Day{day}Test.txt"))
            {
                var input = AdventLibrary.ParseInput.GetTextFromFile(_pathAdjustment + $"..\\TestInput\\{year}\\Day{day}Test.txt");
                return input;
            }
            return string.Empty;
        }

        private async Task GetFromServerAsync(string day, string year)
        {
            var sessionCookie = File.ReadAllText(_pathAdjustment + "SessionCookie.txt");

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

            File.WriteAllText(_pathAdjustment + $"..\\Input\\{year}\\Day{day}.txt", inputResult);
        }
    }

}