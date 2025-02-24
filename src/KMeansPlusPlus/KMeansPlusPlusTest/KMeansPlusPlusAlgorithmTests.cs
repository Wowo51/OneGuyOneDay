using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KMeansPlusPlus;

namespace KMeansPlusPlusTest
{
    [TestClass]
    public class KMeansPlusPlusAlgorithmTests
    {
        [TestMethod]
        public void Fit_NullData_ReturnsEmptyArray()
        {
            KMeansPlusPlusAlgorithm algorithm = new KMeansPlusPlusAlgorithm();
            int[] assignments = algorithm.Fit(null, 3);
            Assert.IsNotNull(assignments);
            Assert.AreEqual(0, assignments.Length);
        }

        [TestMethod]
        public void Fit_EmptyData_ReturnsEmptyArray()
        {
            double[][] data = new double[0][];
            KMeansPlusPlusAlgorithm algorithm = new KMeansPlusPlusAlgorithm();
            int[] assignments = algorithm.Fit(data, 3);
            Assert.IsNotNull(assignments);
            Assert.AreEqual(0, assignments.Length);
        }

        [TestMethod]
        public void Fit_InvalidK_ReturnsEmptyArray()
        {
            double[][] data = new double[][]
            {
                new double[]
                {
                    1.0,
                    2.0
                },
                new double[]
                {
                    3.0,
                    4.0
                }
            };
            KMeansPlusPlusAlgorithm algorithm = new KMeansPlusPlusAlgorithm();
            int[] assignments1 = algorithm.Fit(data, 0);
            Assert.IsNotNull(assignments1);
            Assert.AreEqual(0, assignments1.Length);
            int[] assignments2 = algorithm.Fit(data, 3);
            Assert.IsNotNull(assignments2);
            Assert.AreEqual(0, assignments2.Length);
        }

        [TestMethod]
        public void Fit_SinglePoint_ReturnsSingleCluster()
        {
            double[][] data = new double[][]
            {
                new double[]
                {
                    1.0,
                    2.0
                }
            };
            KMeansPlusPlusAlgorithm algorithm = new KMeansPlusPlusAlgorithm();
            int[] assignments = algorithm.Fit(data, 1);
            Assert.IsNotNull(assignments);
            Assert.AreEqual(1, assignments.Length);
            Assert.AreEqual(0, assignments[0]);
        }

        [TestMethod]
        public void Fit_TwoClusters_CreatesDistinctClusters()
        {
            List<double[]> dataList = new List<double[]>();
            // First cluster: around (1,1)
            for (int i = 0; i < 50; i++)
            {
                double[] point = new double[2];
                point[0] = 1.0 + i * 0.01;
                point[1] = 1.0 + i * 0.01;
                dataList.Add(point);
            }

            // Second cluster: around (10,10)
            for (int i = 0; i < 50; i++)
            {
                double[] point = new double[2];
                point[0] = 10.0 + i * 0.01;
                point[1] = 10.0 + i * 0.01;
                dataList.Add(point);
            }

            double[][] data = dataList.ToArray();
            KMeansPlusPlusAlgorithm algorithm = new KMeansPlusPlusAlgorithm();
            int[] assignments = algorithm.Fit(data, 2);
            Assert.IsNotNull(assignments);
            Assert.AreEqual(data.Length, assignments.Length);
            int countCluster0 = 0;
            int countCluster1 = 0;
            for (int i = 0; i < assignments.Length; i++)
            {
                if (assignments[i] == 0)
                {
                    countCluster0++;
                }
                else if (assignments[i] == 1)
                {
                    countCluster1++;
                }
            }

            Assert.IsTrue(countCluster0 > 0);
            Assert.IsTrue(countCluster1 > 0);
        }

        [TestMethod]
        public void Fit_LargeSyntheticData_CreatesValidAssignments()
        {
            int clusters = 3;
            int pointsPerCluster = 100;
            int dimensions = 2;
            double[][] data = new double[clusters * pointsPerCluster][];
            System.Random random = new System.Random(42);
            double[][] centers = new double[][]
            {
                new double[]
                {
                    0.0,
                    0.0
                },
                new double[]
                {
                    100.0,
                    100.0
                },
                new double[]
                {
                    200.0,
                    200.0
                }
            };
            for (int c = 0; c < clusters; c++)
            {
                for (int i = 0; i < pointsPerCluster; i++)
                {
                    double[] point = new double[dimensions];
                    point[0] = centers[c][0] + random.NextDouble() * 5.0 - 2.5;
                    point[1] = centers[c][1] + random.NextDouble() * 5.0 - 2.5;
                    data[c * pointsPerCluster + i] = point;
                }
            }

            KMeansPlusPlusAlgorithm algorithm = new KMeansPlusPlusAlgorithm();
            int[] assignments = algorithm.Fit(data, clusters);
            Assert.IsNotNull(assignments);
            Assert.AreEqual(data.Length, assignments.Length);
            for (int i = 0; i < assignments.Length; i++)
            {
                Assert.IsTrue(assignments[i] >= 0 && assignments[i] < clusters);
            }

            int[] clusterCounts = new int[clusters];
            for (int i = 0; i < assignments.Length; i++)
            {
                clusterCounts[assignments[i]]++;
            }

            for (int j = 0; j < clusters; j++)
            {
                Assert.IsTrue(clusterCounts[j] > 0);
            }
        }

        [TestMethod]
        public void Fit_TwoPoints_CentroidsAreSet()
        {
            double[][] data = new double[][]
            {
                new double[]
                {
                    1.0,
                    2.0
                },
                new double[]
                {
                    3.0,
                    4.0
                }
            };
            KMeansPlusPlusAlgorithm algorithm = new KMeansPlusPlusAlgorithm();
            int[] assignments = algorithm.Fit(data, 2);
            Assert.IsNotNull(assignments);
            Assert.AreEqual(2, assignments.Length);
            Assert.AreEqual(2, algorithm.Centroids.Count);
        }

        [TestMethod]
        public void Fit_IdenticalPoints_K2_AllAssignedToFirstCluster()
        {
            double[][] data = new double[][]
            {
                new double[]
                {
                    1.0,
                    1.0
                },
                new double[]
                {
                    1.0,
                    1.0
                }
            };
            KMeansPlusPlusAlgorithm algorithm = new KMeansPlusPlusAlgorithm();
            int[] assignments = algorithm.Fit(data, 2);
            Assert.IsNotNull(assignments);
            Assert.AreEqual(2, assignments.Length);
            for (int i = 0; i < assignments.Length; i++)
            {
                Assert.AreEqual(0, assignments[i]);
            }

            Assert.AreEqual(2, algorithm.Centroids.Count);
        }
    }
}