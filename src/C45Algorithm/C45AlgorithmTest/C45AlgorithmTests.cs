using Microsoft.VisualStudio.TestTools.UnitTesting;
using C45Algorithm;
using System;
using System.Collections.Generic;

namespace C45AlgorithmTest
{
    [TestClass]
    public class C45AlgorithmTests
    {
        [TestMethod]
        public void Test_AllSameLabel()
        {
            double[][] features = new double[3][];
            features[0] = new double[]
            {
                1.0,
                2.0
            };
            features[1] = new double[]
            {
                1.5,
                2.5
            };
            features[2] = new double[]
            {
                0.5,
                1.5
            };
            string[] labels = new string[]
            {
                "A",
                "A",
                "A"
            };
            bool[] isContinuous = new bool[]
            {
                true,
                false
            };
            string[] featureNames = new string[]
            {
                "Feature0",
                "Feature1"
            };
            C45Algorithm.C45Algorithm algorithm = new C45Algorithm.C45Algorithm();
            DecisionTreeNode tree = algorithm.BuildTree(features, labels, isContinuous, featureNames);
            Assert.IsNotNull(tree);
            Assert.IsTrue(tree.IsLeaf);
            Assert.IsNotNull(tree.ClassLabel);
            Assert.AreEqual("A", tree.ClassLabel!);
        }

        [TestMethod]
        public void Test_DiscreteSplit()
        {
            double[][] features = new double[4][];
            features[0] = new double[]
            {
                0
            };
            features[1] = new double[]
            {
                1
            };
            features[2] = new double[]
            {
                0
            };
            features[3] = new double[]
            {
                1
            };
            string[] labels = new string[]
            {
                "A",
                "B",
                "A",
                "B"
            };
            bool[] isContinuous = new bool[]
            {
                false
            };
            string[] featureNames = new string[]
            {
                "X"
            };
            C45Algorithm.C45Algorithm algorithm = new C45Algorithm.C45Algorithm();
            DecisionTreeNode tree = algorithm.BuildTree(features, labels, isContinuous, featureNames);
            Assert.IsNotNull(tree);
            Assert.IsFalse(tree.IsLeaf);
            Assert.AreEqual("X", tree.FeatureName!);
            Assert.IsFalse(tree.IsContinuous);
            Assert.AreEqual(0, tree.SplittingFeatureIndex);
            Assert.IsNotNull(tree.Children);
            Assert.IsTrue(tree.Children.ContainsKey("0"));
            Assert.IsTrue(tree.Children.ContainsKey("1"));
            DecisionTreeNode child0 = tree.Children["0"];
            DecisionTreeNode child1 = tree.Children["1"];
            Assert.IsTrue(child0.IsLeaf);
            Assert.IsTrue(child1.IsLeaf);
            Assert.IsNotNull(child0.ClassLabel);
            Assert.IsNotNull(child1.ClassLabel);
            Assert.AreEqual("A", child0.ClassLabel!);
            Assert.AreEqual("B", child1.ClassLabel!);
        }

        [TestMethod]
        public void Test_ContinuousSplit()
        {
            double[][] features = new double[4][];
            features[0] = new double[]
            {
                2.0
            };
            features[1] = new double[]
            {
                3.0
            };
            features[2] = new double[]
            {
                7.0
            };
            features[3] = new double[]
            {
                8.0
            };
            string[] labels = new string[]
            {
                "Low",
                "Low",
                "High",
                "High"
            };
            bool[] isContinuous = new bool[]
            {
                true
            };
            string[] featureNames = new string[]
            {
                "X"
            };
            C45Algorithm.C45Algorithm algorithm = new C45Algorithm.C45Algorithm();
            DecisionTreeNode tree = algorithm.BuildTree(features, labels, isContinuous, featureNames);
            Assert.IsNotNull(tree);
            Assert.IsFalse(tree.IsLeaf);
            Assert.IsTrue(tree.IsContinuous);
            Assert.IsNotNull(tree.Threshold);
            double actualThreshold = tree.Threshold!.Value;
            Assert.AreEqual(5.0, actualThreshold, 0.0001);
            Assert.AreEqual("X", tree.FeatureName!);
            Assert.AreEqual(0, tree.SplittingFeatureIndex);
            DecisionTreeNode leftChild = tree.Left!;
            Assert.IsNotNull(leftChild);
            Assert.IsTrue(leftChild.IsLeaf);
            Assert.IsNotNull(leftChild.ClassLabel);
            Assert.AreEqual("Low", leftChild.ClassLabel!);
            DecisionTreeNode rightChild = tree.Right!;
            Assert.IsNotNull(rightChild);
            Assert.IsTrue(rightChild.IsLeaf);
            Assert.IsNotNull(rightChild.ClassLabel);
            Assert.AreEqual("High", rightChild.ClassLabel!);
        }

        [TestMethod]
        public void Test_NoValidSplit_ReturnsMajorityLeaf()
        {
            double[][] features = new double[3][];
            features[0] = new double[]
            {
                5.0
            };
            features[1] = new double[]
            {
                5.0
            };
            features[2] = new double[]
            {
                5.0
            };
            string[] labels = new string[]
            {
                "A",
                "B",
                "A"
            };
            bool[] isContinuous = new bool[]
            {
                true
            };
            string[] featureNames = new string[]
            {
                "X"
            };
            C45Algorithm.C45Algorithm algorithm = new C45Algorithm.C45Algorithm();
            DecisionTreeNode tree = algorithm.BuildTree(features, labels, isContinuous, featureNames);
            Assert.IsNotNull(tree);
            Assert.IsTrue(tree.IsLeaf);
            Assert.IsNotNull(tree.ClassLabel);
            Assert.AreEqual("A", tree.ClassLabel!);
        }

        [TestMethod]
        public void Test_LargeRandomDataset()
        {
            const int sampleCount = 200;
            const int featureCount = 2;
            double[][] features = new double[sampleCount][];
            string[] labels = new string[sampleCount];
            bool[] isContinuous = new bool[featureCount];
            for (int i = 0; i < featureCount; i++)
            {
                isContinuous[i] = true;
            }

            string[] featureNames = new string[]
            {
                "F1",
                "F2"
            };
            Random random = new Random(42);
            for (int i = 0; i < sampleCount; i++)
            {
                features[i] = new double[featureCount];
                for (int j = 0; j < featureCount; j++)
                {
                    features[i][j] = random.NextDouble() * 10.0;
                }

                double sum = features[i][0] + features[i][1];
                labels[i] = (sum < 10.0) ? "A" : "B";
            }

            C45Algorithm.C45Algorithm algorithm = new C45Algorithm.C45Algorithm();
            DecisionTreeNode tree = algorithm.BuildTree(features, labels, isContinuous, featureNames);
            Assert.IsNotNull(tree);
        }
    }
}