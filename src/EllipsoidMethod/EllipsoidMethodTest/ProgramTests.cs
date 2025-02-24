using Microsoft.VisualStudio.TestTools.UnitTesting;
using EllipsoidMethod;

namespace EllipsoidMethodTest
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void EllipsoidMethodOracle_ReturnsSubgradient_WhenCircleViolated()
        {
            double[] point = new double[]
            {
                3.0,
                0.0
            };
            double[]? result = Program.EllipsoidMethodOracle(point);
            Assert.IsNotNull(result, "Expected subgradient for circle violation.");
            Assert.AreEqual(6.0, result[0], 1e-10, "Incorrect subgradient for x component.");
            Assert.AreEqual(0.0, result[1], 1e-10, "Incorrect subgradient for y component.");
        }

        [TestMethod]
        public void EllipsoidMethodOracle_ReturnsSubgradient_WhenSumViolated()
        {
            double[] point = new double[]
            {
                0.3,
                0.3
            };
            double[]? result = Program.EllipsoidMethodOracle(point);
            Assert.IsNotNull(result, "Expected subgradient for sum violation.");
            Assert.AreEqual(-1.0, result[0], 1e-10, "Incorrect subgradient for x component.");
            Assert.AreEqual(-1.0, result[1], 1e-10, "Incorrect subgradient for y component.");
        }

        [TestMethod]
        public void EllipsoidMethodOracle_ReturnsNull_WhenFeasible()
        {
            double[] point = new double[]
            {
                1.0,
                1.0
            };
            double[]? result = Program.EllipsoidMethodOracle(point);
            Assert.IsNull(result, "Expected null for feasible point.");
        }

        [TestMethod]
        public void EllipsoidMethodOracle_ReturnsEmptyArray_WhenDimensionIncorrect()
        {
            double[] point = new double[]
            {
                1.0,
                1.0,
                1.0
            };
            double[]? result = Program.EllipsoidMethodOracle(point);
            Assert.IsNotNull(result, "Expected empty array for incorrect dimension.");
            Assert.AreEqual(0, result.Length, "Expected empty array when point dimension is not 2.");
        }
    }
}