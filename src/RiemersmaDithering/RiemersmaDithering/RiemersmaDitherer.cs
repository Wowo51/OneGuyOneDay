using System;
using RiemersmaDithering.Drawing;
using System.Collections.Generic;

namespace RiemersmaDithering
{
    /// <summary>
    /// Implements the Riemersma dithering algorithm.
    /// </summary>
    public sealed class RiemersmaDitherer
    {
        /// <summary>
        /// Applies the Riemersma dithering algorithm to the input image.
        /// </summary>
        /// <param name = "input">The bitmap to dither.</param>
        /// <returns>The dithered image or null if input is null.</returns>
        public static Bitmap? ApplyRiemersmaDithering(Bitmap? input)
        {
            if (input == null)
            {
                return null;
            }

            int width = input.Width;
            int height = input.Height;
            double[, ] buffer = new double[width, height];
            bool[, ] processed = new bool[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = input.GetPixel(x, y);
                    double gray = 0.299 * color.R + 0.587 * color.G + 0.114 * color.B;
                    buffer[x, y] = gray;
                    processed[x, y] = false;
                }
            }

            foreach (Point point in SpiralOrder(width, height))
            {
                int x = point.X;
                int y = point.Y;
                double oldValue = buffer[x, y];
                double newValue = oldValue < 128 ? 0 : 255;
                double error = oldValue - newValue;
                buffer[x, y] = newValue;
                processed[x, y] = true;
                List<Point> neighbors = new List<Point>();
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        if (dx == 0 && dy == 0)
                        {
                            continue;
                        }

                        int nx = x + dx;
                        int ny = y + dy;
                        if (nx >= 0 && nx < width && ny >= 0 && ny < height && !processed[nx, ny])
                        {
                            neighbors.Add(new Point(nx, ny));
                        }
                    }
                }

                if (neighbors.Count > 0)
                {
                    double distributedError = error / neighbors.Count;
                    foreach (Point n in neighbors)
                    {
                        buffer[n.X, n.Y] = Clamp(buffer[n.X, n.Y] + distributedError, 0, 255);
                    }
                }
            }

            Bitmap output = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int colorValue = (buffer[x, y] < 128) ? 0 : 255;
                    output.SetPixel(x, y, Color.FromArgb(colorValue, colorValue, colorValue));
                }
            }

            return output;
        }

        /// <summary>
        /// Clamps a value between a specified minimum and maximum.
        /// </summary>
        /// <param name = "value">The value to clamp.</param>
        /// <param name = "min">Minimum allowable value.</param>
        /// <param name = "max">Maximum allowable value.</param>
        /// <returns>The clamped value.</returns>
        private static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Generates points in a spiral order starting from the image center.
        /// </summary>
        /// <param name = "width">The width of the image.</param>
        /// <param name = "height">The height of the image.</param>
        /// <returns>An enumerable collection of points in spiral order.</returns>
        private static IEnumerable<Point> SpiralOrder(int width, int height)
        {
            int total = width * height;
            int yielded = 0;
            int centerX = width / 2;
            int centerY = height / 2;
            int x = centerX;
            int y = centerY;
            if (IsInBounds(x, y, width, height))
            {
                yield return new Point(x, y);
                yielded++;
            }

            int step = 1;
            while (yielded < total)
            {
                for (int i = 0; i < step; i++)
                {
                    x++;
                    if (IsInBounds(x, y, width, height))
                    {
                        yield return new Point(x, y);
                        yielded++;
                        if (yielded >= total)
                        {
                            yield break;
                        }
                    }
                }

                for (int i = 0; i < step; i++)
                {
                    y++;
                    if (IsInBounds(x, y, width, height))
                    {
                        yield return new Point(x, y);
                        yielded++;
                        if (yielded >= total)
                        {
                            yield break;
                        }
                    }
                }

                step++;
                for (int i = 0; i < step; i++)
                {
                    x--;
                    if (IsInBounds(x, y, width, height))
                    {
                        yield return new Point(x, y);
                        yielded++;
                        if (yielded >= total)
                        {
                            yield break;
                        }
                    }
                }

                for (int i = 0; i < step; i++)
                {
                    y--;
                    if (IsInBounds(x, y, width, height))
                    {
                        yield return new Point(x, y);
                        yielded++;
                        if (yielded >= total)
                        {
                            yield break;
                        }
                    }
                }

                step++;
            }
        }

        /// <summary>
        /// Determines if a point is within the specified bounds.
        /// </summary>
        /// <param name = "x">The x-coordinate to test.</param>
        /// <param name = "y">The y-coordinate to test.</param>
        /// <param name = "width">The width boundary.</param>
        /// <param name = "height">The height boundary.</param>
        /// <returns>True if the point is within bounds; otherwise, false.</returns>
        private static bool IsInBounds(int x, int y, int width, int height)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
    }
}