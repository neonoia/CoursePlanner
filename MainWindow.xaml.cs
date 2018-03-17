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
using System.Windows.Shapes;

namespace CoursePlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string FileName;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            FileName = text1.Text;

            Graph gr = ReadFile(FileName);

            // BFS IMPLEMENTATION
            outputBox.AppendText("Running BFS Topological Algorithm...\n");
            List<int> result = gr.topologicalSortBFS();
            outputBox.AppendText("BFS Topological Sort Result: (");
            for (int i = 0; i < result.Count; i++)
            {
                if (i != 0) outputBox.AppendText(",");
                outputBox.AppendText(result[i].ToString());
            }
            outputBox.AppendText(")\n");

            // DFS IMPLEMENTATION
            outputBox.AppendText("Running DFS Topological Algorithm...\n");
            List<int>[] res = gr.topologicalSortDFS();
            outputBox.AppendText("DFS Topological Sort Result: \n");
            for (int i = 0; i < res.Length; i++)
                outputBox.AppendText((res[i])[0] + " Timestamp(start/stop): (" + (res[i])[1] + "/" + (res[i])[2] + ")\n");

            Window1 subWindow = new Window1(FileName);
            subWindow.Show();
        }

        private Graph ReadFile(string FileName)
        {
            // Local Variables
            int ammount = 0;
            string line;
            char ch;
            int num;
            int course_now = -1;

            // Count the total courses
            System.IO.StreamReader file = new System.IO.StreamReader(FileName);
            while ((line = file.ReadLine()) != null)
            {
                ammount++;
            }
            file.Close();
            Graph g = new Graph(ammount);

            // Read each courses and its pre-requisite
            System.IO.StreamReader files = new System.IO.StreamReader(FileName);
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
