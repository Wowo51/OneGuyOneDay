using Microsoft.VisualStudio.TestTools.UnitTesting;
using FELICSImageCompression;

namespace FELICSImageCompressionTest
{
    [TestClass]
    public class FELICSCompressorTests
    {
        [TestMethod]
        public void Compress_NullInput_ReturnsEmpty()
        {
            FELICSCompressor compressor = new FELICSCompressor();
            byte[] result = compressor.Compress(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Compress_EmptyInput_ReturnsEmpty()
        {
            FELICSCompressor compressor = new FELICSCompressor();
            byte[] input = new byte[0];
            byte[] result = compressor.Compress(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Compress_SimpleData_ReturnsCorrectCompressedData()
        {
            FELICSCompressor compressor = new FELICSCompressor();
            byte[] input = new byte[]
            {
                1,
                1,
                1,
                2,
                2,
                3
            };
            byte[] expected = new byte[]
            {
                3,
                1,
                2,
                2,
                1,
                3
            };
            byte[] result = compressor.Compress(input);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress_NullInput_ReturnsEmpty()
        {
            FELICSCompressor compressor = new FELICSCompressor();
            byte[] result = compressor.Decompress(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Decompress_IncompletePair_IgnoresLastByte()
        {
            FELICSCompressor compressor = new FELICSCompressor();
            byte[] compressed = new byte[]
            {
                3,
                1,
                2
            };
            // Expected: first pair (count=3, value=1) decoded as {1,1,1}; incomplete trailing byte is ignored.
            byte[] expected = new byte[]
            {
                1,
                1,
                1
            };
            byte[] result = compressor.Decompress(compressed);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RoundTrip_CompressAndDecompress_ReturnsOriginalData()
        {
            FELICSCompressor compressor = new FELICSCompressor();
            byte[] input = new byte[]
            {
                4,
                4,
                4,
                5,
                5,
                6,
                6,
                6,
                6
            };
            byte[] compressed = compressor.Compress(input);
            byte[] decompressed = compressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void Compress_LongRunSplitsCountAt255()
        {
            FELICSCompressor compressor = new FELICSCompressor();
            byte[] input = new byte[300];
            for (int i = 0; i < 300; i++)
            {
                input[i] = 7;
            }

            byte[] compressed = compressor.Compress(input);
            // Expected: run of 300 is split as 255 and then 45.
            byte[] expected = new byte[]
            {
                255,
                7,
                45,
                7
            };
            CollectionAssert.AreEqual(expected, compressed);
        }

        [TestMethod]
        public void LargeData_RoundTrip_CompressAndDecompress()
        {
            FELICSCompressor compressor = new FELICSCompressor();
            int size = 10000;
            byte[] input = new byte[size];
            for (int i = 0; i < size; i++)
            {
                if ((i % 3) == 0)
                {
                    input[i] = 10;
                }
                else if ((i % 3) == 1)
                {
                    input[i] = 20;
                }
                else
                {
                    input[i] = 30;
                }
            }

            byte[] compressed = compressor.Compress(input);
            byte[] decompressed = compressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }
    }
}