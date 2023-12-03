using BoilerPlateLibrary;
using Runner;

namespace BoilerPlateProgram
{
    static class CreateNewDayProgram
    {
        static void Main(string[] args)
        {
            if (args.Count() != 2)
            {
                Console.WriteLine("./BoilerPlate.exe {day} {year}");
                Console.WriteLine(args[0]);
                Console.WriteLine(args[1]);
                return;
            }

            var solutionRoot = DirectoryHelper.TryGetSolutionDirectoryInfo();
            if (solutionRoot == null)
            {
                throw new Exception("Couldn't find the solution root directory.");
            }

            var creator = new CreateNewDay(args[0].PadLeft(2, '0'), args[1], solutionRoot.FullName);
            creator.SetupFiles();
        }
    }
}