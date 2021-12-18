using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Permissions;
using System.Threading.Tasks;
using aoc2015;
using AdventLibrary;

namespace Runner
{
    public class RunnerHelper
    {
        private Dictionary<string, Assembly> _assemblies;
        public RunnerHelper()
        {
            _assemblies = new Dictionary<string, Assembly>();
            _assemblies.Add("2015", typeof(aoc2015.Day01).Assembly);
            _assemblies.Add("2021", typeof(aoc2021.Day01).Assembly);
        }

        public void GetDateAndYear(string[] args, out string day, out string year)
        {
            if (args.Length == 0)
            {
                year = "2021";
                day = "17";
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
            return (ISolver)Activator.CreateInstance(type);
        }

        public string GetInputPath(string day, string year)
        {
            if (File.Exists($"..\\Input\\{year}\\Day{day}.txt"))
            {
                return $"..\\Input\\{year}\\Day{day}.txt";
            }

            var deleteMe = GetFromServerAsync(day, year).Result;
            return $"..\\Input\\{year}\\Day{day}.txt";
        }

        public string GetTestInputPath(string day, string year)
        {
            if (File.Exists($"..\\TestInput\\{year}\\Day{day}Test.txt"))
            {
                return$"..\\TestInput\\{year}\\Day{day}Test.txt";
            }
            return string.Empty;
        }

        public string GetInput(string day, string year)
        {
            if (File.Exists($"..\\Input\\{year}\\Day{day}.txt"))
            {
                return AdventLibrary.ParseInput.GetTextFromFile($"..\\Input\\{year}\\Day{day}.txt");
            }

            return GetFromServerAsync(day, year).Result;
        }

        public string GetTestInput(string day, string year)
        {
            if (File.Exists($"..\\TestInput\\{year}\\Day{day}Test.txt"))
            {
                var input = AdventLibrary.ParseInput.GetTextFromFile($"..\\TestInput\\{year}\\Day{day}Test.txt");
                return input;
            }
            return string.Empty;
        }

        private async Task<string> GetFromServerAsync(string day, string year)
        {
            var sessionCookie = System.IO.File.ReadAllText($"..\\AdventOfCodeLibrary\\AdventLibrary\\SessionCookie.txt");

            var inputResult = string.Empty;
            var baseAddress = new Uri("https://adventofcode.com");
            using (var handler = new HttpClientHandler { UseCookies = false })
            using (var client2 = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var message = new HttpRequestMessage(HttpMethod.Get, $"/{year}/day/{day.TrimStart('0')}/input");
                message.Headers.Add("Cookie", sessionCookie);
                var result = await client2.SendAsync(message);
                result.EnsureSuccessStatusCode();
                inputResult = await result.Content.ReadAsStringAsync();
            }

            File.WriteAllText($"..\\Input\\{year}\\Day{day}.txt", inputResult);
            return inputResult;
        }
    }

}