using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartialLeastSquares;

namespace PartialLeastSquaresTest
{
    [TestClass]
    public class PLSRegressionTests
    {
        private void AssertDoubleArraysAreEqual(double[] expected, double[] actual, double tolerance)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Array lengths differ.");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(Math.Abs(expected[i] - actual[i]) <= tolerance, $"Arrays differ at index {i}: expected {expected[i]}, got {actual[i]}");
            }
        }

        private void AssertMatrixAreEqual(double[][] expected, double[][] actual, double tolerance)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Matrix row counts differ.");
            for (int i = 0; i < expected.Length; i++)
            {
                AssertDoubleArraysAreEqual(expected[i], actual[i], tolerance);
            }
        }

        [TestMethod]
        public void TestPredictWithNullX()
        {
            PLSRegression regression = new PLSRegression(1);
            double[][] predicted = regression.Predict(null);
            Assert.AreEqual(0, predicted.Length, "Predict should return empty array when X is null.");
        }

        [TestMethod]
        public void TestPLSRegression_Simple()
        {
            double[][] X = new double[][]
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
            double[][] Y = new double[][]
            {
                new double[]
                {
                    2.0
                },
                new double[]
                {
                    4.0
                },
                new double[]
                {
                    6.0
                },
                new double[]
                {
                    8.0
                }
            };
            PLSRegression regression = new PLSRegression(1);
            regression.Fit(X, Y);
            double[][] predicted = regression.Predict(X);
            for (int i = 0; i < Y.Length; i++)
            {
                Assert.AreEqual(Y[i][0], predicted[i][0], 1e-3, $"Prediction at index {i} differs significantly.");
            }
        }

        [TestMethod]
        public void TestPLSRegression_LargeData()
        {
            int numObservations = 100;
            int numPredictors = 5;
            double[][] X = new double[numObservations][];
            double[][] Y = new double[numObservations][];
            Random random = new Random(42);
            double[] v = new double[numObservations];
            for (int i = 0; i < numObservations; i++)
            {
                v[i] = random.NextDouble();
            }

            for (int i = 0; i < numObservations; i++)
            {
                X[i] = new double[numPredictors];
                X[i][0] = v[i];
                X[i][1] = 2 * v[i];
                X[i][2] = 3 * v[i];
                X[i][3] = 4 * v[i];
                X[i][4] = 5 * v[i];
                Y[i] = new double[]
                {
                    15 * v[i]
                };
            }

            PLSRegression regression = new PLSRegression(1);
            regression.Fit(X, Y);
            double[][] predicted = regression.Predict(X);
            double maxError = 0.0;
            for (int i = 0; i < numObservations; i++)
            {
                double error = Math.Abs(predicted[i][0] - Y[i][0]);
                if (error > maxError)
                {
                    maxError = error;
                }
            }

            Assert.IsTrue(maxError < 1e-6, $"Max prediction error {maxError} is too high.");
        }
    }
}