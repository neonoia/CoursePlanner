using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TUBES_STIMA_2
{

    class Graph{
        private int vertice;    // number of vertices
        private List<int>[] adj; // list of adjacencies

        public Graph(int v)
        {
            vertice = v;
            adj = new List<int>[vertice];
            for(int i=0; i<vertice; i++){
                adj[i] = new List<int>();
            }
        }

        public void addEdge(int a, int b)
        {
            (adj[b]).Add(a);
        }

        // implementing kahn's algorithm for topological sorting
        public List<int> topologicalSortBFS()
        {
            // create a list v_degree to store degrees of all vertices
            List<int> v_degree = new List<int>();
            for (int i=0; i<vertice; i++) {
                v_degree.Add(0);
            }

            Console.Write("Jumlah awal derajat masuk : \n");

            // fill list v_degree with number of degrees
            for(int i=0; i<vertice; i++) {
                for(int j=0; j<(adj[i]).Count; j++) {
                    v_degree[i]++;
                }
                Console.Write("V["+i+"] = "+v_degree[i]+"\n");
            }

            // enqueue all vertices with 0 degree into a queue
            Queue q = new Queue();
            for(int i = 0; i<vertice; i++){
                if (v_degree[i] == 0) {
                    q.Enqueue(i);
                }
            }
            
            // initialize count of visited vertices
            int visited_count = 0;

            // create a list to store topological sort result
            List<int> result = new List<int>();

            // deque vertices from queue, and enqueue neighbors whenever its degree becomes 0
            while (q.Count!=0) {
                
                int extract_front = (int)q.Peek();
                q.Dequeue();                    // deque the front of the queue
                result.Add(extract_front);      // insert the front into result list

                // decrement all its neighboring nodes
                for (int i=0; i<vertice; i++) {
                    for(int j=0; j<(adj[i]).Count; j++){
                        // decrement v_degree, insert to queue if it becomes zero
                        if ((adj[i])[j] == extract_front && (--v_degree[i] == 0)) q.Enqueue(i);
                    }
                }
                visited_count++;

                Console.Write("\nJumlah derajat masuk: \n");
                for (int i = 0; i < vertice; i++)
                {
                    Console.WriteLine("V[" + i + "] = " + v_degree[i]);
                }
                Console.Write("\nHasil topological sort sementara : (");
                for (int i = 0; i < result.Count; i++)
                {
                    if (i!=0) Console.Write(",");
                    Console.Write(result[i]);
                }
                Console.Write(")\n");
            }

            if (visited_count != vertice) {
                Console.WriteLine("Unable to sort topologically because there's a cycle within graph");
            }

            return result;
        }

    }
}
