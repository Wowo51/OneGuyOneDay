using System;
using System.Numerics;

namespace GerchbergSaxtonAlgorithm
{
    public static class FourierTransform
    {
        public static Complex[, ] DFT2D(Complex[, ] input)
        {
            int rows = input.GetLength(0);
            int cols = input.GetLength(1);
            Complex[, ] output = new Complex[rows, cols];
            for (int k = 0; k < rows; k++)
            {
                for (int l = 0; l < cols; l++)
                {
                    Complex sum = Complex.Zero;
                    for (int m = 0; m < rows; m++)
                    {
                        for (int n = 0; n < cols; n++)
                        {
                            double angle = -2.0 * Math.PI * ((double)k * m / rows + (double)l * n / cols);
                            Complex exp = Complex.FromPolarCoordinates(1.0, angle);
                            sum += input[m, n] * exp;
                        }
                    }

                    output[k, l] = sum;
                }
            }

            return output;
        }

        public static Complex[, ] IDFT2D(Complex[, ] input)
        {
            int rows = input.GetLength(0);
            int cols = input.GetLength(1);
            Complex[, ] output = new Complex[rows, cols];
            for (int m = 0; m < rows; m++)
            {
                for (int n = 0; n < cols; n++)
                {
                    Complex sum = Complex.Zero;
                    for (int k = 0; k < rows; k++)
                    {
                        for (int l = 0; l < cols; l++)
                        {
                            double angle = 2.0 * Math.PI * ((double)k * m / rows + (double)l * n / cols);
                            Complex exp = Complex.FromPolarCoordinates(1.0, angle);
                            sum += input[k, l] * exp;
                        }
                    }

                    output[m, n] = sum / (rows * cols);
                }
            }

            return output;
        }
    }
}