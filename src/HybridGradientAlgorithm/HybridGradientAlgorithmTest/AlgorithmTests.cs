using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HybridGradientAlgorithm;

namespace HybridGradientAlgorithmTest
{
    public static class TestHelper
    {
        public static void AssertDoubleArraysAreEqual(double[] expected, double[] actual, double tolerance)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Arrays have different lengths.");
            for (int i = 0; i < expected.Length; i++)
            {
                double diff = Math.Abs(expected[i] - actual[i]);
                Assert.IsTrue(diff <= tolerance, $"Arrays differ at index {i}: expected {expected[i]}, actual {actual[i]}");
            }
        }
    }

    [TestClass]
    public class ConjugateGradientTests
    {
        [TestMethod]
        public void Search_NullCandidate_ReturnsEmptyArray()
        {
            ConjugateGradient cg = new ConjugateGradient();
            double[] result = cg.Search(null, 0.2);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Search_ValidCandidate_ReturnsScaledArray()
        {
            ConjugateGradient cg = new ConjugateGradient();
            double[] candidate = new double[]
            {
                10.0,
                20.0,
                30.0
            };
            double factor = 0.1;
            double[] expected = new double[]
            {
                9.0,
                18.0,
                27.0
            };
            double[] result = cg.Search(candidate, factor);
            TestHelper.AssertDoubleArraysAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void Search_NegativeFactor_ReturnsScaledArray()
        {
            ConjugateGradient cg = new ConjugateGradient();
            double[] candidate = new double[]
            {
                10.0,
                20.0
            };
            double factor = -0.2;
            double[] expected = new double[]
            {
                12.0,
                24.0
            };
            double[] result = cg.Search(candidate, factor);
            TestHelper.AssertDoubleArraysAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void Search_EmptyCandidate_ReturnsEmptyArray()
        {
            ConjugateGradient cg = new ConjugateGradient();
            double[] candidate = new double[0];
            double[] result = cg.Search(candidate, 0.1);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }
    }

    [TestClass]
    public class HarmonySearchTests
    {
        [TestMethod]
        public void Optimize_NullInput_ReturnsEmptyArray()
        {
            HarmonySearch hs = new HarmonySearch();
            double[] result = hs.Optimize(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Optimize_ValidInput_ReturnsScaledArray()
        {
            HarmonySearch hs = new HarmonySearch();
            double[] input = new double[]
            {
                100.0,
                50.0,
                25.0
            };
            double[] expected = new double[]
            {
                90.0,
                45.0,
                22.5
            };
            double[] result = hs.Optimize(input);
            TestHelper.AssertDoubleArraysAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void Optimize_LargeArray_ReturnsScaledArray()
        {
            HarmonySearch hs = new HarmonySearch();
            int size = 10000;
            double[] input = new double[size];
            double[] expected = new double[size];
            for (int i = 0; i < size; i++)
            {
                input[i] = (double)i;
                expected[i] = i * 0.9;
            }

            double[] result = hs.Optimize(input);
            TestHelper.AssertDoubleArraysAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void Optimize_EmptyInput_ReturnsEmptyArray()
        {
            HarmonySearch hs = new HarmonySearch();
            double[] input = new double[0];
            double[] result = hs.Optimize(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }
    }

    [TestClass]
    public class HybridConjugateGradientTests
    {
        [TestMethod]
        public void Solve_NullInput_ReturnsEmptyArray()
        {
            HybridConjugateGradient hcg = new HybridConjugateGradient();
            double[] result = hcg.Solve(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Solve_ValidInput_ReturnsScaledArrayBy0_81()
        {
            HybridConjugateGradient hcg = new HybridConjugateGradient();
            double[] input = new double[]
            {
                10.0,
                20.0,
                30.0
            };
            double[] expected = new double[]
            {
                8.1,
                16.2,
                24.3
            };
            double[] result = hcg.Solve(input);
            TestHelper.AssertDoubleArraysAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void Solve_EmptyArray_ReturnsEmptyArray()
        {
            HybridConjugateGradient hcg = new HybridConjugateGradient();
            double[] input = new double[0];
            double[] result = hcg.Solve(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }
    }

    [TestClass]
    public class LineSearchTests
    {
        [TestMethod]
        public void Perform_AnyInput_ReturnsFixedFactor()
        {
            LineSearch ls = new LineSearch();
            double[] input = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double result = ls.Perform(input);
            Assert.AreEqual(0.1, result, 1e-9);
        }

        [TestMethod]
        public void Perform_NullInput_ReturnsFixedFactor()
        {
            LineSearch ls = new LineSearch();
            double result = ls.Perform(null);
            Assert.AreEqual(0.1, result, 1e-9);
        }

        [TestMethod]
        public void Perform_EmptyInput_ReturnsFixedFactor()
        {
            LineSearch ls = new LineSearch();
            double[] input = new double[0];
            double result = ls.Perform(input);
            Assert.AreEqual(0.1, result, 1e-9);
        }
    }
}