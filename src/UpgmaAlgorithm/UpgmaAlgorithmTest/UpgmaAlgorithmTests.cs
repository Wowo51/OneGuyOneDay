using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpgmaAlgorithm;

namespace UpgmaAlgorithmTest
{
    [TestClass]
    public class UpgmaAlgorithmTests
    {
        [TestMethod]
        public void TestNullParameters()
        {
            TreeNode? tree = Upgma.BuildTree(null, null);
            Assert.IsNull(tree);
        }

        [TestMethod]
        public void TestMismatchedDimensions()
        {
            double[, ] matrix = new double[2, 2]
            {
                {
                    0,
                    1
                },
                {
                    1,
                    0
                }
            };
            string[] labels = new string[]
            {
                "A",
                "B",
                "C"
            };
            TreeNode? tree = Upgma.BuildTree(matrix, labels);
            Assert.IsNull(tree);
            matrix = new double[3, 3]
            {
                {
                    0,
                    2,
                    3
                },
                {
                    2,
                    0,
                    4
                },
                {
                    3,
                    4,
                    0
                }
            };
            labels = new string[]
            {
                "A",
                "B"
            };
            tree = Upgma.BuildTree(matrix, labels);
            Assert.IsNull(tree);
        }

        [TestMethod]
        public void TestSingleTaxon()
        {
            double[, ] matrix = new double[1, 1];
            string[] labels = new string[]
            {
                "A"
            };
            TreeNode? tree = Upgma.BuildTree(matrix, labels);
            Assert.IsNotNull(tree);
            Assert.AreEqual("A", tree!.Label);
            Assert.AreEqual(0.0, tree.Height, 0.0001);
            Assert.AreEqual(1, tree.ClusterSize);
        }

        [TestMethod]
        public void TestTwoTaxa()
        {
            double[, ] matrix = new double[2, 2]
            {
                {
                    0,
                    6
                },
                {
                    6,
                    0
                }
            };
            string[] labels = new string[]
            {
                "A",
                "B"
            };
            TreeNode? tree = Upgma.BuildTree(matrix, labels);
            Assert.IsNotNull(tree);
            Assert.AreEqual(2, tree!.ClusterSize);
            Assert.AreEqual(3.0, tree.Height, 0.0001);
            Assert.IsNotNull(tree.Left);
            Assert.IsNotNull(tree.Right);
            Assert.AreEqual("A", tree.Left!.Label);
            Assert.AreEqual("B", tree.Right!.Label);
        }

        [TestMethod]
        public void TestThreeTaxa()
        {
            double[, ] matrix = new double[3, 3]
            {
                {
                    0,
                    3,
                    4
                },
                {
                    3,
                    0,
                    5
                },
                {
                    4,
                    5,
                    0
                }
            };
            string[] labels = new string[]
            {
                "A",
                "B",
                "C"
            };
            TreeNode? tree = Upgma.BuildTree(matrix, labels);
            Assert.IsNotNull(tree);
            Assert.AreEqual(3, tree!.ClusterSize);
            Assert.AreEqual(2.25, tree.Height, 0.0001);
        }

        [TestMethod]
        public void TestRandomLargeDataset()
        {
            int numberOfTaxa = 50;
            double[, ] matrix = new double[numberOfTaxa, numberOfTaxa];
            string[] labels = new string[numberOfTaxa];
            for (int i = 0; i < numberOfTaxa; i++)
            {
                labels[i] = "Taxon" + i.ToString();
            }

            Random random = new Random(42);
            for (int i = 0; i < numberOfTaxa; i++)
            {
                for (int j = i + 1; j < numberOfTaxa; j++)
                {
                    double distance = random.NextDouble() * 100.0;
                    matrix[i, j] = distance;
                    matrix[j, i] = distance;
                }
            }

            TreeNode? tree = Upgma.BuildTree(matrix, labels);
            Assert.IsNotNull(tree);
            Assert.AreEqual(numberOfTaxa, tree!.ClusterSize);
        }
    }
}