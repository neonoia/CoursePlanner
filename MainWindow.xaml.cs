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
using System.IO;

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

            outputBox.AppendText("Hello and Welcome! ");
            outputBox.AppendText("This program will help you plan and sort your college course!\n");
            outputBox.AppendText("Made with heart by Blek Panter Team :\n");
            outputBox.AppendText("13516081 - Rabbi Fijar Mayoza\n");
            outputBox.AppendText("13516106 - Kurniandha Sukma Yunastrian\n");
            outputBox.AppendText("13516137 - Hafizh Budiman\n");
            outputBox.AppendText("Informatics Engineering, Bandung Institute of Technology\n");
            outputBox.AppendText("Copyright 2018\n");
            outputBox.AppendText("Follow README.md to run this program.\n");
        }


        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            FileName = text1.Text;

            Graph gr = ReadFile(FileName);

            // BFS IMPLEMENTATION
            outputBox.AppendText("Running BFS Topological Algorithm...\n");
            try           
            {
                List<int> result = gr.topologicalSortBFS();
                outputBox.AppendText("BFS Topological Sort Result: (");
                for (int i = 0; i < result.Count; i++)
                {
                    if (i != 0) outputBox.AppendText(",");
                    outputBox.AppendText(result[i].ToString());
                }
                outputBox.AppendText(")\n");
            }
            catch(NullReferenceException ne)
            {
                outputBox.AppendText("No result\n");
            }
           

            // DFS IMPLEMENTATION
            outputBox.AppendText("Running DFS Topological Algorithm...\n");

            try
            {
                List<int>[] res = gr.topologicalSortDFS();
                outputBox.AppendText("DFS Topological Sort Result: \n");
                for (int i = 0; i < res.Length; i++)
                    outputBox.AppendText((res[i])[0] + " Timestamp(start/stop): (" + (res[i])[1] + "/" + (res[i])[2] + ")\n");
            }
            catch(NullReferenceException en)
            {
                outputBox.AppendText("No result\n");
            }

            Window1 subWindow = new Window1(FileName);
            subWindow.Show();
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
                outputBox.AppendText("Course Code Number : \n");
                for (course_now = 0; course_now < ammount; course_now++)
                {
                    outputBox.AppendText(course_now.ToString());
                    outputBox.AppendText(" = ");
                    outputBox.AppendText(course_code[course_now].ToString());
                    outputBox.AppendText("\n");
                }
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
            } catch (FileNotFoundException e)
            {
                Console.WriteLine("[Data File Missing] {0}", e);
                outputBox.AppendText("File Not Found !\n");
                return null;
            }
        }
    }
}
