using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ElserDifferenceMap;

namespace ElserDifferenceMapTest
{
    [TestClass]
    public class DifferenceMapSolverTests
    {
        [TestMethod]
        public void TestSolveWithIdentityProjection()
        {
            double[] input = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            DifferenceMapSolver solver = new DifferenceMapSolver(new IdentityProjection(), new IdentityProjection(), -1.0, 100, 1e-6);
            double[] result = solver.Solve(input);
            CollectionAssert.AreEqual(input, result);
        }

        [TestMethod]
        public void TestSolveWithNullProjections()
        {
            double[] input = new double[]
            {
                4.0,
                5.0,
                6.0
            };
            DifferenceMapSolver solver = new DifferenceMapSolver(null, null, -1.0, 100, 1e-6);
            double[] result = solver.Solve(input);
            CollectionAssert.AreEqual(input, result);
        }

        [TestMethod]
        public void TestSolveWithNullInput()
        {
            DifferenceMapSolver solver = new DifferenceMapSolver(new IdentityProjection(), new IdentityProjection(), -1.0, 100, 1e-6);
            double[] result = solver.Solve(null);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestSolveConvergenceWithHalfProjection()
        {
            double[] input = new double[]
            {
                8.0,
                4.0,
                2.0
            };
            IProjection halfProjection = new HalfProjection();
            DifferenceMapSolver solver = new DifferenceMapSolver(halfProjection, new IdentityProjection(), 1.0, 50, 1e-3);
            double[] result = solver.Solve(input);
            double threshold = 1e-3;
            for (int i = 0; i < result.Length; i++)
            {
                Assert.IsTrue(Math.Abs(result[i]) < threshold, "Element at index " + i + " is not small enough.");
            }
        }

        [TestMethod]
        public void TestSolveLargeDataSet()
        {
            int size = 10000;
            double[] input = new double[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                input[i] = random.NextDouble() * 100;
            }

            DifferenceMapSolver solver = new DifferenceMapSolver(new IdentityProjection(), new IdentityProjection(), -1.0, 100, 1e-6);
            double[] result = solver.Solve(input);
            CollectionAssert.AreEqual(input, result);
        }

        [TestMethod]
        public void TestSolveWithEmptyArray()
        {
            double[] input = new double[0];
            DifferenceMapSolver solver = new DifferenceMapSolver(new IdentityProjection(), new IdentityProjection(), -1.0, 10, 1e-6);
            double[] result = solver.Solve(input);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestSolveWithAlwaysNullProjectionA()
        {
            double[] input = new double[]
            {
                5.0,
                6.0,
                7.0
            };
            IProjection alwaysNull = new AlwaysNullProjection();
            DifferenceMapSolver solver = new DifferenceMapSolver(alwaysNull, new IdentityProjection(), 1.0, 50, 1e-6);
            double[] result = solver.Solve(input);
            double[] expected = new double[]
            {
                0.0,
                0.0,
                0.0
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSolveWithAlwaysNullProjectionB()
        {
            double[] input = new double[]
            {
                5.0,
                6.0,
                7.0
            };
            IProjection alwaysNull = new AlwaysNullProjection();
            DifferenceMapSolver solver = new DifferenceMapSolver(new IdentityProjection(), alwaysNull, 1.0, 50, 1e-6);
            double[] result = solver.Solve(input);
            double[] expected = new double[]
            {
                0.0,
                0.0,
                0.0
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSolveWithZeroIterations()
        {
            double[] input = new double[]
            {
                5.0,
                6.0,
                7.0
            };
            DifferenceMapSolver solver = new DifferenceMapSolver(new IdentityProjection(), new IdentityProjection(), -1.0, 0, 1e-6);
            double[] result = solver.Solve(input);
            CollectionAssert.AreEqual(input, result);
        }
    }

    public class HalfProjection : IProjection
    {
        public double[] Project(double[] vector)
        {
            if (vector == null)
            {
                return new double[0];
            }

            int length = vector.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = vector[i] / 2.0;
            }

            return result;
        }
    }

    public class AlwaysNullProjection : IProjection
    {
        public double[] Project(double[] vector)
        {
            return null;
        }
    }
}