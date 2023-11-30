using BoilerPlateLibrary;

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

            var creator = new CreateNewDay(args[0].PadLeft(2, '0'), args[1]);
            creator.SetupFiles();
        }
    }
}