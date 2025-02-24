using System;
using System.Numerics;

namespace GerchbergSaxtonAlgorithm
{
    public class GerchbergSaxton
    {
        public static Complex[, ] Run(double[, ] objectPlaneAmplitude, double[, ] fourierPlaneAmplitude, int iterations)
        {
            int rows = objectPlaneAmplitude.GetLength(0);
            int cols = objectPlaneAmplitude.GetLength(1);
            if (fourierPlaneAmplitude.GetLength(0) != rows || fourierPlaneAmplitude.GetLength(1) != cols)
            {
                Complex[, ] errorResult = new Complex[rows, cols];
                for (int m = 0; m < rows; m++)
                {
                    for (int n = 0; n < cols; n++)
                    {
                        errorResult[m, n] = Complex.Zero;
                    }
                }

                return errorResult;
            }

            Complex[, ] currentGuess = new Complex[rows, cols];
            for (int m = 0; m < rows; m++)
            {
                for (int n = 0; n < cols; n++)
                {
                    double amplitude = objectPlaneAmplitude[m, n];
                    currentGuess[m, n] = new Complex(amplitude, 0.0);
                }
            }

            for (int iter = 0; iter < iterations; iter++)
            {
                Complex[, ] fftResult = FourierTransform.DFT2D(currentGuess);
                for (int k = 0; k < rows; k++)
                {
                    for (int l = 0; l < cols; l++)
                    {
                        double phase = (fftResult[k, l].Magnitude == 0.0) ? 0.0 : fftResult[k, l].Phase;
                        fftResult[k, l] = Complex.FromPolarCoordinates(fourierPlaneAmplitude[k, l], phase);
                    }
                }

                Complex[, ] inverseFft = FourierTransform.IDFT2D(fftResult);
                for (int m = 0; m < rows; m++)
                {
                    for (int n = 0; n < cols; n++)
                    {
                        double phase = (inverseFft[m, n].Magnitude == 0.0) ? 0.0 : inverseFft[m, n].Phase;
                        currentGuess[m, n] = Complex.FromPolarCoordinates(objectPlaneAmplitude[m, n], phase);
                    }
                }
            }

            return currentGuess;
        }
    }
}