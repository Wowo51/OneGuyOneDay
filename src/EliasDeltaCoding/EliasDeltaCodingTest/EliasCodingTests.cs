using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EliasDeltaCoding;

namespace EliasDeltaCodingTest
{
    [TestClass]
    public class EliasCodingTests
    {
        [TestMethod]
        public void TestGammaEncodeInvalidInput()
        {
            int testValue = 0;
            string encoded = EliasCoding.GammaEncode(testValue);
            Assert.AreEqual("", encoded);
            testValue = -5;
            encoded = EliasCoding.GammaEncode(testValue);
            Assert.AreEqual("", encoded);
        }

        [TestMethod]
        public void TestGammaEncodeDecodingConsistency()
        {
            int[] testValues = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                10,
                100
            };
            for (int i = 0; i < testValues.Length; i++)
            {
                int n = testValues[i];
                string encoded = EliasCoding.GammaEncode(n);
                int decoded = EliasCoding.GammaDecode(encoded);
                Assert.AreEqual(n, decoded, "Gamma encoding/decoding failed for " + n);
            }
        }

        [TestMethod]
        public void TestGammaDecodeInvalidInput()
        {
            int decoded = EliasCoding.GammaDecode("");
            Assert.AreEqual(-1, decoded);
            decoded = EliasCoding.GammaDecode("0010");
            Assert.AreEqual(-1, decoded);
            decoded = EliasCoding.GammaDecode("00100extra");
            Assert.AreEqual(-1, decoded);
        }

        [TestMethod]
        public void TestDeltaEncodeDecodingConsistency()
        {
            int[] testValues = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                10,
                100
            };
            for (int i = 0; i < testValues.Length; i++)
            {
                int n = testValues[i];
                string encoded = EliasCoding.DeltaEncode(n);
                int decoded = EliasCoding.DeltaDecode(encoded);
                Assert.AreEqual(n, decoded, "Delta encoding/decoding failed for " + n);
            }
        }

        [TestMethod]
        public void TestDeltaInvalidInput()
        {
            int decoded = EliasCoding.DeltaDecode("");
            Assert.AreEqual(-1, decoded);
            decoded = EliasCoding.DeltaDecode("011");
            Assert.AreEqual(-1, decoded);
        }

        [TestMethod]
        public void TestOmegaEncodeDecodingConsistency()
        {
            int[] testValues = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                10,
                100
            };
            for (int i = 0; i < testValues.Length; i++)
            {
                int n = testValues[i];
                string encoded = EliasCoding.OmegaEncode(n);
                int decoded = EliasCoding.OmegaDecode(encoded);
                Assert.AreEqual(n, decoded, "Omega encoding/decoding failed for " + n);
            }
        }

        [TestMethod]
        public void TestOmegaInvalidInput()
        {
            int decoded = EliasCoding.OmegaDecode("");
            Assert.AreEqual(-1, decoded);
            decoded = EliasCoding.OmegaDecode("101000extra");
            Assert.AreEqual(-1, decoded);
        }

        [TestMethod]
        public void TestLargeSyntheticData()
        {
            System.Random randomGenerator = new System.Random();
            for (int i = 0; i < 100; i++)
            {
                int n = randomGenerator.Next(1, 1000);
                string gamma = EliasCoding.GammaEncode(n);
                int gammaDecoded = EliasCoding.GammaDecode(gamma);
                Assert.AreEqual(n, gammaDecoded, "Gamma coding failed for " + n);
                string delta = EliasCoding.DeltaEncode(n);
                int deltaDecoded = EliasCoding.DeltaDecode(delta);
                Assert.AreEqual(n, deltaDecoded, "Delta coding failed for " + n);
                string omega = EliasCoding.OmegaEncode(n);
                int omegaDecoded = EliasCoding.OmegaDecode(omega);
                Assert.AreEqual(n, omegaDecoded, "Omega coding failed for " + n);
            }
        }
    }
}