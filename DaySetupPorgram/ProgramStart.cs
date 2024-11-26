using AdventLibrary.Helpers;
using SetupLibrary;

namespace DaySetupProgram
{
    static class ProgramStart
    {
        static void Main(string[] args)
        {
            if (args.Count() != 2)
            {
                Console.WriteLine("./DaySetupProgram.exe {day} {year}");
                return;
            }

            if (args.Count() == 2)
            {
                var day = int.Parse(args[0]);
                var year = int.Parse(args[1]);
                if (day < 1 || 25 < day ||
                    year < 2016 || year > 2040)
                {
                    Console.WriteLine("./DaySetupProgram.exe {day} {year}");
                    return;
                }
            }

            var solutionRoot = DirectoryHelper.TryGetSolutionDirectoryInfo();
            if (solutionRoot == null)
            {
                throw new Exception("Couldn't find the solution root directory.");
            }

            var creator = new FileCreator(args[0].PadLeft(2, '0'), args[1], solutionRoot.FullName);
            creator.SetupFiles();
        }
    }
}