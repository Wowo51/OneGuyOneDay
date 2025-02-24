using System;
using System.Collections.Generic;

namespace HoughTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample 10x10 binary image with a diagonal edge.
            int rows = 10;
            int cols = 10;
            bool[, ] image = new bool[rows, cols];
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    image[y, x] = (x == y);
                }
            }

            HoughTransformProcessor processor = new HoughTransformProcessor();
            List<HoughLine> lines = processor.ProcessImage(image, 5);
            Console.WriteLine("Detected lines:");
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine("Theta: " + lines[i].Theta + "°, Rho: " + lines[i].Rho + ", Votes: " + lines[i].Votes);
            }
        }
    }
}