using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExponentialGolombCoding;

namespace ExponentialGolombCodingTest
{
    [TestClass]
    public class ExponentialGolombTests
    {
        [TestMethod]
        public void Encode_Zero_Returns1()
        {
            string encoded = ExponentialGolomb.Encode(0);
            Assert.AreEqual("1", encoded);
        }

        [TestMethod]
        public void Encode_Five_ReturnsExpected()
        {
            string encoded = ExponentialGolomb.Encode(5);
            Assert.AreEqual("00110", encoded);
        }

        [TestMethod]
        public void Decode_ValidCodeword_ReturnsCorrectValue()
        {
            int bitsRead;
            uint value = ExponentialGolomb.Decode("00110", out bitsRead);
            Assert.AreEqual(5u, value);
            Assert.AreEqual(5, bitsRead);
        }

        [TestMethod]
        public void Decode_IncompleteBitstream_ReturnsZero()
        {
            int bitsRead;
            uint value = ExponentialGolomb.Decode("0", out bitsRead);
            Assert.AreEqual(0u, value);
            Assert.AreEqual(1, bitsRead);
        }

        [TestMethod]
        public void Decode_NullBitstream_ReturnsZero()
        {
            int bitsRead;
            uint value = ExponentialGolomb.Decode(null, out bitsRead);
            Assert.AreEqual(0u, value);
            Assert.AreEqual(0, bitsRead);
        }

        [TestMethod]
        public void EncodeDecode_RandomValues()
        {
            System.Random random = new System.Random();
            for (int i = 0; i < 1000; i = i + 1)
            {
                uint testValue = (uint)random.Next(0, 10000);
                string encoded = ExponentialGolomb.Encode(testValue);
                int bitsRead;
                uint decoded = ExponentialGolomb.Decode(encoded, out bitsRead);
                Assert.AreEqual(testValue, decoded, "Failed at value " + testValue);
            }
        }

        [TestMethod]
        public void Decode_WithExtraBits_ReturnsCorrectValueAndCountsBits()
        {
            string codeword = ExponentialGolomb.Encode(10);
            string extra = "111";
            string input = codeword + extra;
            int bitsRead;
            uint decoded = ExponentialGolomb.Decode(input, out bitsRead);
            Assert.AreEqual(10u, decoded);
            Assert.AreEqual(codeword.Length, bitsRead);
        }
    }
}