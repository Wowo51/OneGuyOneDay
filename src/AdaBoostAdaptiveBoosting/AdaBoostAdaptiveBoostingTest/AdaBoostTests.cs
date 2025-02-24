using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AdaBoostAdaptiveBoosting;

namespace AdaBoostAdaptiveBoostingTest
{
    [TestClass]
    public class AdaBoostTests
    {
        [TestMethod]
        public void TestPredictWithoutTraining()
        {
            AdaBoost adaBoost = new AdaBoost();
            double[] sample = new double[]
            {
                5.0
            };
            int prediction = adaBoost.Predict(sample);
            // Without training, prediction returns based on default accumulator sum.
            Assert.IsTrue(prediction == 1 || prediction == -1, "Prediction should be either 1 or -1");
        }

        [TestMethod]
        public void TestTrainingWithValidData()
        {
            double[][] features = new double[][]
            {
                new double[]
                {
                    1.0
                },
                new double[]
                {
                    2.0
                },
                new double[]
                {
                    3.0
                },
                new double[]
                {
                    4.0
                }
            };
            int[] labels = new int[]
            {
                1,
                1,
                -1,
                -1
            };
            AdaBoost adaBoost = new AdaBoost();
            adaBoost.Train(features, labels, 10);
            for (int i = 0; i < features.Length; i++)
            {
                int prediction = adaBoost.Predict(features[i]);
                Assert.IsTrue(prediction == 1 || prediction == -1, $"Prediction for index {i} should be 1 or -1");
            }
        }

        [TestMethod]
        public void TestTrainingWithNullInput()
        {
            AdaBoost adaBoost = new AdaBoost();
            adaBoost.Train(null, null, 5);
            int prediction = adaBoost.Predict(new double[] { 1.0 });
            Assert.IsTrue(prediction == 1 || prediction == -1, "Prediction should be either 1 or -1 even when training input is null");
        }

        [TestMethod]
        public void TestTrainingWithEmptyData()
        {
            double[][] features = new double[0][];
            int[] labels = new int[0];
            AdaBoost adaBoost = new AdaBoost();
            adaBoost.Train(features, labels, 5);
            int prediction = adaBoost.Predict(new double[] { 1.0 });
            Assert.IsTrue(prediction == 1 || prediction == -1, "Prediction should be either 1 or -1 even when training data is empty");
        }

        [TestMethod]
        public void TestSyntheticLargeDataset()
        {
            int sampleCount = 100;
            double[][] features = new double[sampleCount][];
            int[] labels = new int[sampleCount];
            Random random = new Random(0);
            for (int i = 0; i < sampleCount; i++)
            {
                double value = random.NextDouble();
                features[i] = new double[]
                {
                    value
                };
                labels[i] = (value >= 0.5) ? 1 : -1;
            }

            AdaBoost adaBoost = new AdaBoost();
            adaBoost.Train(features, labels, 20);
            int correct = 0;
            for (int i = 0; i < sampleCount; i++)
            {
                int prediction = adaBoost.Predict(features[i]);
                if (prediction == labels[i])
                {
                    correct++;
                }
            }

            double accuracy = (double)correct / sampleCount;
            Assert.IsTrue(accuracy >= 0.7, "Synthetic dataset accuracy should be at least 70%");
        }

        [TestMethod]
        public void TestTrainingWithMismatchedData()
        {
            double[][] features = new double[][]
            {
                new double[]
                {
                    1.0
                },
                new double[]
                {
                    2.0
                }
            };
            int[] labels = new int[]
            {
                1
            };
            AdaBoost adaBoost = new AdaBoost();
            adaBoost.Train(features, labels, 10);
            int prediction = adaBoost.Predict(new double[] { 1.0 });
            Assert.IsTrue(prediction == 1 || prediction == -1, "Prediction with mismatched data should be either 1 or -1");
        }
    }
}