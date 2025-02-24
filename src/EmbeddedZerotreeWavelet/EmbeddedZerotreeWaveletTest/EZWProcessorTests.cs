using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmbeddedZerotreeWavelet;
using System;

namespace EmbeddedZerotreeWaveletTest
{
    [TestClass]
    public class EZWProcessorTests
    {
        [TestMethod]
        public void TestEncodeHeaderFormat()
        {
            double[, ] coefficients = new double[2, 2];
            coefficients[0, 0] = 1.0;
            coefficients[0, 1] = -1.0;
            coefficients[1, 0] = 0.0;
            coefficients[1, 1] = 2.0;
            string encoded = EZWProcessor.Encode(coefficients);
            Assert.IsTrue(encoded.StartsWith("T="), "Encoded string should start with 'T='.");
            Assert.IsTrue(encoded.Contains(";"), "Encoded string should contain ';' to separate header.");
        }

        [TestMethod]
        public void TestRoundTripSmallMatrix()
        {
            double[, ] original = new double[2, 2];
            original[0, 0] = 1.5;
            original[0, 1] = -2.3;
            original[1, 0] = 0.5;
            original[1, 1] = -0.5;
            string encoded = EZWProcessor.Encode(original);
            double[, ] decoded = EZWProcessor.Decode(encoded, 2, 2);
            Assert.AreEqual(1.0, decoded[0, 0], 1e-9, "Row 0, Col 0 mismatch");
            Assert.AreEqual(-3.0, decoded[0, 1], 1e-9, "Row 0, Col 1 mismatch");
            Assert.AreEqual(0.0, decoded[1, 0], 1e-9, "Row 1, Col 0 mismatch");
            Assert.AreEqual(0.0, decoded[1, 1], 1e-9, "Row 1, Col 1 mismatch");
        }

        [TestMethod]
        public void TestZeros()
        {
            double[, ] zeros = new double[3, 3];
            string encoded = EZWProcessor.Encode(zeros);
            double[, ] decoded = EZWProcessor.Decode(encoded, 3, 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(0.0, decoded[i, j], 1e-9, "Element should be zero");
                }
            }
        }

        [TestMethod]
        public void TestRoundTripConsistency()
        {
            double[, ] randomMatrix = new double[10, 10];
            System.Random randomGenerator = new System.Random(42);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    double value = (randomGenerator.NextDouble() * 200.0) - 100.0;
                    randomMatrix[i, j] = value;
                }
            }

            string encoded = EZWProcessor.Encode(randomMatrix);
            double[, ] decoded = EZWProcessor.Decode(encoded, 10, 10);
            string reEncoded = EZWProcessor.Encode(decoded);
            Assert.AreEqual(encoded, reEncoded, "Round trip encoding should be consistent.");
        }

        [TestMethod]
        public void TestNonSquareMatrix()
        {
            double[, ] matrix = new double[2, 3];
            matrix[0, 0] = 3.0;
            matrix[0, 1] = -2.0;
            matrix[0, 2] = 5.0;
            matrix[1, 0] = -1.0;
            matrix[1, 1] = 0.0;
            matrix[1, 2] = 2.0;
            string encoded = EZWProcessor.Encode(matrix);
            double[, ] decoded = EZWProcessor.Decode(encoded, 2, 3);
            string reEncoded = EZWProcessor.Encode(decoded);
            Assert.AreEqual(encoded, reEncoded, "Round trip encoding should be consistent for non-square matrix.");
        }
    }
}