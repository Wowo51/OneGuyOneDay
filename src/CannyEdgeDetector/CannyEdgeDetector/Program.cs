using System;
using CannyEdgeDetector.Imaging;

namespace CannyEdgeDetector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: <inputImagePath> <outputImagePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            Bitmap inputImage = new Bitmap(inputPath);
            Bitmap edgeImage = EdgeDetector.DetectEdges(inputImage, 20.0, 50.0);
            edgeImage.Save(outputPath);
            Console.WriteLine("Edge detection completed.");
        }
    }
}