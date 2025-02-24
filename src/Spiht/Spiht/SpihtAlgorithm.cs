using System;
using System.Collections.Generic;
using System.Text;

namespace Spiht
{
    /// <summary>
    /// A simplified implementation of a SPIHT‐like encoder using bit‐plane coding.
    /// This implementation partitions the coefficient matrix hierarchically by processing
    /// successive bit planes. Although it does not implement the full SPIHT algorithm with
    /// list management (LIP, LIS, LSP), it demonstrates the idea of partitioning in hierarchical trees.
    /// </summary>
    public class SpihtEncoder
    {
        public byte[] Encode(float[, ] coefficients, int maxBits)
        {
            if (coefficients == null)
            {
                return new byte[0];
            }

            int rows = coefficients.GetLength(0);
            int cols = coefficients.GetLength(1);
            float maxVal = 0.0f;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    float absValue = Math.Abs(coefficients[i, j]);
                    if (absValue > maxVal)
                    {
                        maxVal = absValue;
                    }
                }
            }

            int exponent;
            if (maxVal > 0)
            {
                exponent = (int)Math.Floor(Math.Log(maxVal, 2));
            }
            else
            {
                exponent = 0;
            }

            // Initial threshold is 2^exponent.
            float threshold = (float)Math.Pow(2, exponent);
            int numBitPlanes = exponent + 1;
            // Build a bit string by scanning the coefficients for each bit plane.
            // For each plane, a '1' indicates that |coefficient| exceeds the current threshold.
            StringBuilder sb = new StringBuilder();
            for (int bp = 0; bp < numBitPlanes; bp++)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        float value = coefficients[i, j];
                        if (Math.Abs(value) >= threshold)
                        {
                            sb.Append('1');
                        }
                        else
                        {
                            sb.Append('0');
                        }
                    }
                }

                threshold = threshold / 2;
            }

            string bitString = sb.ToString();
            int totalBits = bitString.Length;
            int totalBytes = (totalBits + 7) / 8;
            byte[] encoded = new byte[totalBytes];
            // Pack the bit string into bytes.
            for (int i = 0; i < totalBytes; i++)
            {
                int start = i * 8;
                int len = Math.Min(8, totalBits - start);
                string byteString = bitString.Substring(start, len).PadRight(8, '0');
                byte b = Convert.ToByte(byteString, 2);
                encoded[i] = b;
            }

            // Truncate the output to maxBits (expressed in bits) if necessary.
            int maxBytes = maxBits / 8;
            if (encoded.Length > maxBytes)
            {
                byte[] truncated = new byte[maxBytes];
                Array.Copy(encoded, 0, truncated, 0, maxBytes);
                encoded = truncated;
            }

            return encoded;
        }
    }

    /// <summary>
    /// A simplified SPIHT‐like decoder corresponding to the encoder above.
    /// It reconstructs a coefficient matrix from the packed bit‐plane data.
    /// Note: The sign information is not encoded in this simplified version,
    /// so the decoded coefficients are non-negative approximations.
    /// </summary>
    public class SpihtDecoder
    {
        public float[, ] Decode(byte[] encodedData, int rows, int cols)
        {
            float[, ] coefficients = new float[rows, cols];
            if (encodedData == null || encodedData.Length == 0)
            {
                return coefficients;
            }

            // Convert the byte array back to a binary string.
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encodedData.Length; i++)
            {
                string bits = Convert.ToString(encodedData[i], 2).PadLeft(8, '0');
                sb.Append(bits);
            }

            string bitString = sb.ToString();
            int totalPixels = rows * cols;
            int numBitPlanes = bitString.Length / totalPixels;
            if (numBitPlanes < 1)
            {
                return coefficients;
            }

            // Reconstruct each coefficient from its bit-plane contributions.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    float value = 0.0f;
                    float bitPlaneThreshold = (float)Math.Pow(2, numBitPlanes - 1);
                    for (int bp = 0; bp < numBitPlanes; bp++)
                    {
                        int index = bp * totalPixels + (i * cols + j);
                        if (index < bitString.Length && bitString[index] == '1')
                        {
                            value += bitPlaneThreshold;
                        }

                        bitPlaneThreshold /= 2;
                    }

                    coefficients[i, j] = value;
                }
            }

            return coefficients;
        }
    }
}