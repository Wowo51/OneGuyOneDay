using System;
using System.Collections.Generic;
using System.Linq;

namespace BronKerboschAlgorithm
{
    public static class BronKerbosch
    {
        public static List<List<int>> FindMaximalCliques(Graph graph)
        {
            List<List<int>> cliques = new List<List<int>>();
            HashSet<int> P = new HashSet<int>(graph.Vertices);
            BronKerboschRecursive(new HashSet<int>(), P, new HashSet<int>(), graph, cliques);
            return cliques;
        }

        private static void BronKerboschRecursive(HashSet<int> R, HashSet<int> P, HashSet<int> X, Graph graph, List<List<int>> cliques)
        {
            if (P.Count == 0 && X.Count == 0)
            {
                cliques.Add(R.ToList());
                return;
            }

            List<int> PList = new List<int>(P);
            foreach (int vertex in PList)
            {
                HashSet<int> newR = new HashSet<int>(R);
                newR.Add(vertex);
                HashSet<int> newP = new HashSet<int>(P.Intersect(graph.Neighbors(vertex)));
                HashSet<int> newX = new HashSet<int>(X.Intersect(graph.Neighbors(vertex)));
                BronKerboschRecursive(newR, newP, newX, graph, cliques);
                P.Remove(vertex);
                X.Add(vertex);
            }
        }
    }
}