using System;
using System.IO;

namespace OrderedDithering.Drawing
{
    public struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Color(int r, int g, int b) : this((byte)r, (byte)g, (byte)b)
        {
        }

        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color White = new Color(255, 255, 255);
        public override string ToString() => $"RGB({R},{G},{B})";
    }

    public class Bitmap : IDisposable
    {
        private Color[, ] _pixels;
        public int Width { get; }
        public int Height { get; }

        public Bitmap(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                width = 1;
                height = 1;
            }

            Width = width;
            Height = height;
            _pixels = new Color[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _pixels[x, y] = Color.White;
                }
            }
        }

        public Bitmap(string filename)
        {
            // Simulate loading an image by creating a default 100x100 bitmap
            Width = 100;
            Height = 100;
            _pixels = new Color[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _pixels[x, y] = new Color(128, 128, 128);
                }
            }
        }

        public Color GetPixel(int x, int y)
        {
            return (x < 0 || x >= Width || y < 0 || y >= Height) ? Color.Black : _pixels[x, y];
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                _pixels[x, y] = color;
            }
        }

        public void Save(string filename)
        {
            File.WriteAllText(filename, $"Pseudobitmap: {Width}x{Height}");
        }

        public void Dispose()
        {
        // No unmanaged resources to release in this stub
        }
    }
}