using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupLibrary
{
    public static class DirectoryHelper
    {
        //start in exe directory and go up until we hit the solution. Assumes only 1 solution in this repo.
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }

        public static void CreateEmptyFile(string filepath)
        {
            File.Create(filepath).Dispose();
        }
    }
}
