using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MuLawAlgorithm;

namespace MuLawAlgorithmTest
{
    [TestClass]
    public class MuLawCompanderTests
    {
        private const double Tolerance = 1e-9;
        [TestMethod]
        public void TestEncodeDecodingRoundTrip()
        {
            double[] testValues = new double[]
            {
                0.0,
                0.5,
                -0.5,
                1.0,
                -1.0
            };
            for (int i = 0; i < testValues.Length; i++)
            {
                double original = testValues[i];
                // Clamp the value manually since Encode clamps internally.
                double clamped = original > 1.0 ? 1.0 : (original < -1.0 ? -1.0 : original);
                double encoded = MuLawCompander.Encode(original);
                double decoded = MuLawCompander.Decode(encoded);
                Assert.IsTrue(Math.Abs(decoded - clamped) < Tolerance, "Round-trip conversion failed for value " + original);
            }
        }

        [TestMethod]
        public void TestClamping()
        {
            double above = 1.5;
            double encodedAbove = MuLawCompander.Encode(above);
            double encodedMax = MuLawCompander.Encode(1.0);
            Assert.AreEqual(encodedMax, encodedAbove, Tolerance, "Encoding above 1.0 should be equivalent to encoding 1.0");
            double below = -1.5;
            double encodedBelow = MuLawCompander.Encode(below);
            double encodedMin = MuLawCompander.Encode(-1.0);
            Assert.AreEqual(encodedMin, encodedBelow, Tolerance, "Encoding below -1.0 should be equivalent to encoding -1.0");
        }

        [TestMethod]
        public void TestRandomRoundTrip()
        {
            System.Random randomGenerator = new System.Random(42);
            for (int i = 0; i < 1000; i++)
            {
                double randomValue = (randomGenerator.NextDouble() * 2.0) - 1.0;
                double encoded = MuLawCompander.Encode(randomValue);
                double decoded = MuLawCompander.Decode(encoded);
                Assert.IsTrue(Math.Abs(decoded - randomValue) < Tolerance, "Round-trip conversion failed for random value " + randomValue);
            }
        }

        [TestMethod]
        public void TestEncodeZero()
        {
            double encodedZero = MuLawCompander.Encode(0.0);
            Assert.AreEqual(0.0, encodedZero, Tolerance, "Encoding of 0 should be 0");
            double decodedZero = MuLawCompander.Decode(encodedZero);
            Assert.AreEqual(0.0, decodedZero, Tolerance, "Decoding of 0 should yield 0");
        }
    }
}