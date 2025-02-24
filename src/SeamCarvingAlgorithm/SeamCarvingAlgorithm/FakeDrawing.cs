using System;

namespace SeamCarvingAlgorithm.Drawing
{
    public struct Color
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static Color FromArgb(int r, int g, int b)
        {
            return new Color(r, g, b);
        }
    }

    public class Bitmap
    {
        private Color[, ] pixels;
        public int Width
        {
            get
            {
                return pixels.GetLength(1);
            }
        }

        public int Height
        {
            get
            {
                return pixels.GetLength(0);
            }
        }

        public Bitmap(int width, int height)
        {
            pixels = new Color[height, width];
        }

        public Color GetPixel(int x, int y)
        {
            return pixels[y, x];
        }

        public void SetPixel(int x, int y, Color color)
        {
            pixels[y, x] = color;
        }
    }
}