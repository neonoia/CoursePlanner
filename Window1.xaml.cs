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
using System.IO;
using QuickGraph;

namespace CoursePlanner
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string FileName;
        private IBidirectionalGraph<object, IEdge<object>> _graphToVisualize;

        public IBidirectionalGraph<object, IEdge<object>> GraphToVisualize
        {
            get { return _graphToVisualize; }
        }

        public Window1(string a)
        {
            FileName = a;

            Graph gr = ReadFile(FileName);
            string[] course_code = ReadCourses(FileName);

            // DRAW GRAPH USING GraphSharp and QuickGraph
            var g = new BidirectionalGraph<object, IEdge<object>>();

            //add the vertices to the graph
            try
            {
                string[] vertices = new string[gr.getVertice()];
                for (int i = 0; i < gr.getVertice(); i++)
                {
                    vertices[i] = course_code[i];
                    g.AddVertex(vertices[i]);
                }

                //add some edges to the graph
                for (int i = 0; i < gr.getVertice(); i++)
                {
                    for (int j = 0; j < gr.getAdjIdxLength(i); j++)
                    {
                        g.AddEdge(new Edge<object>(vertices[i], vertices[gr.getAdj(i, j)]));
                    }
                }

                _graphToVisualize = g;
                InitializeComponent();
            }
            catch (NullReferenceException ne)
            {
            }
        }

        private string[] ReadCourses(string FileName)
        {
            // Local Variables
            int ammount = 0;
            string line;
            StringBuilder temp1 = new StringBuilder();
            StringBuilder temp2 = new StringBuilder();
            char ch;
            int num;
            int course_now = 0;

            try
            {
                // Count the total courses
                System.IO.StreamReader file = new System.IO.StreamReader(FileName);
                while ((line = file.ReadLine()) != null)
                {
                    ammount++;
                }
                file.Close();

                Graph g = new Graph(ammount);
                string[] course_code = new string[ammount];

                // Read each course code
                System.IO.StreamReader files = new System.IO.StreamReader(FileName);
                while (!files.EndOfStream)
                {
                    ch = (char)files.Read();
                    if (ch != '.')
                    {
                        if (ch != ',')
                        {
                            temp1.Append(ch);
                        }
                        else
                        {
                            course_code[course_now] = temp1.ToString();
                            course_now++;
                            temp1.Length = 0;
                            while (ch != '.')
                            {
                                ch = (char)files.Read();
                            }
                            ch = (char)files.Read();
                            ch = (char)files.Read();
                        }
                    }
                    else
                    {
                        course_code[course_now] = temp1.ToString();
                        course_now++;
                        temp1.Length = 0;
                        ch = (char)files.Read();
                        ch = (char)files.Read();
                    }
                }
                files.Close();
                return course_code;
            }
            catch (FileNotFoundException e)
            {
                return null;
            }
        }

        private Graph ReadFile(string FileName)
        {
            // Local Variables
            int ammount = 0;
            string line;
            StringBuilder temp1 = new StringBuilder();
            StringBuilder temp2 = new StringBuilder();
            char ch;
            int num;
            int course_now = 0;

            // Count the total courses
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(FileName);
                while ((line = file.ReadLine()) != null)
                {
                    ammount++;
                }
                file.Close();
                Graph g = new Graph(ammount);
                string[] course_code = new string[ammount];

                // Read each course code
                System.IO.StreamReader files = new System.IO.StreamReader(FileName);
                while (!files.EndOfStream)
                {
                    ch = (char)files.Read();
                    if (ch != '.')
                    {
                        if (ch != ',')
                        {
                            temp1.Append(ch);
                        }
                        else
                        {
                            course_code[course_now] = temp1.ToString();
                            course_now++;
                            temp1.Length = 0;
                            while (ch != '.')
                            {
                                ch = (char)files.Read();
                            }
                            ch = (char)files.Read();
                            ch = (char)files.Read();
                        }
                    }
                    else
                    {
                        course_code[course_now] = temp1.ToString();
                        course_now++;
                        temp1.Length = 0;
                        ch = (char)files.Read();
                        ch = (char)files.Read();
                    }
                }
                files.Close();

                // Read each course code pre-requisite
                course_now = 0;
                int sign = 0;
                System.IO.StreamReader filesn = new System.IO.StreamReader(FileName);
                while (!filesn.EndOfStream)
                {
                    ch = (char)filesn.Read();
                    if (ch != '.')
                    {
                        if (ch != ',')
                        {
                            temp2.Append(ch);
                        }
                        else
                        {
                            if (sign == 0)
                            {
                                sign = 1;
                                temp2.Length = 0;
                            }
                            else
                            {
                                for (num = 0; num < ammount; num++)
                                {
                                    if (string.Equals(course_code[num], temp2.ToString()))
                                    {
                                        g.addEdges(num, course_now);
                                        num = ammount + 1;
                                    }
                                }
                                temp2.Length = 0;
                            }
                            ch = (char)filesn.Read();
                        }
                    }
                    else
                    {
                        if (sign == 0)
                        {
                            temp2.Length = 0;
                            course_now++;
                        }
                        else
                        {
                            sign = 0;
                            for (num = 0; num < ammount; num++)
                            {

                                if (string.Compare(course_code[num], temp2.ToString()) == 0)
                                {
                                    g.addEdges(num, course_now);
                                    num = ammount + 1;
                                }
                            }
                            temp2.Length = 0;
                            course_now++;
                        }
                    }
                }
                filesn.Close();
                return g;
            }
            catch(FileNotFoundException e)
            {
                return null;
            }
        }
    }
}
