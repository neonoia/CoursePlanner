using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuickGraph;

namespace CoursePlanner
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private IBidirectionalGraph<object, IEdge<object>> _graphToVisualize;

        public IBidirectionalGraph<object, IEdge<object>> GraphToVisualize
        {
            get { return _graphToVisualize; }
        }

        public Window1()
        {
            Graph g = new Graph(5);
            g.addEdges(2, 0);
            g.addEdges(2, 3);
            g.addEdges(0, 1);
            g.addEdges(0, 3);
            g.addEdges(3, 1);
            g.addEdges(3, 4);
            g.addEdges(1, 4);

            Console.Clear();

            // BFS IMPLEMENTATION
            Console.Write("\nRunning BFS Topological Algorithm...\n");
            List<int> result = g.topologicalSortBFS();
            Console.Write("\nBFS Topological Sort Result: (");
            for (int i = 0; i < result.Count; i++)
            {
                if (i != 0) Console.Write(",");
                Console.Write(result[i]);
            }
            Console.Write(")\n");

            // DFS IMPLEMENTATION
            Console.Write("\nRunning DFS Topological Algorithm...\n");
            List<int>[] res = g.topologicalSortDFS();
            Console.WriteLine("\nDFS Topological Sort Result: \n");
            for (int i = 0; i < res.Length; i++) 
                Console.Write((res[i])[0] + " Timestamp(start/stop): (" + (res[i])[1] + "/" + (res[i])[2] + ")\n\n");
                
            CreateGraphToVisualize();

            InitializeComponent();
        }

        private void CreateGraphToVisualize()
        {
            var g = new BidirectionalGraph<object, IEdge<object>>();

            //add the vertices to the graph
            string[] vertices = new string[5];
            for (int i = 0; i < 5; i++)
            {
                vertices[i] = i.ToString();
                g.AddVertex(vertices[i]);
            }

            //add some edges to the graph
            g.AddEdge(new Edge<object>(vertices[2], vertices[0]));
            g.AddEdge(new Edge<object>(vertices[2], vertices[3]));
            g.AddEdge(new Edge<object>(vertices[0], vertices[1]));
            g.AddEdge(new Edge<object>(vertices[0], vertices[3])); 
            g.AddEdge(new Edge<object>(vertices[3], vertices[1]));
            g.AddEdge(new Edge<object>(vertices[3], vertices[4]));
            g.AddEdge(new Edge<object>(vertices[1], vertices[4]));

            _graphToVisualize = g;
        }
    }
}
