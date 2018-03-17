﻿using System;
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
