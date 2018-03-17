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
            Graph gr = ReadFile();
            Console.Clear();
            
            // BFS IMPLEMENTATION
            Console.Write("\nRunning BFS Topological Algorithm...\n");
            List<int> result = gr.topologicalSortBFS();
            Console.Write("\nBFS Topological Sort Result: (");
            for (int i = 0; i < result.Count; i++)
            {
                if (i != 0) Console.Write(",");
                Console.Write(result[i]);
            }
            Console.Write(")\n");

            // DFS IMPLEMENTATION
            Console.Write("\nRunning DFS Topological Algorithm...\n");
            List<int>[] res = gr.topologicalSortDFS();
            Console.WriteLine("\nDFS Topological Sort Result: \n");
            for (int i = 0; i < res.Length; i++) 
                Console.Write((res[i])[0] + " Timestamp(start/stop): (" + (res[i])[1] + "/" + (res[i])[2] + ")\n\n");
                
            // DRAW GRAPH USING GraphSharp and QuickGraph
            var g = new BidirectionalGraph<object, IEdge<object>>();

            //add the vertices to the graph
            string[] vertices = new string[gr.getVertice()];
            for (int i = 0; i < gr.getVertice(); i++)
            {
                vertices[i] = i.ToString();
                g.AddVertex(vertices[i]);
            }

            //add some edges to the graph
            for (int i=0; i<gr.getVertice(); i++){
                for(int j=0; j<gr.getAdjIdxLength(i); j++){
                    g.AddEdge(new Edge<object>(vertices[i], vertices[gr.getAdj(i,j)]));
                }
            }

            _graphToVisualize = g;

            InitializeComponent();
        }

        private Graph ReadFile()
        {
            // Local Variables
            int ammount = 0;
            string line;
            char ch;
            int num;
            int course_now = -1;

            // Count the total courses
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\Programming\C#\CoursePlanner\File.txt");
            while ((line = file.ReadLine()) != null)
            {
                ammount++;
            }
            file.Close();
            Graph g = new Graph(ammount);
            
            // Read each courses and its pre-requisite
            System.IO.StreamReader files = new System.IO.StreamReader(@"D:\Programming\C#\CoursePlanner\File.txt");
            while (!files.EndOfStream)
            {
                ch = (char)files.Read();
                if (ch == 'C')
                {
                    if (course_now == -1)
                    {
                        ch = (char)files.Read();
                        course_now = (int)Char.GetNumericValue(ch) - 1;
                    }
                    else
                    {
                        ch = (char)files.Read();
                        num = (int)Char.GetNumericValue(ch) - 1;
                        // add edges from two vertices
                        g.addEdges(num, course_now);
                    }
                }
                else
                {
                    if (ch == '.')
                    {
                        course_now = -1;
                    }
                }
            }
            files.Close();
            return g;
        }
    }
}
