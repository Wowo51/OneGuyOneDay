using HistogramEqualization.Drawing;

namespace HistogramEqualization
{
    public class HistogramEqualizer
    {
        public Bitmap? Equalize(Bitmap? original)
        {
            if (original == null)
            {
                return null;
            }

            int width = original.Width;
            int height = original.Height;
            int totalPixels = width * height;
            int[] histogram = new int[256];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int intensity = pixel.R; // assuming grayscale image
                    histogram[intensity]++;
                }
            }

            int[] cdf = new int[256];
            cdf[0] = histogram[0];
            for (int i = 1; i < 256; i++)
            {
                cdf[i] = cdf[i - 1] + histogram[i];
            }

            int cdfMin = 0;
            for (int i = 0; i < 256; i++)
            {
                if (cdf[i] != 0)
                {
                    cdfMin = cdf[i];
                    break;
                }
            }

            if (totalPixels - cdfMin == 0)
            {
                return new Bitmap(original);
            }

            int[] mapping = new int[256];
            for (int i = 0; i < 256; i++)
            {
                double mappedValue = ((double)(cdf[i] - cdfMin) / (totalPixels - cdfMin)) * 255.0;
                if (mappedValue < 0)
                {
                    mapping[i] = 0;
                }
                else if (mappedValue > 255)
                {
                    mapping[i] = 255;
                }
                else
                {
                    mapping[i] = (int)(mappedValue + 0.5);
                }
            }

            Bitmap result = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int intensity = pixel.R;
                    int equalizedIntensity = mapping[intensity];
                    Color newPixel = Color.FromArgb(equalizedIntensity, equalizedIntensity, equalizedIntensity);
                    result.SetPixel(x, y, newPixel);
                }
            }

            return result;
        }
    }
}