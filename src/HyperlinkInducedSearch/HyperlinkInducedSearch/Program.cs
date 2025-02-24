using System;
using System.Collections.Generic;

namespace HyperlinkInducedSearch
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            HyperlinkInducedSearch.Graph graph = new HyperlinkInducedSearch.Graph();
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "C");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "A");
            graph.AddEdge("D", "C");
            graph.AddEdge("D", "B");
            int iterations = 10;
            Dictionary<string, double> hubs;
            Dictionary<string, double> authorities;
            HyperlinkInducedSearch.HITSAlgorithm.Compute(graph, iterations, out hubs, out authorities);
            Console.WriteLine("Authorities:");
            foreach (KeyValuePair<string, double> authority in authorities)
            {
                Console.WriteLine(authority.Key + ": " + authority.Value.ToString("F4"));
            }

            Console.WriteLine("Hubs:");
            foreach (KeyValuePair<string, double> hub in hubs)
            {
                Console.WriteLine(hub.Key + ": " + hub.Value.ToString("F4"));
            }
        }
    }
}