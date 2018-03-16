using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TUBES_STIMA_2
{

    static class Program
    {
        static void Main(){
            Graph g = new Graph(6);
            g.addEdge(5, 2);
            g.addEdge(5, 0);
            g.addEdge(4, 0);
            g.addEdge(4, 1);
            g.addEdge(2, 3);
            g.addEdge(3, 1);

            Console.WriteLine("BFS Topological Sort Result: \n");
            List<int> res = g.topologicalSortBFS();
            for (int i = 0; i < res.Count; i++)
                Console.Write(res[i] + " ");
        }
    }
}
