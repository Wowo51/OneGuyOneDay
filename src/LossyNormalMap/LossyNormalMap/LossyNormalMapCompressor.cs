using System;

namespace LossyNormalMap
{
    public static class LossyNormalMapCompressor
    {
        public static byte[] Compress(NormalMap map)
        {
            if (map == null)
            {
                return new byte[0];
            }

            int width = map.Width;
            int height = map.Height;
            int headerSize = 8;
            int dataSize = width * height * 3;
            byte[] result = new byte[headerSize + dataSize];
            byte[] widthBytes = BitConverter.GetBytes(width);
            byte[] heightBytes = BitConverter.GetBytes(height);
            Array.Copy(widthBytes, 0, result, 0, 4);
            Array.Copy(heightBytes, 0, result, 4, 4);
            int index = headerSize;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Normal normal = map.Normals[x, y];
                    result[index++] = Quantize(normal.X);
                    result[index++] = Quantize(normal.Y);
                    result[index++] = Quantize(normal.Z);
                }
            }

            return result;
        }

        public static NormalMap Decompress(byte[] data)
        {
            if (data == null || data.Length < 8)
            {
                return new NormalMap(0, 0);
            }

            int width = BitConverter.ToInt32(data, 0);
            int height = BitConverter.ToInt32(data, 4);
            NormalMap map = new NormalMap(width, height);
            int headerSize = 8;
            int index = headerSize;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float xValue = Dequantize(data[index++]);
                    float yValue = Dequantize(data[index++]);
                    float zValue = Dequantize(data[index++]);
                    map.Normals[x, y] = new Normal(xValue, yValue, zValue);
                }
            }

            return map;
        }

        private static byte Quantize(float value)
        {
            double quantized = (value + 1.0) * 127.5;
            int intValue = (int)Math.Round(quantized);
            if (intValue < 0)
            {
                intValue = 0;
            }

            if (intValue > 255)
            {
                intValue = 255;
            }

            return (byte)intValue;
        }

        private static float Dequantize(byte value)
        {
            return (value / 127.5f) - 1.0f;
        }
    }
}