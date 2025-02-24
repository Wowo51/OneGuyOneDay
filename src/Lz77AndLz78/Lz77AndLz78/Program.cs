using System;

namespace Lz77AndLz78
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter text to compress:");
            string input = Console.ReadLine() ?? "";
            string lz77Compressed = LZ77Compressor.Compress(input);
            Console.WriteLine("LZ77 Compressed:");
            Console.WriteLine(lz77Compressed);
            string lz77Decompressed = LZ77Compressor.Decompress(lz77Compressed);
            Console.WriteLine("LZ77 Decompressed:");
            Console.WriteLine(lz77Decompressed);
            string lz78Compressed = LZ78Compressor.Compress(input);
            Console.WriteLine("LZ78 Compressed:");
            Console.WriteLine(lz78Compressed);
            string lz78Decompressed = LZ78Compressor.Decompress(lz78Compressed);
            Console.WriteLine("LZ78 Decompressed:");
            Console.WriteLine(lz78Decompressed);
        }
    }
}