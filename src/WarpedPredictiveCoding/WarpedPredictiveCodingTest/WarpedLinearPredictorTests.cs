using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarpedPredictiveCoding;

namespace WarpedPredictiveCodingTest
{
    [TestClass]
    public class WarpedLinearPredictorTests
    {
        [TestMethod]
        public void ComputeCoefficients_NullSignal_ReturnsEmptyArray()
        {
            WarpedLinearPredictor predictor = new WarpedLinearPredictor();
            double[] result = predictor.ComputeCoefficients(null, 2, 0.5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void ComputeCoefficients_EmptySignal_ReturnsEmptyArray()
        {
            WarpedLinearPredictor predictor = new WarpedLinearPredictor();
            double[] result = predictor.ComputeCoefficients(new double[0], 2, 0.5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void ComputeCoefficients_OrderLessThanOne_ReturnsEmptyArray()
        {
            WarpedLinearPredictor predictor = new WarpedLinearPredictor();
            double[] result = predictor.ComputeCoefficients(new double[] { 1.0, 2.0, 3.0 }, 0, 0.5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void ComputeCoefficients_OrderGreaterThanOrEqualSignalLength_ReturnsEmptyArray()
        {
            WarpedLinearPredictor predictor = new WarpedLinearPredictor();
            double[] signal = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double[] result = predictor.ComputeCoefficients(signal, 3, 0.5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void ComputeCoefficients_ValidInput_ReturnsCoefficients()
        {
            WarpedLinearPredictor predictor = new WarpedLinearPredictor();
            double[] signal = new double[]
            {
                1.0,
                2.0,
                3.0,
                4.0,
                5.0
            };
            int order = 2;
            double warp = 0.5;
            double[] coefficients = predictor.ComputeCoefficients(signal, order, warp);
            Assert.IsNotNull(coefficients);
            Assert.AreEqual(order, coefficients.Length);
            // The expected coefficients are approximately calculated.
            double expected0 = 0.81397;
            double expected1 = -0.1193;
            double tolerance = 1e-3;
            Assert.AreEqual(expected0, coefficients[0], tolerance, "Coefficient 0 mismatch");
            Assert.AreEqual(expected1, coefficients[1], tolerance, "Coefficient 1 mismatch");
        }

        [TestMethod]
        public void ComputeCoefficients_SyntheticTest_DataLengthAndNonZero()
        {
            WarpedLinearPredictor predictor = new WarpedLinearPredictor();
            int signalLength = 1000;
            int order = 10;
            double warp = 0.3;
            double[] signal = new double[signalLength];
            Random random = new Random(12345);
            for (int i = 0; i < signalLength; i++)
            {
                signal[i] = random.NextDouble() * 100.0;
            }

            double[] coefficients = predictor.ComputeCoefficients(signal, order, warp);
            Assert.IsNotNull(coefficients);
            Assert.AreEqual(order, coefficients.Length);
            bool nonZeroFound = false;
            for (int i = 0; i < coefficients.Length; i++)
            {
                if (Math.Abs(coefficients[i]) > 1e-8)
                {
                    nonZeroFound = true;
                    break;
                }
            }

            Assert.IsTrue(nonZeroFound, "At least one coefficient should be non-zero for random input.");
        }
    }
}