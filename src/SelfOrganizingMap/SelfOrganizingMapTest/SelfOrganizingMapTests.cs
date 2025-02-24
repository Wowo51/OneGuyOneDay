using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelfOrganizingMap;

namespace SelfOrganizingMapTest
{
    [TestClass]
    public class SelfOrganizingMapTests
    {
        [TestMethod]
        public void TestTrainMethodDoesNotThrow()
        {
            double[][] inputData = new double[5][];
            inputData[0] = new double[]
            {
                1.0,
                2.0
            };
            inputData[1] = new double[]
            {
                2.0,
                3.0
            };
            inputData[2] = new double[]
            {
                3.0,
                4.0
            };
            inputData[3] = new double[]
            {
                4.0,
                5.0
            };
            inputData[4] = new double[]
            {
                5.0,
                6.0
            };
            SelfOrganizingMap.SelfOrganizingMap som = new SelfOrganizingMap.SelfOrganizingMap(2, 5, 5, 50);
            som.Train(inputData);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestTrainWithEmptyData()
        {
            double[][] inputData = new double[0][];
            SelfOrganizingMap.SelfOrganizingMap som = new SelfOrganizingMap.SelfOrganizingMap(2, 5, 5, 10);
            som.Train(inputData);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestMultipleTrainCalls()
        {
            double[][] inputData = new double[2][];
            inputData[0] = new double[]
            {
                0.5,
                0.5
            };
            inputData[1] = new double[]
            {
                0.1,
                0.9
            };
            SelfOrganizingMap.SelfOrganizingMap som = new SelfOrganizingMap.SelfOrganizingMap(2, 4, 4, 50);
            int count;
            for (count = 0; count < 3; count++)
            {
                som.Train(inputData);
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestTrainWithLargeSyntheticData()
        {
            int sampleCount = 1000;
            int dimension = 3;
            double[][] inputData = new double[sampleCount][];
            int i;
            int j;
            Random random = new Random();
            for (i = 0; i < sampleCount; i++)
            {
                inputData[i] = new double[dimension];
                for (j = 0; j < dimension; j++)
                {
                    inputData[i][j] = random.NextDouble();
                }
            }

            SelfOrganizingMap.SelfOrganizingMap som = new SelfOrganizingMap.SelfOrganizingMap(dimension, 10, 10, 100);
            som.Train(inputData);
            Assert.IsTrue(true);
        }
    }
}