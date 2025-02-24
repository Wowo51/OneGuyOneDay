using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ID3Algorithm;

namespace ID3AlgorithmTest
{
    [TestClass]
    public class ID3AlgorithmUnitTests
    {
        [TestMethod]
        public void TestEmptyExamples()
        {
            List<Example> examples = new List<Example>();
            List<string> attributes = new List<string>
            {
                "Any"
            };
            TreeNode tree = ID3.BuildTree(examples, attributes);
            Assert.IsNotNull(tree);
            Assert.IsTrue(tree.IsLeaf);
            Assert.AreEqual("Unknown", tree.Label);
        }

        [TestMethod]
        public void TestAllSameLabels()
        {
            Dictionary<string, string> attr = new Dictionary<string, string>
            {
                {
                    "Outlook",
                    "Sunny"
                }
            };
            Example ex1 = new Example(attr, "Yes");
            Example ex2 = new Example(attr, "Yes");
            List<Example> examples = new List<Example>
            {
                ex1,
                ex2
            };
            List<string> attributes = new List<string>
            {
                "Outlook"
            };
            TreeNode tree = ID3.BuildTree(examples, attributes);
            Assert.IsNotNull(tree);
            Assert.IsTrue(tree.IsLeaf);
            Assert.AreEqual("Yes", tree.Label);
        }

        [TestMethod]
        public void TestNoAttributes()
        {
            Dictionary<string, string> attr = new Dictionary<string, string>
            {
                {
                    "Dummy",
                    "Value"
                }
            };
            Example ex1 = new Example(attr, "Yes");
            Example ex2 = new Example(attr, "No");
            Example ex3 = new Example(attr, "Yes");
            List<Example> examples = new List<Example>
            {
                ex1,
                ex2,
                ex3
            };
            List<string> attributes = new List<string>();
            TreeNode tree = ID3.BuildTree(examples, attributes);
            Assert.IsNotNull(tree);
            Assert.IsTrue(tree.IsLeaf);
            Assert.AreEqual("Yes", tree.Label);
        }

        [TestMethod]
        public void TestDecisionTreeSimpleSplit()
        {
            Dictionary<string, string> attrSunny = new Dictionary<string, string>
            {
                {
                    "Weather",
                    "Sunny"
                }
            };
            Dictionary<string, string> attrRainy = new Dictionary<string, string>
            {
                {
                    "Weather",
                    "Rainy"
                }
            };
            Example ex1 = new Example(attrSunny, "Play");
            Example ex2 = new Example(attrSunny, "Play");
            Example ex3 = new Example(attrRainy, "DontPlay");
            Example ex4 = new Example(attrRainy, "DontPlay");
            List<Example> examples = new List<Example>
            {
                ex1,
                ex2,
                ex3,
                ex4
            };
            List<string> attributes = new List<string>
            {
                "Weather"
            };
            TreeNode tree = ID3.BuildTree(examples, attributes);
            Assert.IsNotNull(tree);
            Assert.IsFalse(tree.IsLeaf);
            Assert.AreEqual("Weather", tree.Attribute);
            Assert.IsNotNull(tree.Children);
            Assert.AreEqual(2, tree.Children.Count);
            TreeNode sunnyNode = tree.Children["Sunny"];
            TreeNode rainyNode = tree.Children["Rainy"];
            Assert.IsTrue(sunnyNode.IsLeaf);
            Assert.AreEqual("Play", sunnyNode.Label);
            Assert.IsTrue(rainyNode.IsLeaf);
            Assert.AreEqual("DontPlay", rainyNode.Label);
        }

        [TestMethod]
        public void TestMissingAttribute()
        {
            Dictionary<string, string> attrPresent = new Dictionary<string, string>
            {
                {
                    "Weather",
                    "Sunny"
                }
            };
            Dictionary<string, string> attrAbsent = new Dictionary<string, string>();
            Example ex1 = new Example(attrAbsent, "Play");
            Example ex2 = new Example(attrAbsent, "Play");
            Example ex3 = new Example(attrPresent, "DontPlay");
            Example ex4 = new Example(attrPresent, "DontPlay");
            List<Example> examples = new List<Example>
            {
                ex1,
                ex2,
                ex3,
                ex4
            };
            List<string> attributes = new List<string>
            {
                "Weather"
            };
            TreeNode tree = ID3.BuildTree(examples, attributes);
            Assert.IsNotNull(tree);
            Assert.IsFalse(tree.IsLeaf);
            Assert.AreEqual("Weather", tree.Attribute);
            Assert.IsNotNull(tree.Children);
            Assert.IsTrue(tree.Children.ContainsKey("Sunny"));
            Assert.IsTrue(tree.Children.ContainsKey(""));
            TreeNode presentNode = tree.Children["Sunny"];
            TreeNode missingNode = tree.Children[""];
            Assert.IsTrue(presentNode.IsLeaf);
            Assert.AreEqual("DontPlay", presentNode.Label);
            Assert.IsTrue(missingNode.IsLeaf);
            Assert.AreEqual("Play", missingNode.Label);
        }

        [TestMethod]
        public void TestSyntheticLargeDataset()
        {
            List<Example> examples = new List<Example>();
            for (int i = 0; i < 1000; i++)
            {
                Dictionary<string, string> attr = new Dictionary<string, string>();
                if (i % 2 == 0)
                {
                    attr.Add("Number", "Even");
                    examples.Add(new Example(attr, "EvenLabel"));
                }
                else
                {
                    attr.Add("Number", "Odd");
                    examples.Add(new Example(attr, "OddLabel"));
                }
            }

            List<string> attributes = new List<string>
            {
                "Number"
            };
            TreeNode tree = ID3.BuildTree(examples, attributes);
            Assert.IsNotNull(tree);
            Assert.IsFalse(tree.IsLeaf);
            Assert.AreEqual("Number", tree.Attribute);
            Assert.IsNotNull(tree.Children);
            Assert.IsTrue(tree.Children.ContainsKey("Even"));
            Assert.IsTrue(tree.Children.ContainsKey("Odd"));
            TreeNode evenNode = tree.Children["Even"];
            TreeNode oddNode = tree.Children["Odd"];
            Assert.IsTrue(evenNode.IsLeaf);
            Assert.AreEqual("EvenLabel", evenNode.Label);
            Assert.IsTrue(oddNode.IsLeaf);
            Assert.AreEqual("OddLabel", oddNode.Label);
        }

        [TestMethod]
        public void TestProcessExitCode()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "dotnet";
            psi.Arguments = "--version";
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            Process proc = new Process();
            proc.StartInfo = psi;
            proc.Start();
            proc.WaitForExit();
            int exitCode = proc.ExitCode;
            string error = proc.StandardError.ReadToEnd();
            Assert.AreEqual(0, exitCode, "Process did not exit with code 0. Error: " + error);
        }

        [TestMethod]
        public void TestParallelExecutionNoErrors()
        {
            ConcurrentBag<Exception> exceptions = new ConcurrentBag<Exception>();
            Parallel.For(0, 100, i =>
            {
                try
                {
                    List<Example> examples = new List<Example>();
                    Dictionary<string, string> attr = new Dictionary<string, string>();
                    attr.Add("Attr", i.ToString());
                    examples.Add(new Example(attr, (i % 2 == 0) ? "Even" : "Odd"));
                    List<string> attributes = new List<string>
                    {
                        "Attr"
                    };
                    TreeNode tree = ID3.BuildTree(examples, attributes);
                    Assert.IsNotNull(tree);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });
            Assert.AreEqual(0, exceptions.Count, "Exceptions occurred during parallel execution.");
        }
    }
}