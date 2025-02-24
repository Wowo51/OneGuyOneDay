using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NearestNeighborSearch;

namespace NearestNeighborSearchTest
{
    [TestClass]
    public class NearestNeighborSearchTests
    {
        [TestMethod]
        public void TestFindNearestNeighbor_NullInputs()
        {
            NearestNeighborSearchAlgorithm algorithm = new NearestNeighborSearchAlgorithm();
            double[] target = new double[]
            {
                1.0,
                2.0
            };
            Assert.IsNull(algorithm.FindNearestNeighbor(null, target));
            double[][] points = new double[][]
            {
            };
            Assert.IsNull(algorithm.FindNearestNeighbor(points, null));
        }

        [TestMethod]
        public void TestFindNearestNeighbor_EmptyPoints()
        {
            NearestNeighborSearchAlgorithm algorithm = new NearestNeighborSearchAlgorithm();
            double[] target = new double[]
            {
                1.0,
                2.0
            };
            double[][] points = new double[][]
            {
            };
            Assert.IsNull(algorithm.FindNearestNeighbor(points, target));
        }

        [TestMethod]
        public void TestFindNearestNeighbor_ValidData()
        {
            NearestNeighborSearchAlgorithm algorithm = new NearestNeighborSearchAlgorithm();
            double[][] points = new double[][]
            {
                new double[]
                {
                    1.0,
                    1.0
                },
                new double[]
                {
                    2.0,
                    2.0
                },
                new double[]
                {
                    3.0,
                    3.0
                }
            };
            double[] target = new double[]
            {
                1.1,
                1.1
            };
            double[] result = algorithm.FindNearestNeighbor(points, target);
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new double[] { 1.0, 1.0 }, result);
        }

        [TestMethod]
        public void TestFindKNearestNeighbors_NullInputs()
        {
            NearestNeighborSearchAlgorithm algorithm = new NearestNeighborSearchAlgorithm();
            double[] target = new double[]
            {
                1.0,
                2.0
            };
            List<double[]> result = algorithm.FindKNearestNeighbors(null, target, 3);
            Assert.AreEqual(0, result.Count);
            double[][] points = new double[][]
            {
                new double[]
                {
                    1.0,
                    1.0
                }
            };
            result = algorithm.FindKNearestNeighbors(points, null, 3);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestFindKNearestNeighbors_InvalidK()
        {
            NearestNeighborSearchAlgorithm algorithm = new NearestNeighborSearchAlgorithm();
            double[][] points = new double[][]
            {
                new double[]
                {
                    1.0,
                    1.0
                }
            };
            double[] target = new double[]
            {
                1.0,
                1.0
            };
            List<double[]> result = algorithm.FindKNearestNeighbors(points, target, 0);
            Assert.AreEqual(0, result.Count);
            result = algorithm.FindKNearestNeighbors(points, target, -1);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestFindKNearestNeighbors_ValidData()
        {
            NearestNeighborSearchAlgorithm algorithm = new NearestNeighborSearchAlgorithm();
            double[][] points = new double[][]
            {
                new double[]
                {
                    1.0,
                    1.0
                },
                new double[]
                {
                    2.0,
                    2.0
                },
                new double[]
                {
                    4.0,
                    4.0
                },
                new double[]
                {
                    3.0,
                    3.0
                }
            };
            double[] target = new double[]
            {
                2.1,
                2.1
            };
            List<double[]> result = algorithm.FindKNearestNeighbors(points, target, 3);
            Assert.AreEqual(3, result.Count);
            CollectionAssert.AreEqual(new double[] { 2.0, 2.0 }, result[0]);
            CollectionAssert.AreEqual(new double[] { 3.0, 3.0 }, result[1]);
            CollectionAssert.AreEqual(new double[] { 1.0, 1.0 }, result[2]);
        }

        [TestMethod]
        public void TestFindKNearestNeighbors_MixedValidity()
        {
            NearestNeighborSearchAlgorithm algorithm = new NearestNeighborSearchAlgorithm();
            double[][] points = new double[][]
            {
                new double[]
                {
                    1.0,
                    1.0
                },
                null,
                new double[]
                {
                    2.0
                },
                new double[]
                {
                    2.0,
                    2.0
                }
            };
            double[] target = new double[]
            {
                1.0,
                1.0
            };
            List<double[]> result = algorithm.FindKNearestNeighbors(points, target, 2);
            Assert.AreEqual(2, result.Count);
            CollectionAssert.AreEqual(new double[] { 1.0, 1.0 }, result[0]);
            CollectionAssert.AreEqual(new double[] { 2.0, 2.0 }, result[1]);
        }

        [TestMethod]
        public void TestSyntheticLargeDataset()
        {
            NearestNeighborSearchAlgorithm algorithm = new NearestNeighborSearchAlgorithm();
            int size = 1000;
            double[][] points = new double[size][];
            for (int i = 0; i < size; i++)
            {
                double[] point = new double[2];
                point[0] = (double)i;
                point[1] = (double)i;
                points[i] = point;
            }

            double[] target = new double[]
            {
                500.0,
                500.0
            };
            double[] nearest = algorithm.FindNearestNeighbor(points, target);
            CollectionAssert.AreEqual(new double[] { 500.0, 500.0 }, nearest);
            int k = 10;
            List<double[]> kNearest = algorithm.FindKNearestNeighbors(points, target, k);
            Assert.AreEqual(k, kNearest.Count);
            CollectionAssert.AreEqual(new double[] { 500.0, 500.0 }, kNearest[0]);
            double previousDistance = 0.0;
            for (int i = 0; i < kNearest.Count; i++)
            {
                double currentDistance = Math.Sqrt(Math.Pow(kNearest[i][0] - target[0], 2.0) + Math.Pow(kNearest[i][1] - target[1], 2.0));
                if (i > 0)
                {
                    Assert.IsTrue(currentDistance >= previousDistance);
                }

                previousDistance = currentDistance;
            }
        }
    }
}