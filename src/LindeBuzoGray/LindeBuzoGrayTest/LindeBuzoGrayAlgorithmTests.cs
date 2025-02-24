using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LindeBuzoGray;

namespace LindeBuzoGrayTest
{
    [TestClass]
    public class LindeBuzoGrayAlgorithmTests
    {
        [TestMethod]
        public void TestGenerateCodebook_NullTrainingData_ReturnsEmptyList()
        {
            List<double[]> trainingData = null;
            int codebookSize = 5;
            double epsilon = 0.01;
            double convergenceThreshold = 0.001;
            List<double[]> result = LindeBuzoGrayAlgorithm.GenerateCodebook(trainingData, codebookSize, epsilon, convergenceThreshold);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestGenerateCodebook_EmptyTrainingData_ReturnsEmptyList()
        {
            List<double[]> trainingData = new List<double[]>();
            int codebookSize = 5;
            double epsilon = 0.01;
            double convergenceThreshold = 0.001;
            List<double[]> result = LindeBuzoGrayAlgorithm.GenerateCodebook(trainingData, codebookSize, epsilon, convergenceThreshold);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestGenerateCodebook_InvalidCodebookSize_ReturnsEmptyList()
        {
            List<double[]> trainingData = new List<double[]>
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
            int codebookSize = 0;
            double epsilon = 0.01;
            double convergenceThreshold = 0.001;
            List<double[]> result = LindeBuzoGrayAlgorithm.GenerateCodebook(trainingData, codebookSize, epsilon, convergenceThreshold);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestGenerateCodebook_SmallDataset_ReturnsExpectedCount()
        {
            List<double[]> trainingData = new List<double[]>
            {
                new double[]
                {
                    1.0,
                    1.0
                },
                new double[]
                {
                    1.1,
                    0.9
                },
                new double[]
                {
                    0.9,
                    1.1
                }
            };
            int codebookSize = 2;
            double epsilon = 0.01;
            double convergenceThreshold = 0.0001;
            List<double[]> result = LindeBuzoGrayAlgorithm.GenerateCodebook(trainingData, codebookSize, epsilon, convergenceThreshold);
            Assert.IsNotNull(result);
            Assert.AreEqual(codebookSize, result.Count);
        }

        [TestMethod]
        public void TestGenerateCodebook_LargeSyntheticDataset_ReturnsExpectedCount()
        {
            List<double[]> trainingData = new List<double[]>();
            int dimension = 3;
            int dataSize = 1000;
            Random random = new Random(0);
            for (int i = 0; i < dataSize; i++)
            {
                double[] point = new double[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    point[j] = random.NextDouble();
                }

                trainingData.Add(point);
            }

            int codebookSize = 16;
            double epsilon = 0.02;
            double convergenceThreshold = 0.00001;
            List<double[]> result = LindeBuzoGrayAlgorithm.GenerateCodebook(trainingData, codebookSize, epsilon, convergenceThreshold);
            Assert.IsNotNull(result);
            Assert.AreEqual(codebookSize, result.Count);
        }

        [TestMethod]
        public void TestGenerateCodebook_InconsistentDimensions_IgnoresBadData()
        {
            List<double[]> trainingData = new List<double[]>
            {
                new double[]
                {
                    1.0,
                    1.0
                },
                new double[]
                {
                    2.0
                }, // Inconsistent dimension, should be ignored
                new double[]
                {
                    1.1,
                    1.1
                }
            };
            int codebookSize = 2;
            double epsilon = 0.01;
            double convergenceThreshold = 0.001;
            List<double[]> result = LindeBuzoGrayAlgorithm.GenerateCodebook(trainingData, codebookSize, epsilon, convergenceThreshold);
            Assert.IsNotNull(result);
            Assert.AreEqual(codebookSize, result.Count);
        }
    }
}