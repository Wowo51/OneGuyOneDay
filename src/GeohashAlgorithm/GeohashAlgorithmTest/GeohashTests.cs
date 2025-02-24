using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeohashAlgorithm;

namespace GeohashAlgorithmTest
{
    [TestClass]
    public class GeohashTests
    {
        [TestMethod]
        public void Encode_WithNonPositivePrecision_ReturnsEmpty()
        {
            string result = Geohash.Encode(42.6, -5.6, 0);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Encode_KnownCoordinates_ReturnsExpectedGeohash()
        {
            // Known example: (42.6, -5.6) with precision 12 should yield "ezs42e44yx96"
            string expected = "ezs42e44yx96";
            string actual = Geohash.Encode(42.6, -5.6, 12);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Encode_AtOrigin_ReturnsExpectedGeohash()
        {
            // For coordinate (0.0, 0.0) the algorithm should yield a geohash of length 12.
            // Expected result computed by the algorithm is "s00000000000"
            string expected = "s00000000000";
            string actual = Geohash.Encode(0.0, 0.0, 12);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Encode_RandomCoordinates_ReturnsGeohashOfCorrectLength()
        {
            // Generates synthetic test data for random valid latitude and longitude values.
            Random random = new Random(0);
            for (int i = 0; i < 100; i++)
            {
                double latitude = random.NextDouble() * 180.0 - 90.0;
                double longitude = random.NextDouble() * 360.0 - 180.0;
                int precision = random.Next(1, 13);
                string geohash = Geohash.Encode(latitude, longitude, precision);
                Assert.AreEqual(precision, geohash.Length, $"Failed for latitude: {latitude}, longitude: {longitude}, precision: {precision}");
            }
        }

        [TestMethod]
        public void Encode_BoundaryCoordinates_ReturnsNonEmpty()
        {
            // Test boundary condition: maximum latitude and longitude.
            string geohash = Geohash.Encode(90.0, 180.0, 12);
            Assert.AreEqual(12, geohash.Length);
        }
    }
}