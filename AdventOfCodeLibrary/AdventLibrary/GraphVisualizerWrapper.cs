using System.Diagnostics;

namespace AdventLibrary
{
    public static class GraphVisualizerWrapper
    {
        private static string _graphInputPath = "..\\..\\..\\..\\GraphVisualizerProgram\\graphInput.txt";
        private static string _graphVisualizerExePath = "..\\..\\..\\..\\GraphVisualizerProgram\\bin\\Debug\\net10.0-windows\\GraphVisualizerProgram.exe";

        public static void StartVisualizerProgram(Dictionary<string, List<string>> adjListGraph)
        {
            if (!System.IO.File.Exists(_graphVisualizerExePath))
            {
                throw new Exception("Build GraphVisualizer if you're seeing this. GraphVisualizerProgram.exe not found at path: " + _graphVisualizerExePath);
            }
            if (!System.IO.File.Exists(_graphInputPath))
            {
                throw new Exception("Graph Inpput not found at path: " + _graphInputPath);
            }

            var outputLines = new List<string>();
            foreach (var item in adjListGraph)
            {
                var str = item.Key + ":";

                var i = 0;
                for (; i < item.Value.Count - 1; i++)
                {
                    str += item.Value[i] + ";";
                }
                str += item.Value[i];
                outputLines.Add(str);
            }

            File.WriteAllLines(_graphInputPath, outputLines);

            var visualizerProcess = new Process();
            visualizerProcess.StartInfo.FileName = _graphVisualizerExePath;  // just for example, you can use yours.
            visualizerProcess.Start();
        }
    }
}