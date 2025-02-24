using System;
using SeamCarvingAlgorithm.Drawing;

namespace SeamCarvingAlgorithm
{
    public sealed class SeamCarver
    {
        private Color[, ] image;
        private int width;
        private int height;
        public SeamCarver(Bitmap? bitmap)
        {
            if (bitmap == null)
            {
                bitmap = new Bitmap(1, 1);
            }

            width = bitmap.Width;
            height = bitmap.Height;
            image = new Color[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    image[y, x] = bitmap.GetPixel(x, y);
                }
            }
        }

        public Bitmap Resize(int targetWidth, int targetHeight)
        {
            while (width > targetWidth)
            {
                double[, ] energy = ComputeEnergy();
                int[] seam = FindVerticalSeam(energy);
                RemoveVerticalSeam(seam);
                width = width - 1;
            }

            while (height > targetHeight)
            {
                TransposeImage();
                double[, ] energy = ComputeEnergy();
                int[] seam = FindVerticalSeam(energy);
                RemoveVerticalSeam(seam);
                width = width - 1;
                TransposeImage();
            }

            Bitmap result = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result.SetPixel(x, y, image[y, x]);
                }
            }

            return result;
        }

        private double[, ] ComputeEnergy()
        {
            double[, ] energy = new double[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    energy[y, x] = ComputeEnergyAt(x, y);
                }
            }

            return energy;
        }

        private double ComputeEnergyAt(int x, int y)
        {
            if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
            {
                return 1000.0;
            }

            Color left = image[y, x - 1];
            Color right = image[y, x + 1];
            Color up = image[y - 1, x];
            Color down = image[y + 1, x];
            double deltaX = Math.Pow(right.R - left.R, 2) + Math.Pow(right.G - left.G, 2) + Math.Pow(right.B - left.B, 2);
            double deltaY = Math.Pow(down.R - up.R, 2) + Math.Pow(down.G - up.G, 2) + Math.Pow(down.B - up.B, 2);
            return Math.Sqrt(deltaX + deltaY);
        }

        private int[] FindVerticalSeam(double[, ] energy)
        {
            int[, ] edgeTo = new int[height, width];
            double[, ] distTo = new double[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0)
                    {
                        distTo[y, x] = energy[y, x];
                    }
                    else
                    {
                        distTo[y, x] = double.MaxValue;
                    }

                    edgeTo[y, x] = 0;
                }
            }

            for (int y = 1; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        int prevX = x + i;
                        if (prevX >= 0 && prevX < width)
                        {
                            double newDist = distTo[y - 1, prevX] + energy[y, x];
                            if (distTo[y, x] > newDist)
                            {
                                distTo[y, x] = newDist;
                                edgeTo[y, x] = prevX;
                            }
                        }
                    }
                }
            }

            double minEnergy = double.MaxValue;
            int minIndex = 0;
            for (int x = 0; x < width; x++)
            {
                if (distTo[height - 1, x] < minEnergy)
                {
                    minEnergy = distTo[height - 1, x];
                    minIndex = x;
                }
            }

            int[] seam = new int[height];
            seam[height - 1] = minIndex;
            for (int y = height - 1; y > 0; y--)
            {
                seam[y - 1] = edgeTo[y, seam[y]];
            }

            return seam;
        }

        private void RemoveVerticalSeam(int[] seam)
        {
            Color[, ] newImage = new Color[height, width - 1];
            for (int y = 0; y < height; y++)
            {
                int newCol = 0;
                for (int x = 0; x < width; x++)
                {
                    if (x != seam[y])
                    {
                        newImage[y, newCol] = image[y, x];
                        newCol++;
                    }
                }
            }

            image = newImage;
        }

        private void TransposeImage()
        {
            Color[, ] transposed = new Color[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    transposed[x, y] = image[y, x];
                }
            }

            int temp = width;
            width = height;
            height = temp;
            image = transposed;
        }
    }
}