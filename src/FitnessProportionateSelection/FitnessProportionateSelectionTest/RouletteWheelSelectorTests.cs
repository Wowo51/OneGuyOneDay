using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FitnessProportionateSelection;

namespace FitnessProportionateSelectionTest
{
    [TestClass]
    public class RouletteWheelSelectorTests
    {
        private class FakeRandom : Random
        {
            private readonly double[] _values;
            private int _index;
            public FakeRandom(double[] values)
            {
                _values = values;
                _index = 0;
            }

            protected override double Sample()
            {
                double value = _values[_index];
                _index = (_index + 1) % _values.Length;
                return value;
            }
        }

        [TestMethod]
        public void Test_NullPopulation()
        {
            string result = RouletteWheelSelector.Select<string>(null, s => 1.0, new FakeRandom(new double[] { 0.1 }));
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_NullFitnessSelector()
        {
            List<string> list = new List<string>
            {
                "test"
            };
            string result = RouletteWheelSelector.Select<string>(list, null, new FakeRandom(new double[] { 0.1 }));
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_NullRandom()
        {
            List<string> list = new List<string>
            {
                "test"
            };
            string result = RouletteWheelSelector.Select<string>(list, s => 1.0, null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_EmptyPopulation()
        {
            List<string> list = new List<string>();
            string result = RouletteWheelSelector.Select<string>(list, s => 1.0, new FakeRandom(new double[] { 0.1 }));
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_SingleElement()
        {
            List<string> list = new List<string>
            {
                "only"
            };
            string result = RouletteWheelSelector.Select<string>(list, s => 10.0, new FakeRandom(new double[] { 0.2 }));
            Assert.AreEqual("only", result);
        }

        [TestMethod]
        public void Test_TotalFitnessZero()
        {
            List<string> list = new List<string>
            {
                "first",
                "second",
                "third"
            };
            string result = RouletteWheelSelector.Select<string>(list, s => 0.0, new FakeRandom(new double[] { 0.7 }));
            Assert.AreEqual("third", result);
        }

        [TestMethod]
        public void Test_Selection_FirstElement()
        {
            List<string> list = new List<string>
            {
                "first",
                "second"
            };
            string result = RouletteWheelSelector.Select<string>(list, s => s == "first" ? 1.0 : 3.0, new FakeRandom(new double[] { 0.2 }));
            Assert.AreEqual("first", result);
        }

        [TestMethod]
        public void Test_Selection_SecondElement()
        {
            List<string> list = new List<string>
            {
                "first",
                "second"
            };
            string result = RouletteWheelSelector.Select<string>(list, s => s == "first" ? 1.0 : 3.0, new FakeRandom(new double[] { 0.5 }));
            Assert.AreEqual("second", result);
        }

        [TestMethod]
        public void Test_LargePopulation_UniformFitness()
        {
            int populationSize = 10000;
            List<string> list = new List<string>();
            for (int i = 0; i < populationSize; i++)
            {
                list.Add(i.ToString());
            }

            // Each individual has fitness 1, so totalFitness = populationSize.
            // With a FakeRandom sample of 0.4321, threshold = 0.4321 * 10000 = 4321.
            // The cumulative sum reaches 4321 at the 4320th index (0-indexed).
            FakeRandom fake = new FakeRandom(new double[] { 0.4321 });
            string result = RouletteWheelSelector.Select<string>(list, s => 1.0, fake);
            Assert.AreEqual("4320", result);
        }

        [TestMethod]
        public void Test_LargePopulation_IncreasingFitness()
        {
            int populationSize = 100;
            List<string> list = new List<string>();
            for (int i = 0; i < populationSize; i++)
            {
                list.Add(i.ToString());
            }

            // Fitness: f(i) = i + 1, so totalFitness = (populationSize * (populationSize + 1)) / 2 = 5050.
            // With a FakeRandom sample of 0.5, threshold = 0.5 * 5050 = 2525.
            // Cumulative sum from index 0 increases as 1, 3, 6, ..., and the first index where the cumulative
            // sum is >= 2525 is index 70 (since sum(1..70) = 2485 and sum(1..71) = 2556).
            FakeRandom fake = new FakeRandom(new double[] { 0.5 });
            string result = RouletteWheelSelector.Select<string>(list, s =>
            {
                int parsed;
                if (Int32.TryParse(s, out parsed))
                {
                    return parsed + 1.0;
                }

                return 0.0;
            }, fake);
            Assert.AreEqual("70", result);
        }
    }
}