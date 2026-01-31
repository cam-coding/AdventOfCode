using AdventLibrary;

namespace aoc2022
{
    public class Day07 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;

            var rootDir = SetupTree(lines);
            var temp = SumPart1Sizes(rootDir);
            return SumPart1Sizes(rootDir);
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;

            var rootDir = SetupTree(lines);
            var currentSpace = 70000000 - rootDir.Size;
            var neededSpace = 30000000 - currentSpace;
            return FindSpace(rootDir, neededSpace, long.MaxValue);
        }

        private Dir SetupTree(List<string> lines)
        {
            var rootDir = new Dir("/", null);
            var currentDir = rootDir;
            foreach (var line in lines)
            {
                if (line.Contains("$"))
                {
                    if (line.Contains("cd"))
                    {
                        if (line.Contains("/"))
                        {
                            currentDir = rootDir;
                        }
                        else if (line.Contains(".."))
                        {
                            currentDir = currentDir.Parent;
                            if (currentDir == null)
                            {
                                currentDir = rootDir;
                            }
                        }
                        else
                        {
                            var dirName = line.Split(' ')[2];
                            var tempDir = currentDir.SubDirs.FirstOrDefault(x => x.Name.Equals(dirName));
                            if (tempDir == default)
                            {
                                tempDir = new Dir(dirName, currentDir);
                                currentDir.SubDirs.Add(tempDir);
                            }
                            currentDir = tempDir;
                        }
                    }
                }
                else
                {
                    if (line.Contains("dir"))
                    {
                        var dirName = line.Split(' ')[1];
                        var tempDir = currentDir.SubDirs.FirstOrDefault(x => x.Name.Equals(dirName));
                        if (tempDir == default)
                        {
                            tempDir = new Dir(dirName, currentDir);
                            currentDir.SubDirs.Add(tempDir);
                        }
                    }
                    else
                    {
                        var tempFile = new File(line);
                        currentDir.Files.Add(tempFile);
                    }
                }
            }
            SizeDirs(rootDir);
            return rootDir;
        }

        private long SizeDirs(Dir current)
        {
            long size = 0;

            foreach (var dir in current.SubDirs)
            {
                size += SizeDirs(dir);
            }
            foreach (var file in current.Files)
            {
                size += file.Size;
            }
            current.Size = size;
            return size;
        }

        private long SumPart1Sizes(Dir current)
        {
            long size = 0;

            foreach (var dir in current.SubDirs)
            {
                size += SumPart1Sizes(dir);
            }
            if (current.Size < 100000)
            {
                size += current.Size;
            }
            return size;
        }

        private long FindSpace(Dir current, long spaceNeeded, long best)
        {
            if (current.SubDirs.Count == 0)
            {
                if (current.Size >= spaceNeeded)
                {
                    return current.Size;
                }
            }
            foreach (var dir in current.SubDirs)
            {
                best = Math.Min(FindSpace(dir, spaceNeeded, best), best);
            }
            if (current.Size >= spaceNeeded)
            {
                best = Math.Min(current.Size, best);
            }
            return best;
        }

        private class Dir
        {
            public Dir(string name, Dir parent)
            {
                Name = name;
                SubDirs = new List<Dir>();
                Files = new List<File>();
                Size = 0;
                Parent = parent;
            }

            public string Name { get; set; }
            public List<Dir> SubDirs { get; set; }
            public List<File> Files { get; set; }
            public long Size { get; set; }
            public Dir Parent { get; set; }
        }

        private class File
        {
            public File(string str)
            {
                var tokens = str.Split(' ');
                Name = tokens[1];
                Size = long.Parse(tokens[0]);
            }

            public string Name { get; set; }
            public long Size { get; set; }
        }
    }
}