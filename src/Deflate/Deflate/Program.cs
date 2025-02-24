using System;
using System.IO;

namespace Deflate
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: Deflate.exe [compress|decompress] [inputFile] [outputFile]");
                return;
            }

            string mode = args[0];
            string inputPath = args[1];
            string outputPath = args[2];
            try
            {
                byte[] inputData = File.ReadAllBytes(inputPath);
                byte[] outputData;
                if (mode.Equals("compress", StringComparison.OrdinalIgnoreCase))
                {
                    outputData = DeflateCompressor.Compress(inputData);
                }
                else if (mode.Equals("decompress", StringComparison.OrdinalIgnoreCase))
                {
                    outputData = DeflateCompressor.Decompress(inputData);
                }
                else
                {
                    Console.WriteLine("Invalid mode. Use compress or decompress.");
                    return;
                }

                File.WriteAllBytes(outputPath, outputData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}