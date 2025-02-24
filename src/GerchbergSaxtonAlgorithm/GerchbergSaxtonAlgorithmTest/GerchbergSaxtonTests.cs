using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GerchbergSaxtonAlgorithm;

namespace GerchbergSaxtonAlgorithmTest
{
    [TestClass]
    public class GerchbergSaxtonTests
    {
        private void AssertComplexAlmostEqual(Complex expected, Complex actual, double tolerance)
        {
            Assert.IsTrue(Math.Abs(expected.Real - actual.Real) <= tolerance, "Real parts differ: expected " + expected.Real + ", got " + actual.Real);
            Assert.IsTrue(Math.Abs(expected.Imaginary - actual.Imaginary) <= tolerance, "Imaginary parts differ: expected " + expected.Imaginary + ", got " + actual.Imaginary);
        }

        private void AssertMatrixAlmostEqual(Complex[, ] expected, Complex[, ] actual, double tolerance)
        {
            int rowsExpected = expected.GetLength(0);
            int colsExpected = expected.GetLength(1);
            int rowsActual = actual.GetLength(0);
            int colsActual = actual.GetLength(1);
            Assert.AreEqual(rowsExpected, rowsActual, "Row counts differ.");
            Assert.AreEqual(colsExpected, colsActual, "Column counts differ.");
            for (int i = 0; i < rowsExpected; i++)
            {
                for (int j = 0; j < colsExpected; j++)
                {
                    AssertComplexAlmostEqual(expected[i, j], actual[i, j], tolerance);
                }
            }
        }

        [TestMethod]
        public void TestDFTAndIDFT()
        {
            // Create a simple 2x2 matrix of Complex numbers.
            Complex[, ] input = new Complex[2, 2];
            input[0, 0] = new Complex(1.0, 0.0);
            input[0, 1] = new Complex(2.0, 0.0);
            input[1, 0] = new Complex(3.0, 0.0);
            input[1, 1] = new Complex(4.0, 0.0);
            Complex[, ] fft = FourierTransform.DFT2D(input);
            Complex[, ] inverse = FourierTransform.IDFT2D(fft);
            double tolerance = 1e-6;
            AssertMatrixAlmostEqual(input, inverse, tolerance);
        }

        [TestMethod]
        public void TestGerchbergSaxtonDimensionMismatch()
        {
            // Create object amplitude of size 2x2 and Fourier amplitude of size 2x3.
            double[, ] objectAmplitude = new double[2, 2]
            {
                {
                    1.0,
                    1.0
                },
                {
                    1.0,
                    1.0
                }
            };
            double[, ] fourierAmplitude = new double[2, 3]
            {
                {
                    1.0,
                    1.0,
                    1.0
                },
                {
                    1.0,
                    1.0,
                    1.0
                }
            };
            Complex[, ] result = GerchbergSaxton.Run(objectAmplitude, fourierAmplitude, 5);
            int rows = result.GetLength(0);
            int cols = result.GetLength(1);
            double tolerance = 1e-6;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    AssertComplexAlmostEqual(Complex.Zero, result[i, j], tolerance);
                }
            }
        }

        [TestMethod]
        public void TestGerchbergSaxtonConvergence()
        {
            // Use a 3x3 matrix with distinct amplitudes and constant Fourier amplitude.
            int rows = 3;
            int cols = 3;
            double[, ] objectAmplitude = new double[, ]
            {
                {
                    1.0,
                    2.0,
                    3.0
                },
                {
                    4.0,
                    5.0,
                    6.0
                },
                {
                    7.0,
                    8.0,
                    9.0
                }
            };
            double[, ] fourierAmplitude = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    fourierAmplitude[i, j] = 1.0;
                }
            }

            int iterations = 10;
            Complex[, ] result = GerchbergSaxton.Run(objectAmplitude, fourierAmplitude, iterations);
            double tolerance = 1e-6;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double amplitude = result[i, j].Magnitude;
                    Assert.IsTrue(Math.Abs(amplitude - objectAmplitude[i, j]) <= tolerance, "Mismatch at (" + i + ", " + j + "): expected " + objectAmplitude[i, j] + ", got " + amplitude);
                }
            }
        }

        [TestMethod]
        public void TestLargeMatrix()
        {
            // Generate a moderately large 16x16 matrix with synthetic data.
            int rows = 16;
            int cols = 16;
            double[, ] objectAmplitude = new double[rows, cols];
            double[, ] fourierAmplitude = new double[rows, cols];
            Random random = new Random(42);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double value = 0.1 + random.NextDouble() * 9.9;
                    objectAmplitude[i, j] = value;
                    fourierAmplitude[i, j] = 1.0 + random.NextDouble();
                }
            }

            int iterations = 5;
            Complex[, ] result = GerchbergSaxton.Run(objectAmplitude, fourierAmplitude, iterations);
            double tolerance = 1e-6;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double amplitude = result[i, j].Magnitude;
                    Assert.IsTrue(Math.Abs(amplitude - objectAmplitude[i, j]) <= tolerance, "Mismatch at (" + i + ", " + j + "): expected " + objectAmplitude[i, j] + ", got " + amplitude);
                }
            }
        }
    }
}