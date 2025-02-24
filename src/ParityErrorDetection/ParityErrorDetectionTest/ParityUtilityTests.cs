using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityErrorDetection;

namespace ParityErrorDetectionTest
{
    [TestClass]
    public class ParityUtilityTests
    {
        [TestMethod]
        public void TestComputeEvenParityBit_KnownValues()
        {
            Assert.AreEqual(0, ParityUtility.ComputeEvenParityBit(0));
            Assert.AreEqual(1, ParityUtility.ComputeEvenParityBit(1));
            Assert.AreEqual(0, ParityUtility.ComputeEvenParityBit(3));
            Assert.AreEqual(1, ParityUtility.ComputeEvenParityBit(7));
            Assert.AreEqual(1, ParityUtility.ComputeEvenParityBit(8));
        }

        [TestMethod]
        public void TestValidateParity_CorrectAndIncorrect()
        {
            int computedParity = ParityUtility.ComputeEvenParityBit(1);
            bool result = ParityUtility.ValidateParity(1, computedParity);
            Assert.IsTrue(result);
            bool resultIncorrect = ParityUtility.ValidateParity(1, (computedParity == 1) ? 0 : 1);
            Assert.IsFalse(resultIncorrect);
        }

        [TestMethod]
        public void TestAppendParityBit_NullAndEmpty()
        {
            byte[] resultNull = ParityUtility.AppendParityBit(null);
            Assert.IsNotNull(resultNull);
            Assert.AreEqual(0, resultNull.Length);
            byte[] emptyArray = new byte[0];
            byte[] resultEmpty = ParityUtility.AppendParityBit(emptyArray);
            Assert.AreEqual(1, resultEmpty.Length);
            Assert.AreEqual(0, resultEmpty[0]);
        }

        [TestMethod]
        public void TestAppendParityBit_NonEmpty()
        {
            byte[] input = new byte[]
            {
                1,
                2,
                3
            };
            byte[] output = ParityUtility.AppendParityBit(input);
            Assert.AreEqual(input.Length + 1, output.Length);
            for (int i = 0; i < input.Length; i++)
            {
                Assert.AreEqual(input[i], output[i]);
            }

            byte expectedParity = 0;
            for (int j = 0; j < input.Length; j++)
            {
                expectedParity = (byte)(expectedParity ^ input[j]);
            }

            Assert.AreEqual(expectedParity, output[input.Length]);
        }

        [TestMethod]
        public void TestAppendParityBit_RandomData()
        {
            System.Random random = new System.Random(12345);
            for (int testIndex = 0; testIndex < 10; testIndex++)
            {
                int size = random.Next(1, 100);
                byte[] testData = new byte[size];
                for (int i = 0; i < size; i++)
                {
                    testData[i] = (byte)random.Next(0, 256);
                }

                byte[] result = ParityUtility.AppendParityBit(testData);
                Assert.AreEqual(testData.Length + 1, result.Length);
                byte parity = 0;
                for (int j = 0; j < testData.Length; j++)
                {
                    parity = (byte)(parity ^ testData[j]);
                }

                Assert.AreEqual(parity, result[testData.Length]);
            }
        }

        [TestMethod]
        public void TestComputeEvenParityBit_AllBytes()
        {
            for (int i = 0; i < 256; i++)
            {
                byte value = (byte)i;
                int count = 0;
                for (int bit = 0; bit < 8; bit++)
                {
                    if ((value & (1 << bit)) != 0)
                    {
                        count = count + 1;
                    }
                }

                int expected = (count % 2 != 0) ? 1 : 0;
                Assert.AreEqual(expected, ParityUtility.ComputeEvenParityBit(value));
            }
        }
    }
}