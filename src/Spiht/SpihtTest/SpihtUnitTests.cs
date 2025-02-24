using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spiht;
using System;

namespace SpihtTest
{
    [TestClass]
    public class SpihtTests
    {
        [TestMethod]
        public void Encode_NullInput_ReturnsEmptyArray()
        {
            SpihtEncoder encoder = new SpihtEncoder();
            byte[] result = encoder.Encode(null, 8);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Encode_OneValue1_ReturnsEncoded128()
        {
            SpihtEncoder encoder = new SpihtEncoder();
            float[, ] coefficients = new float[1, 1];
            coefficients[0, 0] = 1f;
            byte[] result = encoder.Encode(coefficients, 8);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(128, result[0]);
        }

        [TestMethod]
        public void Encode_OneValue3_ReturnsEncoded192()
        {
            SpihtEncoder encoder = new SpihtEncoder();
            float[, ] coefficients = new float[1, 1];
            coefficients[0, 0] = 3f;
            byte[] result = encoder.Encode(coefficients, 16);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(192, result[0]);
        }

        [TestMethod]
        public void Encode_TruncateOutput_ReturnsEmptyArray()
        {
            SpihtEncoder encoder = new SpihtEncoder();
            float[, ] coefficients = new float[1, 1]
            {
                {
                    1f
                }
            };
            byte[] result = encoder.Encode(coefficients, 4);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Decode_NullInput_ReturnsZeroMatrix()
        {
            SpihtDecoder decoder = new SpihtDecoder();
            float[, ] result = decoder.Decode(null, 2, 3);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.GetLength(0));
            Assert.AreEqual(3, result.GetLength(1));
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(0f, result[i, j]);
                }
            }
        }

        [TestMethod]
        public void Decode_EmptyInput_ReturnsZeroMatrix()
        {
            SpihtDecoder decoder = new SpihtDecoder();
            byte[] emptyArray = new byte[0];
            float[, ] result = decoder.Decode(emptyArray, 2, 2);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.GetLength(0));
            Assert.AreEqual(2, result.GetLength(1));
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.AreEqual(0f, result[i, j]);
                }
            }
        }

        [TestMethod]
        public void Decode_KnownEncodedData_ReturnsExpectedMatrix()
        {
            SpihtDecoder decoder = new SpihtDecoder();
            byte[] encoded = new byte[1]
            {
                128
            };
            float[, ] result = decoder.Decode(encoded, 1, 1);
            Assert.AreEqual(1, result.GetLength(0));
            Assert.AreEqual(1, result.GetLength(1));
            Assert.AreEqual(128f, result[0, 0]);
        }

        [TestMethod]
        public void RoundTrip_EncodeDecode_ReturnsConsistentMatrix()
        {
            SpihtEncoder encoder = new SpihtEncoder();
            SpihtDecoder decoder = new SpihtDecoder();
            float[, ] coefficients = new float[2, 2];
            coefficients[0, 0] = 3f;
            coefficients[0, 1] = 1f;
            coefficients[1, 0] = 0f;
            coefficients[1, 1] = 2f;
            byte[] encoded = encoder.Encode(coefficients, 32);
            float[, ] decoded = decoder.Decode(encoded, 2, 2);
            Assert.AreEqual(2, decoded.GetLength(0));
            Assert.AreEqual(2, decoded.GetLength(1));
            Assert.AreEqual(3f, decoded[0, 0]);
            Assert.AreEqual(1f, decoded[0, 1]);
            Assert.AreEqual(0f, decoded[1, 0]);
            Assert.AreEqual(3f, decoded[1, 1]);
        }

        [TestMethod]
        public void EncodeDecode_RandomMatrix_ProducesMatrixOfCorrectDimensions()
        {
            Random random = new Random(0);
            int rows = 10;
            int cols = 10;
            float[, ] coefficients = new float[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    coefficients[i, j] = (float)(random.NextDouble() * 100.0);
                }
            }

            SpihtEncoder encoder = new SpihtEncoder();
            SpihtDecoder decoder = new SpihtDecoder();
            byte[] encoded = encoder.Encode(coefficients, 1024);
            float[, ] decoded = decoder.Decode(encoded, rows, cols);
            Assert.AreEqual(rows, decoded.GetLength(0));
            Assert.AreEqual(cols, decoded.GetLength(1));
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (coefficients[i, j] == 0f)
                    {
                        Assert.AreEqual(0f, decoded[i, j]);
                    }
                    else
                    {
                        Assert.IsTrue(decoded[i, j] > 0f);
                    }
                }
            }
        }

        [TestMethod]
        public void Encode_NegativeValue_ReturnsSameAsPositive()
        {
            SpihtEncoder encoder = new SpihtEncoder();
            float[, ] negativeCoefficients = new float[1, 1]
            {
                {
                    -1f
                }
            };
            byte[] negativeEncoded = encoder.Encode(negativeCoefficients, 8);
            float[, ] positiveCoefficients = new float[1, 1]
            {
                {
                    1f
                }
            };
            byte[] positiveEncoded = encoder.Encode(positiveCoefficients, 8);
            Assert.AreEqual(positiveEncoded.Length, negativeEncoded.Length);
            Assert.AreEqual(positiveEncoded[0], negativeEncoded[0]);
        }
    }
}