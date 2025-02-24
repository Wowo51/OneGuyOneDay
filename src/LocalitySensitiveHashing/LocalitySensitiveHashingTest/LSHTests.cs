using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LocalitySensitiveHashing;

namespace LocalitySensitiveHashingTest
{
    [TestClass]
    public class LSHTests
    {
        [TestMethod]
        public void ComputeSignature_NullVector_ReturnsEmptySignature()
        {
            LSH lsh = new LSH(5, 4);
            string signature = lsh.ComputeSignature(null);
            Assert.AreEqual("", signature);
        }

        [TestMethod]
        public void ComputeSignature_WrongDimension_ReturnsEmptySignature()
        {
            LSH lsh = new LSH(5, 4);
            List<double> vector = new List<double>
            {
                1.0,
                2.0
            };
            string signature = lsh.ComputeSignature(vector);
            Assert.AreEqual("", signature);
        }

        [TestMethod]
        public void ComputeSignature_ZeroVector_ReturnsAllOnesSignature()
        {
            int dimensions = 5;
            int hashSize = 4;
            LSH lsh = new LSH(dimensions, hashSize);
            List<double> vector = new List<double>();
            for (int i = 0; i < dimensions; i++)
            {
                vector.Add(0.0);
            }

            string signature = lsh.ComputeSignature(vector);
            string expected = new string ('1', hashSize);
            Assert.AreEqual(expected, signature);
        }

        [TestMethod]
        public void ComputeSignature_ConsistentForSameInput()
        {
            int dimensions = 5;
            int hashSize = 4;
            LSH lsh = new LSH(dimensions, hashSize);
            List<double> vector = new List<double>();
            for (int i = 0; i < dimensions; i++)
            {
                vector.Add(i * 0.1);
            }

            string firstSignature = lsh.ComputeSignature(vector);
            string secondSignature = lsh.ComputeSignature(vector);
            Assert.AreEqual(firstSignature, secondSignature);
        }

        [TestMethod]
        public void ComputeSignature_RandomVector_ReturnsValidBinarySignature()
        {
            int dimensions = 10;
            int hashSize = 8;
            LSH lsh = new LSH(dimensions, hashSize);
            List<double> vector = new List<double>();
            Random random = new Random();
            for (int i = 0; i < dimensions; i++)
            {
                vector.Add(random.NextDouble() * 10 - 5);
            }

            string signature = lsh.ComputeSignature(vector);
            Assert.AreEqual(hashSize, signature.Length);
            for (int i = 0; i < signature.Length; i++)
            {
                Assert.IsTrue(signature[i] == '0' || signature[i] == '1');
            }
        }

        [TestMethod]
        public void ComputeSignature_LargeVector_ReturnsSignatureOfCorrectLength()
        {
            int dimensions = 1000;
            int hashSize = 50;
            LSH lsh = new LSH(dimensions, hashSize);
            List<double> vector = new List<double>();
            for (int i = 0; i < dimensions; i++)
            {
                vector.Add((double)i / dimensions);
            }

            string signature = lsh.ComputeSignature(vector);
            Assert.AreEqual(hashSize, signature.Length);
            for (int i = 0; i < signature.Length; i++)
            {
                Assert.IsTrue(signature[i] == '0' || signature[i] == '1');
            }
        }

        [TestMethod]
        public void ComputeSignature_MultipleRandomVectors_ReturnsConsistentBinarySignature()
        {
            int dimensions = 10;
            int hashSize = 8;
            LSH lsh = new LSH(dimensions, hashSize);
            Random random = new Random();
            for (int k = 0; k < 100; k++)
            {
                List<double> vector = new List<double>();
                for (int i = 0; i < dimensions; i++)
                {
                    vector.Add(random.NextDouble() * 200 - 100);
                }

                string signature = lsh.ComputeSignature(vector);
                Assert.AreEqual(hashSize, signature.Length);
                for (int i = 0; i < signature.Length; i++)
                {
                    Assert.IsTrue(signature[i] == '0' || signature[i] == '1');
                }
            }
        }
    }
}