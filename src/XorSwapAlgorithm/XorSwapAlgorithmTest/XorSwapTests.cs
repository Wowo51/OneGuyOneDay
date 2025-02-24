using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XorSwapAlgorithm;

namespace XorSwapAlgorithmTest
{
    [TestClass]
    public class XorSwapTests
    {
        [TestMethod]
        public void TestSwapPositiveNumbers()
        {
            int a = 1;
            int b = 2;
            XorSwap.Swap(ref a, ref b);
            Assert.AreEqual(2, a);
            Assert.AreEqual(1, b);
        }

        [TestMethod]
        public void TestSwapNegativeNumbers()
        {
            int a = -10;
            int b = -20;
            XorSwap.Swap(ref a, ref b);
            Assert.AreEqual(-20, a);
            Assert.AreEqual(-10, b);
        }

        [TestMethod]
        public void TestSwapWithZero()
        {
            int a = 0;
            int b = 5;
            XorSwap.Swap(ref a, ref b);
            Assert.AreEqual(5, a);
            Assert.AreEqual(0, b);
        }

        [TestMethod]
        public void TestSwapSameNumbers()
        {
            int a = 42;
            int b = 42;
            XorSwap.Swap(ref a, ref b);
            Assert.AreEqual(42, a);
            Assert.AreEqual(42, b);
        }

        [TestMethod]
        public void TestSwapMinMaxValues()
        {
            int a = int.MinValue;
            int b = int.MaxValue;
            XorSwap.Swap(ref a, ref b);
            Assert.AreEqual(int.MaxValue, a);
            Assert.AreEqual(int.MinValue, b);
        }

        [TestMethod]
        public void TestSwapDoubleSwap()
        {
            int a = 10;
            int b = 20;
            XorSwap.Swap(ref a, ref b);
            XorSwap.Swap(ref a, ref b);
            Assert.AreEqual(10, a);
            Assert.AreEqual(20, b);
        }

        [TestMethod]
        public void TestSwapRandomValues()
        {
            Random random = new Random(100);
            for (int i = 0; i < 100; i++)
            {
                int originalX = random.Next();
                int originalY = random.Next();
                int x = originalX;
                int y = originalY;
                XorSwap.Swap(ref x, ref y);
                if (originalX != originalY)
                {
                    Assert.AreEqual(originalY, x);
                    Assert.AreEqual(originalX, y);
                }
                else
                {
                    Assert.AreEqual(originalX, x);
                    Assert.AreEqual(originalY, y);
                }
            }
        }
    }
}