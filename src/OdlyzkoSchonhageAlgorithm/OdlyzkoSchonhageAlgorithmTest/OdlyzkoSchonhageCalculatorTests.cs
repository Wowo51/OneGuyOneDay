using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdlyzkoSchonhageAlgorithm;

namespace OdlyzkoSchonhageAlgorithmTest
{
    [TestClass]
    public class OdlyzkoSchonhageCalculatorTests
    {
        [TestMethod]
        public void ComputeZerosEfficient_InvalidInput_ReturnsEmpty()
        {
            OdlyzkoSchonhageCalculator calculator = new OdlyzkoSchonhageCalculator();
            List<Complex> zeros = calculator.ComputeZerosEfficient(20.0, 10.0, 5);
            Assert.AreEqual(0, zeros.Count);
            zeros = calculator.ComputeZerosEfficient(10.0, 20.0, 0);
            Assert.AreEqual(0, zeros.Count);
        }

        [TestMethod]
        public void ComputeZeros_InvalidInput_ReturnsEmpty()
        {
            OdlyzkoSchonhageCalculator calculator = new OdlyzkoSchonhageCalculator();
            List<Complex> zeros = calculator.ComputeZeros(20.0, 10.0, 5);
            Assert.AreEqual(0, zeros.Count);
            zeros = calculator.ComputeZeros(10.0, 20.0, 0);
            Assert.AreEqual(0, zeros.Count);
        }

        [TestMethod]
        public void ComputeZerosEfficient_ValidInput_ReturnsZerosInRange()
        {
            OdlyzkoSchonhageCalculator calculator = new OdlyzkoSchonhageCalculator();
            double start = 14.0;
            double end = 24.0;
            int requestedCount = 10;
            List<Complex> zeros = calculator.ComputeZerosEfficient(start, end, requestedCount);
            // In the interval [14, 24] the algorithm typically detects two crossings.
            Assert.AreEqual(2, zeros.Count);
            foreach (Complex zero in zeros)
            {
                Assert.AreEqual(0.5, zero.Real, 1e-10);
                Assert.IsTrue(zero.Imaginary >= start && zero.Imaginary <= end);
            }
        }

        [TestMethod]
        public void ComputeZeros_ValidInput_ReturnsZerosInRange()
        {
            OdlyzkoSchonhageCalculator calculator = new OdlyzkoSchonhageCalculator();
            double start = 14.0;
            double end = 24.0;
            int requestedCount = 10;
            List<Complex> zeros = calculator.ComputeZeros(start, end, requestedCount);
            // Expect similar behavior as in the efficient case.
            Assert.AreEqual(2, zeros.Count);
            foreach (Complex zero in zeros)
            {
                Assert.AreEqual(0.5, zero.Real, 1e-10);
                Assert.IsTrue(zero.Imaginary >= start && zero.Imaginary <= end);
            }
        }

        [TestMethod]
        public void ComputeZerosEfficient_RandomRanges_ReturnZerosWithinRange()
        {
            OdlyzkoSchonhageCalculator calculator = new OdlyzkoSchonhageCalculator();
            Random random = new Random(12345);
            for (int i = 0; i < 5; i++)
            {
                double start = 10.0 + random.NextDouble() * 10.0;
                double end = start + 5.0 + random.NextDouble() * 10.0;
                int requestedCount = random.Next(1, 6);
                List<Complex> zeros = calculator.ComputeZerosEfficient(start, end, requestedCount);
                foreach (Complex zero in zeros)
                {
                    Assert.AreEqual(0.5, zero.Real, 1e-10);
                    Assert.IsTrue(zero.Imaginary >= start && zero.Imaginary <= end);
                }

                Assert.IsTrue(zeros.Count <= requestedCount);
            }
        }

        [TestMethod]
        public void ComputeZeros_RandomRanges_ReturnZerosWithinRange()
        {
            OdlyzkoSchonhageCalculator calculator = new OdlyzkoSchonhageCalculator();
            Random random = new Random(54321);
            for (int i = 0; i < 5; i++)
            {
                double start = 10.0 + random.NextDouble() * 10.0;
                double end = start + 5.0 + random.NextDouble() * 10.0;
                int requestedCount = random.Next(1, 6);
                List<Complex> zeros = calculator.ComputeZeros(start, end, requestedCount);
                foreach (Complex zero in zeros)
                {
                    Assert.AreEqual(0.5, zero.Real, 1e-10);
                    Assert.IsTrue(zero.Imaginary >= start && zero.Imaginary <= end);
                }

                Assert.IsTrue(zeros.Count <= requestedCount);
            }
        }
    }
}