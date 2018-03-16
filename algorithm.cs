using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TUBES_STIMA_2
{

    class Graph{
        private int vertice;    // number of vertices
        private int start;
        private List<int>[] adj; // list of adjacencies

        public Graph(int v)
        {
            start = 0;
            vertice = v;
            adj = new List<int>[vertice];
            for(int i=0; i<vertice; i++){
                adj[i] = new List<int>();
            }
        }

        public void addEdge(int a, int b)
        {
            (adj[a]).Add(b);
        }

        // implementing kahn's algorithm for topological sorting
        public List<int> topologicalSortBFS()
        {
            // create a list v_degree to store degrees of all vertices
            List<int> v_degree = new List<int>();
            for (int i=0; i<vertice; i++) {
                v_degree.Add(0);
            }

            Console.Write("Initial in-degrees of each vertices : \n");

            // fill list v_degree with number of degrees
            for(int i=0; i<vertice; i++) {
                for(int j=0; j<(adj[i]).Count; j++) {
                    for (int k=0; k<vertice; k++){
                        if ((adj[i])[j]==k) v_degree[k]++;
                    }
                }
            }

            for (int i = 0; i < vertice; i++) {
                Console.Write("V[" + i + "] = " + v_degree[i] + "\n");
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
                
                // print to screen number of indegrees
                Console.Write("\nCurrent number of in degrees within each vertices: \n");
                for (int i = 0; i < vertice; i++)
                {
                    Console.WriteLine("V[" + i + "] = " + v_degree[i]);
                }
                
                int extract_front = (int)q.Peek();
                q.Dequeue();                    // deque the front of the queue
                result.Add(extract_front);      // insert the front into result list

                // print to screen current topological sort result
                Console.Write("\nHasil topological sort sementara : (");
                for (int i = 0; i < result.Count; i++)
                {
                    if (i != 0) Console.Write(",");
                    Console.Write(result[i]);
                }
                Console.Write(")\n");

                // decrement all its neighboring nodes
                for(int j=0; j<(adj[extract_front]).Count; j++){
                    for (int i=0; i<vertice; i++){
                        if ((adj[extract_front])[j]==i) {
                            v_degree[i]--;
                            if (v_degree[i] == 0) q.Enqueue(i);
                        }
                    }
                    // decrement v_degree, insert to queue if it becomes zero
                }
                visited_count++;

            }

            if (visited_count != vertice) {
                Console.WriteLine("Unable to sort topologically because there's a cycle within graph");
            }

            return result;
        }

        public void utilityDFS(int v, bool[] visited, Stack<int> st, List<int>[] time){
            visited[v] = true;
            start++;
            (time[v]).Add(start);

            for(int i=0; i<(adj[v]).Count; i++){
                for(int j=0; j<vertice; j++) {
                    if((adj[v])[i] == j){
                        if(!visited[j]) utilityDFS(j, visited, st, time);
                    }
                }
            }
            st.Push(v);
            start++;
            (time[v]).Add(start);
        }

        public List<int>[] topologicalSortDFS(){
            Stack<int> st = new Stack<int>();
            List<int>[] result = new List<int>[vertice];
            List<int>[] time = new List<int>[vertice];
            for (int i=0;i<vertice; i++){
                result[i] = new List<int>();
                time[i] = new List<int>();
            }

            // create a list v_degree to store in-degrees of each vertices
            List<int> v_degree = new List<int>();
            for (int i = 0; i < vertice; i++)
            {
                v_degree.Add(0);
            }

            // fill list v_degree with number of in-degrees of each vertices
            for (int i = 0; i < vertice; i++)
            {
                for (int j = 0; j < (adj[i]).Count; j++)
                {
                    for (int k = 0; k < vertice; k++)
                    {
                        if ((adj[i])[j] == k) v_degree[k]++;
                    }
                }
            }

            bool[] visited = new bool[vertice];
            for (int i=0; i<vertice; i++){
                visited[i]=false;
            }

            for(int i=0; i<vertice; i++){
                if (visited[i] == false && v_degree[i]==0){
                    utilityDFS(i, visited, st, time);
                }
            }

            int idx = 0;
            while (st.Count!=0){
                int top = (int)st.Peek();
                st.Pop();
                (result[idx]).Add(top);
                (result[idx]).Add((time[top])[0]);
                (result[idx]).Add((time[top])[1]);
                idx++;
            }

            return result;

        }

    }
}
