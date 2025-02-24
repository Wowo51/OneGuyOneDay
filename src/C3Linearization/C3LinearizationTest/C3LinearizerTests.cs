using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using C3Linearization;

namespace C3LinearizationTest
{
    [TestClass]
    public class C3LinearizerTests
    {
        [TestMethod]
        public void TestLinearize_NodeWithoutInheritance()
        {
            Dictionary<string, List<string>> inheritance = new Dictionary<string, List<string>>();
            List<string> result = C3Linearizer.Linearize("A", inheritance);
            CollectionAssert.AreEqual(new List<string> { "A" }, result);
        }

        [TestMethod]
        public void TestLinearize_SingleInheritance()
        {
            Dictionary<string, List<string>> inheritance = new Dictionary<string, List<string>>();
            inheritance["A"] = new List<string>
            {
                "B"
            };
            List<string> result = C3Linearizer.Linearize("A", inheritance);
            CollectionAssert.AreEqual(new List<string> { "A", "B" }, result);
        }

        [TestMethod]
        public void TestLinearize_DiamondInheritance()
        {
            Dictionary<string, List<string>> inheritance = new Dictionary<string, List<string>>();
            inheritance["C"] = new List<string>
            {
                "A",
                "B"
            };
            inheritance["A"] = new List<string>
            {
                "O"
            };
            inheritance["B"] = new List<string>
            {
                "O"
            };
            List<string> result = C3Linearizer.Linearize("C", inheritance);
            CollectionAssert.AreEqual(new List<string> { "C", "A", "B", "O" }, result);
        }

        [TestMethod]
        public void TestLinearize_ComplexInheritance()
        {
            Dictionary<string, List<string>> inheritance = new Dictionary<string, List<string>>();
            inheritance["D"] = new List<string>
            {
                "B",
                "C"
            };
            inheritance["B"] = new List<string>
            {
                "A"
            };
            inheritance["C"] = new List<string>
            {
                "A"
            };
            List<string> result = C3Linearizer.Linearize("D", inheritance);
            CollectionAssert.AreEqual(new List<string> { "D", "B", "C", "A" }, result);
        }

        [TestMethod]
        public void TestLinearize_LargeChain()
        {
            Dictionary<string, List<string>> inheritance = new Dictionary<string, List<string>>();
            int chainLength = 100;
            for (int i = 1; i < chainLength; i++)
            {
                string key = "Node" + i.ToString();
                string value = "Node" + (i + 1).ToString();
                inheritance[key] = new List<string>
                {
                    value
                };
            }

            List<string> result = C3Linearizer.Linearize("Node1", inheritance);
            List<string> expected = new List<string>();
            for (int i = 1; i <= chainLength; i++)
            {
                expected.Add("Node" + i.ToString());
            }

            CollectionAssert.AreEqual(expected, result);
        }
    }
}