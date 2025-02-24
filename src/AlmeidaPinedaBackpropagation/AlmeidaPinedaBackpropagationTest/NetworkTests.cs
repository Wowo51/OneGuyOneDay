using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlmeidaPinedaBackpropagation;

namespace AlmeidaPinedaBackpropagationTest
{
    [TestClass]
    public class NetworkTests
    {
        [TestMethod]
        public void Compute_WithNullInput_ReturnsExpectedOutputLength()
        {
            int inputCount = 3;
            int hiddenCount = 4;
            int outputCount = 2;
            AlmeidaPinedaNetwork network = new AlmeidaPinedaNetwork(inputCount, hiddenCount, outputCount);
            double[] outputs = network.Compute(null);
            Assert.IsNotNull(outputs);
            Assert.AreEqual(outputCount, outputs.Length);
        }

        [TestMethod]
        public void Compute_WithValidInput_ReturnsOutputInRange()
        {
            int inputCount = 3;
            int hiddenCount = 4;
            int outputCount = 2;
            AlmeidaPinedaNetwork network = new AlmeidaPinedaNetwork(inputCount, hiddenCount, outputCount);
            double[] inputs = new double[3]
            {
                0.1,
                0.5,
                -0.3
            };
            double[] outputs = network.Compute(inputs);
            Assert.IsNotNull(outputs);
            Assert.AreEqual(outputCount, outputs.Length);
            for (int i = 0; i < outputs.Length; i++)
            {
                Assert.IsTrue(outputs[i] >= 0.0 && outputs[i] <= 1.0, "Output is not in the range 0 to 1.");
            }
        }

        [TestMethod]
        public void Train_OutputChangesAfterTraining()
        {
            int inputCount = 3;
            int hiddenCount = 4;
            int outputCount = 2;
            AlmeidaPinedaNetwork network = new AlmeidaPinedaNetwork(inputCount, hiddenCount, outputCount);
            double[] inputs = new double[3]
            {
                0.2,
                -0.1,
                0.3
            };
            double[] targets = new double[2]
            {
                0.7,
                0.3
            };
            double[] outputsBefore = network.Compute(inputs);
            double[] trainOutputs = network.Train(inputs, targets, 0.5);
            double[] outputsAfter = network.Compute(inputs);
            bool changed = false;
            for (int i = 0; i < outputCount; i++)
            {
                if (Math.Abs(outputsAfter[i] - outputsBefore[i]) > 1e-6)
                {
                    changed = true;
                    break;
                }
            }

            Assert.IsTrue(changed, "Outputs did not significantly change after training.");
        }

        [TestMethod]
        public void SyntheticStressTest_ForTraining()
        {
            int inputCount = 5;
            int hiddenCount = 6;
            int outputCount = 3;
            AlmeidaPinedaNetwork network = new AlmeidaPinedaNetwork(inputCount, hiddenCount, outputCount);
            System.Random testRandom = new System.Random(42);
            for (int test = 0; test < 100; test++)
            {
                double[] inputs = new double[inputCount];
                double[] targets = new double[outputCount];
                for (int i = 0; i < inputCount; i++)
                {
                    inputs[i] = (testRandom.NextDouble() * 2.0) - 1.0;
                }

                for (int j = 0; j < outputCount; j++)
                {
                    targets[j] = testRandom.NextDouble();
                }

                network.Train(inputs, targets, 0.1);
            }

            double[] finalOutputs = network.Compute(new double[inputCount]);
            Assert.IsNotNull(finalOutputs);
            Assert.AreEqual(outputCount, finalOutputs.Length);
            for (int i = 0; i < finalOutputs.Length; i++)
            {
                Assert.IsTrue(finalOutputs[i] >= 0.0 && finalOutputs[i] <= 1.0, "Final output out of range.");
            }
        }

        // Additional tests for thorough coverage
        [TestMethod]
        public void Train_WithNullInputsAndNullTargets_ReturnsExpectedOutput()
        {
            int inputCount = 4;
            int hiddenCount = 5;
            int outputCount = 3;
            AlmeidaPinedaNetwork network = new AlmeidaPinedaNetwork(inputCount, hiddenCount, outputCount);
            double[] outputs = network.Train(null, null, 0.1);
            Assert.IsNotNull(outputs);
            Assert.AreEqual(outputCount, outputs.Length);
            for (int i = 0; i < outputs.Length; i++)
            {
                Assert.IsTrue(outputs[i] >= 0.0 && outputs[i] <= 1.0, "Output is not in the range 0 to 1.");
            }
        }

        [TestMethod]
        public void Compute_ConsistentOutputs_ForSameInput()
        {
            int inputCount = 3;
            int hiddenCount = 4;
            int outputCount = 2;
            AlmeidaPinedaNetwork network = new AlmeidaPinedaNetwork(inputCount, hiddenCount, outputCount);
            double[] inputs = new double[3]
            {
                0.3,
                -0.2,
                0.8
            };
            double[] firstOutput = network.Compute(inputs);
            double[] secondOutput = network.Compute(inputs);
            Assert.AreEqual(firstOutput.Length, secondOutput.Length);
            for (int i = 0; i < firstOutput.Length; i++)
            {
                Assert.AreEqual(firstOutput[i], secondOutput[i], 1e-6, "Outputs differ between calls for same input.");
            }
        }
    }
}