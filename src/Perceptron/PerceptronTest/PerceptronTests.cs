using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Perceptron;
using Pcp = Perceptron.Perceptron;

namespace PerceptronTest
{
    [TestClass]
    public class PerceptronTests
    {
        [TestMethod]
        public void TestPredict_NullInput_ReturnsZero()
        {
            Pcp perceptron = new Pcp(2, 0.1);
            int prediction = perceptron.Predict(null);
            Assert.AreEqual(0, prediction);
        }

        [TestMethod]
        public void TestPredict_InvalidInputLength_ReturnsZero()
        {
            Pcp perceptron = new Pcp(2, 0.1);
            double[] invalidInput = new double[]
            {
                1.0
            };
            int prediction = perceptron.Predict(invalidInput);
            Assert.AreEqual(0, prediction);
        }

        [TestMethod]
        public void TestPredict_BasicThreshold()
        {
            Pcp perceptron = new Pcp(2, 0.1);
            double[] testInput = new double[]
            {
                1.0,
                1.0
            };
            int prediction = perceptron.Predict(testInput);
            Assert.AreEqual(1, prediction);
        }

        [TestMethod]
        public void TestTrain_UpdatesBias()
        {
            Pcp perceptron = new Pcp(2, 0.1);
            double[][] trainingInputs = new double[][]
            {
                new double[]
                {
                    0.0,
                    0.0
                }
            };
            int[] labels = new int[]
            {
                1
            };
            double initialBias = perceptron.Bias;
            perceptron.Train(trainingInputs, labels, 1);
            Assert.AreEqual(initialBias, perceptron.Bias);
            double[][] trainingInputs2 = new double[][]
            {
                new double[]
                {
                    1.0,
                    1.0
                }
            };
            int[] labels2 = new int[]
            {
                0
            };
            double oldBias = perceptron.Bias;
            perceptron.Train(trainingInputs2, labels2, 1);
            Assert.IsTrue(perceptron.Bias < oldBias);
        }

        [TestMethod]
        public void TestTrain_CorrectsPredictions()
        {
            Pcp perceptron = new Pcp(2, 0.1);
            double[][] trainingInputs = new double[][]
            {
                new double[]
                {
                    0.0,
                    0.0
                },
                new double[]
                {
                    0.0,
                    1.0
                },
                new double[]
                {
                    1.0,
                    0.0
                },
                new double[]
                {
                    1.0,
                    1.0
                }
            };
            int[] labels = new int[]
            {
                0,
                0,
                0,
                1
            };
            int epochs = 100;
            perceptron.Train(trainingInputs, labels, epochs);
            for (int i = 0; i < trainingInputs.Length; i++)
            {
                int prediction = perceptron.Predict(trainingInputs[i]);
                Assert.AreEqual(labels[i], prediction, "Mismatch at index " + i);
            }
        }

        [TestMethod]
        public void TestTrain_WithSyntheticData()
        {
            int numInputs = 3;
            Pcp perceptron = new Pcp(numInputs, 0.05);
            const int sampleSize = 200;
            double[][] trainingInputs = new double[sampleSize][];
            int[] labels = new int[sampleSize];
            double[] trueWeights = new double[]
            {
                0.5,
                -0.4,
                0.3
            };
            double trueBias = -0.1;
            Random random = new Random(42);
            for (int i = 0; i < sampleSize; i++)
            {
                trainingInputs[i] = new double[numInputs];
                double sum = trueBias;
                for (int j = 0; j < numInputs; j++)
                {
                    trainingInputs[i][j] = random.NextDouble() * 2 - 1;
                    sum += trainingInputs[i][j] * trueWeights[j];
                }

                labels[i] = (sum >= 0) ? 1 : 0;
            }

            int epochs = 50;
            perceptron.Train(trainingInputs, labels, epochs);
            int correct = 0;
            for (int i = 0; i < sampleSize; i++)
            {
                int prediction = perceptron.Predict(trainingInputs[i]);
                if (prediction == labels[i])
                {
                    correct++;
                }
            }

            double accuracy = (double)correct / sampleSize;
            Assert.IsTrue(accuracy >= 0.70, "Accuracy below expected threshold: " + accuracy.ToString("P"));
        }

        [TestMethod]
        public void TestTrain_NullParameters_NoException()
        {
            Pcp perceptron = new Pcp(2, 0.1);
            perceptron.Train(null, null, 1);
            perceptron.Train(new double[][] { (double[]? )null }, new int[] { 0 }, 1);
            Assert.IsTrue(true);
        }
    }
}