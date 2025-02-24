using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TernarySearch;

namespace TernarySearchTest
{
    [TestClass]
    public class TernarySearcherTests
    {
        private const double TestTolerance = 1e-5;
        [TestMethod]
        public void Test_FindMaximum_Parabola()
        {
            double left = 0.0;
            double right = 4.0;
            Func<double, double> function = delegate (double x)
            {
                return -Math.Pow(x - 2.0, 2.0) + 5.0;
            };
            double maximum = TernarySearcher.FindMaximum(function, left, right);
            Assert.IsTrue(Math.Abs(maximum - 2.0) < TestTolerance, $"Expected maximum near 2.0 but got {maximum}");
        }

        [TestMethod]
        public void Test_FindMinimum_Parabola()
        {
            double left = 0.0;
            double right = 6.0;
            Func<double, double> function = delegate (double x)
            {
                return Math.Pow(x - 3.0, 2.0) + 1.0;
            };
            double minimum = TernarySearcher.FindMinimum(function, left, right);
            Assert.IsTrue(Math.Abs(minimum - 3.0) < TestTolerance, $"Expected minimum near 3.0 but got {minimum}");
        }

        [TestMethod]
        public void Test_FindMaximum_NullFunction()
        {
            double left = 1.0;
            double right = 10.0;
            double result = TernarySearcher.FindMaximum(null, left, right);
            Assert.AreEqual(left, result, TestTolerance, "Expected left value when function is null");
        }

        [TestMethod]
        public void Test_FindMinimum_NullFunction()
        {
            double left = 1.0;
            double right = 10.0;
            double result = TernarySearcher.FindMinimum(null, left, right);
            Assert.AreEqual(left, result, TestTolerance, "Expected left value when function is null");
        }

        [TestMethod]
        public void Test_FindMaximum_SinFunction()
        {
            double left = 0.0;
            double right = Math.PI;
            Func<double, double> function = delegate (double x)
            {
                return Math.Sin(x);
            };
            double maximum = TernarySearcher.FindMaximum(function, left, right);
            double expected = Math.PI / 2.0;
            Assert.IsTrue(Math.Abs(maximum - expected) < TestTolerance, $"Expected maximum near {expected} but got {maximum}");
        }

        [TestMethod]
        public void Test_FindMinimum_SinFunction()
        {
            double left = Math.PI;
            double right = 2.0 * Math.PI;
            Func<double, double> function = delegate (double x)
            {
                return Math.Sin(x);
            };
            double minimum = TernarySearcher.FindMinimum(function, left, right);
            double expected = 3.0 * Math.PI / 2.0;
            Assert.IsTrue(Math.Abs(minimum - expected) < TestTolerance, $"Expected minimum near {expected} but got {minimum}");
        }

        [TestMethod]
        public void Test_FindMaximum_RandomQuadratic()
        {
            System.Random random = new System.Random(42);
            int testsCount = 10;
            for (int i = 0; i < testsCount; i++)
            {
                double mid = random.NextDouble() * 90.0 + 10.0; // mid in [10, 100]
                double left = mid - 50.0;
                double right = mid + 50.0;
                Func<double, double> function = delegate (double x)
                {
                    return -(x - mid) * (x - mid);
                };
                double maximum = TernarySearcher.FindMaximum(function, left, right);
                Assert.IsTrue(Math.Abs(maximum - mid) < TestTolerance, $"Test {i}: Expected maximum near {mid} but got {maximum}");
            }
        }

        [TestMethod]
        public void Test_FindMinimum_RandomQuadratic()
        {
            System.Random random = new System.Random(42);
            int testsCount = 10;
            for (int i = 0; i < testsCount; i++)
            {
                double mid = random.NextDouble() * 90.0 + 10.0; // mid in [10, 100]
                double left = mid - 50.0;
                double right = mid + 50.0;
                Func<double, double> function = delegate (double x)
                {
                    return (x - mid) * (x - mid);
                };
                double minimum = TernarySearcher.FindMinimum(function, left, right);
                Assert.IsTrue(Math.Abs(minimum - mid) < TestTolerance, $"Test {i}: Expected minimum near {mid} but got {minimum}");
            }
        }

        [TestMethod]
        public void Test_FindMaximum_ZeroInterval()
        {
            double left = 5.0;
            double right = 5.0;
            Func<double, double> function = delegate (double x)
            {
                return x * x;
            };
            double maximum = TernarySearcher.FindMaximum(function, left, right);
            Assert.AreEqual(left, maximum, TestTolerance, "Expected value equal to left when interval is zero");
        }

        [TestMethod]
        public void Test_FindMinimum_ZeroInterval()
        {
            double left = -3.0;
            double right = -3.0;
            Func<double, double> function = delegate (double x)
            {
                return x * x;
            };
            double minimum = TernarySearcher.FindMinimum(function, left, right);
            Assert.AreEqual(left, minimum, TestTolerance, "Expected value equal to left when interval is zero");
        }

        [TestMethod]
        public void Test_FindMaximum_ConstantFunction()
        {
            double left = 2.0;
            double right = 8.0;
            Func<double, double> function = delegate (double x)
            {
                return 5.0;
            };
            double maximum = TernarySearcher.FindMaximum(function, left, right);
            Assert.IsTrue(Math.Abs(maximum - left) < TestTolerance, "Expected maximum to converge to left for constant function");
        }

        [TestMethod]
        public void Test_FindMinimum_ConstantFunction()
        {
            double left = -10.0;
            double right = 0.0;
            Func<double, double> function = delegate (double x)
            {
                return 3.14;
            };
            double minimum = TernarySearcher.FindMinimum(function, left, right);
            Assert.IsTrue(Math.Abs(minimum - left) < TestTolerance, "Expected minimum to converge to left for constant function");
        }
    }
}