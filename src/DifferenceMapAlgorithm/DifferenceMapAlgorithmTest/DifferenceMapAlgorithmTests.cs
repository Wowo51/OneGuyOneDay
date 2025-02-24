using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DifferenceMapAlgorithm;

namespace DifferenceMapAlgorithmTest
{
    [TestClass]
    public class DifferenceMapAlgorithmTests
    {
        public class AddProjection : IProjection
        {
            private double _offset;
            public AddProjection(double offset)
            {
                this._offset = offset;
            }

            public double[] Project(double[] input)
            {
                if (input == null)
                {
                    return new double[0];
                }

                double[] result = new double[input.Length];
                for (int i = 0; i < input.Length; i++)
                {
                    result[i] = input[i] + this._offset;
                }

                return result;
            }
        }

        public class MultiplyProjection : IProjection
        {
            private double _factor;
            public MultiplyProjection(double factor)
            {
                this._factor = factor;
            }

            public double[] Project(double[] input)
            {
                if (input == null)
                {
                    return new double[0];
                }

                double[] result = new double[input.Length];
                for (int i = 0; i < input.Length; i++)
                {
                    result[i] = input[i] * this._factor;
                }

                return result;
            }
        }

        [TestMethod]
        public void TestSolveNullInitial()
        {
            DifferenceMap map = new DifferenceMap(0.5, null, null);
            double[] result = map.Solve(null, 10, 1e-6);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestSolveIdentity()
        {
            double[] initial = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            DifferenceMap map = new DifferenceMap(0.5, null, null);
            double[] result = map.Solve(initial, 100, 1e-8);
            CollectionAssert.AreEqual(initial, result);
        }

        [TestMethod]
        public void TestSolveWithAddProjection()
        {
            double[] initial = new double[]
            {
                0.0,
                10.0,
                -5.0
            };
            AddProjection addProjection = new AddProjection(1.0);
            DifferenceMap map = new DifferenceMap(1.0, addProjection, null);
            double[] result = map.Solve(initial, 1, 1e-6);
            double[] expected = new double[]
            {
                1.0,
                11.0,
                -4.0
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSolveMaxIterations()
        {
            double[] initial = new double[]
            {
                7.0
            };
            AddProjection addProjection = new AddProjection(1.0);
            DifferenceMap map = new DifferenceMap(1.0, addProjection, null);
            double[] result = map.Solve(initial, 5, 0.1);
            double[] expected = new double[]
            {
                12.0
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSolveNegativeBeta()
        {
            double[] initial = new double[]
            {
                2.0,
                3.0
            };
            AddProjection addProjection = new AddProjection(1.0);
            DifferenceMap map = new DifferenceMap(-1.0, addProjection, null);
            double[] result = map.Solve(initial, 1, 1e-6);
            double[] expected = new double[]
            {
                1.0,
                2.0
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSolveZeroIterations()
        {
            double[] initial = new double[]
            {
                5.0,
                6.0,
                7.0
            };
            DifferenceMap map = new DifferenceMap(0.5, null, null);
            double[] result = map.Solve(initial, 0, 1e-6);
            CollectionAssert.AreEqual(initial, result);
        }

        [TestMethod]
        public void TestSolveWithMultiplyProjection()
        {
            double[] initial = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            MultiplyProjection multiplyProjection = new MultiplyProjection(2.0);
            DifferenceMap map = new DifferenceMap(1.0, null, multiplyProjection);
            double[] result = map.Solve(initial, 1, 1e-6);
            double[] expected = new double[]
            {
                2.0,
                4.0,
                6.0
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSolveLargeDataset()
        {
            int size = 10000;
            double[] initial = new double[size];
            for (int i = 0; i < size; i++)
            {
                initial[i] = i * 0.001;
            }

            DifferenceMap map = new DifferenceMap(0.5, null, null);
            double[] result = map.Solve(initial, 1, 1e-6);
            CollectionAssert.AreEqual(initial, result);
        }
    }
}