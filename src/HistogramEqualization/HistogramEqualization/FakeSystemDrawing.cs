namespace HistogramEqualization.Drawing
{
    public class Bitmap
    {
        private Color[, ] _pixels;
        private readonly int _width;
        private readonly int _height;
        public Bitmap(int width, int height)
        {
            _width = width;
            _height = height;
            _pixels = new Color[height, width];
        }

        public Bitmap(Bitmap original)
        {
            _width = original.Width;
            _height = original.Height;
            _pixels = new Color[_height, _width];
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _pixels[y, x] = original.GetPixel(x, y);
                }
            }
        }

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

        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                return new Color(0, 0, 0);
            }

            return _pixels[y, x];
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x >= 0 && x < _width && y >= 0 && y < _height)
            {
                _pixels[y, x] = color;
            }
        }
    }

    public class Color
    {
        private int _r;
        private int _g;
        private int _b;
        public Color(int r, int g, int b)
        {
            _r = r;
            _g = g;
            _b = b;
        }

        public int R
        {
            get
            {
                return _r;
            }
        }

        public int G
        {
            get
            {
                return _g;
            }
        }

        public int B
        {
            get
            {
                return _b;
            }
        }

        public static Color FromArgb(int r, int g, int b)
        {
            return new Color(r, g, b);
        }
    }
}