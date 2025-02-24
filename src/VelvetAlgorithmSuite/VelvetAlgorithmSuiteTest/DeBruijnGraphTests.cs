using Microsoft.VisualStudio.TestTools.UnitTesting;
using VelvetAlgorithmSuite;
using System.Collections.Generic;

namespace VelvetAlgorithmSuiteTest
{
    [TestClass]
    public class DeBruijnGraphTests
    {
        [TestMethod]
        public void AddSequence_NullOrEmpty_DoesNotModifyGraph()
        {
            DeBruijnGraph graph = new DeBruijnGraph(3);
            graph.AddSequence(null);
            graph.AddSequence(string.Empty);
            Assert.AreEqual(0, graph.GetGraph().Count);
        }

        [TestMethod]
        public void AddSequence_ShortSequence_DoesNotModifyGraph()
        {
            DeBruijnGraph graph = new DeBruijnGraph(4);
            graph.AddSequence("ABC");
            Assert.AreEqual(0, graph.GetGraph().Count);
        }

        [TestMethod]
        public void AddSequence_ValidSequence_PopulatesGraphCorrectly()
        {
            DeBruijnGraph graph = new DeBruijnGraph(3);
            graph.AddSequence("ATGC");
            Dictionary<string, List<string>> actualGraph = graph.GetGraph();
            Assert.IsTrue(actualGraph.ContainsKey("AT"));
            Assert.IsTrue(actualGraph.ContainsKey("TG"));
            Assert.IsTrue(actualGraph.ContainsKey("GC"));
            CollectionAssert.AreEqual(new List<string> { "TG" }, actualGraph["AT"]);
            CollectionAssert.AreEqual(new List<string> { "GC" }, actualGraph["TG"]);
            CollectionAssert.AreEqual(new List<string>(), actualGraph["GC"]);
        }

        [TestMethod]
        public void AddSequence_MultipleSequences_AggregatesGraph()
        {
            DeBruijnGraph graph = new DeBruijnGraph(3);
            graph.AddSequence("ATGC");
            graph.AddSequence("TGCA");
            Dictionary<string, List<string>> actualGraph = graph.GetGraph();
            Assert.IsTrue(actualGraph.ContainsKey("AT"));
            Assert.IsTrue(actualGraph.ContainsKey("TG"));
            Assert.IsTrue(actualGraph.ContainsKey("GC"));
            Assert.IsTrue(actualGraph.ContainsKey("CA"));
            CollectionAssert.AreEqual(new List<string> { "TG" }, actualGraph["AT"]);
            CollectionAssert.AreEqual(new List<string> { "GC", "GC" }, actualGraph["TG"]);
            CollectionAssert.AreEqual(new List<string> { "CA" }, actualGraph["GC"]);
            CollectionAssert.AreEqual(new List<string>(), actualGraph["CA"]);
        }

        [TestMethod]
        public void AddSequence_LargeSequence_PopulatesGraphCorrectly()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Random random = new System.Random(42);
            for (int i = 0; i < 1000; i++)
            {
                int value = random.Next(0, 4);
                char nucleotide = value == 0 ? 'A' : value == 1 ? 'C' : value == 2 ? 'G' : 'T';
                sb.Append(nucleotide);
            }

            string seq = sb.ToString();
            int k = 10;
            DeBruijnGraph graph = new DeBruijnGraph(k);
            graph.AddSequence(seq);
            Dictionary<string, List<string>> result = graph.GetGraph();
            System.Collections.Generic.HashSet<string> expectedKeys = new System.Collections.Generic.HashSet<string>();
            for (int i = 0; i <= seq.Length - k; i++)
            {
                string kmer = seq.Substring(i, k);
                string prefix = kmer.Substring(0, k - 1);
                string suffix = kmer.Substring(1, k - 1);
                expectedKeys.Add(prefix);
                expectedKeys.Add(suffix);
            }

            Assert.AreEqual(expectedKeys.Count, result.Count);
        }
    }
}