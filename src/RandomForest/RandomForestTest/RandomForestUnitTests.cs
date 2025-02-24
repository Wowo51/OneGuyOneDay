using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomForest;
using RF = RandomForest.RandomForest;

namespace RandomForestTest
{
    [TestClass]
    public class DecisionTreeTests
    {
        [TestMethod]
        public void TestDecisionTree_WithEmptyData_ReturnsZero()
        {
            DecisionTree tree = new DecisionTree();
            List<DataPoint> trainingData = new List<DataPoint>();
            tree.Train(trainingData);
            double[] testFeatures = new double[]
            {
                0.0
            };
            int prediction = tree.Predict(testFeatures);
            Assert.AreEqual(0, prediction, "Empty training data should result in default label 0.");
        }

        [TestMethod]
        public void TestDecisionTree_WithUniformLabel_ReturnsSameLabel()
        {
            DecisionTree tree = new DecisionTree();
            List<DataPoint> trainingData = new List<DataPoint>
            {
                new DataPoint(new double[] { 1.0, 2.0 }, 1),
                new DataPoint(new double[] { 1.5, 2.5 }, 1),
                new DataPoint(new double[] { 2.0, 3.0 }, 1)
            };
            tree.Train(trainingData);
            double[] testFeatures = new double[]
            {
                1.0,
                2.0
            };
            int prediction = tree.Predict(testFeatures);
            Assert.AreEqual(1, prediction, "Uniform label training data should result in predicted label 1.");
        }

        [TestMethod]
        public void TestDecisionTree_WithMajorityLabel_ReturnsMostCommon()
        {
            DecisionTree tree = new DecisionTree();
            List<DataPoint> trainingData = new List<DataPoint>
            {
                new DataPoint(new double[] { 1.0 }, 1),
                new DataPoint(new double[] { 1.1 }, 1),
                new DataPoint(new double[] { 1.2 }, 2),
                new DataPoint(new double[] { 1.3 }, 1)
            };
            tree.Train(trainingData);
            double[] testFeatures = new double[]
            {
                1.0
            };
            int prediction = tree.Predict(testFeatures);
            Assert.AreEqual(1, prediction, "Majority label should be predicted.");
        }
    }

    [TestClass]
    public class RandomForestTests
    {
        private List<DataPoint> CreateUniformDataPoints(int count, int label)
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            for (int i = 0; i < count; i++)
            {
                double[] features = new double[]
                {
                    (double)i,
                    (double)(i + 1)
                };
                dataPoints.Add(new DataPoint(features, label));
            }

            return dataPoints;
        }

        private List<DataPoint> CreateMixedDataPoints(int count, int majorityLabel, int minorityLabel)
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            int majorityCount = (int)(count * 0.7);
            int minorityCount = count - majorityCount;
            for (int i = 0; i < majorityCount; i++)
            {
                double[] features = new double[]
                {
                    (double)i,
                    (double)(i + 1)
                };
                dataPoints.Add(new DataPoint(features, majorityLabel));
            }

            for (int i = 0; i < minorityCount; i++)
            {
                double[] features = new double[]
                {
                    (double)i,
                    (double)(i + 1)
                };
                dataPoints.Add(new DataPoint(features, minorityLabel));
            }

            return dataPoints;
        }

        [TestMethod]
        public void TestRandomForest_WithEmptyData_ReturnsZero()
        {
            RF forest = new RF(5);
            List<DataPoint> trainingData = new List<DataPoint>();
            forest.Train(trainingData);
            double[] testFeatures = new double[]
            {
                0.0
            };
            int prediction = forest.Predict(testFeatures);
            Assert.AreEqual(0, prediction, "Empty training data should result in default label 0.");
        }

        [TestMethod]
        public void TestRandomForest_WithUniformData_ReturnsUniformLabel()
        {
            int label = 2;
            List<DataPoint> trainingData = CreateUniformDataPoints(10, label);
            RF forest = new RF(10);
            forest.Train(trainingData);
            double[] testFeatures = new double[]
            {
                5.0,
                6.0
            };
            int prediction = forest.Predict(testFeatures);
            Assert.AreEqual(label, prediction, "All trees trained with uniform label should predict that label.");
        }

        [TestMethod]
        public void TestRandomForest_WithSyntheticMixedData_PredictsMajorityLabel()
        {
            List<DataPoint> trainingData = CreateMixedDataPoints(100, 3, 1);
            RF forest = new RF(15);
            forest.Train(trainingData);
            double[] testFeatures = new double[]
            {
                10.0,
                11.0
            };
            int prediction = forest.Predict(testFeatures);
            Assert.AreEqual(3, prediction, "Majority label should be predicted by the RandomForest.");
        }
    }
}