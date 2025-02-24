using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using AC3Algorithm;

namespace AC3AlgorithmTest
{
    [TestClass]
    public class AC3AlgorithmTests
    {
        [TestMethod]
        public void TestNoConstraints()
        {
            CSP<int> csp = new CSP<int>();
            csp.AddVariable("X", new List<int> { 1, 2, 3 });
            bool result = AC3.EnforceArcConsistency(csp);
            Assert.IsTrue(result, "AC3 should succeed when no constraints.");
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, csp.Domains["X"]);
        }

        [TestMethod]
        public void TestInequalityConstraintSatisfiable()
        {
            CSP<int> csp = new CSP<int>();
            csp.AddVariable("X", new List<int> { 1, 2 });
            csp.AddVariable("Y", new List<int> { 1, 2 });
            Constraint<int> notEqual = delegate (int a, int b)
            {
                return a != b;
            };
            csp.AddConstraint("X", "Y", notEqual);
            csp.AddConstraint("Y", "X", notEqual);
            bool result = AC3.EnforceArcConsistency(csp);
            Assert.IsTrue(result, "AC3 should succeed for satisfiable inequality constraint.");
            CollectionAssert.AreEquivalent(new List<int> { 1, 2 }, csp.Domains["X"]);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2 }, csp.Domains["Y"]);
        }

        [TestMethod]
        public void TestInequalityConstraintUnsatisfiable()
        {
            CSP<int> csp = new CSP<int>();
            csp.AddVariable("X", new List<int> { 1 });
            csp.AddVariable("Y", new List<int> { 1 });
            Constraint<int> notEqual = delegate (int a, int b)
            {
                return a != b;
            };
            csp.AddConstraint("X", "Y", notEqual);
            csp.AddConstraint("Y", "X", notEqual);
            bool result = AC3.EnforceArcConsistency(csp);
            Assert.IsFalse(result, "AC3 should fail for unsatisfiable inequality constraint.");
        }

        [TestMethod]
        public void TestDirectionalConstraint()
        {
            CSP<int> csp = new CSP<int>();
            csp.AddVariable("A", new List<int> { 1, 2, 3 });
            csp.AddVariable("B", new List<int> { 2, 3 });
            Constraint<int> greaterThan = delegate (int a, int b)
            {
                return a > b;
            };
            csp.AddConstraint("A", "B", greaterThan);
            bool result = AC3.EnforceArcConsistency(csp);
            Assert.IsTrue(result, "AC3 should succeed for directional constraint.");
            CollectionAssert.AreEqual(new List<int> { 3 }, csp.Domains["A"]);
            CollectionAssert.AreEqual(new List<int> { 2, 3 }, csp.Domains["B"]);
        }

        [TestMethod]
        public void TestLargeCSP()
        {
            int count = 100;
            CSP<int> csp = new CSP<int>();
            for (int i = 1; i <= count; i++)
            {
                List<int> domain = new List<int>
                {
                    1,
                    2
                };
                csp.AddVariable("V" + i.ToString(), domain);
            }

            Constraint<int> notEqual = delegate (int a, int b)
            {
                return a != b;
            };
            for (int i = 1; i < count; i++)
            {
                string var1 = "V" + i.ToString();
                string var2 = "V" + (i + 1).ToString();
                csp.AddConstraint(var1, var2, notEqual);
                csp.AddConstraint(var2, var1, notEqual);
            }

            bool result = AC3.EnforceArcConsistency(csp);
            Assert.IsTrue(result, "AC3 should succeed on large chained CSP.");
            for (int i = 1; i <= count; i++)
            {
                CollectionAssert.AreEquivalent(new List<int> { 1, 2 }, csp.Domains["V" + i.ToString()]);
            }
        }
    }
}