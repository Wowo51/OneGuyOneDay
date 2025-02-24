using System;
using System.Collections.Generic;

namespace TarjanSCC
{
    public static class TarjanSCCAlgorithm
    {
        public static List<List<int>> FindStronglyConnectedComponents(Dictionary<int, List<int>> graph)
        {
            if (graph == null)
            {
                return new List<List<int>>();
            }

            List<List<int>> sccs = new List<List<int>>();
            Dictionary<int, int> indices = new Dictionary<int, int>();
            Dictionary<int, int> lowlinks = new Dictionary<int, int>();
            int index = 0;
            Stack<int> stack = new Stack<int>();
            HashSet<int> onStack = new HashSet<int>();
            void StrongConnect(int v)
            {
                indices[v] = index;
                lowlinks[v] = index;
                index = index + 1;
                stack.Push(v);
                onStack.Add(v);
                List<int> neighbors;
                if (!graph.TryGetValue(v, out List<int>? value) || value is null)
                {
                    neighbors = new List<int>();
                }
                else
                {
                    neighbors = value;
                }

                foreach (int w in neighbors)
                {
                    if (!indices.ContainsKey(w))
                    {
                        StrongConnect(w);
                        lowlinks[v] = Math.Min(lowlinks[v], lowlinks[w]);
                    }
                    else if (onStack.Contains(w))
                    {
                        lowlinks[v] = Math.Min(lowlinks[v], indices[w]);
                    }
                }

                if (lowlinks[v] == indices[v])
                {
                    List<int> component = new List<int>();
                    int w;
                    do
                    {
                        w = stack.Pop();
                        onStack.Remove(w);
                        component.Add(w);
                    }
                    while (w != v);
                    sccs.Add(component);
                }
            }

            foreach (int v in graph.Keys)
            {
                if (!indices.ContainsKey(v))
                {
                    StrongConnect(v);
                }
            }

            return sccs;
        }
    }
}