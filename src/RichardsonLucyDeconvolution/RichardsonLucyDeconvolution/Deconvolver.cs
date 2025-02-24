using System;

namespace RichardsonLucyDeconvolution
{
    public static class Deconvolver
    {
        public static double[, ] Deconvolve(double[, ] blurred, double[, ] psf, int iterations)
        {
            int height = blurred.GetLength(0);
            int width = blurred.GetLength(1);
            double[, ] estimate = CopyArray(blurred);
            for (int iter = 0; iter < iterations; iter++)
            {
                double[, ] convEstimate = Convolve(estimate, psf);
                double[, ] relativeBlur = ElementWiseDivide(blurred, convEstimate);
                double[, ] psfReflected = Reflect(psf);
                double[, ] correction = Convolve(relativeBlur, psfReflected);
                estimate = ElementWiseMultiply(estimate, correction);
            }

            return estimate;
        }

        public static double[, ] Convolve(double[, ] image, double[, ] kernel)
        {
            int imgH = image.GetLength(0);
            int imgW = image.GetLength(1);
            int kerH = kernel.GetLength(0);
            int kerW = kernel.GetLength(1);
            double[, ] result = new double[imgH, imgW];
            int kerCenterY = kerH / 2;
            int kerCenterX = kerW / 2;
            for (int i = 0; i < imgH; i++)
            {
                for (int j = 0; j < imgW; j++)
                {
                    double sum = 0;
                    for (int m = 0; m < kerH; m++)
                    {
                        for (int n = 0; n < kerW; n++)
                        {
                            int y = i + m - kerCenterY;
                            int x = j + n - kerCenterX;
                            if (y >= 0 && y < imgH && x >= 0 && x < imgW)
                            {
                                sum += image[y, x] * kernel[m, n];
                            }
                        }
                    }

                    result[i, j] = sum;
                }
            }

            return result;
        }

        public static double[, ] ElementWiseDivide(double[, ] numerator, double[, ] denominator)
        {
            int height = numerator.GetLength(0);
            int width = numerator.GetLength(1);
            double[, ] result = new double[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (denominator[i, j] != 0)
                    {
                        result[i, j] = numerator[i, j] / denominator[i, j];
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
            }

            return result;
        }

        public static double[, ] ElementWiseMultiply(double[, ] a, double[, ] b)
        {
            int height = a.GetLength(0);
            int width = a.GetLength(1);
            double[, ] result = new double[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = a[i, j] * b[i, j];
                }
            }

            return result;
        }

        public static double[, ] Reflect(double[, ] kernel)
        {
            int kerH = kernel.GetLength(0);
            int kerW = kernel.GetLength(1);
            double[, ] result = new double[kerH, kerW];
            for (int i = 0; i < kerH; i++)
            {
                for (int j = 0; j < kerW; j++)
                {
                    result[i, j] = kernel[kerH - 1 - i, kerW - 1 - j];
                }
            }

            return result;
        }

        public static double[, ] CopyArray(double[, ] source)
        {
            int height = source.GetLength(0);
            int width = source.GetLength(1);
            double[, ] copy = new double[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    copy[i, j] = source[i, j];
                }
            }

            return copy;
        }
    }
}