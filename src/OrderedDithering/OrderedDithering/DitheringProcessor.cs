using OrderedDithering.Drawing;

namespace OrderedDithering
{
    public class DitheringProcessor
    {
        // A 4x4 Bayer matrix used for ordered dithering.
        private static readonly int[, ] _bayerMatrix = new int[4, 4]
        {
            {
                0,
                8,
                2,
                10
            },
            {
                12,
                4,
                14,
                6
            },
            {
                3,
                11,
                1,
                9
            },
            {
                15,
                7,
                13,
                5
            }
        };
        public Bitmap ApplyOrderedDithering(Bitmap? source)
        {
            if (source == null)
            {
                // Return a minimal valid image when the source is null.
                return new Bitmap(1, 1);
            }

            int width = source.Width;
            int height = source.Height;
            Bitmap result = new Bitmap(width, height);
            int matrixSize = 4;
            double scale = 255.0 / (matrixSize * matrixSize);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    OrderedDithering.Drawing.Color pixelColor = source.GetPixel(x, y);
                    int gray = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                    int i = x % matrixSize;
                    int j = y % matrixSize;
                    double threshold = (_bayerMatrix[j, i] + 0.5) * scale;
                    OrderedDithering.Drawing.Color newColor = gray < threshold ? OrderedDithering.Drawing.Color.Black : OrderedDithering.Drawing.Color.White;
                    result.SetPixel(x, y, newColor);
                }
            }

            return result;
        }
    }
}