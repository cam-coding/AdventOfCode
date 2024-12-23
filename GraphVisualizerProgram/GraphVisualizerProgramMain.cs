namespace GraphVisualizer
{
    internal static class GraphVisualizerProgramMain
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //create a form
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            var inputPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\GraphInput.txt");

            var input = File.ReadAllLines(inputPath);

            foreach (var line in input)
            {
                var halves = line.Split(':');
                var key = halves[0];
                var nodes = halves[1].Split(';').ToList();

                foreach (var node in nodes)
                {
                    graph.AddEdge(key, node);
                }
            }

            /*
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;*/
            //bind the graph to the viewer
            viewer.Graph = graph;
            //associate the viewer with the form
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form
            form.ShowDialog();
        }
    }
}