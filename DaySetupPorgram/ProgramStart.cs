using AdventLibrary.Helpers;
using SetupLibrary;

namespace DaySetupProgram
{
    static class ProgramStart
    {
        static void Main(string[] args)
        {
            var day = -1;
            var year = -1;
            if (args.Count() != 2)
            {
                if (args.Count() == 0)
                {
                    day = DaySetupStaticArgs.DAY;
                    year = DaySetupStaticArgs.YEAR;
                }
                else
                {
                    Console.WriteLine("./DaySetupProgram.exe {day} {year}");
                    return;
                }
            }
            else if (args.Count() == 2)
            {
                day = int.Parse(args[0]);
                year = int.Parse(args[1]);
            }

            if (day < 1 || 25 < day ||
                year < 2016 || year > 2040)
            {
                Console.WriteLine("./DaySetupProgram.exe {day} {year}");
                return;
            }

            var solutionRoot = DirectoryHelper.TryGetSolutionDirectoryInfo();
            if (solutionRoot == null)
            {
                throw new Exception("Couldn't find the solution root directory.");
            }

            var creator = new FileCreator(day.ToString().PadLeft(2, '0'), year.ToString(), solutionRoot.FullName);
            creator.SetupFiles();
        }
    }
}