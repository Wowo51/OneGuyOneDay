using System;

namespace RiemersmaDithering.Drawing
{
    public struct Color
    {
        public int R { get; }
        public int G { get; }
        public int B { get; }

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

    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Bitmap
    {
        private Color[, ] _pixels;
        public int Width { get; }
        public int Height { get; }

        public Bitmap(int width, int height)
        {
            Width = width;
            Height = height;
            _pixels = new Color[width, height];
        }

        public Color GetPixel(int x, int y)
        {
            return _pixels[x, y];
        }

        public void SetPixel(int x, int y, Color color)
        {
            _pixels[x, y] = color;
        }
    }
}