using System;
using System.IO;
using System.IO.Compression;

namespace Deflate
{
    public static class DeflateCompressor
    {
        public static byte[] Compress(byte[] data)
        {
            if (data == null)
            {
                return new byte[0];
            }

            using (MemoryStream compressedStream = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress, true))
                {
                    deflateStream.Write(data, 0, data.Length);
                }

                return compressedStream.ToArray();
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            if (data == null)
            {
                return new byte[0];
            }

            using (MemoryStream compressedStream = new MemoryStream(data))
            {
                using (DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
                {
                    using (MemoryStream resultStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead = 0;
                        while ((bytesRead = deflateStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            resultStream.Write(buffer, 0, bytesRead);
                        }

                        return resultStream.ToArray();
                    }
                }
            }
        }
    }
}