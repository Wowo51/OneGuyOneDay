using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adler32;

namespace Adler32Test
{
    [TestClass]
    public class Adler32UnitTests
    {
        [TestMethod]
        public void Compute_NullData_ReturnsOne()
        {
            uint result = Adler32.Adler32.Compute(null);
            Assert.AreEqual(1u, result);
        }

        [TestMethod]
        public void Compute_EmptyArray_ReturnsOne()
        {
            byte[] data = new byte[0];
            uint result = Adler32.Adler32.Compute(data);
            Assert.AreEqual(1u, result);
        }

        [TestMethod]
        public void Compute_SmallArray_ReturnsExpectedValue()
        {
            byte[] data = new byte[]
            {
                1,
                2,
                3,
                4
            };
            uint result = Adler32.Adler32.Compute(data);
            Assert.AreEqual(1572875u, result);
        }

        [TestMethod]
        public void Compute_KnownInput_ReturnsExpectedChecksum()
        {
            string input = "123456789";
            byte[] data = Encoding.ASCII.GetBytes(input);
            uint result = Adler32.Adler32.Compute(data);
            uint expected = 0x091E01DE;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Compute_LargeRandomArray_ReturnsConsistentChecksum()
        {
            System.Random rng = new System.Random(42);
            int length = 100000;
            byte[] data = new byte[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = (byte)rng.Next(0, 256);
            }

            uint checksum1 = Adler32.Adler32.Compute(data);
            uint checksum2 = Adler32.Adler32.Compute(data);
            Assert.AreEqual(checksum1, checksum2);
        }

        [TestMethod]
        public void Compute_SingleElement_ReturnsExpectedValue()
        {
            byte[] data = new byte[]
            {
                255
            };
            uint result = Adler32.Adler32.Compute(data);
            // For a single element 255:
            // a = (1 + 255) = 256, b = 256,
            // so result = (256 << 16) | 256 = 16777472.
            Assert.AreEqual(16777472u, result);
        }
    }
}