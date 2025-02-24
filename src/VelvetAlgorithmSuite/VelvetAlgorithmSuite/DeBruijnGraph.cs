using System;
using System.Collections.Generic;

namespace VelvetAlgorithmSuite
{
    public class DeBruijnGraph
    {
        public int K { get; }

        private Dictionary<string, List<string>> graph;
        public DeBruijnGraph(int k)
        {
            if (k < 2)
            {
                k = 2;
            }

            this.K = k;
            this.graph = new Dictionary<string, List<string>>();
        }

        public void AddSequence(string sequence)
        {
            if (string.IsNullOrEmpty(sequence) || sequence.Length < this.K)
            {
                return;
            }

            for (int i = 0; i <= sequence.Length - this.K; i++)
            {
                string kmer = sequence.Substring(i, this.K);
                string prefix = kmer.Substring(0, this.K - 1);
                string suffix = kmer.Substring(1, this.K - 1);
                if (!this.graph.ContainsKey(prefix))
                {
                    this.graph[prefix] = new List<string>();
                }

                this.graph[prefix].Add(suffix);
                if (!this.graph.ContainsKey(suffix))
                {
                    this.graph[suffix] = new List<string>();
                }
            }
        }

        public Dictionary<string, List<string>> GetGraph()
        {
            return this.graph;
        }
    }
}