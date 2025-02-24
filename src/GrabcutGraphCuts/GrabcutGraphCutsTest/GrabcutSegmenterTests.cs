using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrabcutGraphCuts;

namespace GrabcutGraphCutsTest
{
    [TestClass]
    public class GrabcutSegmenterTests
    {
        [TestMethod]
        public void Test_Segmenting5x5Image()
        {
            ImageData image = new ImageData(5, 5);
            GraphCutSolver solver = new GraphCutSolver();
            bool[, ] segmentation = solver.Segment(image);
            Assert.AreEqual(5, segmentation.GetLength(0));
            Assert.AreEqual(5, segmentation.GetLength(1));
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (j < 2)
                    {
                        Assert.IsTrue(segmentation[i, j]);
                    }
                    else
                    {
                        Assert.IsFalse(segmentation[i, j]);
                    }
                }
            }
        }

        [TestMethod]
        public void Test_SegmentLargeImage()
        {
            int width = 100;
            int height = 100;
            ImageData image = new ImageData(width, height);
            GraphCutSolver solver = new GraphCutSolver();
            bool[, ] segmentation = solver.Segment(image);
            Assert.AreEqual(height, segmentation.GetLength(0));
            Assert.AreEqual(width, segmentation.GetLength(1));
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j < width / 2)
                    {
                        Assert.IsTrue(segmentation[i, j]);
                    }
                    else
                    {
                        Assert.IsFalse(segmentation[i, j]);
                    }
                }
            }
        }
    }
}