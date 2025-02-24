using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadialBasisNetwork;

namespace RadialBasisNetworkTest
{
    [TestClass]
    public class RadialBasisNetworkTests
    {
        [TestMethod]
        public void TestRadialBasisFunctionActivate_ValidInputs()
        {
            double[] input = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double[] center = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double beta = 1.0;
            double result = RadialBasisFunction.Activate(input, center, beta);
            Assert.AreEqual(1.0, result, 0.0001);
        }

        [TestMethod]
        public void TestRadialBasisFunctionActivate_NullOrMismatchedInputs()
        {
            double[] input = new double[]
            {
                1.0,
                2.0
            };
            double[] center = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double beta = 1.0;
            double result = RadialBasisFunction.Activate(input, center, beta);
            Assert.AreEqual(0.0, result, 0.0001);
            result = RadialBasisFunction.Activate(null, center, beta);
            Assert.AreEqual(0.0, result, 0.0001);
            result = RadialBasisFunction.Activate(input, null, beta);
            Assert.AreEqual(0.0, result, 0.0001);
        }

        [TestMethod]
        public void TestRadialBasisNetworkComputeOutput_ValidInput()
        {
            List<double[]> centers = new List<double[]>
            {
                new double[]
                {
                    0.0,
                    0.0
                },
                new double[]
                {
                    1.0,
                    1.0
                }
            };
            List<double> weights = new List<double>
            {
                1.0,
                2.0
            };
            double beta = 0.5;
            RadialBasisNetwork.RadialBasisNetwork network = new RadialBasisNetwork.RadialBasisNetwork(centers, weights, beta);
            double[] input = new double[]
            {
                0.0,
                0.0
            };
            double activation0 = Math.Exp(-beta * 0.0);
            double activation1 = Math.Exp(-beta * (((0.0 - 1.0) * (0.0 - 1.0)) + ((0.0 - 1.0) * (0.0 - 1.0))));
            double expected = weights[0] * activation0 + weights[1] * activation1;
            double output = network.ComputeOutput(input);
            Assert.AreEqual(expected, output, 0.0001);
        }

        [TestMethod]
        public void TestRadialBasisNetworkComputeOutput_NullInput()
        {
            List<double[]> centers = new List<double[]>
            {
                new double[]
                {
                    0.0,
                    0.0
                }
            };
            List<double> weights = new List<double>
            {
                1.0
            };
            double beta = 1.0;
            RadialBasisNetwork.RadialBasisNetwork network = new RadialBasisNetwork.RadialBasisNetwork(centers, weights, beta);
            double output = network.ComputeOutput(null);
            Assert.AreEqual(0.0, output, 0.0001);
        }

        [TestMethod]
        public void TestRadialBasisNetworkComputeOutput_SyntheticData()
        {
            int dimensions = 5;
            int numCenters = 50;
            List<double[]> centers = new List<double[]>();
            List<double> weights = new List<double>();
            Random rnd = new Random(0);
            for (int i = 0; i < numCenters; i++)
            {
                double[] center = new double[dimensions];
                for (int j = 0; j < dimensions; j++)
                {
                    center[j] = rnd.NextDouble() * 10.0;
                }

                centers.Add(center);
                weights.Add(rnd.NextDouble() * 2.0 - 1.0);
            }

            double beta = 0.1;
            RadialBasisNetwork.RadialBasisNetwork network = new RadialBasisNetwork.RadialBasisNetwork(centers, weights, beta);
            double[] input = new double[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                input[i] = rnd.NextDouble() * 10.0;
            }

            double output = network.ComputeOutput(input);
            Assert.IsTrue(output >= -1000 && output <= 1000);
        }

        [TestMethod]
        public void TestRadialBasisNetworkWithMismatchedCenterInputDimensions()
        {
            List<double[]> centers = new List<double[]>
            {
                new double[]
                {
                    1.0,
                    1.0
                },
                new double[]
                {
                    2.0,
                    2.0,
                    2.0
                }
            };
            List<double> weights = new List<double>
            {
                1.0,
                2.0
            };
            double beta = 1.0;
            RadialBasisNetwork.RadialBasisNetwork network = new RadialBasisNetwork.RadialBasisNetwork(centers, weights, beta);
            double[] input = new double[]
            {
                1.0,
                1.0
            };
            double activation = Math.Exp(-beta * 0.0);
            double expected = 1.0 * activation;
            double output = network.ComputeOutput(input);
            Assert.AreEqual(expected, output, 0.0001);
        }

        [TestMethod]
        public void TestRadialBasisFunctionActivate_EmptyArrays()
        {
            double[] input = new double[0];
            double[] center = new double[0];
            double beta = 1.0;
            double result = RadialBasisFunction.Activate(input, center, beta);
            Assert.AreEqual(1.0, result, 0.0001);
        }

        [TestMethod]
        public void TestRadialBasisNetworkConstructorWithNullArguments()
        {
            List<double[]> centers = null;
            List<double> weights = null;
            double beta = 1.0;
            RadialBasisNetwork.RadialBasisNetwork network = new RadialBasisNetwork.RadialBasisNetwork(centers, weights, beta);
            double[] input = new double[]
            {
                1.0,
                2.0
            };
            double result = network.ComputeOutput(input);
            Assert.AreEqual(0.0, result, 0.0001);
        }

        [TestMethod]
        public void TestRadialBasisNetworkConstructorWithMismatchedCounts()
        {
            List<double[]> centers = new List<double[]>
            {
                new double[]
                {
                    1.0,
                    1.0
                }
            };
            List<double> weights = new List<double>
            {
                1.0,
                2.0
            };
            double beta = 1.0;
            RadialBasisNetwork.RadialBasisNetwork network = new RadialBasisNetwork.RadialBasisNetwork(centers, weights, beta);
            double[] input = new double[]
            {
                1.0,
                1.0
            };
            double result = network.ComputeOutput(input);
            Assert.AreEqual(0.0, result, 0.0001);
        }
    }
}