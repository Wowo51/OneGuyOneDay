using System;
using System.IO;
using OrderedDithering.Drawing;

namespace OrderedDithering
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("Usage: OrderedDithering <inputImagePath> <outputImagePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file does not exist.");
                return;
            }

            Bitmap sourceBitmap;
            try
            {
                sourceBitmap = new Bitmap(inputPath);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not load image.");
                return;
            }

            using (sourceBitmap)
            {
                DitheringProcessor processor = new DitheringProcessor();
                Bitmap result = processor.ApplyOrderedDithering(sourceBitmap);
                using (result)
                {
                    try
                    {
                        result.Save(outputPath);
                        Console.WriteLine("Dithering applied and image saved.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Could not save image.");
                    }
                }
            }
        }
    }
}