using Microsoft.VisualStudio.TestTools.UnitTesting;
using SurfFeatureDetector;
using System;
using System.Collections.Generic;

namespace SurfFeatureDetectorTest
{
    [TestClass]
    public class SurfDetectorTests
    {
        [TestMethod]
        public void DetectFeatures_NullImage_ReturnsEmpty()
        {
            SurfDetector detector = new SurfDetector();
            List<KeyPoint> keypoints = detector.DetectFeatures(null);
            Assert.IsNotNull(keypoints);
            Assert.AreEqual(0, keypoints.Count);
        }

        [TestMethod]
        public void DetectFeatures_UniformImage_NoKeypoints()
        {
            int[, ] image = new int[, ]
            {
                {
                    10,
                    10,
                    10
                },
                {
                    10,
                    10,
                    10
                },
                {
                    10,
                    10,
                    10
                }
            };
            SurfDetector detector = new SurfDetector();
            List<KeyPoint> keypoints = detector.DetectFeatures(image);
            Assert.IsNotNull(keypoints);
            Assert.AreEqual(0, keypoints.Count);
        }

        [TestMethod]
        public void DetectFeatures_SyntheticKeypoint()
        {
            int[, ] image = new int[, ]
            {
                {
                    10,
                    10,
                    10
                },
                {
                    10,
                    1,
                    10
                },
                {
                    10,
                    10,
                    10
                }
            };
            SurfDetector detector = new SurfDetector();
            List<KeyPoint> keypoints = detector.DetectFeatures(image);
            Assert.IsNotNull(keypoints);
            Assert.AreEqual(1, keypoints.Count);
            KeyPoint kp = keypoints[0];
            Assert.AreEqual(1.0, kp.X, 0.0001);
            Assert.AreEqual(1.0, kp.Y, 0.0001);
            Assert.AreEqual(1.2, kp.Scale, 0.0001);
            Assert.AreEqual(0.0, kp.Orientation, 0.0001);
        }

        [TestMethod]
        public void DetectFeatures_RandomImage_KeypointsInInterior()
        {
            int height = 100;
            int width = 100;
            int[, ] image = new int[height, width];
            Random random = new Random(42);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    image[i, j] = random.Next(0, 256);
                }
            }

            SurfDetector detector = new SurfDetector();
            List<KeyPoint> keypoints = detector.DetectFeatures(image);
            Assert.IsNotNull(keypoints);
            foreach (KeyPoint kp in keypoints)
            {
                Assert.IsTrue(kp.X >= 1 && kp.X < width - 1, "KeyPoint.X out of range");
                Assert.IsTrue(kp.Y >= 1 && kp.Y < height - 1, "KeyPoint.Y out of range");
            }
        }
    }
}