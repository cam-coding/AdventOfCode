namespace Runner
{
    class Runner
    {
        static async Task Main(string[] args)
        {
            var helper = new RunnerHelper();
            helper.GetDateAndYear(args, out string day, out string year);
            var solver = helper.GetSolver(day, year);
            var filePath = await helper.GetInputPath(day, year);
            var testFilePath = helper.GetTestInputPath(day, year);
            var historyPath = helper.GetHistoryPath(day, year);
            if (!testFilePath.Equals(String.Empty) && new FileInfo(testFilePath).Length != 0)
            {
                var testSolver = helper.GetSolver(day, year);
                Console.WriteLine("<<<<<TEST INPUT START>>>>>");
                testSolver.Solve(testFilePath, true).OutputWithTime();
                Console.WriteLine("<<<<< TEST INPUT END >>>>>");
            }
            var solution = solver.Solve(filePath, false);
            solution.OutputWithTime();
            helper.OutputHistory(historyPath, solution.GetHistoryOutput());
        }
    }
}
