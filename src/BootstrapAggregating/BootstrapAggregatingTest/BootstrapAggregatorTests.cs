using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BootstrapAggregating;

namespace BootstrapAggregatingTest
{
    [TestClass]
    public class BootstrapAggregatorTests
    {
        private class FakePredictor : IPredictor
        {
            private readonly double _returnValue;
            public FakePredictor(double returnValue)
            {
                _returnValue = returnValue;
            }

            public double Predict(double[]? input)
            {
                return _returnValue;
            }
        }

        private class DynamicFakePredictor : IPredictor
        {
            public double Predict(double[]? input)
            {
                if (input == null || input.Length == 0)
                {
                    return 0;
                }

                double sum = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    sum += input[i];
                }

                return sum / input.Length;
            }
        }

        private class SumFakePredictor : IPredictor
        {
            public double Predict(double[]? input)
            {
                if (input == null)
                {
                    return 0;
                }

                double sum = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    sum += input[i];
                }

                return sum;
            }
        }

        [TestMethod]
        public void TestPredict_WithNullInput()
        {
            List<IPredictor?> predictors = new List<IPredictor?>();
            predictors.Add(new FakePredictor(5.0));
            BootstrapAggregator aggregator = new BootstrapAggregator(predictors);
            double result = aggregator.Predict(null);
            Assert.AreEqual(0.0, result, 0.0001);
        }

        [TestMethod]
        public void TestPredict_WithEmptyPredictors()
        {
            List<IPredictor?> predictors = new List<IPredictor?>();
            BootstrapAggregator aggregator = new BootstrapAggregator(predictors);
            double result = aggregator.Predict(new double[] { 1.0, 2.0, 3.0 });
            Assert.AreEqual(0.0, result, 0.0001);
        }

        [TestMethod]
        public void TestPredict_WithMultiplePredictors()
        {
            List<IPredictor?> predictors = new List<IPredictor?>();
            predictors.Add(new FakePredictor(4.0));
            predictors.Add(new FakePredictor(6.0));
            BootstrapAggregator aggregator = new BootstrapAggregator(predictors);
            double result = aggregator.Predict(new double[] { 1.0, 2.0, 3.0 });
            Assert.AreEqual(5.0, result, 0.0001);
        }

        [TestMethod]
        public void TestPredict_WithNullPredictorInList()
        {
            List<IPredictor?> predictors = new List<IPredictor?>();
            predictors.Add(new FakePredictor(6.0));
            predictors.Add(null);
            predictors.Add(new FakePredictor(2.0));
            BootstrapAggregator aggregator = new BootstrapAggregator(predictors);
            double result = aggregator.Predict(new double[] { 5.0 });
            Assert.AreEqual(4.0, result, 0.0001);
        }

        [TestMethod]
        public void TestPredict_WithDynamicPredictor()
        {
            List<IPredictor?> predictors = new List<IPredictor?>();
            predictors.Add(new DynamicFakePredictor());
            BootstrapAggregator aggregator = new BootstrapAggregator(predictors);
            double[] input = new double[]
            {
                10.0,
                20.0,
                30.0,
                40.0,
                50.0
            };
            double result = aggregator.Predict(input);
            Assert.AreEqual(30.0, result, 0.0001);
        }

        [TestMethod]
        public void TestPredict_WithSyntheticLargeData()
        {
            List<IPredictor?> predictors = new List<IPredictor?>();
            predictors.Add(new SumFakePredictor());
            BootstrapAggregator aggregator = new BootstrapAggregator(predictors);
            double[] input = new double[1000];
            for (int i = 0; i < 1000; i++)
            {
                input[i] = i;
            }

            double expected = 0;
            for (int i = 0; i < 1000; i++)
            {
                expected += input[i];
            }

            double result = aggregator.Predict(input);
            Assert.AreEqual(expected, result, 0.0001);
        }
    }
}