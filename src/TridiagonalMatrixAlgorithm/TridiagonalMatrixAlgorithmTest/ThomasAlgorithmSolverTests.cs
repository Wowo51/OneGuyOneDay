using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TridiagonalMatrixAlgorithm;

namespace TridiagonalMatrixAlgorithmTest
{
    [TestClass]
    public class ThomasAlgorithmSolverTests
    {
        [TestMethod]
        public void Solve_ValidSmallSystem_ReturnsCorrectSolution()
        {
            double[] lower = new double[1]
            {
                1.0
            };
            double[] main = new double[2]
            {
                2.0,
                2.0
            };
            double[] upper = new double[1]
            {
                1.0
            };
            double[] d = new double[2]
            {
                3.0,
                3.0
            };
            double[] result = ThomasAlgorithmSolver.Solve(lower, main, upper, d);
            Assert.IsNotNull(result, "Result should not be null for valid input.");
            Assert.AreEqual(2, result.Length, "Result length must equal 2.");
            double tolerance = 1e-6;
            Assert.AreEqual(1.0, result[0], tolerance, "Solution at index 0 is incorrect.");
            Assert.AreEqual(1.0, result[1], tolerance, "Solution at index 1 is incorrect.");
        }

        [TestMethod]
        public void Solve_InvalidDimensions_ReturnsNull()
        {
            // main has length 3 but lower should have length 2 (it has 1 instead)
            double[] lower = new double[1]
            {
                1.0
            };
            double[] main = new double[3]
            {
                2.0,
                2.0,
                2.0
            };
            double[] upper = new double[2]
            {
                1.0,
                1.0
            };
            double[] d = new double[3]
            {
                3.0,
                3.0,
                3.0
            };
            double[] result = ThomasAlgorithmSolver.Solve(lower, main, upper, d);
            Assert.IsNull(result, "Result should be null when array dimensions mismatch.");
        }

        [TestMethod]
        public void Solve_DivisionByZero_ReturnsNull()
        {
            // Force division by zero by setting the first element of main to zero.
            double[] lower = new double[1]
            {
                1.0
            };
            double[] main = new double[2]
            {
                0.0,
                2.0
            };
            double[] upper = new double[1]
            {
                1.0
            };
            double[] d = new double[2]
            {
                3.0,
                3.0
            };
            double[] result = ThomasAlgorithmSolver.Solve(lower, main, upper, d);
            Assert.IsNull(result, "Result should be null when a zero is encountered on the main diagonal.");
        }

        [TestMethod]
        public void Solve_NullInput_ReturnsNull()
        {
            double[] validLower = new double[1]
            {
                1.0
            };
            double[] validMain = new double[2]
            {
                2.0,
                2.0
            };
            double[] validUpper = new double[1]
            {
                1.0
            };
            double[] validD = new double[2]
            {
                3.0,
                3.0
            };
            double[] result1 = ThomasAlgorithmSolver.Solve(null, validMain, validUpper, validD);
            double[] result2 = ThomasAlgorithmSolver.Solve(validLower, null, validUpper, validD);
            double[] result3 = ThomasAlgorithmSolver.Solve(validLower, validMain, null, validD);
            double[] result4 = ThomasAlgorithmSolver.Solve(validLower, validMain, validUpper, null);
            Assert.IsNull(result1, "Result should be null when lower array is null.");
            Assert.IsNull(result2, "Result should be null when main array is null.");
            Assert.IsNull(result3, "Result should be null when upper array is null.");
            Assert.IsNull(result4, "Result should be null when d array is null.");
        }

        [TestMethod]
        public void Solve_LargeSystem_ReturnsCorrectSolution()
        {
            const int n = 50;
            double[] lower = new double[n - 1];
            double[] main = new double[n];
            double[] upper = new double[n - 1];
            double[] d = new double[n];
            double[] expected = new double[n];
            Random random = new Random(12345);
            int i;
            // Generate a known solution vector with values between -10 and 10.
            for (i = 0; i < n; i++)
            {
                expected[i] = random.NextDouble() * 20.0 - 10.0;
            }

            // Generate matrix coefficients ensuring diagonal dominance.
            for (i = 0; i < n; i++)
            {
                double lowerVal = (i > 0) ? random.NextDouble() * 5.0 : 0.0;
                double upperVal = (i < n - 1) ? random.NextDouble() * 5.0 : 0.0;
                if (i > 0)
                {
                    lower[i - 1] = lowerVal;
                }

                if (i < n - 1)
                {
                    upper[i] = upperVal;
                }

                double diag = 10.0 + ((i > 0) ? lowerVal : 0.0) + ((i < n - 1) ? upperVal : 0.0);
                main[i] = diag;
            }

            // Compute the right-hand side vector d based on the known solution.
            d[0] = main[0] * expected[0] + upper[0] * expected[1];
            for (i = 1; i < n - 1; i++)
            {
                d[i] = lower[i - 1] * expected[i - 1] + main[i] * expected[i] + upper[i] * expected[i + 1];
            }

            d[n - 1] = lower[n - 2] * expected[n - 2] + main[n - 1] * expected[n - 1];
            double[] result = ThomasAlgorithmSolver.Solve(lower, main, upper, d);
            Assert.IsNotNull(result, "Result should not be null for a valid large system.");
            double tolerance = 1e-6;
            for (i = 0; i < n; i++)
            {
                Assert.AreEqual(expected[i], result[i], tolerance, "Mismatch at index " + i.ToString());
            }
        }

        [TestMethod]
        public void Solve_SingleEquationSystem_ReturnsCorrectSolution()
        {
            double[] lower = new double[0];
            double[] main = new double[1]
            {
                5.0
            };
            double[] upper = new double[0];
            double[] d = new double[1]
            {
                10.0
            };
            double[] result = ThomasAlgorithmSolver.Solve(lower, main, upper, d);
            Assert.IsNotNull(result, "Result should not be null for single equation system.");
            Assert.AreEqual(1, result.Length, "Result length must equal 1.");
            double tolerance = 1e-6;
            Assert.AreEqual(2.0, result[0], tolerance, "Solution is incorrect for single equation system.");
        }
    }
}