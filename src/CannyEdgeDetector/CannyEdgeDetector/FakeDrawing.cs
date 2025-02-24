using System;
using System.IO;
using System.Text;

namespace CannyEdgeDetector.Imaging
{
    public class Bitmap
    {
        private int _width;
        private int _height;
        private Color[, ] _pixels;
        public int Width
        {
            get
            {
                return _width;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
        }

        public Bitmap(int width, int height)
        {
            _width = width;
            _height = height;
            _pixels = new Color[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _pixels[x, y] = Color.Black;
                }
            }
        }

        public Bitmap(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _width = 100;
                _height = 100;
                _pixels = new Color[100, 100];
                for (int y = 0; y < 100; y++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        _pixels[x, y] = Color.Black;
                    }
                }

                return;
            }

            try
            {
                string fileContent = File.ReadAllText(filePath);
                string[] tokens = fileContent.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 4 || tokens[0] != "P3")
                {
                    _width = 100;
                    _height = 100;
                    _pixels = new Color[100, 100];
                    for (int y = 0; y < 100; y++)
                    {
                        for (int x = 0; x < 100; x++)
                        {
                            _pixels[x, y] = Color.Black;
                        }
                    }

                    return;
                }

                _width = Int32.Parse(tokens[1]);
                _height = Int32.Parse(tokens[2]);
                int maxVal = Int32.Parse(tokens[3]);
                _pixels = new Color[_width, _height];
                int index = 4;
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        if (index + 2 < tokens.Length)
                        {
                            int r = Int32.Parse(tokens[index]);
                            int g = Int32.Parse(tokens[index + 1]);
                            int b = Int32.Parse(tokens[index + 2]);
                            index += 3;
                            _pixels[x, y] = new Color(r, g, b);
                        }
                        else
                        {
                            _pixels[x, y] = Color.Black;
                        }
                    }
                }
            }
            catch (Exception)
            {
                _width = 100;
                _height = 100;
                _pixels = new Color[100, 100];
                for (int y = 0; y < 100; y++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        _pixels[x, y] = Color.Black;
                    }
                }
            }
        }

        public Color GetPixel(int x, int y)
        {
            return _pixels[x, y];
        }

        public void SetPixel(int x, int y, Color color)
        {
            _pixels[x, y] = color;
        }

        public void Save(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("P3");
            sb.AppendLine($"{_width} {_height}");
            sb.AppendLine("255");
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Color pixel = _pixels[x, y];
                    sb.Append($"{pixel.R} {pixel.G} {pixel.B} ");
                }

                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString());
        }
    }

    public struct Color
    {
        public int R;
        public int G;
        public int B;
        public int A;
        public Color(int red, int green, int blue)
        {
            R = red;
            G = green;
            B = blue;
            A = 255;
        }

        public static Color FromArgb(int red, int green, int blue)
        {
            return new Color(red, green, blue);
        }

        public static Color Black
        {
            get
            {
                return new Color(0, 0, 0);
            }
        }

        public static Color White
        {
            get
            {
                return new Color(255, 255, 255);
            }
        }
    }

    public struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}