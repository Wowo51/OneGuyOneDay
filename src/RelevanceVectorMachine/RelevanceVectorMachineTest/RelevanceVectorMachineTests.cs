using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RelevanceVectorMachine;

namespace RelevanceVectorMachineTest
{
    [TestClass]
    public class RelevanceVectorMachineTests
    {
        [TestMethod]
        public void TestTrainWithNullInputs()
        {
            RelevanceVectorMachine.RelevanceVectorMachine machine = new RelevanceVectorMachine.RelevanceVectorMachine();
            machine.Train(null, null);
            double probability = machine.PredictProbability(null);
            Assert.AreEqual(0.0, probability, 0.00001);
        }

        [TestMethod]
        public void TestPredictProbabilityWithNullInput()
        {
            RelevanceVectorMachine.RelevanceVectorMachine machine = new RelevanceVectorMachine.RelevanceVectorMachine();
            double probability = machine.PredictProbability(null);
            Assert.AreEqual(0.0, probability, 0.00001);
        }

        [TestMethod]
        public void TestTrainAndPredictSimple()
        {
            double[][] trainingData = new double[4][];
            trainingData[0] = new double[]
            {
                1.0
            };
            trainingData[1] = new double[]
            {
                2.0
            };
            trainingData[2] = new double[]
            {
                3.0
            };
            trainingData[3] = new double[]
            {
                4.0
            };
            double[] targets = new double[]
            {
                0.0,
                0.0,
                1.0,
                1.0
            };
            RelevanceVectorMachine.RelevanceVectorMachine machine = new RelevanceVectorMachine.RelevanceVectorMachine();
            machine.Train(trainingData, targets);
            double probabilityLow = machine.PredictProbability(new double[] { 1.0 });
            double probabilityHigh = machine.PredictProbability(new double[] { 4.0 });
            Assert.IsTrue(probabilityLow < probabilityHigh, "Expected lower prediction for input 1.0 than for input 4.0.");
        }

        [TestMethod]
        public void TestSyntheticLargeData()
        {
            System.Random random = new System.Random(42);
            int sampleCount = 1000;
            int featureCount = 5;
            double[][] trainingData = new double[sampleCount][];
            double[] targets = new double[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                trainingData[i] = new double[featureCount];
                double sum = 0.0;
                for (int j = 0; j < featureCount; j++)
                {
                    double value = (random.NextDouble() * 2.0) - 1.0;
                    trainingData[i][j] = value;
                    sum += value;
                }

                targets[i] = (sum > 0.0) ? 1.0 : 0.0;
            }

            RelevanceVectorMachine.RelevanceVectorMachine machine = new RelevanceVectorMachine.RelevanceVectorMachine();
            machine.Train(trainingData, targets);
            for (int i = 0; i < 10; i++)
            {
                double[] sample = trainingData[i];
                double probability = machine.PredictProbability(sample);
                Assert.IsTrue(probability >= 0.0 && probability <= 1.0, "Probability out of bounds: " + probability);
            }
        }

        [TestMethod]
        public void TestEmptyTrainingData()
        {
            double[][] emptyTrainingData = new double[0][];
            double[] emptyTargets = new double[0];
            RelevanceVectorMachine.RelevanceVectorMachine machine = new RelevanceVectorMachine.RelevanceVectorMachine();
            machine.Train(emptyTrainingData, emptyTargets);
            double probability = machine.PredictProbability(new double[] { 1.0 });
            Assert.AreEqual(0.0, probability, 0.00001);
        }

        [TestMethod]
        public void TestPredictProbabilityWithWrongDimension()
        {
            double[][] trainingData = new double[][]
            {
                new double[]
                {
                    0.5
                }
            };
            double[] targets = new double[]
            {
                1.0
            };
            RelevanceVectorMachine.RelevanceVectorMachine machine = new RelevanceVectorMachine.RelevanceVectorMachine();
            machine.Train(trainingData, targets);
            double probability = machine.PredictProbability(new double[] { 0.5, 1.0 });
            Assert.AreEqual(0.0, probability, 0.00001);
        }
    }
}