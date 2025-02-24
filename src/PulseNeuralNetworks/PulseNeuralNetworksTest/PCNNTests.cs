using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PulseNeuralNetworks;

namespace PulseNeuralNetworksTest
{
    [TestClass]
    public class PCNNTests
    {
        [TestMethod]
        public void Constructor_NullInput_ShouldInitializeToDefault()
        {
            double testBeta = 2.0;
            double testVF = 1.0;
            double testVL = 0.5;
            double testDecay = 0.9;
            double testThreshold = 0.5;
            int testMaxIterations = 1;
            PulseNeuralNetworks.PCNN pcnn = new PulseNeuralNetworks.PCNN(null, testBeta, testVF, testVL, testDecay, testThreshold, testMaxIterations);
            Assert.AreEqual(1, pcnn.Height);
            Assert.AreEqual(1, pcnn.Width);
            Assert.AreEqual(0.0, pcnn.Input[0, 0]);
        }

        [TestMethod]
        public void Process_SingleCellFiring()
        {
            double[, ] input = new double[, ]
            {
                {
                    1.0
                }
            };
            double testBeta = 2.0;
            double testVF = 1.0;
            double testVL = 0.5;
            double testDecay = 0.9;
            double testThreshold = 0.5;
            int testMaxIterations = 1;
            PulseNeuralNetworks.PCNN pcnn = new PulseNeuralNetworks.PCNN(input, testBeta, testVF, testVL, testDecay, testThreshold, testMaxIterations);
            double[, ] output = pcnn.Process();
            Assert.AreEqual(1.0, output[0, 0]);
        }

        [TestMethod]
        public void Process_SingleCellNotFiring()
        {
            double[, ] input = new double[1, 1];
            input[0, 0] = 0.0;
            double testBeta = 2.0;
            double testVF = 1.0;
            double testVL = 0.5;
            double testDecay = 0.9;
            double testThreshold = 0.5;
            int testMaxIterations = 5;
            PulseNeuralNetworks.PCNN pcnn = new PulseNeuralNetworks.PCNN(input, testBeta, testVF, testVL, testDecay, testThreshold, testMaxIterations);
            double[, ] output = pcnn.Process();
            Assert.AreEqual(0.0, output[0, 0]);
        }

        [TestMethod]
        public void Process_MultiCell_AllFiring()
        {
            double[, ] input = new double[, ]
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
            double testBeta = 2.0;
            double testVF = 1.0;
            double testVL = 0.5;
            double testDecay = 0.9;
            double testThreshold = 0.5;
            int testMaxIterations = 3;
            PulseNeuralNetworks.PCNN pcnn = new PulseNeuralNetworks.PCNN(input, testBeta, testVF, testVL, testDecay, testThreshold, testMaxIterations);
            double[, ] output = pcnn.Process();
            for (int i = 0; i < pcnn.Height; i++)
            {
                for (int j = 0; j < pcnn.Width; j++)
                {
                    Assert.AreEqual(1.0, output[i, j]);
                }
            }
        }

        [TestMethod]
        public void Process_ZeroIterations()
        {
            double[, ] input = new double[, ]
            {
                {
                    1.0,
                    0.5
                },
                {
                    0.2,
                    1.0
                }
            };
            double testBeta = 2.0;
            double testVF = 1.0;
            double testVL = 0.5;
            double testDecay = 0.9;
            double testThreshold = 0.5;
            int testMaxIterations = 0;
            PulseNeuralNetworks.PCNN pcnn = new PulseNeuralNetworks.PCNN(input, testBeta, testVF, testVL, testDecay, testThreshold, testMaxIterations);
            double[, ] output = pcnn.Process();
            for (int i = 0; i < pcnn.Height; i++)
            {
                for (int j = 0; j < pcnn.Width; j++)
                {
                    Assert.AreEqual(0.0, output[i, j]);
                }
            }
        }

        [TestMethod]
        public void Process_LargeMatrix()
        {
            int rows = 100;
            int cols = 100;
            double[, ] input = new double[rows, cols];
            Random random = new Random(42);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    input[i, j] = random.NextDouble();
                }
            }

            double testBeta = 2.0;
            double testVF = 1.0;
            double testVL = 0.5;
            double testDecay = 0.9;
            double testThreshold = 0.5;
            int testMaxIterations = 5;
            PulseNeuralNetworks.PCNN pcnn = new PulseNeuralNetworks.PCNN(input, testBeta, testVF, testVL, testDecay, testThreshold, testMaxIterations);
            double[, ] output = pcnn.Process();
            Assert.AreEqual(rows, output.GetLength(0));
            Assert.AreEqual(cols, output.GetLength(1));
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (output[i, j] != 0.0 && output[i, j] != 1.0)
                    {
                        Assert.Fail("Output value at (" + i + "," + j + ") is not binary: " + output[i, j]);
                    }
                }
            }
        }

        [TestMethod]
        public void Process_RectangularMatrix()
        {
            int rows = 3;
            int cols = 5;
            double[, ] input = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    input[i, j] = 1.0;
                }
            }

            double testBeta = 2.0;
            double testVF = 1.0;
            double testVL = 0.5;
            double testDecay = 0.9;
            double testThreshold = 0.5;
            int testMaxIterations = 2;
            PulseNeuralNetworks.PCNN pcnn = new PulseNeuralNetworks.PCNN(input, testBeta, testVF, testVL, testDecay, testThreshold, testMaxIterations);
            double[, ] output = pcnn.Process();
            Assert.AreEqual(rows, pcnn.Height);
            Assert.AreEqual(cols, pcnn.Width);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Assert.AreEqual(1.0, output[i, j]);
                }
            }
        }

        [TestMethod]
        public void Process_NegativeInput()
        {
            double[, ] input = new double[, ]
            {
                {
                    -1.0
                }
            };
            double testBeta = 2.0;
            double testVF = 1.0;
            double testVL = 0.5;
            double testDecay = 0.9;
            double testThreshold = 0.5;
            int testMaxIterations = 1;
            PulseNeuralNetworks.PCNN pcnn = new PulseNeuralNetworks.PCNN(input, testBeta, testVF, testVL, testDecay, testThreshold, testMaxIterations);
            double[, ] output = pcnn.Process();
            Assert.AreEqual(0.0, output[0, 0]);
        }
    }
}