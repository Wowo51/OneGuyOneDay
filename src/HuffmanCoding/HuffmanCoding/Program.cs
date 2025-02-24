using System;

namespace HuffmanCoding
{
    public class Program
    {
        public static void Main()
        {
            string input = "this is an example for huffman encoding";
            HuffmanTree tree = HuffmanTree.BuildTree(input);
            string compressed = HuffmanCoding.Compress(input, tree);
            string decompressed = HuffmanCoding.Decompress(compressed, tree);
            Console.WriteLine("Input: " + input);
            Console.WriteLine("Compressed: " + compressed);
            Console.WriteLine("Decompressed: " + decompressed);
        }
    }
}