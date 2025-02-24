using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaxWendroffMethod;

namespace LaxWendroffMethodTest
{
    [TestClass]
    public class LaxWendroffSolverTests
    {
        [TestMethod]
        public void TestConstantArray()
        {
            int n = 5;
            double[] constantArray = new double[n];
            for (int i = 0; i < n; i++)
            {
                constantArray[i] = 1.0;
            }

            double c = 1.0;
            double dt = 0.1;
            double dx = 1.0;
            double[] result = LaxWendroffSolver.Step(constantArray, c, dt, dx);
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(1.0, result[i], 1e-6);
            }
        }

        [TestMethod]
        public void TestSingleStep()
        {
            double[] input = new double[]
            {
                0.0,
                1.0,
                2.0
            };
            double c = 1.0;
            double dt = 0.1;
            double dx = 1.0;
            // Expected values computed manually for r = c*dt/dx = 0.1:
            // newU[0] = 0 - 0.5*0.1*(1-2) + 0.5*0.01*(1 - 0 + 2) = 0.065
            // newU[1] = 1 - 0.5*0.1*(2-0) + 0.5*0.01*(2 - 2*1 + 0) = 0.9
            // newU[2] = 2 - 0.5*0.1*(0-1) + 0.5*0.01*(0 - 4 + 1) = 2.035
            double[] expected = new double[]
            {
                0.065,
                0.9,
                2.035
            };
            double[] result = LaxWendroffSolver.Step(input, c, dt, dx);
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(expected[i], result[i], 1e-3);
            }
        }

        [TestMethod]
        public void TestLargeSyntheticData()
        {
            int n = 1000;
            double[] syntheticData = new double[n];
            System.Random rand = new System.Random(42);
            for (int i = 0; i < n; i++)
            {
                syntheticData[i] = rand.NextDouble();
            }

            double c = 1.0;
            double dt = 0.005;
            double dx = 1.0 / (n - 1);
            double[] result = LaxWendroffSolver.Step(syntheticData, c, dt, dx);
            Assert.AreEqual(n, result.Length);
        }

        [TestMethod]
        public void TestMultipleTimeStepsConservation()
        {
            int gridSize = 101;
            double[] u = new double[gridSize];
            double dx = 1.0 / (gridSize - 1);
            double c = 1.0;
            double dt = 0.005;
            int timeSteps = 200;
            for (int i = 0; i < gridSize; i++)
            {
                double x = i * dx;
                u[i] = Math.Sin(2 * Math.PI * x);
            }

            double sumInitial = 0.0;
            for (int i = 0; i < gridSize; i++)
            {
                sumInitial += u[i];
            }

            double meanInitial = sumInitial / gridSize;
            double[] current = u;
            for (int t = 0; t < timeSteps; t++)
            {
                current = LaxWendroffSolver.Step(current, c, dt, dx);
            }

            double sumFinal = 0.0;
            for (int i = 0; i < gridSize; i++)
            {
                sumFinal += current[i];
            }

            double meanFinal = sumFinal / gridSize;
            Assert.AreEqual(meanInitial, meanFinal, 1e-6);
        }
    }
}