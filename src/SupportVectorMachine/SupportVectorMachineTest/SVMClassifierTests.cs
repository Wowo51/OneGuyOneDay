using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupportVectorMachine;

namespace SupportVectorMachineTest
{
    [TestClass]
    public class SVMClassifierTests
    {
        [TestMethod]
        public void TestSimpleTraining()
        {
            double[][] features = new double[2][];
            features[0] = new double[2]
            {
                1.0,
                1.0
            };
            features[1] = new double[2]
            {
                -1.0,
                -1.0
            };
            int[] labels = new int[2]
            {
                1,
                -1
            };
            SVMClassifier classifier = new SVMClassifier(2, 0.01, 0.01, 300);
            classifier.Train(features, labels);
            int predictionPositive = classifier.Predict(new double[2] { 1.0, 1.0 });
            int predictionNegative = classifier.Predict(new double[2] { -1.0, -1.0 });
            Assert.AreEqual(1, predictionPositive, "The classifier should predict 1 for [1,1].");
            Assert.AreEqual(-1, predictionNegative, "The classifier should predict -1 for [-1,-1].");
        }

        [TestMethod]
        public void TestSyntheticLargeDataset()
        {
            int sampleCount = 500;
            int featureCount = 2;
            double[][] features = new double[sampleCount][];
            int[] labels = new int[sampleCount];
            Random random = new Random(42);
            for (int i = 0; i < sampleCount; i++)
            {
                features[i] = new double[featureCount];
                for (int j = 0; j < featureCount; j++)
                {
                    features[i][j] = random.NextDouble() * 20 - 10;
                }

                double sum = features[i][0] + features[i][1];
                labels[i] = (sum > 0) ? 1 : -1;
            }

            SVMClassifier classifier = new SVMClassifier(featureCount, 0.001, 0.01, 500);
            classifier.Train(features, labels);
            int errorCount = 0;
            for (int i = 0; i < sampleCount; i++)
            {
                int prediction = classifier.Predict(features[i]);
                if (prediction != labels[i])
                {
                    errorCount++;
                }
            }

            double errorRate = (double)errorCount / sampleCount;
            Assert.IsTrue(errorRate < 0.15, "Error rate is too high: " + errorRate.ToString());
        }

        [TestMethod]
        public void TestGetWeightsAndBias()
        {
            SVMClassifier classifier = new SVMClassifier(2, 0.01, 0.01, 100);
            double[] weights = classifier.GetWeights();
            double bias = classifier.GetBias();
            Assert.AreEqual(0.0, bias, "Initial bias should be 0.");
            Assert.AreEqual(0.0, weights[0], "Initial weight[0] should be 0.");
            Assert.AreEqual(0.0, weights[1], "Initial weight[1] should be 0.");
        }
    }
}