using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RuzzoTompaAlgorithm;
using static RuzzoTompaAlgorithm.RuzzoTompaAlgorithm;

namespace RuzzoTompaAlgorithmTest
{
    [TestClass]
    public class RuzzoTompaAlgorithmTests
    {
        [TestMethod]
        public void TestNullInput()
        {
            List<ScoreSegment> result = FindMaximalSubsequences(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEmptyArray()
        {
            double[] scores = new double[0];
            List<ScoreSegment> result = FindMaximalSubsequences(scores);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestAllNegatives()
        {
            double[] scores = new double[]
            {
                -1.0,
                -2.0,
                -3.0
            };
            List<ScoreSegment> result = FindMaximalSubsequences(scores);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestSinglePositiveSegment()
        {
            double[] scores = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            List<ScoreSegment> result = FindMaximalSubsequences(scores);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].Start);
            Assert.AreEqual(2, result[0].End);
            Assert.AreEqual(6.0, result[0].Score, 0.0001);
        }

        [TestMethod]
        public void TestMultipleSegments()
        {
            double[] scores = new double[]
            {
                -2,
                3,
                -2,
                -2,
                4,
                1,
                -5,
                6
            };
            List<ScoreSegment> result = FindMaximalSubsequences(scores);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[0].Start);
            Assert.AreEqual(1, result[0].End);
            Assert.AreEqual(3.0, result[0].Score, 0.0001);
            Assert.AreEqual(4, result[1].Start);
            Assert.AreEqual(5, result[1].End);
            Assert.AreEqual(5.0, result[1].Score, 0.0001);
            Assert.AreEqual(7, result[2].Start);
            Assert.AreEqual(7, result[2].End);
            Assert.AreEqual(6.0, result[2].Score, 0.0001);
        }

        [TestMethod]
        public void TestComplexSegment()
        {
            double[] scores = new double[]
            {
                1,
                -0.5,
                0.3,
                -0.1
            };
            List<ScoreSegment> result = FindMaximalSubsequences(scores);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].Start);
            Assert.AreEqual(0, result[0].End);
            Assert.AreEqual(1.0, result[0].Score, 0.0001);
        }

        [TestMethod]
        public void TestLargeRandomData()
        {
            Random rand = new Random(42);
            int size = 10000;
            double[] scores = new double[size];
            for (int i = 0; i < size; i++)
            {
                scores[i] = ((double)rand.Next(-100, 101)) / 10.0;
            }

            List<ScoreSegment> result = FindMaximalSubsequences(scores);
            Assert.IsNotNull(result);
        }
    }
}