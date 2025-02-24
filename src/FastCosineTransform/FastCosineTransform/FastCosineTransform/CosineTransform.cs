using System;
using System.Numerics;

namespace FastCosineTransform
{
    public static class CosineTransform
    {
        public static double[] ComputeDCT(double[] input)
        {
            // For optimal performance, ensure that input length is such that 2 * input.Length is a power-of-two.
            if (input == null)
            {
                return new double[0];
            }

            int N = input.Length;
            int length = 2 * N;
            Complex[] data = new Complex[length];
            for (int index = 0; index < N; index++)
            {
                data[index] = new Complex(input[index], 0.0);
                data[length - index - 1] = new Complex(input[index], 0.0);
            }

            Complex[] transform = FFT.Compute(data);
            double[] result = new double[N];
            for (int k = 0; k < N; k++)
            {
                double cosTerm = Math.Cos(Math.PI * k / (2.0 * N));
                double sinTerm = Math.Sin(Math.PI * k / (2.0 * N));
                result[k] = transform[k].Real * cosTerm + transform[k].Imaginary * sinTerm;
            }

            return result;
        }
    }
}