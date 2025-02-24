using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlauberDynamics;

namespace GlauberDynamicsTest
{
    [TestClass]
    public class GlauberDynamicsTests
    {
        [TestMethod]
        public void TestIsingModelInitialization()
        {
            int rows = 10;
            int cols = 10;
            double temperature = 2.269;
            double coupling = 1.0;
            double externalField = 0.0;
            IsingModel model = new IsingModel(rows, cols, temperature, coupling, externalField);
            Assert.AreEqual(rows, model.Rows);
            Assert.AreEqual(cols, model.Columns);
            Assert.AreEqual(temperature, model.Temperature);
            Assert.AreEqual(coupling, model.Coupling);
            Assert.AreEqual(externalField, model.ExternalField);
            Assert.IsNotNull(model.Spins);
            Assert.IsNotNull(model.Random);
        }

        [TestMethod]
        public void TestRandomizeSpins()
        {
            int rows = 5;
            int cols = 5;
            IsingModel model = new IsingModel(rows, cols, 2.5, 1.0, 0.0);
            model.RandomizeSpins();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int spin = model.Spins[i, j];
                    Assert.IsTrue(spin == 1 || spin == -1, "Spin must be either 1 or -1");
                }
            }
        }

        [TestMethod]
        public void TestCalculateDeltaEnergyUniformPositive()
        {
            int size = 3;
            double temperature = 2.269;
            double coupling = 1.0;
            double externalField = 0.0;
            IsingModel model = new IsingModel(size, size, temperature, coupling, externalField);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    model.Spins[i, j] = 1;
                }
            }

            double deltaEnergy = model.CalculateDeltaEnergy(1, 1);
            // For a uniform grid, neighbor sum = 1+1+1+1 = 4, so deltaE = 2 * 1 * (1*4 + 0) = 8.
            Assert.AreEqual(8.0, deltaEnergy, 0.0001);
        }

        [TestMethod]
        public void TestCalculateDeltaEnergyCheckerboard()
        {
            int rows = 2;
            int cols = 2;
            double temperature = 1.0;
            double coupling = 1.0;
            double externalField = 0.0;
            IsingModel model = new IsingModel(rows, cols, temperature, coupling, externalField);
            // Checkerboard configuration: [ [1, -1], [-1, 1] ]
            model.Spins[0, 0] = 1;
            model.Spins[0, 1] = -1;
            model.Spins[1, 0] = -1;
            model.Spins[1, 1] = 1;
            double deltaEnergy = model.CalculateDeltaEnergy(0, 0);
            // For (0,0): neighbors are taken with periodic boundaries yielding all -1's, so deltaE = 2*1*((1*-4)+0) = -8.
            Assert.AreEqual(-8.0, deltaEnergy, 0.0001);
        }

        [TestMethod]
        public void TestTotalEnergyUniform()
        {
            int rows = 2;
            int cols = 2;
            double temperature = 2.0;
            double coupling = 1.0;
            double externalField = 0.0;
            IsingModel model = new IsingModel(rows, cols, temperature, coupling, externalField);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    model.Spins[i, j] = 1;
                }
            }

            // For a 2x2 grid with all spins 1, the total energy is expected to be -8.
            double energy = model.TotalEnergy();
            Assert.AreEqual(-8.0, energy, 0.0001);
        }

        [TestMethod]
        public void TestSimulatorStep()
        {
            int rows = 2;
            int cols = 2;
            double temperature = 1.0;
            double coupling = 1.0;
            double externalField = 0.0;
            IsingModel model = new IsingModel(rows, cols, temperature, coupling, externalField);
            // Initialize all spins to 1.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    model.Spins[i, j] = 1;
                }
            }

            GlauberDynamicsSimulator simulator = new GlauberDynamicsSimulator(model);
            int beforeOnes = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (model.Spins[i, j] == 1)
                    {
                        beforeOnes++;
                    }
                }
            }

            simulator.Step();
            int afterOnes = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (model.Spins[i, j] == 1)
                    {
                        afterOnes++;
                    }
                }
            }

            // Since only one spin can flip at most, the count of ones should be either unchanged (4) or reduced by one (3).
            Assert.IsTrue(afterOnes == 3 || afterOnes == 4, "Simulator step should flip at most one spin.");
        }

        [TestMethod]
        public void TestLargeSimulation()
        {
            int rows = 100;
            int cols = 100;
            double temperature = 2.0;
            double coupling = 1.0;
            double externalField = 0.0;
            IsingModel model = new IsingModel(rows, cols, temperature, coupling, externalField);
            model.RandomizeSpins();
            GlauberDynamicsSimulator simulator = new GlauberDynamicsSimulator(model);
            simulator.Run(10000);
            double energy = model.TotalEnergy();
            // Check that energy is within plausible bounds.
            Assert.IsTrue(energy <= rows * cols * 2 && energy >= -rows * cols * 2, "Energy should be within plausible bounds.");
        }
    }
}