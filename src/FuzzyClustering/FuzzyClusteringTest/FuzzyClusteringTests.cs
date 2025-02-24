using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FuzzyClustering;

namespace FuzzyClusteringTest
{
    [TestClass]
    public class FuzzyClusteringTests
    {
        [TestMethod]
        public void Test_NullPoints()
        {
            FuzzyClusteringAlgorithm algorithm = new FuzzyClusteringAlgorithm();
            double[][]? points = null;
            int clusterCount = 2;
            double fuzziness = 2.0;
            int maxIterations = 10;
            double epsilon = 0.001;
            FuzzyClusterResult result = algorithm.Cluster(points, clusterCount, fuzziness, maxIterations, epsilon);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Centers.Length, "Centers should be empty for null points.");
            Assert.AreEqual(0, result.Membership.Length, "Membership should be empty for null points.");
        }

        [TestMethod]
        public void Test_ZeroClusterCount()
        {
            FuzzyClusteringAlgorithm algorithm = new FuzzyClusteringAlgorithm();
            double[][] points = new double[][]
            {
                new double[]
                {
                    1.0,
                    1.0
                }
            };
            int clusterCount = 0;
            double fuzziness = 2.0;
            int maxIterations = 10;
            double epsilon = 0.001;
            FuzzyClusterResult result = algorithm.Cluster(points, clusterCount, fuzziness, maxIterations, epsilon);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Centers.Length, "Centers should be empty for zero cluster count.");
            Assert.AreEqual(0, result.Membership.Length, "Membership should be empty for zero cluster count.");
        }

        [TestMethod]
        public void Test_OnePointOneCluster()
        {
            FuzzyClusteringAlgorithm algorithm = new FuzzyClusteringAlgorithm();
            double[][] points = new double[][]
            {
                new double[]
                {
                    3.5,
                    4.5
                }
            };
            int clusterCount = 1;
            double fuzziness = 2.0;
            int maxIterations = 10;
            double epsilon = 0.001;
            FuzzyClusterResult result = algorithm.Cluster(points, clusterCount, fuzziness, maxIterations, epsilon);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(1, result.Centers.Length, "Should have one center.");
            Assert.AreEqual(1, result.Membership.Length, "Should have one membership row.");
            double[] center = result.Centers[0];
            Assert.AreEqual(2, center.Length, "Center dimension mismatch.");
            double tolerance = 1e-6;
            Assert.IsTrue(Math.Abs(center[0] - 3.5) < tolerance && Math.Abs(center[1] - 4.5) < tolerance, "Center does not match the input point.");
            Assert.AreEqual(1.0, result.Membership[0][0], tolerance, "Membership for the single cluster should be 1.");
        }

        [TestMethod]
        public void Test_TwoClusters_MembershipSum()
        {
            FuzzyClusteringAlgorithm algorithm = new FuzzyClusteringAlgorithm();
            int pointsPerCluster = 5;
            double[][] points = new double[pointsPerCluster * 2][];
            int index = 0;
            for (int i = 0; i < pointsPerCluster; i++)
            {
                points[index] = new double[]
                {
                    0.0 + i * 0.1,
                    0.0 + i * 0.1
                };
                index++;
            }

            for (int i = 0; i < pointsPerCluster; i++)
            {
                points[index] = new double[]
                {
                    10.0 + i * 0.1,
                    10.0 + i * 0.1
                };
                index++;
            }

            int clusterCount = 2;
            double fuzziness = 2.0;
            int maxIterations = 50;
            double epsilon = 0.001;
            FuzzyClusterResult result = algorithm.Cluster(points, clusterCount, fuzziness, maxIterations, epsilon);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(clusterCount, result.Centers.Length, "Should have two centers.");
            double tolerance = 1e-6;
            foreach (double[] membershipRow in result.Membership)
            {
                double sum = membershipRow.Sum();
                Assert.IsTrue(Math.Abs(sum - 1.0) < tolerance, "Each membership row should sum to 1.");
            }
        }

        [TestMethod]
        public void Test_FuzzinessUnderflow()
        {
            FuzzyClusteringAlgorithm algorithm = new FuzzyClusteringAlgorithm();
            double[][] points = new double[2][];
            points[0] = new double[]
            {
                1.0,
                1.0
            };
            points[1] = new double[]
            {
                5.0,
                5.0
            };
            int clusterCount = 2;
            double fuzziness = 1.0;
            int maxIterations = 30;
            double epsilon = 0.001;
            FuzzyClusterResult result = algorithm.Cluster(points, clusterCount, fuzziness, maxIterations, epsilon);
            Assert.IsNotNull(result, "Result should not be null.");
            double tolerance = 1e-6;
            for (int i = 0; i < result.Membership.Length; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < result.Membership[i].Length; j++)
                {
                    sum += result.Membership[i][j];
                }

                Assert.IsTrue(Math.Abs(sum - 1.0) < tolerance, "Membership row should sum to 1 even when fuzziness is under threshold.");
            }
        }

        [TestMethod]
        public void Test_MembershipSums()
        {
            FuzzyClusteringAlgorithm algorithm = new FuzzyClusteringAlgorithm();
            double[][] points = new double[10][];
            for (int i = 0; i < 10; i++)
            {
                points[i] = new double[]
                {
                    i,
                    i + 0.5
                };
            }

            int clusterCount = 3;
            double fuzziness = 2.0;
            int maxIterations = 30;
            double epsilon = 0.001;
            FuzzyClusterResult result = algorithm.Cluster(points, clusterCount, fuzziness, maxIterations, epsilon);
            Assert.IsNotNull(result, "Result should not be null.");
            double tolerance = 1e-6;
            for (int i = 0; i < points.Length; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < clusterCount; j++)
                {
                    sum += result.Membership[i][j];
                }

                Assert.IsTrue(Math.Abs(sum - 1.0) < tolerance, "Each membership row should sum to 1.");
            }
        }

        [TestMethod]
        public void Test_SyntheticLargeData()
        {
            FuzzyClusteringAlgorithm algorithm = new FuzzyClusteringAlgorithm();
            int numberOfPoints = 1000;
            int dimensions = 2;
            double[][] points = new double[numberOfPoints][];
            Random random = new Random();
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i] = new double[dimensions];
                for (int d = 0; d < dimensions; d++)
                {
                    points[i][d] = random.NextDouble() * 100.0;
                }
            }

            int clusterCount = 3;
            double fuzziness = 2.0;
            int maxIterations = 50;
            double epsilon = 0.001;
            FuzzyClusterResult result = algorithm.Cluster(points, clusterCount, fuzziness, maxIterations, epsilon);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(clusterCount, result.Centers.Length, "Should have three centers.");
            double tolerance = 1e-6;
            for (int i = 0; i < points.Length; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < clusterCount; j++)
                {
                    double membershipValue = result.Membership[i][j];
                    Assert.IsTrue(membershipValue >= 0.0 && membershipValue <= 1.0, "Membership value should be between 0 and 1.");
                    sum += membershipValue;
                }

                Assert.IsTrue(Math.Abs(sum - 1.0) < tolerance, "Each membership row should sum to 1.");
            }
        }
    }
}