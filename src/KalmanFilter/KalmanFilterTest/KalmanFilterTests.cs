using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KalmanFilter;

namespace KalmanFilterTest
{
    [TestClass]
    public class KalmanFilterTests
    {
        private void AssertArrayAreEqual(double[] expected, double[] actual, double tolerance)
        {
            int expectedLength = expected.Length;
            int actualLength = actual.Length;
            Assert.AreEqual(expectedLength, actualLength, "Array lengths differ.");
            for (int index = 0; index < expectedLength; index++)
            {
                double diff = Math.Abs(expected[index] - actual[index]);
                Assert.IsTrue(diff < tolerance, $"Arrays differ at index {index}: Expected {expected[index]}, got {actual[index]}.");
            }
        }

        [TestMethod]
        public void TestPredictWithoutControl()
        {
            double[, ] A = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] H = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] Q = new double[, ]
            {
                {
                    0.1
                }
            };
            double[, ] R = new double[, ]
            {
                {
                    0.1
                }
            };
            double[] x0 = new double[]
            {
                1.0
            };
            double[, ] P0 = new double[, ]
            {
                {
                    1.0
                }
            };
            KalmanFilter.KalmanFilter filter = new KalmanFilter.KalmanFilter(A, H, Q, R, x0, P0);
            double[] predicted = filter.Predict(null);
            double[] expected = new double[]
            {
                1.0
            }; // A*x0 = 1*1 = 1
            AssertArrayAreEqual(expected, predicted, 1e-6);
        }

        [TestMethod]
        public void TestPredictWithControl()
        {
            double[, ] A = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] H = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] Q = new double[, ]
            {
                {
                    0.1
                }
            };
            double[, ] R = new double[, ]
            {
                {
                    0.1
                }
            };
            double[] x0 = new double[]
            {
                1.0
            };
            double[, ] P0 = new double[, ]
            {
                {
                    1.0
                }
            };
            KalmanFilter.KalmanFilter filter = new KalmanFilter.KalmanFilter(A, H, Q, R, x0, P0);
            double[, ] B = new double[, ]
            {
                {
                    1.0
                }
            };
            filter.ControlMatrix = B;
            double[] predicted = filter.Predict(new double[] { 2.0 });
            // Expected state: A*x0 + B*controlInput = 1 + 2 = 3
            double[] expected = new double[]
            {
                3.0
            };
            AssertArrayAreEqual(expected, predicted, 1e-6);
        }

        [TestMethod]
        public void TestUpdateWithMeasurement()
        {
            double[, ] A = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] H = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] Q = new double[, ]
            {
                {
                    0.1
                }
            };
            double[, ] R = new double[, ]
            {
                {
                    0.1
                }
            };
            double[] x0 = new double[]
            {
                1.0
            };
            double[, ] P0 = new double[, ]
            {
                {
                    1.0
                }
            };
            KalmanFilter.KalmanFilter filter = new KalmanFilter.KalmanFilter(A, H, Q, R, x0, P0);
            filter.Predict(null);
            double[] updated = filter.Update(new double[] { 2.0 });
            // Calculation details for 1D:
            // After predict, state = 1.0 and covariance becomes: 1*1 + 0.1 = 1.1.
            // Innovation = 2.0 - (1*1) = 1.0.
            // Innovation covariance = 1.1 + 0.1 = 1.2.
            // Kalman gain = 1.1 / 1.2 ≈ 0.916667.
            // Corrected state = 1.0 + (0.916667 * 1.0) ≈ 1.916667.
            double[] expected = new double[]
            {
                1.916667
            };
            AssertArrayAreEqual(expected, updated, 1e-5);
        }

        [TestMethod]
        public void TestUpdateNoMeasurement()
        {
            double[, ] A = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] H = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] Q = new double[, ]
            {
                {
                    0.1
                }
            };
            double[, ] R = new double[, ]
            {
                {
                    0.1
                }
            };
            double[] x0 = new double[]
            {
                1.0
            };
            double[, ] P0 = new double[, ]
            {
                {
                    1.0
                }
            };
            KalmanFilter.KalmanFilter filter = new KalmanFilter.KalmanFilter(A, H, Q, R, x0, P0);
            double[] stateBeforeUpdate = filter.Predict(null);
            double[] updated = filter.Update(null);
            AssertArrayAreEqual(stateBeforeUpdate, updated, 1e-6);
        }

        [TestMethod]
        public void TestUpdatePerfectMeasurement()
        {
            double[, ] A = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] H = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] Q = new double[, ]
            {
                {
                    0.1
                }
            };
            double[, ] R = new double[, ]
            {
                {
                    0.1
                }
            };
            double[] x0 = new double[]
            {
                1.0
            };
            double[, ] P0 = new double[, ]
            {
                {
                    1.0
                }
            };
            KalmanFilter.KalmanFilter filter = new KalmanFilter.KalmanFilter(A, H, Q, R, x0, P0);
            double[] predicted = filter.Predict(null);
            // Generate a perfect measurement: H * predicted
            double[] measurement = MatrixMath.MultiplyMatrixVector(H, predicted);
            double[] updated = filter.Update(measurement);
            // With zero innovation, the state should remain unchanged.
            AssertArrayAreEqual(predicted, updated, 1e-6);
        }

        [TestMethod]
        public void TestUpdateWithNoninvertibleInnovation()
        {
            // In this test, we force innovation covariance to be noninvertible.
            // Set Q and R to zero and initial covariance to zero.
            double[, ] A = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] H = new double[, ]
            {
                {
                    1.0
                }
            };
            double[, ] Q = new double[, ]
            {
                {
                    0.0
                }
            };
            double[, ] R = new double[, ]
            {
                {
                    0.0
                }
            };
            double[] x0 = new double[]
            {
                5.0
            };
            double[, ] P0 = new double[, ]
            {
                {
                    0.0
                }
            };
            KalmanFilter.KalmanFilter filter = new KalmanFilter.KalmanFilter(A, H, Q, R, x0, P0);
            filter.Predict(null);
            double[] updated = filter.Update(new double[] { 10.0 });
            // When innovation covariance is zero, matrix inversion fails so update returns the current state.
            AssertArrayAreEqual(x0, updated, 1e-6);
        }

        [TestMethod]
        public void TestSyntheticLargeData()
        {
            int size = 5;
            System.Random random = new System.Random(1);
            double[, ] A = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    A[i, j] = (i == j) ? 1.0 : random.NextDouble() * 0.01;
                }
            }

            double[, ] H = MatrixMath.IdentityMatrix(size);
            double[, ] Q = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Q[i, j] = (i == j) ? 0.1 : 0.0;
                }
            }

            double[, ] R = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    R[i, j] = (i == j) ? 0.1 : 0.0;
                }
            }

            double[, ] P0 = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    P0[i, j] = (i == j) ? 1.0 : 0.0;
                }
            }

            double[] x0 = new double[size];
            for (int i = 0; i < size; i++)
            {
                x0[i] = random.NextDouble() * 10.0;
            }

            KalmanFilter.KalmanFilter filter = new KalmanFilter.KalmanFilter(A, H, Q, R, x0, P0);
            for (int iter = 0; iter < 10; iter++)
            {
                double[] predicted = filter.Predict(null);
                double[] noise = new double[size];
                for (int i = 0; i < size; i++)
                {
                    noise[i] = (random.NextDouble() - 0.5) * 0.1;
                }

                double[] measurement = MatrixMath.AddVectors(predicted, noise);
                double[] updated = filter.Update(measurement);
                Assert.AreEqual(size, predicted.Length, "Predicted state length mismatch.");
                Assert.AreEqual(size, updated.Length, "Updated state length mismatch.");
                for (int i = 0; i < size; i++)
                {
                    Assert.IsFalse(double.IsNaN(predicted[i]), "Predicted state contains NaN.");
                    Assert.IsFalse(double.IsNaN(updated[i]), "Updated state contains NaN.");
                }
            }
        }
    }
}