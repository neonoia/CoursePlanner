using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CoursePlanner
{

    static class Program
    {
        static void Main(){
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
        }
    }
}
