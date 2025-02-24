using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VectorQuantization;

namespace VectorQuantizationTest
{
    [TestClass]
    public class VectorQuantizerTests
    {
        [TestMethod]
        public void TestTrainWithNullData()
        {
            VectorQuantizer quantizer = new VectorQuantizer();
            List<double[]> codebook = quantizer.Train(null, 3, 10);
            Assert.AreEqual(0, codebook.Count, "Expected empty codebook when training with null data.");
        }

        [TestMethod]
        public void TestQuantizeWithNullData()
        {
            VectorQuantizer quantizer = new VectorQuantizer();
            List<int> indicesDataNull = quantizer.Quantize(null, new List<double[]> { new double[] { 1, 2 } });
            Assert.AreEqual(0, indicesDataNull.Count, "Expected empty indices when data is null.");
            List<int> indicesCodebookNull = quantizer.Quantize(new List<double[]> { new double[] { 1, 2 } }, null);
            Assert.AreEqual(0, indicesCodebookNull.Count, "Expected empty indices when codebook is null.");
        }

        [TestMethod]
        public void TestQuantizeReturnsCorrectIndex()
        {
            VectorQuantizer quantizer = new VectorQuantizer();
            double[] vector1 = new double[]
            {
                1,
                2
            };
            double[] vector2 = new double[]
            {
                3,
                4
            };
            List<double[]> codebook = new List<double[]>();
            codebook.Add(vector1);
            codebook.Add(vector2);
            List<double[]> data = new List<double[]>();
            data.Add(vector1);
            data.Add(vector2);
            data.Add(vector1);
            List<int> indices = quantizer.Quantize(data, codebook);
            Assert.AreEqual(3, indices.Count, "Expected three quantized indices.");
            Assert.AreEqual(0, indices[0]);
            Assert.AreEqual(1, indices[1]);
            Assert.AreEqual(0, indices[2]);
        }

        [TestMethod]
        public void TestTrainWithSingleCluster()
        {
            VectorQuantizer quantizer = new VectorQuantizer();
            double[] vector1 = new double[]
            {
                1,
                2
            };
            double[] vector2 = new double[]
            {
                3,
                4
            };
            List<double[]> data = new List<double[]>();
            data.Add(vector1);
            data.Add(vector2);
            List<double[]> codebook = quantizer.Train(data, 1, 10);
            Assert.AreEqual(1, codebook.Count, "Expected one centroid in codebook.");
            double[] centroid = codebook[0];
            Assert.AreEqual(2, centroid.Length, "Centroid should have 2 dimensions.");
            double expected0 = 2;
            double expected1 = 3;
            double tol = 1e-6;
            Assert.IsTrue(System.Math.Abs(centroid[0] - expected0) < tol, "Centroid first dimension not as expected.");
            Assert.IsTrue(System.Math.Abs(centroid[1] - expected1) < tol, "Centroid second dimension not as expected.");
        }

        [TestMethod]
        public void TestTrainWithLargeData()
        {
            VectorQuantizer quantizer = new VectorQuantizer();
            int count = 100;
            int dimension = 3;
            List<double[]> data = new List<double[]>();
            System.Random random = new System.Random(42);
            for (int i = 0; i < count; i++)
            {
                double[] vector = new double[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    vector[j] = random.NextDouble() * 100;
                }

                data.Add(vector);
            }

            int requestedCodebookSize = 5;
            List<double[]> codebook = quantizer.Train(data, requestedCodebookSize, 20);
            Assert.AreEqual(requestedCodebookSize, codebook.Count, "Codebook count does not match requested codebook size.");
            for (int i = 0; i < codebook.Count; i++)
            {
                Assert.AreEqual(dimension, codebook[i].Length, "Centroid dimension mismatch.");
            }
        }

        [TestMethod]
        public void TestQuantizeLargeData()
        {
            VectorQuantizer quantizer = new VectorQuantizer();
            int count = 50;
            int dimension = 3;
            List<double[]> data = new List<double[]>();
            System.Random random = new System.Random(84);
            for (int i = 0; i < count; i++)
            {
                double[] vector = new double[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    vector[j] = random.NextDouble() * 50;
                }

                data.Add(vector);
            }

            int codebookSize = 5;
            List<double[]> codebook = new List<double[]>();
            for (int i = 0; i < codebookSize; i++)
            {
                double[] vector = new double[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    vector[j] = random.NextDouble() * 50;
                }

                codebook.Add(vector);
            }

            List<int> indices = quantizer.Quantize(data, codebook);
            Assert.AreEqual(count, indices.Count, "Quantized index count should match data count.");
            for (int i = 0; i < indices.Count; i++)
            {
                int index = indices[i];
                Assert.IsTrue(index >= 0 && index < codebookSize, "Quantized index out of range.");
            }
        }
    }
}