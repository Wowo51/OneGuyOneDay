using System.Collections.Generic;

namespace BoruvkasAlgorithm
{
    public static class Boruvka
    {
        public static List<Edge> ComputeMST(Graph graph)
        {
            int V = graph.Vertices;
            List<Edge> result = new List<Edge>();
            UnionFind uf = new UnionFind(V);
            int numTrees = V;
            while (numTrees > 1)
            {
                Edge[] cheapest = new Edge[V];
                foreach (Edge edge in graph.Edges)
                {
                    int set1 = uf.Find(edge.Source);
                    int set2 = uf.Find(edge.Destination);
                    if (set1 == set2)
                    {
                        continue;
                    }

                    if (cheapest[set1] == null || edge.Weight < cheapest[set1].Weight)
                    {
                        cheapest[set1] = edge;
                    }

                    if (cheapest[set2] == null || edge.Weight < cheapest[set2].Weight)
                    {
                        cheapest[set2] = edge;
                    }
                }

                bool added = false;
                for (int i = 0; i < V; i++)
                {
                    if (cheapest[i] != null)
                    {
                        int set1 = uf.Find(cheapest[i].Source);
                        int set2 = uf.Find(cheapest[i].Destination);
                        if (set1 == set2)
                        {
                            continue;
                        }

                        result.Add(cheapest[i]);
                        uf.Union(set1, set2);
                        added = true;
                        numTrees--;
                    }
                }

                if (!added)
                {
                    break;
                }
            }

            return result;
        }
    }

    public class UnionFind
    {
        private int[] parent;
        private int[] rank;
        public UnionFind(int size)
        {
            parent = new int[size];
            rank = new int[size];
            for (int i = 0; i < size; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int i)
        {
            if (parent[i] != i)
            {
                parent[i] = Find(parent[i]);
            }

            return parent[i];
        }

        public void Union(int x, int y)
        {
            int xroot = Find(x);
            int yroot = Find(y);
            if (xroot == yroot)
            {
                return;
            }

            if (rank[xroot] < rank[yroot])
            {
                parent[xroot] = yroot;
            }
            else if (rank[xroot] > rank[yroot])
            {
                parent[yroot] = xroot;
            }
            else
            {
                parent[yroot] = xroot;
                rank[xroot]++;
            }
        }
    }
}