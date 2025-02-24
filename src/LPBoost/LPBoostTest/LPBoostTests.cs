using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LPBoost;

namespace LPBoostTest
{
    [TestClass]
    public class LPBoostTests
    {
        [TestMethod]
        public void TestLinearProgramSolver_NullInput()
        {
            double result = LinearProgramSolver.Solve(null);
            Assert.AreEqual(0.0, result, "Expected median for null input is 0.0");
        }

        [TestMethod]
        public void TestLinearProgramSolver_EmptyList()
        {
            List<double> emptyList = new List<double>();
            double result = LinearProgramSolver.Solve(emptyList);
            Assert.AreEqual(0.0, result, "Expected median for empty list is 0.0");
        }

        [TestMethod]
        public void TestLinearProgramSolver_OddCount()
        {
            List<double> data = new List<double>
            {
                3.0,
                1.0,
                2.0
            };
            double result = LinearProgramSolver.Solve(data);
            Assert.AreEqual(2.0, result, "Expected median for odd count list is 2.0");
        }

        [TestMethod]
        public void TestLinearProgramSolver_EvenCount()
        {
            List<double> data = new List<double>
            {
                2.0,
                4.0,
                6.0,
                8.0
            };
            double result = LinearProgramSolver.Solve(data);
            Assert.AreEqual(5.0, result, "Expected median for even count list is 5.0");
        }

        [TestMethod]
        public void TestLPBoostAlgorithm_TrainAndPredict()
        {
            LPBoostAlgorithm algorithm = new LPBoostAlgorithm();
            double[] trainingData = new double[]
            {
                5.0,
                3.0,
                1.0,
                2.0,
                4.0
            };
            algorithm.Train(trainingData);
            double predicted = algorithm.Predict(10.0);
            Assert.AreEqual(30.0, predicted, "Expected prediction to be median (3.0) * input (10.0) = 30.0");
        }

        [TestMethod]
        public void TestLPBoostAlgorithm_EmptyTrain()
        {
            LPBoostAlgorithm algorithm = new LPBoostAlgorithm();
            double[] emptyData = new double[0];
            algorithm.Train(emptyData);
            double predicted = algorithm.Predict(10.0);
            Assert.AreEqual(0.0, predicted, "Expected prediction with empty training data to be 0.0");
        }

        [TestMethod]
        public void TestLargeDataset_LinearProgramSolver()
        {
            List<double> largeData = new List<double>();
            for (int i = 10000; i >= 1; i--)
            {
                largeData.Add((double)i);
            }

            double result = LinearProgramSolver.Solve(largeData);
            Assert.AreEqual(5000.5, result, 0.000001, "Expected median for large dataset is 5000.5");
        }

        [TestMethod]
        public void TestLinearProgramSolver_SingleElement()
        {
            List<double> data = new List<double>
            {
                42.0
            };
            double result = LinearProgramSolver.Solve(data);
            Assert.AreEqual(42.0, result, "Expected median for single element list to be equal to the element itself");
        }

        [TestMethod]
        public void TestProgram_MainOutput()
        {
            StringWriter stringWriter = new StringWriter();
            System.IO.TextWriter originalOut = Console.Out;
            try
            {
                Console.SetOut(stringWriter);
                Program.Main(new string[0]);
                string output = stringWriter.ToString().Trim();
                Assert.AreEqual("LPBoost training completed.", output, "Expected console output to indicate training completion.");
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}