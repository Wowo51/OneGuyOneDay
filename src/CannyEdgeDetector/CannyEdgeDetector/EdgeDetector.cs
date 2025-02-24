using System;
using CannyEdgeDetector.Imaging;
using System.Collections.Generic;

namespace CannyEdgeDetector
{
    public class EdgeDetector
    {
        public static Bitmap DetectEdges(Bitmap input, double lowThreshold, double highThreshold)
        {
            Bitmap grayscaleImage = Grayscale(input);
            Bitmap blurredImage = GaussianBlur(grayscaleImage);
            int width = blurredImage.Width;
            int height = blurredImage.Height;
            double[, ] gradientMagnitude = new double[width, height];
            double[, ] gradientDirection = new double[width, height];
            ComputeGradients(blurredImage, gradientMagnitude, gradientDirection);
            double[, ] nms = NonMaximumSuppression(gradientMagnitude, gradientDirection);
            Bitmap edgeImage = HysteresisThreshold(nms, lowThreshold, highThreshold);
            return edgeImage;
        }

        private static Bitmap Grayscale(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            Bitmap grayBmp = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color original = bmp.GetPixel(x, y);
                    int grayValue = (int)(0.299 * original.R + 0.587 * original.G + 0.114 * original.B);
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    grayBmp.SetPixel(x, y, grayColor);
                }
            }

            return grayBmp;
        }

        private static Bitmap GaussianBlur(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            Bitmap blurred = new Bitmap(width, height);
            double[, ] kernel = new double[3, 3]
            {
                {
                    1,
                    2,
                    1
                },
                {
                    2,
                    4,
                    2
                },
                {
                    1,
                    2,
                    1
                }
            };
            double kernelWeight = 16.0;
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    double sum = 0.0;
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            Color neighbor = bmp.GetPixel(x + kx, y + ky);
                            sum += neighbor.R * kernel[ky + 1, kx + 1];
                        }
                    }

                    int blurredVal = (int)(sum / kernelWeight);
                    if (blurredVal < 0)
                    {
                        blurredVal = 0;
                    }

                    if (blurredVal > 255)
                    {
                        blurredVal = 255;
                    }

                    Color blurredColor = Color.FromArgb(blurredVal, blurredVal, blurredVal);
                    blurred.SetPixel(x, y, blurredColor);
                }
            }

            for (int x = 0; x < width; x++)
            {
                blurred.SetPixel(x, 0, bmp.GetPixel(x, 0));
                blurred.SetPixel(x, height - 1, bmp.GetPixel(x, height - 1));
            }

            for (int y = 0; y < height; y++)
            {
                blurred.SetPixel(0, y, bmp.GetPixel(0, y));
                blurred.SetPixel(width - 1, y, bmp.GetPixel(width - 1, y));
            }

            return blurred;
        }

        private static void ComputeGradients(Bitmap bmp, double[, ] magnitude, double[, ] direction)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            int[, ] gxKernel = new int[3, 3]
            {
                {
                    -1,
                    0,
                    1
                },
                {
                    -2,
                    0,
                    2
                },
                {
                    -1,
                    0,
                    1
                }
            };
            int[, ] gyKernel = new int[3, 3]
            {
                {
                    -1,
                    -2,
                    -1
                },
                {
                    0,
                    0,
                    0
                },
                {
                    1,
                    2,
                    1
                }
            };
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    double gx = 0.0;
                    double gy = 0.0;
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            int pixelVal = bmp.GetPixel(x + kx, y + ky).R;
                            gx += gxKernel[ky + 1, kx + 1] * pixelVal;
                            gy += gyKernel[ky + 1, kx + 1] * pixelVal;
                        }
                    }

                    double gradMag = Math.Sqrt(gx * gx + gy * gy);
                    double gradDir = Math.Atan2(gy, gx) * (180.0 / Math.PI);
                    if (gradDir < 0)
                    {
                        gradDir += 180.0;
                    }

                    magnitude[x, y] = gradMag;
                    direction[x, y] = gradDir;
                }
            }
        }

        private static double[, ] NonMaximumSuppression(double[, ] magnitude, double[, ] direction)
        {
            int width = magnitude.GetLength(0);
            int height = magnitude.GetLength(1);
            double[, ] nms = new double[width, height];
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    double angle = direction[x, y];
                    double mag = magnitude[x, y];
                    double neighbor1 = 0.0;
                    double neighbor2 = 0.0;
                    if ((angle >= 0 && angle < 22.5) || (angle >= 157.5 && angle <= 180))
                    {
                        neighbor1 = magnitude[x - 1, y];
                        neighbor2 = magnitude[x + 1, y];
                    }
                    else if (angle >= 22.5 && angle < 67.5)
                    {
                        neighbor1 = magnitude[x + 1, y - 1];
                        neighbor2 = magnitude[x - 1, y + 1];
                    }
                    else if (angle >= 67.5 && angle < 112.5)
                    {
                        neighbor1 = magnitude[x, y - 1];
                        neighbor2 = magnitude[x, y + 1];
                    }
                    else if (angle >= 112.5 && angle < 157.5)
                    {
                        neighbor1 = magnitude[x - 1, y - 1];
                        neighbor2 = magnitude[x + 1, y + 1];
                    }

                    if (mag >= neighbor1 && mag >= neighbor2)
                    {
                        nms[x, y] = mag;
                    }
                    else
                    {
                        nms[x, y] = 0;
                    }
                }
            }

            return nms;
        }

        private static Bitmap HysteresisThreshold(double[, ] nms, double lowThreshold, double highThreshold)
        {
            int width = nms.GetLength(0);
            int height = nms.GetLength(1);
            bool[, ] edges = new bool[width, height];
            bool[, ] visited = new bool[width, height];
            Queue<Point> edgeQueue = new Queue<Point>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (nms[x, y] >= highThreshold)
                    {
                        edges[x, y] = true;
                        visited[x, y] = true;
                        edgeQueue.Enqueue(new Point(x, y));
                    }
                }
            }

            while (edgeQueue.Count > 0)
            {
                Point current = edgeQueue.Dequeue();
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        int newX = current.X + dx;
                        int newY = current.Y + dy;
                        if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                        {
                            if (!visited[newX, newY] && nms[newX, newY] >= lowThreshold)
                            {
                                edges[newX, newY] = true;
                                visited[newX, newY] = true;
                                edgeQueue.Enqueue(new Point(newX, newY));
                            }
                        }
                    }
                }
            }

            Bitmap output = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = edges[x, y] ? Color.White : Color.Black;
                    output.SetPixel(x, y, color);
                }
            }

            return output;
        }
    }
}