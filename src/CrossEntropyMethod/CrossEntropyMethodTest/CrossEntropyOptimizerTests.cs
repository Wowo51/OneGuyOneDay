using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrossEntropyMethod;

namespace CrossEntropyMethodTest
{
    [TestClass]
    public class CrossEntropyOptimizerTests
    {
        private static double SphereFunction(double[] x)
        {
            double result = 0.0;
            for (int i = 0; i < x.Length; i++)
            {
                result += x[i] * x[i];
            }

            return result;
        }

        [TestMethod]
        public void Test_Optimize_Returns_CorrectDimension()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialMean = new double[]
            {
                5.0,
                5.0,
                5.0
            };
            double[] initialStd = new double[]
            {
                1.0,
                1.0,
                1.0
            };
            double[] solution = optimizer.Optimize(SphereFunction, initialMean, initialStd, 100, 0.2, 50);
            Assert.AreEqual(initialMean.Length, solution.Length);
        }

        [TestMethod]
        public void Test_Optimize_Improves_Objective()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialMean = new double[]
            {
                5.0,
                5.0,
                5.0
            };
            double[] initialStd = new double[]
            {
                1.0,
                1.0,
                1.0
            };
            double initialScore = SphereFunction(initialMean);
            double[] solution = optimizer.Optimize(SphereFunction, initialMean, initialStd, 200, 0.3, 100);
            double solutionScore = SphereFunction(solution);
            Assert.IsTrue(solutionScore < initialScore);
        }

        [TestMethod]
        public void Test_NullObjective_ReturnsInitialMean()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialMean = new double[]
            {
                3.0,
                3.0
            };
            double[] initialStd = new double[]
            {
                1.0,
                1.0
            };
            double[] solution = optimizer.Optimize(null, initialMean, initialStd, 50, 0.5, 50);
            CollectionAssert.AreEqual(initialMean, solution);
        }

        [TestMethod]
        public void Test_NullInitialMean_ReturnsEmptyArray()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialStd = new double[]
            {
                1.0,
                1.0
            };
            double[] solution = optimizer.Optimize(SphereFunction, null, initialStd, 50, 0.5, 50);
            Assert.AreEqual(0, solution.Length);
        }

        [TestMethod]
        public void Test_InvalidParameter_LengthMismatch_ReturnsInitialMean()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialMean = new double[]
            {
                4.0,
                4.0
            };
            double[] initialStd = new double[]
            {
                1.0,
                1.0,
                1.0
            };
            double[] solution = optimizer.Optimize(SphereFunction, initialMean, initialStd, 50, 0.5, 50);
            CollectionAssert.AreEqual(initialMean, solution);
        }

        [TestMethod]
        public void Test_Optimize_LargeDimension()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            int dimension = 50;
            double[] initialMean = new double[dimension];
            double[] initialStd = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                initialMean[i] = 5.0;
                initialStd[i] = 2.0;
            }

            double[] solution = optimizer.Optimize(SphereFunction, initialMean, initialStd, 200, 0.1, 20);
            Assert.AreEqual(dimension, solution.Length);
        }

        [TestMethod]
        public void Test_NullInitialStd_ReturnsInitialMean()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialMean = new double[]
            {
                3.0,
                3.0
            };
            double[] solution = optimizer.Optimize(SphereFunction, initialMean, null, 50, 0.5, 50);
            CollectionAssert.AreEqual(initialMean, solution);
        }

        [TestMethod]
        public void Test_SampleSizeNonPositive_ReturnsInitialMean()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialMean = new double[]
            {
                3.0,
                3.0
            };
            double[] initialStd = new double[]
            {
                1.0,
                1.0
            };
            double[] solution = optimizer.Optimize(SphereFunction, initialMean, initialStd, 0, 0.5, 50);
            CollectionAssert.AreEqual(initialMean, solution);
        }

        [TestMethod]
        public void Test_EliteFractionNonPositive_ReturnsInitialMean()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialMean = new double[]
            {
                3.0,
                3.0
            };
            double[] initialStd = new double[]
            {
                1.0,
                1.0
            };
            double[] solution = optimizer.Optimize(SphereFunction, initialMean, initialStd, 50, 0.0, 50);
            CollectionAssert.AreEqual(initialMean, solution);
        }

        [TestMethod]
        public void Test_EliteFractionGreaterThanOne_ReturnsInitialMean()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialMean = new double[]
            {
                3.0,
                3.0
            };
            double[] initialStd = new double[]
            {
                1.0,
                1.0
            };
            double[] solution = optimizer.Optimize(SphereFunction, initialMean, initialStd, 50, 1.1, 50);
            CollectionAssert.AreEqual(initialMean, solution);
        }

        [TestMethod]
        public void Test_MaxIterationsNonPositive_ReturnsInitialMean()
        {
            CrossEntropyOptimizer optimizer = new CrossEntropyOptimizer();
            double[] initialMean = new double[]
            {
                3.0,
                3.0
            };
            double[] initialStd = new double[]
            {
                1.0,
                1.0
            };
            double[] solution = optimizer.Optimize(SphereFunction, initialMean, initialStd, 50, 0.5, 0);
            CollectionAssert.AreEqual(initialMean, solution);
        }
    }
}