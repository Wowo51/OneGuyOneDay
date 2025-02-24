using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VincentysFormulae;

namespace VincentysFormulaeTest
{
    [TestClass]
    public class VincentysFormulaeTests
    {
        [TestMethod]
        public void TestZeroDistance()
        {
            double distance = VincentyCalculator.CalculateDistance(0.0, 0.0, 0.0, 0.0);
            Assert.AreEqual(0.0, distance, 0.0001, "Distance should be zero for identical points.");
        }

        [TestMethod]
        public void TestKnownDistance()
        {
            // Test with known coordinates.
            // Coordinates: (50.06638889, -5.71472222) to (58.64388889, -3.07)
            // Expected distance is approximately 968900 meters.
            double distance = VincentyCalculator.CalculateDistance(50.06638889, -5.71472222, 58.64388889, -3.07);
            double expected = 968900.0;
            Assert.IsTrue(Math.Abs(distance - expected) < 5000, "Distance is not within expected tolerance.");
        }

        [TestMethod]
        public void TestRandomDistances()
        {
            System.Random random = new System.Random(42);
            for (System.Int32 i = 0; i < 100; i++)
            {
                double lat1 = random.NextDouble() * 180.0 - 90.0;
                double lon1 = random.NextDouble() * 360.0 - 180.0;
                double lat2 = random.NextDouble() * 180.0 - 90.0;
                double lon2 = random.NextDouble() * 360.0 - 180.0;
                double distance = VincentyCalculator.CalculateDistance(lat1, lon1, lat2, lon2);
                Assert.IsTrue(distance >= 0.0, "Distance should always be non-negative.");
            }
        }
    }
}