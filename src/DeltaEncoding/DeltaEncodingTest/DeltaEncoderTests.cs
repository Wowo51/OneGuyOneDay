using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeltaEncoding;

namespace DeltaEncodingTest
{
    [TestClass]
    public class DeltaEncoderTests
    {
        [TestMethod]
        public void TestEncode_NullInput_ReturnsEmptyArray()
        {
            int[]? input = null;
            int[] encoded = DeltaEncoder.Encode(input);
            Assert.IsNotNull(encoded);
            Assert.AreEqual(0, encoded.Length);
        }

        [TestMethod]
        public void TestEncode_EmptyArray_ReturnsEmptyArray()
        {
            int[] input = new int[0];
            int[] encoded = DeltaEncoder.Encode(input);
            Assert.IsNotNull(encoded);
            Assert.AreEqual(0, encoded.Length);
        }

        [TestMethod]
        public void TestEncode_SingleElement_ReturnsSameElement()
        {
            int[] input = new int[]
            {
                5
            };
            int[] encoded = DeltaEncoder.Encode(input);
            Assert.IsNotNull(encoded);
            Assert.AreEqual(1, encoded.Length);
            Assert.AreEqual(5, encoded[0]);
        }

        [TestMethod]
        public void TestEncode_MultipleElements()
        {
            int[] input = new int[]
            {
                3,
                7,
                12,
                20
            };
            int[] encoded = DeltaEncoder.Encode(input);
            Assert.IsNotNull(encoded);
            Assert.AreEqual(input.Length, encoded.Length);
            Assert.AreEqual(3, encoded[0]);
            Assert.AreEqual(4, encoded[1]);
            Assert.AreEqual(5, encoded[2]);
            Assert.AreEqual(8, encoded[3]);
        }

        [TestMethod]
        public void TestDecode_NullInput_ReturnsEmptyArray()
        {
            int[]? encoded = null;
            int[] decoded = DeltaEncoder.Decode(encoded);
            Assert.IsNotNull(decoded);
            Assert.AreEqual(0, decoded.Length);
        }

        [TestMethod]
        public void TestDecode_EmptyArray_ReturnsEmptyArray()
        {
            int[] encoded = new int[0];
            int[] decoded = DeltaEncoder.Decode(encoded);
            Assert.IsNotNull(decoded);
            Assert.AreEqual(0, decoded.Length);
        }

        [TestMethod]
        public void TestDecode_SingleElement_ReturnsSameElement()
        {
            int[] encoded = new int[]
            {
                5
            };
            int[] decoded = DeltaEncoder.Decode(encoded);
            Assert.IsNotNull(decoded);
            Assert.AreEqual(1, decoded.Length);
            Assert.AreEqual(5, decoded[0]);
        }

        [TestMethod]
        public void TestDecode_MultipleElements()
        {
            int[] encoded = new int[]
            {
                10,
                -3,
                5,
                2
            };
            int[] expected = new int[]
            {
                10,
                7,
                12,
                14
            };
            int[] decoded = DeltaEncoder.Decode(encoded);
            Assert.IsNotNull(decoded);
            Assert.AreEqual(expected.Length, decoded.Length);
            for (int index = 0; index < expected.Length; index++)
            {
                Assert.AreEqual(expected[index], decoded[index]);
            }
        }

        [TestMethod]
        public void TestEncodeDecode_Cycle()
        {
            int[] input = new int[]
            {
                2,
                4,
                8,
                16,
                32
            };
            int[] encoded = DeltaEncoder.Encode(input);
            int[] decoded = DeltaEncoder.Decode(encoded);
            Assert.IsNotNull(decoded);
            Assert.AreEqual(input.Length, decoded.Length);
            for (int index = 0; index < input.Length; index++)
            {
                Assert.AreEqual(input[index], decoded[index]);
            }
        }

        [TestMethod]
        public void TestLargeDataSet()
        {
            int largeSize = 10000;
            int[] input = new int[largeSize];
            System.Random random = new System.Random(0);
            for (int index = 0; index < largeSize; index++)
            {
                input[index] = random.Next(-1000, 1000);
            }

            int[] encoded = DeltaEncoder.Encode(input);
            int[] decoded = DeltaEncoder.Decode(encoded);
            Assert.AreEqual(input.Length, encoded.Length);
            Assert.AreEqual(input.Length, decoded.Length);
            for (int index = 0; index < input.Length; index++)
            {
                Assert.AreEqual(input[index], decoded[index]);
            }
        }
    }
}