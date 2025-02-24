using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogitBoost;

namespace LogitBoostTest
{
    [TestClass]
    public class LogitBoostTests
    {
        [TestMethod]
        public void TestNullInputs()
        {
            LogitBoostAlgorithm algorithm = new LogitBoostAlgorithm();
            // Calling Fit with null arrays should not throw and PredictProbability with null returns 0.0.
            algorithm.Fit(null, null, 5);
            double probability = algorithm.PredictProbability(null);
            Assert.AreEqual(0.0, probability, 1e-6, "PredictProbability with null input should return 0.0.");
        }

        [TestMethod]
        public void TestDecisionStump_NullPrediction()
        {
            DecisionStump stump = new DecisionStump();
            double prediction = stump.Predict(null);
            Assert.AreEqual(0.0, prediction, 1e-6, "DecisionStump.Predict with null input should return 0.0.");
        }

        [TestMethod]
        public void TestTrivialClassification()
        {
            double[][] X = new double[][]
            {
                new double[]
                {
                    1.0,
                    2.0
                },
                new double[]
                {
                    2.0,
                    1.0
                },
                new double[]
                {
                    3.0,
                    2.0
                },
                new double[]
                {
                    4.0,
                    1.0
                }
            };
            int[] y = new int[]
            {
                0,
                0,
                1,
                1
            };
            LogitBoostAlgorithm algorithm = new LogitBoostAlgorithm();
            algorithm.Fit(X, y, 10);
            for (int i = 0; i < X.Length; i++)
            {
                int prediction = algorithm.Predict(X[i]);
                Assert.IsTrue(prediction == 0 || prediction == 1, "Prediction must be 0 or 1.");
                double probability = algorithm.PredictProbability(X[i]);
                Assert.IsTrue(probability >= 0.0 && probability <= 1.0, "Probability must be between 0 and 1.");
            }
        }

        [TestMethod]
        public void TestPredictProbabilityRange()
        {
            double[] sample = new double[]
            {
                1.0,
                -1.0
            };
            LogitBoostAlgorithm algorithm = new LogitBoostAlgorithm();
            double probability = algorithm.PredictProbability(sample);
            Assert.IsTrue(probability >= 0.0 && probability <= 1.0, "Probability must be between 0 and 1.");
        }

        [TestMethod]
        public void TestLargeSyntheticDataset()
        {
            int sampleCount = 1000;
            int featureCount = 2;
            double[][] X = new double[sampleCount][];
            int[] y = new int[sampleCount];
            Random random = new Random(42);
            for (int i = 0; i < sampleCount; i++)
            {
                X[i] = new double[featureCount];
                X[i][0] = random.NextDouble() * 10.0;
                X[i][1] = random.NextDouble() * 10.0;
                // Simple decision rule: class 1 if the sum of features > 10, else class 0.
                y[i] = ((X[i][0] + X[i][1]) > 10.0) ? 1 : 0;
            }

            LogitBoostAlgorithm algorithm = new LogitBoostAlgorithm();
            algorithm.Fit(X, y, 50);
            int correct = 0;
            for (int i = 0; i < sampleCount; i++)
            {
                int prediction = algorithm.Predict(X[i]);
                if (prediction == y[i])
                {
                    correct++;
                }
            }

            double accuracy = (double)correct / sampleCount;
            // Expecting at least 70% accuracy on the synthetic dataset.
            Assert.IsTrue(accuracy >= 0.70, "Accuracy should be at least 70% on the synthetic dataset.");
        }

        [TestMethod]
        public void TestConsistencyOfPredictMethod()
        {
            double[][] X = new double[][]
            {
                new double[]
                {
                    2.0,
                    3.0
                },
                new double[]
                {
                    4.0,
                    1.0
                }
            };
            int[] y = new int[]
            {
                0,
                1
            };
            LogitBoostAlgorithm algorithm = new LogitBoostAlgorithm();
            algorithm.Fit(X, y, 5);
            double probability1 = algorithm.PredictProbability(X[0]);
            double probability2 = algorithm.PredictProbability(X[0]);
            Assert.AreEqual(probability1, probability2, 1e-6, "PredictProbability should return consistent values for the same input.");
        }

        [TestMethod]
        public void TestDecisionStump_Boundary()
        {
            DecisionStump stump = new DecisionStump();
            stump.FeatureIndex = 0;
            stump.Threshold = 2.5;
            stump.LeftValue = 10.0;
            stump.RightValue = 20.0;
            double predictionEqual = stump.Predict(new double[] { 2.5 });
            double predictionBelow = stump.Predict(new double[] { 2.49 });
            double predictionAbove = stump.Predict(new double[] { 2.51 });
            Assert.AreEqual(10.0, predictionEqual, 1e-6, "Boundary test: equal threshold should return left value.");
            Assert.AreEqual(10.0, predictionBelow, 1e-6, "Boundary test: below threshold should return left value.");
            Assert.AreEqual(20.0, predictionAbove, 1e-6, "Boundary test: above threshold should return right value.");
        }

        [TestMethod]
        public void TestPredictWithoutFit()
        {
            LogitBoostAlgorithm algorithm = new LogitBoostAlgorithm();
            double probability = algorithm.PredictProbability(new double[] { 1.0, 2.0 });
            int prediction = algorithm.Predict(new double[] { 1.0, 2.0 });
            Assert.AreEqual(0.5, probability, 1e-6, "PredictProbability without fit should return 0.5.");
            Assert.AreEqual(1, prediction, "Predict without fit should return 1 as probability is 0.5.");
        }
    }
}